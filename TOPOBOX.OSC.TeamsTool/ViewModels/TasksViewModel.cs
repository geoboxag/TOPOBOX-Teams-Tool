using Microsoft.Graph.Auth;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.IO;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        #region Properties and Attributes

        private MainViewModel mainViewModel;

        private InteractiveAuthenticationProvider authenticationProvider;
        public InteractiveAuthenticationProvider AuthenticationProvider
        {
            internal get
            {
                return authenticationProvider;
            }
            set
            {
                authenticationProvider = value;
            }
        }

        private List<TeamOverview> teams;
        public List<TeamOverview> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        private TeamOverview selectedTeam;
        public TeamOverview SelectedTeam
        {
            get
            {
                return selectedTeam;
            }
            set
            {
                selectedTeam = value;
                OnPropertyChanged(nameof(SelectedTeam));
            }
        }

        private List<PlannerConfiguration> plannerConfigurations;
        public List<PlannerConfiguration> PlannerConfigurations
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

        private List<UserOverview> users;
        public List<UserOverview> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private UserOverview selectedUser;
        public UserOverview SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
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

        private string checklistEntry = string.Empty;
        public string ChecklistEntry
        {
            get
            {
                return checklistEntry;
            }
            set
            {
                checklistEntry = value;
                OnPropertyChanged("ChecklistEntry");
            }
        }

        private ObservableCollection<string> checklistEntries = new ObservableCollection<string>();
        public ObservableCollection<string> ChecklistEntries
        {
            get
            {
                return checklistEntries;
            }
            set
            {
                checklistEntries = value;
                OnPropertyChanged("ChecklistEntries");
            }
        }

        private Dictionary<string, List<PlannerTask>> predefinedPlannerTasks;
        public Dictionary<string, List<PlannerTask>> PredefinedPlannerTasks
        {
            get
            {
                return predefinedPlannerTasks;
            }
            set
            {
                predefinedPlannerTasks = value;
                OnPropertyChanged(nameof(PredefinedPlannerTasks));
            }
        }

        private KeyValuePair<string, List<PlannerTask>> selectedPredefinedPlannerTask;
        public KeyValuePair<string, List<PlannerTask>> SelectedPredefinedPlannerTask
        {
            get
            {
                return selectedPredefinedPlannerTask;
            }
            set
            {
                selectedPredefinedPlannerTask = value;
                IsPredefinedTaskSelected = SelectedPredefinedPlannerTask.Key != null;
                OnPropertyChanged(nameof(SelectedPredefinedPlannerTask));
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructor
        public TasksViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            LoadConfigFiles();
            LoadUserSpecificContent();
        }
        #endregion

        #region Methods for Loading Data from Files
        private void LoadConfigFiles()
        {
            LoadTeams();
            SetSelectedTeamToDefault();
            LoadPlannerConfigurations();
            LoadChannels();
            LoadUsers();
        }
        private void LoadUserSpecificContent()
        {
            LoadPlannerConfigurations();
        }

        private void LoadTeams()
        {
            string filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                    Common.Properties.Settings.Default.RelFilePathTeamsJson);
            var teamsOverview = JSONSerializer.ReadJson<List<TeamOverview>>(filePath);

            if (!teamsOverview.Any())
            {
                mainViewModel.Logger.WriteError($"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            }

            Teams = teamsOverview;
        }

        private void LoadPlannerConfigurations()
        {
            var folderPath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                                    Common.Properties.Settings.Default.RelPathPlannerFolders);

            //PlannerOverviewHelper plannerOverviewHelper = new PlannerOverviewHelper(
            //    mainViewModel.GraphConnectorHelper, 
            //    new Common.Logging.Logger());

            var plannerConfigs = new List<PlannerConfiguration>(); //plannerOverviewHelper.GetMyData();
            if (!plannerConfigs.Any())
            {
                mainViewModel.Logger.WriteError($"{Properties.Resources.ErrorReadingFileMessage}{folderPath}");
            }

            PlannerConfigurations = plannerConfigs;
        }

        private void LoadChannels()
        {
            //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //                        Common.Properties.Settings.Default.RelFilePathChannelJson);

            //var channels = JSONSerializer.ReadJson<List<Channel>>(filePath);

            //if (!channels.Any())
            //{
            //    LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            //}

            //Channels = channels;
        }

        private void LoadUsers()
        {
            var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                                    Common.Properties.Settings.Default.RelFilePathUsersJson);

            var users = JSONSerializer.ReadJson<List<UserOverview>>(filePath);

            if (!users.Any())
            {
                mainViewModel.Logger.WriteError($"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            }

            Users = users;
        }
        #endregion

        #region Help-Methods
        private void SetSelectedTeamToDefault()
        {
            SelectedTeam = Teams.Find(team => team.Team.Name.Equals(
                Common.Properties.Settings.Default.DefaultSelectedTeamName));
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

        internal void ResetFields()
        {
            SelectedPlannerConfiguration = null;
            SelectedBucket = null;
            Title = string.Empty;
            ProductName = string.Empty;
            ChecklistEntry = string.Empty;
            ChecklistEntries.Clear();
            SelectedPredefinedPlannerTask = new KeyValuePair<string, List<PlannerTask>>();
            IsPredefinedTaskSelected = false;
            Description = string.Empty;
        }

        internal void AddChecklistEntry()
        {
            if(ChecklistEntry.Length > 0)
            {
                ChecklistEntries.Add(ChecklistEntry);
                ChecklistEntry = string.Empty;
            }
        }
        #endregion


        #region Validate-Methods

        internal bool IsTeamSelected()
        {
            return !(SelectedTeam is null);
        }

        internal bool IsBucketSelected()
        {
            return !(SelectedBucket is null);
        }

        internal bool IsPlannerConfigurationSelected()
        {
            return !(SelectedPlannerConfiguration is null);
        }

        internal bool IsPredefinedPlannerTaskSelected()
        {
            if (SelectedPredefinedPlannerTask.Key != null)
            {
                return false;
            }
            return true;
        }

        internal bool IsTasksFormValid()
        {
            return !string.IsNullOrEmpty(Title);
        }
        #endregion


    }
}
