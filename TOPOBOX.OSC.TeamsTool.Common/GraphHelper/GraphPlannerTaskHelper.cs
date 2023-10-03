using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphPlannerTaskHelper
    /// </summary>
    public sealed class GraphPlannerTaskHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphPlannerTaskHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
        }

        /// <summary>
        /// Send PlannerTask to the specified Planner, Bucket
        /// </summary>
        /// <returns>dictionary with bucketID and Graph.PlannerBucket Objects</returns>
        public PlannerTask SendPlannerTask(PlannerTask plannerTask)
        {
            var graphRequest = graphClient.Planner.Tasks.ToGetRequestInformation();

            var task = new Task<PlannerTask>( gR =>
            {
                var request = graphClient.Planner.Tasks.PostAsync(plannerTask);
                var answer = request.Result;

                if (answer != null)
                {
                    return answer;
                }

                return new PlannerTask();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
    }
}
