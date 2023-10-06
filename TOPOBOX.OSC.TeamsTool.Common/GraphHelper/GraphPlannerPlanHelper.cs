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
        public async Task<Dictionary<string, PlannerPlan>> GetPlanners()
        {
            var graphRequest = graphClient.Planner.Plans.ToGetRequestInformation();
            var task = new Task<Dictionary<string, PlannerPlan>>(gR =>
            {
                var groups = graphClient.Groups.GetAsync().Result.Value;
                foreach (var group in groups)
                {
                    var request = graphClient.Groups[group.Id].Planner.Plans.GetAsync();
                    var answer = request.Result.Value;
                    if (answer.Any())
                    {
                        Dictionary<string, PlannerPlan> returnDict = new Dictionary<string, PlannerPlan>();
                        foreach (var plannerPlan in answer)
                        {
                            returnDict.Add(plannerPlan.Id, plannerPlan);
                        }
                        return returnDict;
                    }
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
        public async Task<Dictionary<string, PlannerPlan>> GetMyPlanners()
        {
            var graphRequest = graphClient.Me.Planner.Plans.ToGetRequestInformation();

            var task = new Task<Dictionary<string, PlannerPlan>>(gR =>
            {
                var request = graphClient.Me.Planner.Plans.GetAsync();
                //var request = graphClient.Users["{user-id}"].Planner.Tasks.GetAsync();

                //var answer = request.GetAsync().Result;
                var answer = request.Result.Value;
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
