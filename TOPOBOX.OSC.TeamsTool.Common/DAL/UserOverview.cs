using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// UserOverview
    /// </summary>
    public class UserOverview
    {
        public User User { get; set; } = new User();

        public List<Team> Teams { get; set; }


        public UserOverview(User user)
        {
            User = user;
            
        }

        public void AddTeam(Team team)
        {
            if (Teams is null) Teams = new List<Team>();

            Teams.Add(team);
        }

    }
}
