using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Represents a planner and its buckets
    /// </summary>
    public class PlannerConfiguration
    {
        /// <summary>
        /// Internal Planner
        /// </summary>
        public Planner Planner { get; set; }

        /// <summary>
        /// A List of internal Buckets
        /// </summary>
        public List<Bucket> Buckets { get; set; }
    }
}
