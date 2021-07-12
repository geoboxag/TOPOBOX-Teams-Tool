using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.TeamOverview
{
    internal sealed class TeamOverviewHTMLGenerator : BaseHTMLGenerator
    {
        private const string PAGETITLE = "Teams-Übersicht";

        public TeamOverviewHTMLGenerator() : base(PAGETITLE)
        {

        }

        public string GenerateHTMLContent(List<DAL.TeamOverview> teamOverviews)
        {
            AddContentAsReplacement(CreateContent(teamOverviews));
            return GetHTMLfromReplacement();
        }

        private string CreateContent(List<DAL.TeamOverview> teamOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {

                var list = teamOverviews.OrderBy(t => t.Team.Name);

                foreach (var teamOverview in list)
                {
                    var userDiv = GetInformationsDIV(teamOverview.Team.Name);

                    userDiv.Controls.Add(GetSpan($"ID: {teamOverview.Team.Id}", true));
                    userDiv.Controls.Add(GetLineBreack());
                    // ToDO check is empty
                    userDiv.Controls.Add(GetSpan($"Beschrieb: {teamOverview.Team.Description}", true));
                    
                    AddUsers(userDiv, "Besitzer (Owners):", teamOverview.Owners);
                    AddUsers(userDiv,"Mitglieder (Members):", teamOverview.Members);
                    AddUsers(userDiv, "Gäste (Guests):", teamOverview.Guests);

                    userDiv.RenderControl(textWriter);
                }

                textWriter.Flush();
            }
            return stringBuilder.ToString();
        }

        private void AddUsers(Control divControl,string title, List<User> users)
        {
            divControl.Controls.Add(GetH3Title(title));

            if (users != null && users.Count > 0)
            {
                foreach(var user in users)
                {
                    if (string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Firstname))
                    {
                        divControl.Controls.Add(GetParagraphAndDescription($"{user.DisplayName} ({user.Email})", $"(ID: {user.Id})"));
                    }
                    else
                    {
                        divControl.Controls.Add(GetParagraphAndDescription($"{user.Surname} {user.Firstname} ({user.Email})", $"(ID: {user.Id})"));
                    }                   
                }
            }
            else
            {
                divControl.Controls.Add(GetSpan($"- keine Benutzer gefunden", true));
            }
        }

        private IEnumerable<InformationTable> CreateTeamTables(List<DAL.TeamOverview> teamOverviews)
        {
            foreach (var teamOverview in teamOverviews)
            {
                yield return new InformationTable
                    (teamOverview.Team.Name, 
                        teamOverview.Team.Description, //Document head
                        CreateMemberTables(teamOverview)
                    );
            }
        }

        private IEnumerable<Table> CreateMemberTables(DAL.TeamOverview teamOverview)
        {
            var memberTables = new List<Table>();

            if(teamOverview.Owners != null && teamOverview.Owners.Count > 0)
            {
                memberTables.Add(GetMemberTable("Team Besitzer", teamOverview.Owners));
            }
            if (teamOverview.Members != null && teamOverview.Members.Count > 0)
            {
                memberTables.Add(GetMemberTable("Team Mitglieder", teamOverview.Members));
            }
            if (teamOverview.Guests != null && teamOverview.Guests.Count > 0)
            {
                memberTables.Add(GetMemberTable("Gäste", teamOverview.Guests));
            }

            return memberTables;
        }

        private IEnumerable<Paragraph> CreateParagraphs(DAL.TeamOverview teamOverview)
        {
            var memberList = new List<Paragraph>();

            if (teamOverview.Owners != null && teamOverview.Owners.Count > 0)
            {
                memberList.Add(GetParagraphs("Team Besitzer", teamOverview.Owners));
            }
            if (teamOverview.Members != null && teamOverview.Members.Count > 0)
            {
                memberList.Add(GetParagraphs("Team Mitglieder", teamOverview.Members));
            }
            if (teamOverview.Guests != null && teamOverview.Guests.Count > 0)
            {
                memberList.Add(GetParagraphs("Gäste", teamOverview.Guests));
            }

            return memberList;
        }


        private List<IEnumerable<string>> CreateMemberRows(List<DAL.User> members)
        {
            var rows = new List<IEnumerable<string>>();

            foreach (var member in members)
            {
                rows.Add(new[] { member.DisplayName , member.Email });
            }
          
            return rows;
        }

        private List<string> CreateParagraphs(List<DAL.User> members)
        {
            var paragraphs = new List<string>();

            foreach (var member in members)
            {
                paragraphs.Add($"{member.DisplayName} ({member.Email})");
            }

            return paragraphs;
        }


        private Table GetMemberTable(string title, List<DAL.User> members)
        {
            return new Table(title, null, CreateMemberRows(members));
        }


        private Paragraph GetParagraphs(string title, List<DAL.User> members)
        {
            return new Paragraph(title, CreateParagraphs(members));
        }

    }
}
