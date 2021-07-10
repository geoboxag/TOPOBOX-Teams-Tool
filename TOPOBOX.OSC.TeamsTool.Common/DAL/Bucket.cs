namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object ChatMessage, which partially corresponds to the object <see cref="Microsoft.Graph.PlannerBucket"/>
    /// </summary>
    public class Bucket : BaseData
    {
        /// <summary>
        /// Name <see cref="Microsoft.Graph.PlannerBucket.Name"/>
        /// </summary>
        public string Name { get; set; }
    }
}