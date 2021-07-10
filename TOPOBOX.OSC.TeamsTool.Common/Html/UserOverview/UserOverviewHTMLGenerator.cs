using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

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
                // ToDo order by last and first name
                var list = userOverviews.OrderBy(u => u.User.DisplayName);

                foreach(var userOverview in list)
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
            // ToDo Check Email is not empty (is empty write only user.Name)
            // ToDo Display: lastname and firstname
            return $"{user.DisplayName} ({user.Email})";
        }

        private void AddTeams(Control divControl, List<Team> teams)
        {
            // ToDo order teams by name
            if (teams != null && teams.Count > 0)
            {
                foreach(var team in teams)
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