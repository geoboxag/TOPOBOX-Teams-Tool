using System.Windows;
using System.Windows.Controls;

using TOPOBOX.OSC.TeamsTool.ViewModels;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for TasksForm.xaml
    /// </summary>
    public partial class TasksForm : UserControl
    {
        private TasksViewModel tasksViewModel;

        internal TasksForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.tasksViewModel = new TasksViewModel(mainViewModel);
            this.DataContext = tasksViewModel;
        }
        
        internal void CreateTaskButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tasksViewModel.AuthenticationProvider != null ||
                !IsTeamSelected() ||
                !IsPlannerSelected() || 
                !IsBucketSelected() ||
                !IsTasksFormValid()) 
            {
                return;
            }

            //PlannerTask plannerTask;

            //if (mainViewModel.IsUserSelected())
            //{
            //    plannerTask = CreatePlannerTask(SelectedPlannerConfiguration.GbxPlanner.Id,
            //        SelectedBucket.Id,
            //        GetTaskTitle(Title, ProductName),
            //        Description,
            //        mainViewModel.SelectedUser.Id);
            //}
            //else
            //{
            //    plannerTask = CreatePlannerTask(SelectedPlannerConfiguration.GbxPlanner.Id,
            //        SelectedBucket.Id,
            //        GetTaskTitle(Title, ProductName),
            //        Description);
            //}

            //if (plannerTask != null)
            //{
            //    SendTask(plannerTask);
            //}

        }

        private void ResetFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            tasksViewModel.ResetFields();
        }

        private void CreatePredefinedTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (tasksViewModel.AuthenticationProvider != null ||
                !IsTeamSelected() ||
                !IsPlannerSelected() ||
                !IsBucketSelected() ||
                !IsPredefinedPlannerTaskSelected())
            {
                return;
            }

            //var mappingToGraphItem = new GraphItemMapping();

            //foreach (var predefinedTask in selectedPredefinedTask.Value)
            //{
            //    if (predefinedTask != null)
            //    {
            //        predefinedTask.Title = GetTaskTitle(Title, ProductName, predefinedTask.Title);
            //    }

            //    var plannerTask = mappingToGraphItem.MapGbxTaskToPlannerTask(predefinedTask, previewType);
            //    plannerTask.BucketId = predefinedTask.BucketId;
            //    plannerTask.PlanId = SelectedPlannerConfiguration.GbxPlanner.Id;
            //    if (mainViewModel.SelectedUser != null)
            //    {
            //        plannerTask.Assignments = mappingToGraphItem.GetPlannerAssignments(mainViewModel.SelectedUser.Id);
            //    }

            //    SendTask(plannerTask);
            //}

        }

        //private PlannerTask CreatePlannerTask(string planId, string bucketId, string title, string description, string userId = null)
        //{
        //    //var graphItemMapping = new GraphItemMapping();

        //    //return new PlannerTask()
        //    //{
        //    //    PlanId = planId,
        //    //    BucketId = bucketId,
        //    //    Title = title,
        //    //    Assignments = graphItemMapping.GetPlannerAssignments(userId),
        //    //    Details = graphItemMapping.GetPlannerTaskDetails(description, previewType)
        //    //};
        //    return null;
        //}


        //private void SendTask(PlannerTask plannerTask)
        //{
        //    //GraphConnector graphConnector = new GraphConnector(mainViewModel.AuthenticationProvider);
        //    //StatusMessageLbl.Content = "wird gesendet...";

        //    //Parallel.Invoke(() =>
        //    //{
        //    //    var task = graphConnector.SendPlannerTask(plannerTask);
        //    //    task.Start();
        //    //    if (task.Result)
        //    //    {
        //    //        Dispatcher.Invoke(() => StatusMessageLbl.Content = "Aufgabe erstellt!");
        //    //    }
        //    //    else
        //    //    {
        //    //        Dispatcher.Invoke(() => StatusMessageLbl.Content = "Aufgabe nicht erstellt!");
        //    //    }
        //    //});

        //}


        private void ChecklistInputTxtBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(System.Windows.Input.Key.Enter))
            {
                tasksViewModel.AddChecklistEntry();
            }
        }

        #region Validate-Methods
        private bool IsTeamSelected()
        {
            if (tasksViewModel.IsTeamSelected())
            {
                string message = string.Format(Properties.Resources.SelectAnyItemFromListMessage, "Team");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsBucketSelected()
        {
            if (tasksViewModel.IsBucketSelected())
            {
                string message = string.Format(Properties.Resources.SelectAnyItemFromListMessage, "Bucket");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsPlannerSelected()
        {
            if (tasksViewModel.IsPlannerConfigurationSelected())
            {
                string message = string.Format(Properties.Resources.SelectAnyItemFromListMessage, "Planner");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsPredefinedPlannerTaskSelected()
        {
            if (tasksViewModel.IsPredefinedPlannerTaskSelected())
            {
                string message = string.Format(Properties.Resources.SelectAnyItemFromListMessage, "Planner");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsTasksFormValid()
        {
            if (tasksViewModel.IsTasksFormValid())
            {
                MessageBox.Show(Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }
        #endregion

    }
}
