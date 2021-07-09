using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// PlannerTask
    /// </summary>
    public class PlannerTask : BaseData
    {
        public string BucketId { get; set; } 

        public string Title { get; set; }

        public string TaskDescription { get; set; }

        public bool IsChecked { get; set; }

        public List<ChecklistItem> Checklist { get; set; } = new List<ChecklistItem>();

    }
}
