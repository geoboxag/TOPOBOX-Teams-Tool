using Microsoft.Graph;
using Microsoft.Graph.Models;
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
        public async Task<Dictionary<string, PlannerPlan>> GetPlannersAsync()
        {
            var graphRequest = graphClient.Planner.Plans.ToGetRequestInformation();
            var request = await graphClient.Planner.Plans.GetAsync();
            var task = new Task<Dictionary<string, PlannerPlan>>(gR =>
            {
                //var request = gR as PlannerPlansCollectionRequest;
                //var answer = request.GetAsync().Result;
                var answer = request.Value;

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


        /// <summary>
        /// Get all my planners
        /// </summary>
        /// <returns>dictionary with plannerID and Graph.Planner Objects</returns>
        public async Task<Dictionary<string, PlannerPlan>> GetMyPlannersAsync()
        {
            var graphRequest = graphClient.Me.Planner.Plans.ToGetRequestInformation();

            var request = await graphClient.Me.Planner.Plans.GetAsync();
            var task = new Task<Dictionary<string, PlannerPlan>>( gR =>
            {
                //var answer = request.GetAsync().Result;
                var answer = request.Value;
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
