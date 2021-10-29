using GEOBOX.OSC.Common.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Html.TeamOverview;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Domain
{
    /// <summary>
    /// TeamsOverviewHelper for Mapping Teams and Members, Import and Export Data
    /// </summary>
    public class TeamsOverviewHelper
    {
        private GraphConnectorHelper connectorHelper;
        private ILogger Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphConnectorHelper"></param>
        /// <param name="logger"></param>
        public TeamsOverviewHelper(GraphConnectorHelper graphConnectorHelper, ILogger logger)
        {
            connectorHelper = graphConnectorHelper;
            Logger = logger;
        }

        /// <summary>
        /// Export the TeamsOverview as a JSON-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsJsonFile(string saveFilePath)
        {
            var teamsOverview = CollectData();
            try
            {
                if (JSONSerializer.WriteJson(saveFilePath, teamsOverview, Logger))
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
        /// Export the TeamsOverview as a PDF-File
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        public bool SaveAsHTMLFile(string saveFilePath)
        {
            var teamOverviews = CollectData();

            var htmlGenerator = new TeamOverviewHTMLGenerator();
            var htmlContent = htmlGenerator.GenerateHTMLContent(teamOverviews);

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
        /// Returns the teams and their members
        /// </summary>
        /// <returns></returns>
        internal List<TeamOverview> CollectData()
        {
            GraphUserHelper userHelper = new GraphUserHelper(connectorHelper.GraphServiceClient);
            var users = userHelper.GetUsers();
            GraphTeamsHelper teamsHelper = new GraphTeamsHelper(connectorHelper.GraphServiceClient);
            var teamGroups = teamsHelper.GetTeamsFromGroups();
            var teamsOverviews = MapTeamsAndMembers(teamGroups, users);
            return teamsOverviews.OrderBy(t => t.Team.Name).ToList();
        }

        /// <summary>
        /// Returns the teams and their members from configFile
        /// </summary>
        /// <returns></returns>
        internal List<TeamOverview> CollectDataFromConfigFile()
        {
            string filePath = Path.Combine(Properties.Settings.Default.TeamsToolConfigRootPath,
                Properties.Settings.Default.RelFilePathTeamsJson);
            var teamsOverviews = JSONSerializer.ReadJson<List<TeamOverview>>(filePath, Logger);

            if (!teamsOverviews.Any())
            {
                Logger?.WriteWarning($"{Properties.Resources.NoEntriesInListFoundMessage}: {filePath}");
            }

            return teamsOverviews.OrderBy(t => t.Team.Name).ToList();
        }

        /// <summary>
        /// Maps the teams and their members
        /// </summary>
        /// <param name="teamGroups"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private List<TeamOverview> MapTeamsAndMembers(Dictionary<string, Graph.Group> teamGroups, Dictionary<string, Graph.User> users)
        {
            List<TeamOverview> teamOverviews = new List<TeamOverview>();

            GraphUserConversationMemberHelper userConversationMemberHelper = new GraphUserConversationMemberHelper(connectorHelper.GraphServiceClient);

            foreach (KeyValuePair<string, Graph.Group> teamGroup in teamGroups)
            {

                Team team = TeamMapper.MapFrom(teamGroup.Value);
                TeamOverview teamOverview = new TeamOverview(team);

                var members = userConversationMemberHelper.GetTeamMembers(team.Id);

                foreach(var member in members)
                {
                    users.TryGetValue(member.Value.UserId, out Graph.User userAsMember);
                    if (userAsMember is null)
                    {
                        // ToDo message or logger?
                        continue;
                    }
                    User user = UserMapper.MapFrom(userAsMember);

                    if (IsOwnerRole(member.Value.Roles.ToList()))
                    {
                        teamOverview.AddOwner(user);
                    }
                    else if (IsGuestRole(member.Value.Roles.ToList()))
                    {
                        teamOverview.AddGuest(user);
                    }
                    else
                    {
                        teamOverview.AddMember(user);
                    }

                }

                if (teamOverview.Owners != null && teamOverview.Owners.Any())
                {
                    teamOverview.Owners = teamOverview.Owners.OrderBy(p => p.Surname).ThenBy(p => p.Firstname).ToList();
                }

                if (teamOverview.Members != null && teamOverview.Members.Any())
                {
                    teamOverview.Members = teamOverview.Members.OrderBy(p => p.Surname).ThenBy(p => p.Firstname).ToList();
                }

                if (teamOverview.Guests != null && teamOverview.Guests.Any())
                {
                    teamOverview.Guests = teamOverview.Guests.OrderBy(p => p.Surname).ThenBy(p => p.Firstname).ToList();
                }

                teamOverviews.Add(teamOverview);
            }

            return teamOverviews.OrderBy(to => to.Team.Name).ToList();
        }

        /// <summary>
        /// Checks if the role list contains owner
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        private bool IsOwnerRole(List<string> roles)
        {
            foreach(string role in roles)
            {
                if (role == "owner") return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the role list contains guest
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        private bool IsGuestRole(List<string> roles)
        {
            foreach (string role in roles)
            {
                if (role == "guest") return true;
            }

            return false;
        }

    }
}
