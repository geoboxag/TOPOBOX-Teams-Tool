using System;
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
            this.tasksViewModel = new TasksViewModel(mainViewModel);
            InitializeComponent();
            this.DataContext = tasksViewModel;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            tasksViewModel.LoadData();
        }
        
        internal void CreateTaskButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            tasksViewModel.CreatePlannerTask();
        }

        private void ResetFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            tasksViewModel.ResetFields();
        }

        private void AssignUserButton_Click(object sender, RoutedEventArgs e)
        {
            tasksViewModel.AssignSelectedUser();
        }

        private void CreatePredefinedTaskButton_Click(object sender, RoutedEventArgs e)
        {
            tasksViewModel.CreatePredefinedPlannerTask();
        }


        private void ChecklistInputTxtBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(System.Windows.Input.Key.Enter))
            {
                tasksViewModel.AddChecklistEntry();
            }
        }
    }
}
