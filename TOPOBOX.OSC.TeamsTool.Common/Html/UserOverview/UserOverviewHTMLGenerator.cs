using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.DAL;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.UserOverview
{
    internal sealed class UserOverviewHTMLGenerator : BaseHTMLGenerator
    {
        private const string PAGETITLE = "Benutzer-Übersicht";

        public UserOverviewHTMLGenerator() : base(PAGETITLE)
        {

        }

        public string GenerateHTMLContent(List<DAL.UserOverview> userOverviews)
        {
            AddContentAsReplacement(CreateContent(userOverviews));
            return GetHTMLfromReplacement();
        }

        private string CreateContent(List<DAL.UserOverview> userOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {
                var orderedList = userOverviews.OrderBy(u => u.User.Surname).ThenBy(u => u.User.Firstname);

                foreach (var userOverview in orderedList)
                {
                    var userDiv = GetInformationsDIV(CreateUserTitle(userOverview.User));

                    userDiv.Controls.Add(GetSpan($"ID: {userOverview.User.Id}", true));
                    userDiv.Controls.Add(GetH3Title("zugeordnete Teams:"));
                    AddTeams(userDiv, userOverview.Teams);

                    userDiv.RenderControl(textWriter);
                }

                textWriter.Flush();
            }
            return stringBuilder.ToString();
        }

        private string CreateUserTitle(User user)
        {
            string resultUserTitle = string.Empty;

            if (string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Firstname))
            {
                resultUserTitle += $"{user.DisplayName}";
            }
            else
            {
                resultUserTitle += $"{user.Surname} {user.Firstname}";
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return resultUserTitle;
            }

            resultUserTitle += $" ({user.Email})";

            return resultUserTitle;
        }

        private void AddTeams(Control divControl, List<Team> teams)
        {
            if (teams != null && teams.Count > 0)
            {
                var sortedTeams = teams.OrderBy(t => t.Name);
                foreach (var team in sortedTeams)
                {
                    if (string.IsNullOrEmpty(team.Description))
                    {
                        divControl.Controls.Add(GetParagraph(team.Name));
                    }
                    else
                    {
                        divControl.Controls.Add(GetParagraphAndDescription(team.Name, team.Description));
                    }
                }                
            }
        }
    }
}