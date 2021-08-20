using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphPlannerPlanHelper
    /// </summary>
    public sealed class GraphPlannerPlanHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphPlannerPlanHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
        }

        /// <summary>
        /// Get all planners
        /// </summary>
        /// <returns>dictionary with plannerID and Graph.Planner Objects</returns>
        public Dictionary<string, PlannerPlan> GetPlanners()
        {
            var graphRequest = graphClient.Planner.Plans.Request();

            var task = new Task<Dictionary<string, PlannerPlan>>(gR =>
            {
                var request = gR as PlannerPlansCollectionRequest;
                var answer = request.GetAsync().Result;

                if (answer.Any())
                {
                    Dictionary<string, PlannerPlan> returnDict = new Dictionary<string, PlannerPlan>();
                    foreach (var plannerPlan in answer)
                    {
                        returnDict.Add(plannerPlan.Id, plannerPlan);
                    }
                    return returnDict;
                }

                return new Dictionary<string, PlannerPlan>();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
    }
}
