using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object PlannerTask, which partially corresponds to the object <see cref="Microsoft.Graph.PlannerTask"/>
    /// </summary>
    public class PlannerTask : BaseData
    {
        /// <summary>
        /// BucketId <see cref="Microsoft.Graph.PlannerTask.BucketId"/>
        /// </summary>
        public string BucketId { get; set; }

        /// <summary>
        /// Title <see cref="Microsoft.Graph.PlannerTask.Title"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// TaskDescription <see cref="Microsoft.Graph.PlannerTaskDetails.Description"/>
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// A List of internal ChecklistItems <see cref="ChecklistItem"/>
        /// </summary>
        public List<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();

    }
}
