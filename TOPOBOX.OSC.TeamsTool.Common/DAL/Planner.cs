namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object Planner, which partially corresponds to the object <see cref="Microsoft.Graph.PlannerPlan"/>
    /// </summary>
    public class Planner : BaseData
    {
        /// <summary>
        /// Name <see cref="Microsoft.Graph.PlannerPlan.Title"/>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Internal path to the config of the planner
        /// </summary>
        public string ConfigPath { get; set; }
    }
}
