namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object Team, which partially corresponds to the object <see cref="Microsoft.Graph.Group"/> / <see cref="Microsoft.Graph.Team"/>
    /// </summary>
    public class Team : BaseData
    {
        /// <summary>
        /// Name <see cref="Microsoft.Graph.Group.DisplayName"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description <see cref="Microsoft.Graph.Group.Description"/>
        /// </summary>
        public string Description { get; set; }
    }
}
