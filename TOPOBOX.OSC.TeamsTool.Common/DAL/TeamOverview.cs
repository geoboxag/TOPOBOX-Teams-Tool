using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Representing a team with its owners, members and guests
    /// </summary>
    public class TeamOverview
    {
        /// <summary>
        /// Team <see cref="DAL.Team"/>
        /// </summary>
        public Team Team { get; set; } = new Team();

        /// <summary>
        /// A List of internal Users (owners of the team)
        /// </summary>
        public List<User> Owners { get; set; }

        /// <summary>
        /// A List of internal Users (members of the team)
        /// </summary>
        public List<User> Members { get; set; }

        /// <summary>
        /// A List of internal Users (guests of the team)
        /// </summary>
        public List<User> Guests { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="team"></param>
        public TeamOverview(Team team)
        {
            Team = team;          
        }

        /// <summary>
        /// Add an owner to the Owners (creates a new List, if its null)
        /// </summary>
        /// <param name="owner"></param>
        public void AddOwner(User owner)
        {
            if (Owners is null) Owners = new List<User>();

            Owners.Add(owner);
        }

        /// <summary>
        /// Add a member to the Members (creates a new List, if its null)
        /// </summary>
        /// <param name="member"></param>
        public void AddMember(User member)
        {
            if (Members is null) Members = new List<User>();

            Members.Add(member);
        }

        /// <summary>
        /// Add a guest to the Guests (creates a new List, if its null)
        /// </summary>
        /// <param name="guest"></param>
        public void AddGuest(User guest)
        {
            if(Guests is null) Guests = new List<User>();

            Guests.Add(guest);
        }
    }
}
