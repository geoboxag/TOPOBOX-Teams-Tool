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
        public static 
            PlannerTask MapFrom(Graph.Models.PlannerTask graphPlannerTask)
        {
            var plannerTask = new PlannerTask();
            plannerTask.Id = graphPlannerTask.Id;
            plannerTask.BucketId = graphPlannerTask.BucketId;

            plannerTask.Title = graphPlannerTask.Title;
            plannerTask.TaskDescription = graphPlannerTask.Details?.Description;
            plannerTask.ChecklistItems = MapFrom(graphPlannerTask.Details?.Checklist);
            
            return plannerTask;
        }

        public static Graph.Models.PlannerTask MapTo(PlannerTask plannerTask)
        {
            var graphPlannerTask = new Graph.Models.PlannerTask();

            graphPlannerTask.Id = plannerTask.Id;
            graphPlannerTask.Title = plannerTask.Title;
            graphPlannerTask.BucketId = plannerTask.BucketId;
            graphPlannerTask.Details = new Graph.Models.PlannerTaskDetails();
            graphPlannerTask.Details.Description = plannerTask.TaskDescription;
            graphPlannerTask.Details.Checklist = MapTo(plannerTask.ChecklistItems);

            return graphPlannerTask;
        }

        private static List<ChecklistItem> MapFrom(Graph.Models.PlannerChecklistItems graphPlannerChecklistItems)
        {
            List<ChecklistItem> checklistItems = new List<ChecklistItem>();

            //foreach (var graphChecklistItem in graphPlannerChecklistItems.AdditionalData)
            //{
            //    ChecklistItem checklistItem = new ChecklistItem();
            //checklistItem.Title = graphChecklistItem.Value.Title;
            //    checklistItem.IsChecked = graphChecklistItem.Value.IsChecked ?? false;
            //}

            return checklistItems;
        }

        private static Graph.Models.PlannerChecklistItems MapTo(List<ChecklistItem> checklistItems)
        {
            Graph.Models.PlannerChecklistItems plannerChecklistItems = new Graph.Models.PlannerChecklistItems();
            //TODO: figure out what why
            //List<ChecklistItem> ChecklistList = new List<ChecklistItem>();
         
            foreach (var checklistItem in checklistItems)
            {
                if (!string.IsNullOrEmpty(checklistItem.Title))
                {
                    plannerChecklistItems.AdditionalData.Add(System.Guid.NewGuid().ToString(), new
                    {
                        OdataType = "microsoft.graph.plannerChecklistItem",
                        Title = checklistItem.Title,
                        IsChecked = checklistItem.IsChecked,
                    });
                }
            }

            return plannerChecklistItems;
        }

# pragma warning restore CS1591

    }
}
