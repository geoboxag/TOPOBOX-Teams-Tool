using System.Collections.Generic;
using System.IO;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Html.UserOverview;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using Graph = Microsoft.Graph;
using Logger = TOPOBOX.OSC.TeamsTool.Common.Logging.Logger;

namespace TOPOBOX.OSC.TeamsTool.Common.Controller
{
    /// <summary>
    /// UsersOverviewHelper for Mapping Users and Teams, Import and Export Data
    /// </summary>
    public class UsersOverviewHelper
    {
        private GraphConnectorHelper connectorHelper;
        private Logger Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphConnectorHelper"></param>
        public UsersOverviewHelper(GraphConnectorHelper graphConnectorHelper, Logger logger)
        {
            connectorHelper = graphConnectorHelper;
            Logger = logger;
        }

        /// <summary>
        /// Export the UsersOverview as a JSON-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsJsonFile(string saveFilePath)
        {
            var usersOverview = CollectData();
            try
            {
                if (JSONSerializer.WriteJson(saveFilePath, usersOverview))
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
        /// Export the UsersOverview as a HTML-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsHTMLFile(string saveFilePath)
        {
            var usersOverviews = CollectData();

            var htmlGenerator = new UserOverviewHTMLGenerator();
            var htmlContent = htmlGenerator.GenerateHTMLContent(usersOverviews);

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
        /// Returns the users and their teams
        /// </summary>
        /// <returns></returns>
        private List<UserOverview> CollectData()
        {
            GraphUserHelper userHelper = new GraphUserHelper(connectorHelper.GraphServiceClient);
            var users = userHelper.GetUsers();
            GraphTeamsHelper teamsHelper = new GraphTeamsHelper(connectorHelper.GraphServiceClient);
            var teamGroups = teamsHelper.GetTeamsFromGroups();
            return MapUsersAndTeams(users, teamGroups);
        }

        /// <summary>
        /// Maps the users and their joined teams
        /// </summary>
        /// <param name="users"></param>
        /// <param name="teamGroups"></param>
        /// <returns></returns>
        private List<UserOverview> MapUsersAndTeams(Dictionary<string, Graph.User> users, Dictionary<string, Graph.Group> teamGroups)
        {
            List<UserOverview> userOverviews = new List<UserOverview>();

            Dictionary<string, List<Graph.AadUserConversationMember>> teamsAndMembers = GetTeamsAndTheirMembers(teamGroups);

            foreach (KeyValuePair<string, Graph.User> currentUser in users)
            {
                User user = UserMapper.MapFrom(currentUser.Value);
                UserOverview userOverview = new UserOverview(user);

                foreach (var teamAndMember in teamsAndMembers)
                {
                    foreach (var member in teamAndMember.Value)
                    {
                        if (member.UserId.Equals(user.Id))
                        {
                            teamGroups.TryGetValue(teamAndMember.Key, out Graph.Group teamFromTeamGroups);
                            if(teamFromTeamGroups is null)
                            {
                                userOverviews.Add(userOverview);
                            }
                            else
                            {
                                Team team = TeamMapper.MapFrom(teamFromTeamGroups);
                                userOverview.AddTeam(team);
                                break;
                            }
                        }
                    }

                }
                userOverviews.Add(userOverview);
            }

            return userOverviews;
        }


        private Dictionary<string, List<Graph.AadUserConversationMember>> GetTeamsAndTheirMembers(Dictionary<string, Graph.Group> teamGroups)
        {
            Dictionary<string, List<Graph.AadUserConversationMember>> teamsAndMembers =
                new Dictionary<string, List<Graph.AadUserConversationMember>>();

            GraphUserConversationMemberHelper userConversationMemberHelper = new GraphUserConversationMemberHelper(connectorHelper.GraphServiceClient);

            foreach (KeyValuePair<string, Graph.Group> teamGroup in teamGroups)
            {
                var members = userConversationMemberHelper.GetTeamMembers(teamGroup.Key);

                List<Graph.AadUserConversationMember> aadUsers = new List<Graph.AadUserConversationMember>();
                foreach (var member in members)
                {
                    aadUsers.Add(member.Value);
                }

                teamsAndMembers.Add(teamGroup.Key, aadUsers);
            }

            return teamsAndMembers;
        }

    }
}
