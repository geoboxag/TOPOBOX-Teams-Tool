namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object ChecklistItem, which partially corresponds to the object <see cref="Microsoft.Graph.PlannerChecklistItem"/>
    /// </summary>
    public class ChecklistItem : BaseData
    {
        /// <summary>
        /// Description <see cref="Microsoft.Graph.PlannerChecklistItem.Title"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// IsChecked <see cref="Microsoft.Graph.PlannerChecklistItem.IsChecked"/>
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
