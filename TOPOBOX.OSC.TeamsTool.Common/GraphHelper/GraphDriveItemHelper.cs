using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphDriveItemHelper
    /// </summary>
    public sealed class GraphDriveItemHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphDriveItemHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
            
        }

        /// <summary>
        /// Get all driveItems from specific user by userId
        /// </summary>
        /// <returns>dictionary with driveItemId and Graph.DriveItem Objects</returns>
        public Dictionary<string, DriveItem> GetDriveItems(string userId)
        {
            var graphRequest = graphClient.Users[userId].Drive.ToGetRequestInformation();
            //var graphRequest = graphClient.Users[userId].Drive.Root.Children.Request();
            var task = new Task<Dictionary<string, DriveItem>>(gR =>
            {
                var request = graphClient.Users[userId].Drive;
                var answer = request.GetAsync().Result.Items;

                if (answer.Any())
                {
                    Dictionary<string, DriveItem> returnDict = new Dictionary<string, DriveItem>();
                    foreach (var driveItem in answer)
                    {
                        returnDict.Add(driveItem.Id, driveItem);
                    }
                    return returnDict;
                }

                return new Dictionary<string, DriveItem>();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
    }
}
