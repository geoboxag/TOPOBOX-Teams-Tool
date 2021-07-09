using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// TeamOverview
    /// </summary>
    public class TeamOverview
    {
        public Team Team { get; set; } = new Team();

        public List<User> Owners { get; set; }

        public List<User> Members { get; set; }

        public List<User> Guests { get; set; }


        public TeamOverview(Team team)
        {
            Team = team;
            
        }

        public void AddOwner(User member)
        {
            if (Owners is null) Owners = new List<User>();

            Owners.Add(member);
        }
        public void AddMember(User member)
        {
            if (Members is null) Members = new List<User>();

            Members.Add(member);
        }

        public void AddGuest(User member)
        {
            if(Guests is null) Guests = new List<User>();

            Guests.Add(member);
        }
    }
}
