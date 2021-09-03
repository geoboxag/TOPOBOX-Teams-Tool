using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using System.IO;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for TasksForm.xaml
    /// </summary>
    public partial class TasksForm : UserControl, INotifyPropertyChanged
    {
        private TasksViewModel tasksViewModel;

        private IList<PlannerConfiguration> plannerConfigurations;
        public IList<PlannerConfiguration> PlannerConfigurations
        {
            get
            {
                return plannerConfigurations;
            }
            set
            {
                plannerConfigurations = value;
                OnPropertyChanged("PlannerConfigurations");
            }
        }

        private PlannerConfiguration selectedPlannerConfiguration;
        public PlannerConfiguration SelectedPlannerConfiguration
        {
            get
            {
                return selectedPlannerConfiguration;
            }
            set
            {
                selectedPlannerConfiguration = value;
                OnPropertyChanged("SelectedPlannerConfiguration");
            }
        }

        private List<Bucket> buckets;
        public List<Bucket> Buckets
        {
            get
            {
                return buckets;
            }
            set
            {
                buckets = value;
                OnPropertyChanged("Buckets");
            }
        }

        private Bucket selectedBucket;
        public Bucket SelectedBucket
        {
            get
            {
                return selectedBucket;
            }
            set
            {
                selectedBucket = value;
                OnPropertyChanged("SelectedBucket");
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string productName = string.Empty;
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        private string description = string.Empty;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        //private PlannerPreviewType previewType = PlannerPreviewType.Checklist;
        //private bool isDescriptionChecked;
        //public bool IsDescriptionChecked
        //{
        //    get
        //    {
        //        return isDescriptionChecked;
        //    }
        //    set
        //    {
        //        isDescriptionChecked = value;
        //        if (IsDescriptionChecked)
        //        {
        //            previewType = PlannerPreviewType.Description;
        //        }
        //        else
        //        {
        //            previewType = PlannerPreviewType.Checklist;
        //        }
        //        OnPropertyChanged("IsDescriptionChecked");
        //    }
        //}

        private Dictionary<string, List<Task>> predefinedTasks;
        public Dictionary<string, List<Task>> PredefinedTasks
        {
            get
            {
                return predefinedTasks;
            }
            set
            {
                predefinedTasks = value;
                OnPropertyChanged("PredefinedTasks");
            }
        }

        private KeyValuePair<string, List<Task>> selectedPredefinedTask;
        public KeyValuePair<string, List<Task>> SelectedPredefinedTask
        {
            get
            {
                return selectedPredefinedTask;
            }
            set
            {
                selectedPredefinedTask = value;
                IsPredefinedTaskSelected = SelectedPredefinedTask.Key != null;
                OnPropertyChanged("SelectedPredefinedTask");
            }
        }

        private bool isPredefinedTaskSelected = false;
        public bool IsPredefinedTaskSelected
        {
            get
            {
                return isPredefinedTaskSelected;
            }
            set
            {
                isPredefinedTaskSelected = value;
                DescriptionIsEnabled = !IsPredefinedTaskSelected;
                OnPropertyChanged("IsPredefinedTaskSelected");
            }
        }

        private bool descriptionIsEnabled = true;
        public bool DescriptionIsEnabled
        {
            get
            {
                return descriptionIsEnabled;
            }
            set
            {
                descriptionIsEnabled = value;
                OnPropertyChanged("DescriptionIsEnabled");
            }
        }

        internal TasksForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.tasksViewModel = new TasksViewModel(mainViewModel.AuthenticationProvider);
            this.DataContext = tasksViewModel;

        }
        
        internal void CreateTaskButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tasksViewModel.AuthenticationProvider != null || 
                !IsPlannerConfigurationSelected() || 
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

        private void Planner_DropDownOpened(object sender, System.EventArgs e)
        {
            if (!tasksViewModel.IsTeamSelected())
            {
                return;
            }

            var rootPath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath, Common.Properties.Settings.Default.RelPathPlannerFolders);
            PlannerConfigurations = JSONSerializer.ReadJson<List<PlannerConfiguration>>(
                Path.Combine(rootPath, Common.Properties.Settings.Default.ConfigFileName));
        }

        private void PredefinedTasks_DropDownOpened(object sender, System.EventArgs e)
        {
            if (!IsPlannerConfigurationSelected())
            {
                return;
            }

            PredefinedTasks = JSONSerializer.ReadJson<Dictionary<string, List<Task>>>(SelectedPlannerConfiguration.Planner.ConfigPath);  

        }

        private void Bucket_DropDownOpened(object sender, System.EventArgs e)
        {
            if (!IsPlannerConfigurationSelected())
            {
                return;
            }
            Buckets = new List<Bucket>(SelectedPlannerConfiguration.Buckets);
        }

        private void ResetFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlannerConfiguration = null;
            SelectedBucket = null;
            Title = string.Empty;
            ProductName = string.Empty;
            SelectedPredefinedTask = new KeyValuePair<string, List<Task>>();
            IsPredefinedTaskSelected = false;
            Description = string.Empty;
        }

        private void CreatePredefinedTaskButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!mainViewModel.IsAuthTokenSet() || 
            //    !IsPlannerConfigurationSelected() || 
            //    !IsTasksFormValid() ||
            //    IsPredefinedTaskSelected)
            //{
            //    return;
            //}

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


        private string GetTaskTitle(string title, string productName, string predefinedTaskTitle = "")
        {
            if (string.IsNullOrEmpty(predefinedTaskTitle) && string.IsNullOrEmpty(productName))
            {
                return title;
            }

            string resultTitle = string.Empty;

            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(title))
            {
                resultTitle = predefinedTaskTitle.Replace("[TitlePlaceHolder]", $"{title} - {productName}");
            }
            else if (!string.IsNullOrEmpty(title))
            {
                resultTitle = predefinedTaskTitle.Replace("[TitlePlaceHolder]", title);
            }

            return resultTitle;
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


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Validate-Methods
        private bool IsBucketSelected()
        {
            if (SelectedBucket is null)
            {
                string message = Properties.Resources.SelectAnyItemFromListMessage.Replace("{0}", "Bucket");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsPlannerConfigurationSelected()
        {
            if (SelectedPlannerConfiguration is null)
            {
                string message = Properties.Resources.SelectAnyItemFromListMessage.Replace("{0}", "Planner");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        private bool IsTasksFormValid()
        {
            if (string.IsNullOrEmpty(Title))
            {
                MessageBox.Show(Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }



        #endregion

    }
}
