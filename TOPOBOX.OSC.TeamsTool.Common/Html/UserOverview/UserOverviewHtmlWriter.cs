using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.UserOverview
{
    internal sealed class UserOverviewHtmlWriter
    {
        public string Write(List<DAL.UserOverview> userOverviews)
        {
            var result = StringTemplateReplacer.Replace
                (
                    Resources.htmlTemplate,
                    CreateReplacements(userOverviews)
                );

            return result;
        }

        private IEnumerable<KeyValuePair<string, string>> CreateReplacements(List<DAL.UserOverview> userOverviews)
        {
            var replacements = HtmlWriterReplacements.DefaultReplacements("Benutzer-Übersicht");
            replacements.Add("[CONTENT]", CreateList(userOverviews));
            return replacements;
        }

        private string CreateList(List<DAL.UserOverview> userOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {
                new HtmlWriter().WriteParagraphs(textWriter, CreateUserList(userOverviews));
                textWriter.Flush();
            }
            return stringBuilder.ToString();
        }

        private IEnumerable<InformationList> CreateUserList(List<DAL.UserOverview> userOverviews)
        {
            foreach (var userOverview in userOverviews)
            {
                yield return new InformationList
                    ($"{userOverview.User.Name} ({userOverview.User.Email}) - {userOverview.User.Id}",
                        userOverview.User.Id,
                        CreateParagraphs(userOverview)
                    );
            }
        }

        private IEnumerable<Paragraph> CreateParagraphs(DAL.UserOverview userOverview)
        {
            var teamList = new List<Paragraph>();

            if (userOverview.Teams != null && userOverview.Teams.Count > 0)
            {
                teamList.Add(GetParagraph("Teams", userOverview.Teams));
            }

            return teamList;
        }

        private List<string> CreateParagraph(List<Team> teams)
        {
            var paragraphs = new List<string>();

            foreach (var team in teams)
            {
                paragraphs.Add($"{team.Name} ({team.Description})");
            }

            return paragraphs;
        }


        private Paragraph GetParagraph(string title, List<Team> teams)
        {
            return new Paragraph(title, CreateParagraph(teams));
        }

    }
}
