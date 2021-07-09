using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.TeamOverview
{
    internal sealed class TeamOverviewHtmlWriter
    {

        public string Write(List<DAL.TeamOverview> teamOverviews)
        {
            var result = StringTemplateReplacer.Replace
                (
                    Resources.htmlTemplate,
                    CreateReplacements(teamOverviews)
                );

            return result;
        }

        private IEnumerable<KeyValuePair<string, string>> CreateReplacements(List<DAL.TeamOverview> teamOverviews)
        {
            var replacements = HtmlWriterReplacements.DefaultReplacements("Teams-Übersicht");
            replacements.Add("[CONTENT]", CreateTables(teamOverviews));
            return replacements;
        }

        private string CreateTables(List<DAL.TeamOverview> teamOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {
                new HtmlWriter().Write(textWriter, CreateTeamTables(teamOverviews));
                textWriter.Flush();
            }
            return stringBuilder.ToString();
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
                rows.Add(new[] { member.Name , member.Email });
            }
          
            return rows;
        }

        private List<string> CreateParagraphs(List<DAL.User> members)
        {
            var paragraphs = new List<string>();

            foreach (var member in members)
            {
                paragraphs.Add($"{member.Name} ({member.Email})");
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
