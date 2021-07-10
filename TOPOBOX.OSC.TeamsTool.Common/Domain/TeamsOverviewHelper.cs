using System.Collections.Generic;
using System.IO;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Html.TeamOverview;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Controller
{
    /// <summary>
    /// TeamsOverviewHelper for Mapping Teams and Members, Import and Export Data
    /// </summary>
    public class TeamsOverviewHelper
    {
        private GraphConnectorHelper connectorHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphConnectorHelper"></param>
        public TeamsOverviewHelper(GraphConnectorHelper graphConnectorHelper)
        {
            connectorHelper = graphConnectorHelper;
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
                if (JSONSerializer.WriteJson(saveFilePath, teamsOverview))
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
        private List<TeamOverview> CollectData()
        {
            GraphUserHelper userHelper = new GraphUserHelper(connectorHelper.GraphServiceClient);
            var users = userHelper.GetUsers();
            GraphTeamsHelper teamsHelper = new GraphTeamsHelper(connectorHelper.GraphServiceClient);
            var teamGroups = teamsHelper.GetTeamsFromGroups();
            return MapTeamsAndMembers(teamGroups, users);
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

                teamOverviews.Add(teamOverview);
            }

            return teamOverviews;
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
