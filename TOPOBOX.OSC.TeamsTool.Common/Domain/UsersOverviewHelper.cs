using GEOBOX.OSC.Common.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Html.UserOverview;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Domain
{
    /// <summary>
    /// UsersOverviewHelper for Mapping Users and Teams, Import and Export Data
    /// </summary>
    public class UsersOverviewHelper
    {
        private GraphConnectorHelper connectorHelper;
        private ILogger Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphConnectorHelper"></param>
        /// <param name="logger"></param>
        public UsersOverviewHelper(GraphConnectorHelper graphConnectorHelper, ILogger logger)
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
                if (JSONSerializer.WriteJson(saveFilePath, usersOverview, Logger))
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
        internal List<UserOverview> CollectData()
        {
            GraphUserHelper userHelper = new GraphUserHelper(connectorHelper.GraphServiceClient);
            var users = userHelper.GetUsers();
            GraphTeamsHelper teamsHelper = new GraphTeamsHelper(connectorHelper.GraphServiceClient);
            var teamGroups = teamsHelper.GetTeamsFromGroups();
            var usersAndTeams = MapUsersAndTeams(users, teamGroups);
            return usersAndTeams.OrderBy(u => u.User.FullName).ToList();
        }

        /// <summary>
        /// Returns the users and their teams from configFile
        /// </summary>
        /// <returns></returns>
        internal List<UserOverview> CollectDataFromConfigFile()
        {
            var filePath = Path.Combine(Properties.Settings.Default.TeamsToolConfigRootPath,
                Properties.Settings.Default.RelFilePathUsersJson);

            var users = JSONSerializer.ReadJson<List<UserOverview>>(filePath, Logger);

            if (!users.Any())
            {
                Logger?.WriteError($"{Properties.Resources.NoEntriesInListFoundMessage}: {filePath}");
            }

            return users.OrderBy(u => u.User.FullName).ToList();
        }

        /// <summary>
        /// Maps the users and their joined teams
        /// </summary>
        /// <param name="users"></param>
        /// <param name="teamGroups"></param>
        /// <returns></returns>
        private List<UserOverview> MapUsersAndTeams(Dictionary<string, Graph.Models.User> users, Dictionary<string, Graph.Models.Group> teamGroups)
        {
            List<UserOverview> userOverviews = new List<UserOverview>();

            Dictionary<string, List<Graph.Models.AadUserConversationMember>> teamsAndMembers = GetTeamsAndTheirMembers(teamGroups);

            foreach (KeyValuePair<string, Graph.Models.User> currentUser in users)
            {
                User user = UserMapper.MapFrom(currentUser.Value);
                UserOverview userOverview = new UserOverview(user);

                foreach (var teamAndMember in teamsAndMembers)
                {
                    foreach (var member in teamAndMember.Value)
                    {
                        if (member.UserId.Equals(user.Id))
                        {
                            teamGroups.TryGetValue(teamAndMember.Key, out Graph.Models.Group teamFromTeamGroups);
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


        private Dictionary<string, List<Graph.Models.AadUserConversationMember>> GetTeamsAndTheirMembers(Dictionary<string, Graph.Models.Group> teamGroups)
        {
            Dictionary<string, List<Graph.Models.AadUserConversationMember>> teamsAndMembers =
                new Dictionary<string, List<Graph.Models.AadUserConversationMember>>();

            GraphUserConversationMemberHelper userConversationMemberHelper = new GraphUserConversationMemberHelper(connectorHelper.GraphServiceClient);

            foreach (KeyValuePair<string, Graph.Models.Group> teamGroup in teamGroups)
            {
                var members = userConversationMemberHelper.GetTeamMembers(teamGroup.Key).Result;

                List<Graph.Models.AadUserConversationMember> aadUsers = new List<Graph.Models.AadUserConversationMember>();
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
