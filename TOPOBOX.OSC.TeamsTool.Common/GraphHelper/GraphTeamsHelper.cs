using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphTeamsHelper
    /// </summary>
    public sealed class GraphTeamsHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphTeamsHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
        }

        #region Groups
        /// <summary>
        /// Get Groups in the Organization
        /// </summary>
        /// <param name="resourceProvisioningOptionsSearchPattern"></param>
        /// <returns></returns>
        public Dictionary<string, Group> GetGroups(string resourceProvisioningOptionsSearchPattern = "[]") // when Empty Array, it's a group
        {                                                                                          // when Array Contains "Team", it's a Team
            var graphRequest = graphClient.Groups.Request();

            var task = new Task<Dictionary<string, Group>>(gR =>
            {
                var request = gR as GraphServiceGroupsCollectionRequest;
                var answer = request.GetAsync().Result;

                if (answer.Any())
                {
                    Dictionary<string, Group> returnDict = new Dictionary<string, Group>();

                    foreach (var group in answer)
                    {
                        if (group.AdditionalData["resourceProvisioningOptions"].ToString()
                        .IndexOf(resourceProvisioningOptionsSearchPattern) >= 0)
                        {
                            returnDict.Add(group.Id, group);
                        }
                    }
                    return returnDict;
                }

                return new Dictionary<string, Group>();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
        #endregion

        #region Teams from Groups
        /// <summary>
        /// Get the Teams from Groups (with "Team" Filter)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Group> GetTeamsFromGroups()
        {
            return GetGroups("Team");
            
        } 
        #endregion
    }
}
