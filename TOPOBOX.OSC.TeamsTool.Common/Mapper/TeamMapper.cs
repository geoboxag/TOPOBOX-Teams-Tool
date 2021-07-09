using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// TeamMapper
    /// </summary>
    public sealed class TeamMapper : BaseObjectMapper
    {
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
    }
}
