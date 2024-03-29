﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using TOPOBOX.OSC.TeamsTool.Common.DAL;

namespace TOPOBOX.OSC.TeamsTool.Common.Html.PlannerOverview
{
    internal sealed class PlannerOverviewHTMLGenerator : BaseHTMLGenerator
    {

        private const string PAGETITLE = "Planner-Übersicht";

        public PlannerOverviewHTMLGenerator() : base(PAGETITLE)
        {

        }

        public string GenerateHTMLContent(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            AddContentAsReplacement(CreateContent(plannerOverviews));
            return GetHTMLfromReplacement();
        }


        private string CreateContent(List<DAL.PlannerConfiguration> plannerOverviews)
        {
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var textWriter = new HtmlTextWriter(stringWriter))
            {
                var list = plannerOverviews.OrderBy(p => p.Planner.Name);

                foreach (var plannerOverview in list)
                {
                    var div = GetInformationsDIV(plannerOverview.Planner.Name);

                    div.Controls.Add(GetSpan($"ID: {plannerOverview.Planner.Id}", true));
                    div.Controls.Add(GetH3Title("Buckets:"));
                    AddBuckets(div, plannerOverview.Buckets);

                    div.RenderControl(textWriter);
                }

                textWriter.Flush();
            }
            return stringBuilder.ToString();
        }

        private void AddBuckets(Control divControl, List<Bucket> buckets)
        {
            List<Bucket> sortedBuckets = buckets.OrderBy(b => b.Name).ToList();

            if (sortedBuckets != null && sortedBuckets.Count > 0)
            {
                foreach (var bucket in sortedBuckets)
                {
                    divControl.Controls.Add(GetParagraphAndDescription(bucket.Name, $"(ID: {bucket.Id})"));
                }
            }
        }
    }
}