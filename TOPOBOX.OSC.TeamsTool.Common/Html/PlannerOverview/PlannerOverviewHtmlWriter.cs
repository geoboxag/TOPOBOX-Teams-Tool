using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.PlannerOverview
{
    internal sealed class PlannerOverviewHtmlWriter
    {
        private IEnumerable<KeyValuePair<string, string>> CreateReplacements(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            var replacements = HtmlWriterReplacements.DefaultReplacements("Planner-Übersicht");
            replacements.Add("[CONTENT]", CreateList(plannerOverviews));
            return replacements;
        }


        private string CreateList(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {
                new HtmlWriter().WriteParagraphs(textWriter, CreatePlannerList(plannerOverviews));
                textWriter.Flush();
            }
            return stringBuilder.ToString();
        }


        private IEnumerable<InformationList> CreatePlannerList(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            foreach (var plannerOverview in plannerOverviews)
            {
                yield return new InformationList
                    (plannerOverview.Planner.Name, string.Empty,
                        CreateParagraph(plannerOverview.Buckets)
                    );
            }
        }


        private IEnumerable<Paragraph> CreateParagraph(List<DAL.Bucket> buckets)
        {
            var bucketNames = new List<Paragraph>();

            bucketNames.Add(new Paragraph("Bucket-Übersicht", CreateParagraphRows(buckets)));

            return bucketNames;
        }


        private IEnumerable<string> CreateParagraphRows(List<DAL.Bucket> buckets)
        {
            var bucketNames = new List<string>();

            foreach (var bucket in buckets)
            {
                bucketNames.Add(bucket.Name);
            }

            return bucketNames;
        }


        public string Write(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            var result = StringTemplateReplacer.Replace
                (
                    Resources.htmlTemplate,
                    CreateReplacements(plannerOverviews)
                );

            return result;
        }
    }
}
