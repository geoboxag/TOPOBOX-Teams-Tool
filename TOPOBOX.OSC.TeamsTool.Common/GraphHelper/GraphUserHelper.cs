using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphUserHelper
    /// </summary>
    public sealed class GraphUserHelper
    {
        private GraphServiceClient graphClient;
        public User registeredUser;
        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphUserHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;

        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>dictionary with userID and Graph.User Objects</returns>
        public Dictionary<string, User> GetUsers()
        {
            var graphRequest = graphClient.Users.ToGetRequestInformation();

            var task = new Task<Dictionary<string, User>>(gR =>
            {
                var request = graphClient.Users.GetAsync();
                //var request = gR as GraphServiceUsersCollectionRequest;
                var answer = request.Result.Value;

                if (answer.Any())
                {
                    Dictionary<string, User> returnDict = new Dictionary<string, User>();
                    foreach (var user in answer)
                    {
                        returnDict.Add(user.Id, user);
                    }
                    return returnDict;
                }

                return new Dictionary<string, User>();
            }, graphRequest);

            task.Start();
            return task.Result;
        }
    }
}
