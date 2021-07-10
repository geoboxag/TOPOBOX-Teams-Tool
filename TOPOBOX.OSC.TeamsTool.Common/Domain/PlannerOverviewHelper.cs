using System.Collections.Generic;
using System.IO;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Html.PlannerOverview;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Controller
{
    /// <summary>
    /// PlannerOverviewHelper for Mapping Planners and Buckets, Import and Export Data
    /// </summary>
    public class PlannerOverviewHelper
    {
        private GraphConnectorHelper connectorHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphConnectorHelper"></param>
        public PlannerOverviewHelper(GraphConnectorHelper graphConnectorHelper)
        {
            connectorHelper = graphConnectorHelper;
        }

        /// <summary>
        /// Export the PlannerConfigration as a JSON-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsJsonFile(string saveFilePath)
        {
            var plannerOverview = CollectData();
            try
            {
                if (JSONSerializer.WriteJson(saveFilePath, plannerOverview))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                // ToDo Exception and Logger
                return false;
            }
        }

        /// <summary>
        /// Export the PlannerConfigration as a HTML-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsHTMLFile(string saveFilePath)
        {
            var plannerOverview = CollectData();

            var htmlGenerator = new PlannerOverviewHTMLGenerator();
            var htmlContent = htmlGenerator.GenerateHTMLContent(plannerOverview);

            try
            {
                using (StreamWriter fileStream = new StreamWriter(saveFilePath))
                {
                    fileStream.Write(htmlContent);
                }
                return true;
            }
            catch
            {
                // ToDo Exception and Logger
                return false;
            }
        }

        /// <summary>
        /// Returns the planner and their buckets
        /// </summary>
        /// <returns></returns>
        private List<PlannerConfiguration> CollectData()
        {
            GraphPlannerPlanHelper plannerHelper = new GraphPlannerPlanHelper(connectorHelper.GraphServiceClient);
            var planners = plannerHelper.GetPlanners();
            return MapPlannerAndBuckets(planners);
        }

        /// <summary>
        /// Maps the planners and their buckets
        /// </summary>
        /// <param name="planners"></param>
        /// <returns></returns>
        private List<PlannerConfiguration> MapPlannerAndBuckets(Dictionary<string, Graph.PlannerPlan> planners)
        {
            List<PlannerConfiguration> plannerOverview = new List<PlannerConfiguration>();

            GraphBucketHelper graphBucketHelper = new GraphBucketHelper(connectorHelper.GraphServiceClient);

            foreach (KeyValuePair<string, Graph.PlannerPlan> planner in planners)
            {
                PlannerConfiguration plannerConfiguration = new PlannerConfiguration();
                plannerConfiguration.Planner = new Planner() { Id = planner.Value.Id, Name = planner.Value.Title };
                
                plannerConfiguration.Buckets = new List<Bucket>();
               
                var buckets = graphBucketHelper.GetBuckets(planner.Value.Id);
                BucketMapper bucketMapper = new BucketMapper();

                foreach (var bucket in buckets)
                {
                    plannerConfiguration.Buckets.Add((Bucket)bucketMapper.MapFrom(bucket));
                }

                plannerOverview.Add(plannerConfiguration);
            }

            return plannerOverview;
        }

    }
}
