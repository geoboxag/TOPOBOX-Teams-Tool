using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// PlannerConfiguration
    /// </summary>
    public class PlannerConfiguration
    {
        public Planner Planner { get; set; }
        public List<Bucket> Buckets { get; set; }
    }
}
