using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphUserConversationMemberHelper
    /// </summary>
    public sealed class GraphUserConversationMemberHelper
    {
        private GraphServiceClient graphClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphServiceClient"></param>
        public GraphUserConversationMemberHelper(GraphServiceClient graphServiceClient)
        {
            graphClient = graphServiceClient;
        }


        #region Members for Teams
        /// <summary>
        /// Get all Members of the given TeamId
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>dictionary with userID and Graph.AadUserConversationMember Objects</returns>
        public Dictionary<string, AadUserConversationMember> GetTeamMembers(string teamId)
        {
            var graphRequest = graphClient.Teams[teamId].Members.ToGetRequestInformation();
            //var answer = graphRequest.GetAsync().Result;
            var task = new Task<Dictionary<string, AadUserConversationMember>>(gR =>
            {
                Dictionary<string, AadUserConversationMember> returnDict = new Dictionary<string, AadUserConversationMember>();
                try
                {
                    var result = graphClient.Teams[teamId].Members.GetAsync();
                    var answer = result.Result.Value;

                    if (answer.Any())
                    {
                        foreach (var conversationMember in answer)
                        {
                            var aadUser = (AadUserConversationMember)conversationMember;
                            returnDict.Add(aadUser.Id, aadUser);
                        }
                        return returnDict;
                    }
                }
                catch
                {
                    returnDict = new Dictionary<string, AadUserConversationMember>();
                }
                return returnDict;

            }, graphRequest);

            task.Start();
            return task.Result;
        }
        #endregion
    }
}
