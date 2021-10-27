using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// PlannerTaskMapper
    /// </summary>
    public sealed class PlannerTaskMapper : BaseObjectMapper
    {
#pragma warning disable CS1591
        public static PlannerTask MapFrom(Graph.PlannerTask graphPlannerTask)
        {
            var plannerTask = new PlannerTask();
            plannerTask.Id = graphPlannerTask.Id;
            plannerTask.BucketId = graphPlannerTask.BucketId;

            plannerTask.Title = graphPlannerTask.Title;
            plannerTask.TaskDescription = graphPlannerTask.Details?.Description;
            plannerTask.ChecklistItems = MapFrom(graphPlannerTask.Details?.Checklist);
            
            return plannerTask;
        }

        public static Graph.PlannerTask MapTo(PlannerTask plannerTask)
        {
            var graphPlannerTask = new Graph.PlannerTask();

            graphPlannerTask.Id = plannerTask.Id;
            graphPlannerTask.Title = plannerTask.Title;
            graphPlannerTask.BucketId = plannerTask.BucketId;
            graphPlannerTask.Details.Description = plannerTask.TaskDescription;

            return graphPlannerTask;
        }

        private static List<ChecklistItem> MapFrom(Graph.PlannerChecklistItems graphPlannerChecklistItems)
        {
            List<ChecklistItem> checklistItems = new List<ChecklistItem>();
            
            foreach(var graphChecklistItem in graphPlannerChecklistItems)
            {
                ChecklistItem checklistItem = new ChecklistItem();
                checklistItem.Description = graphChecklistItem.Value.Title;
                checklistItem.IsChecked = graphChecklistItem.Value.IsChecked ?? false;
            }

            return checklistItems;
        }

        private static Graph.PlannerChecklistItems MapTo(List<ChecklistItem> checklistItems)
        {
            Graph.PlannerChecklistItems plannerChecklistItems = new Graph.PlannerChecklistItems();

            foreach (var checklistItem in checklistItems)
            {               
                plannerChecklistItems.AddChecklistItem(checklistItem.Description);
            }

            return plannerChecklistItems;
        }

# pragma warning restore CS1591

    }
}
