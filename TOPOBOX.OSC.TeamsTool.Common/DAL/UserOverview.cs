using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Representing an user with its joined teams
    /// </summary>
    public class UserOverview
    {
        /// <summary>
        /// Internal User 
        /// </summary>
        public User User { get; set; } = new User();

        /// <summary>
        /// A List of internal Teams
        /// </summary>
        public List<Team> Teams { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>
        public UserOverview(User user)
        {
            User = user;           
        }

        /// <summary>
        /// Add a team to the Teams (creates a new List, if its null)
        /// </summary>
        /// <param name="team"></param>
        public void AddTeam(Team team)
        {
            if (Teams is null) Teams = new List<Team>();

            Teams.Add(team);
        }

    }
}
