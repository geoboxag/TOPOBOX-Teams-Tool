using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphBucketsHelper
    /// </summary>
    public sealed class GraphBucketHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphBucketHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
        }

        /// <summary>
        /// Get all buckets of plannerplan
        /// </summary>
        /// <returns>dictionary with bucketID and Graph.PlannerBucket Objects</returns>
        public Dictionary<string, PlannerBucket> GetBuckets(string plannerId)
        {
            var graphRequest = graphClient.Planner.Plans[plannerId].Buckets.ToGetRequestInformation();

            var task = new Task<Dictionary<string, PlannerBucket>>(gR =>
            {
                //var request = gR as PlannerPlanBucketsCollectionRequest;
                var request = graphClient.Planner.Plans[plannerId].Buckets;
                var answer = request.GetAsync().Result.Value;

                if (answer.Any())
                {
                    Dictionary<string, PlannerBucket> returnDict = new Dictionary<string, PlannerBucket>();
                    foreach (var bucket in answer)
                    {
                        returnDict.Add(bucket.Id, bucket);
                    }
                    return returnDict;
                }

                return new Dictionary<string, PlannerBucket>();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
    }
}
