using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph.Models;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// TeamMapper
    /// </summary>
    public sealed class TeamMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static Team MapFrom(Graph.Team graphTeam)
        {
            var team = new Team();
            team.Id = graphTeam.Id;
            team.Name = graphTeam.DisplayName;
            team.Description = graphTeam.Description;

            return team;
        }

        public static Team MapFrom(Graph.Group graphGroup)
        {
            var team = new Team();
            team.Id = graphGroup.Id;
            team.Name = graphGroup.DisplayName;
            team.Description = graphGroup.Description;

            return team;
        }
# pragma warning restore CS1591
    }
}
