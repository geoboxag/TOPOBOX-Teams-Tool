using GEOBOX.OSC.Common.Logging;
using TOPOBOX.OSC.TeamsTool.Common.Mapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.Domain;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using Directory = System.IO.Directory;
using PlannerTask = TOPOBOX.OSC.TeamsTool.Common.DAL.PlannerTask;
using Microsoft.Graph.Models;
//using System.Xml.Serialization;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        #region Properties and Attributes

        private MainViewModel mainViewModel;
        private ILogger logger;

        private List<TeamOverview> teamOverviews;
        public List<TeamOverview> TeamOverviews
        {
            get
            {
                return teamOverviews;
            }
            set
            {
                teamOverviews = value;
                OnPropertyChanged(nameof(TeamOverviews));
            }
        }

        private TeamOverview selectedTeamOverview;
        public TeamOverview SelectedTeamOverview
        {
            get
            {
                return selectedTeamOverview;
            }
            set
            {
                selectedTeamOverview = value;
                OnPropertyChanged(nameof(SelectedTeamOverview));
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
                OnPropertyChanged(nameof(PlannerConfigurations));
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
                OnPropertyChanged(nameof(SelectedPlannerConfiguration));
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
                OnPropertyChanged(nameof(SelectedBucket));
            }
        }

        private List<UserOverview> userOverviews;
        public List<UserOverview> UserOverviews
        {
            get
            {
                return userOverviews;
            }
            set
            {
                userOverviews = value;
                OnPropertyChanged(nameof(UserOverviews));
            }
        }

        private UserOverview selectedUserOverview;
        public UserOverview SelectedUserOverview
        {
            get
            {
                return selectedUserOverview;
            }
            set
            {
                selectedUserOverview = value;
                OnPropertyChanged(nameof(SelectedUserOverview));
            }
        }

        private ObservableCollection<UserOverview> assignedUsers;
        public ObservableCollection<UserOverview> AssignedUsers
        {
            get
            {
                return assignedUsers;
            }
            set
            {
                assignedUsers = value;
                OnPropertyChanged(nameof(AssignedUsers));
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
                OnPropertyChanged(nameof(Title));
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
                OnPropertyChanged(nameof(ProductName));
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
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanged(nameof(ChecklistEntry));
            }
        }

        private bool checklistIsChecked = true;
        public bool ChecklistIsChecked
        {
            get
            {
                return checklistIsChecked;
            }
            set
            {
                checklistIsChecked = value;
                OnPropertyChanged(nameof(ChecklistIsChecked));
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
                OnPropertyChanged(nameof(ChecklistEntries));
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
                OnPropertyChanged(nameof(IsPredefinedTaskSelected));
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
                OnPropertyChanged(nameof(DescriptionIsEnabled));
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
            logger = mainViewModel.Logger;
        }
        #endregion

        #region Methods for Loading Data from Files
        internal void LoadData()
        {
            LoadTeams();
            LoadUsers();
            LoadPlanners();
            LoadChannels();
            SetSelectedTeamToDefault();
            LoadPredefinedPlannerTasks();
        }

        private void LoadTeams()
        {
            TeamsOverviewHelper teamsOverviewHelper =
                new TeamsOverviewHelper(mainViewModel.GraphConnectorHelper, logger);

            List<TeamOverview> teamsOverviews = new List<TeamOverview>();
            teamsOverviews.Add(new TeamOverview(new Common.DAL.Team()));
            teamsOverviews.AddRange(teamsOverviewHelper.CollectDataFromConfigFile());
            teamsOverviews.AddRange(teamsOverviewHelper.CollectData());

            TeamOverviews = teamsOverviews;
        }

        private void LoadPlanners()
        {
            PlannerOverviewHelper plannerOverviewHelper =
                new PlannerOverviewHelper(mainViewModel.GraphConnectorHelper, logger);

            List<PlannerConfiguration> plannersConfigurations = new List<PlannerConfiguration>();
            plannersConfigurations.Add(new PlannerConfiguration()
            {
                Planner = new Common.DAL.Planner()
            });
            plannersConfigurations.AddRange(plannerOverviewHelper.CollectMyData());
            plannersConfigurations.AddRange(plannerOverviewHelper.CollectData());

            PlannerConfigurations = plannersConfigurations;
        }

        private void LoadPredefinedPlannerTasks()
        {
            var folderPath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                                    Common.Properties.Settings.Default.RelPathPlannerFolders);

            if (!Directory.Exists(folderPath)) return;

            var predefinedPlannerTask = new Dictionary<string, List<PlannerTask>>();
            predefinedPlannerTask.Add("", null);

            logger?.WriteInformation(string.Format(Properties.Resources.ListFoldersMessage, folderPath));
            var directories = Directory.GetDirectories(folderPath);
            foreach(var dir in directories)
            {
                logger?.WriteInformation(string.Format(Properties.Resources.ListFilesMessage, dir));
                foreach (var file in Directory.GetFiles(dir))
                {                   
                    try
                    {
                        List<PlannerTask> plannerTasks = XmlSerializer.ReadXml<List<PlannerTask>>(file, logger);

                        if (plannerTasks.Any())
                        {
                            predefinedPlannerTask.Add(Path.GetFileName(file), plannerTasks);
                        }
                        else
                        {
                            logger?.WriteWarning(Common.Properties.Resources.NoEntriesInListFoundMessage);
                        }
                    }
                    catch
                    {
                        logger?.WriteWarning(string.Format(Common.Properties.Resources.ReadFromFileErrorMessage, file));
                    }                  
                }
            }

            if (!predefinedPlannerTask.Any())
            {
                logger?.WriteWarning($"{Common.Properties.Resources.NoEntriesInListFoundMessage}{folderPath}");
            }

            PredefinedPlannerTasks = predefinedPlannerTask;
        }

        internal void CreatePlannerTask()
        {
            if (mainViewModel.IsUserAuthCredentialsSet() ||
                !IsTeamSelected() ||
                !IsPlannerConfigurationSelected() ||
                !IsBucketSelected() ||
                !IsTasksFormValid() ||
                !IsPredefinedPlannerTaskSelected())
            {
                return;
            }

            logger?.WriteError("Not Implemented");
        }

        internal void AssignSelectedUser()
        {
            if (AssignedUsers == null)
            {
                AssignedUsers = new ObservableCollection<UserOverview>();
            }

            if (SelectedUserOverview != null)
            {
                AssignedUsers.Add(SelectedUserOverview);
            }

            SelectedUserOverview = null;
        }
         internal void CreatePredefinedPlannerTaskFromXml(string path)
        {
            PlannerTask plannerTask = new PlannerTask();
            try
            {
                 plannerTask = XmlSerializer.ReadXml<PlannerTask>(path, logger);

                if (plannerTask != null)
                {
                    plannerTask.Id = System.Guid.NewGuid().ToString();
                    plannerTask.BucketId = selectedBucket.Id;
                    plannerTask.Title = plannerTask.Title.Replace("({$userInput$})", title);
                    plannerTask.TaskDescription = description;

                    //predefinedPlannerTask.Add(Path.GetFileName(path), plannerTasks);
                }
                else
                {
                    logger?.WriteWarning(Common.Properties.Resources.NoEntriesInListFoundMessage);
                }
            }
            catch
            {
                logger?.WriteWarning(string.Format(Common.Properties.Resources.ReadFromFileErrorMessage, path));
            }
            GraphPlannerTaskHelper graphPlannerTaskHelper =
                   new GraphPlannerTaskHelper(mainViewModel.GraphConnectorHelper.GraphServiceClient);

            var plannerTaskGraph = PlannerTaskMapper.MapTo(plannerTask);
            plannerTaskGraph.PlanId = selectedPlannerConfiguration.Planner.Id;
            plannerTaskGraph.Details.PreviewType = GetPlannerPreviewType();
            plannerTaskGraph.OdataType = "Microsoft.Graph.PlannerTask";

            if (AssignedUsers != null && AssignedUsers.Any())
            {
                plannerTaskGraph.Assignments = GetPlannerAssignments(AssignedUsers);
            }
            var result = graphPlannerTaskHelper.SendPlannerTask(plannerTaskGraph);

            if (result != null && result.Title == plannerTask.Title)
            {
                logger?.WriteInformation(string.Format(Properties.Resources.CreatedPlannerTaskMessage,
                    plannerTask.Title,
                    SelectedBucket.Name));
            }
            else
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotCreatedPlannerTaskMessage,
                    plannerTask.Title,
                    SelectedBucket.Name));
            }

        }


        internal void CreatePredefinedPlannerTask()
        {
            if (!mainViewModel.IsUserAuthCredentialsSet() ||
                !IsTeamSelected() ||
                !IsPlannerConfigurationSelected() ||
                !IsBucketSelected() ||
                !IsTasksFormValid() ||
                !IsPredefinedPlannerTaskSelected())
            {
                return;
            }

            foreach (var predefinedTask in SelectedPredefinedPlannerTask.Value)
            {
                if (predefinedTask != null)
                {
                    predefinedTask.Title = GetTaskTitle(Title, ProductName, predefinedTask.Title);
                    logger?.WriteInformation(string.Format(Properties.Resources.ReplacedTitleMessage, predefinedTask.Title));
                }
                
                var plannerTask = PlannerTaskMapper.MapTo(predefinedTask);
                plannerTask.BucketId = SelectedBucket.Id;
                plannerTask.PlanId = SelectedPlannerConfiguration.Planner.Id;

                plannerTask.Details.PreviewType = GetPlannerPreviewType();

                if (AssignedUsers != null && AssignedUsers.Any())
                {
                    plannerTask.Assignments = GetPlannerAssignments(AssignedUsers);
                }

                GraphPlannerTaskHelper graphPlannerTaskHelper =
                    new GraphPlannerTaskHelper(mainViewModel.GraphConnectorHelper.GraphServiceClient);

                var result = graphPlannerTaskHelper.SendPlannerTask(plannerTask);

                if (result != null && result.Title == plannerTask.Title)
                {
                    logger?.WriteInformation(string.Format(Properties.Resources.CreatedPlannerTaskMessage, 
                        plannerTask.Title, 
                        SelectedBucket.Name));
                }
                else
                {
                    logger?.WriteWarning(string.Format(Properties.Resources.NotCreatedPlannerTaskMessage,
                        plannerTask.Title,
                        SelectedBucket.Name));
                }

            }
        }

        private PlannerPreviewType GetPlannerPreviewType()
        {
            if (ChecklistIsChecked)
            {
                return PlannerPreviewType.Checklist;
            }
            return PlannerPreviewType.Description;
        }

        private PlannerAssignments GetPlannerAssignments(IEnumerable<Common.DAL.UserOverview> userOverviews)
        {
            //TODO: figure out what why
            var assignments = new PlannerAssignments();
             foreach (var userOverview in userOverviews)
             {
                //assignments.GetFieldDeserializers().Add(userOverview.User.Id);
            }

            return assignments;
        }

        private void LoadChannels()
        {
            //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //                        Common.Properties.Settings.Default.RelFilePathChannelJson);

            //var channels = JSONSerializer.ReadJson<List<Channel>>(filePath);

            //if (!channels.Any())
            //{
            //    logger?.WriteError($"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            //}

            //Channels = channels;
        }

        private void LoadUsers()
        {
            UsersOverviewHelper usersOverviewHelper =
                new UsersOverviewHelper(mainViewModel.GraphConnectorHelper, logger);

            List<UserOverview> userOverviews = new List<UserOverview>();
            userOverviews.Add(null);
            userOverviews.AddRange(usersOverviewHelper.CollectDataFromConfigFile());
            userOverviews.AddRange(usersOverviewHelper.CollectData());

            UserOverviews = userOverviews;
        }

        #endregion

        #region Help-Methods
        private void SetSelectedTeamToDefault()
        {
            if (TeamOverviews.Any())
            {
                SelectedTeamOverview = TeamOverviews.Find(team => team.Team.Name.Equals(
                    Common.Properties.Settings.Default.DefaultSelectedTeamName));
            }
        }

        private string GetTaskTitle(string title, string productName, string predefinedTaskTitle = null)
        {
            if (string.IsNullOrEmpty(predefinedTaskTitle) && string.IsNullOrEmpty(productName))
            {
                return title;
            }

            string resultTitle = string.Empty;

            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(title))
            {
                resultTitle = predefinedTaskTitle.Replace("[TitlePlaceHolder]", $"{title} - {productName}");
                return resultTitle;
            }
            
            if (!string.IsNullOrEmpty(title))
            {
                resultTitle = predefinedTaskTitle.Replace("[TitlePlaceHolder]", title);
                return resultTitle;
            }

            return resultTitle;
        }

        internal void ResetFields()
        {
            SelectedTeamOverview = null;
            SelectedPlannerConfiguration = null;
            SelectedBucket = null;
            Title = string.Empty;
            ProductName = string.Empty;
            ChecklistEntry = string.Empty;
            ChecklistIsChecked = true;
            ChecklistEntries.Clear();
            SelectedPredefinedPlannerTask = new KeyValuePair<string, List<PlannerTask>>();
            SelectedUserOverview = null;
            AssignedUsers = new ObservableCollection<UserOverview>();
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
            if (SelectedTeamOverview is null)
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage, 
                    Properties.Resources.TeamName));
                return false;
            }

            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage, 
                Properties.Resources.TeamName, 
                SelectedTeamOverview.Team.Name));
            return true;
        }

        internal bool IsBucketSelected()
        {
            if (SelectedBucket is null)
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage, 
                    Properties.Resources.BucketName));
                return false;
            }

            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage, 
                Properties.Resources.BucketName, SelectedBucket.Name));
            return true;
        }

        internal bool IsPlannerConfigurationSelected()
        {
            if (SelectedPlannerConfiguration is null)
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage, 
                    Properties.Resources.PlannerName));
                return false;
            }

            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage, 
                Properties.Resources.PlannerName, 
                SelectedPlannerConfiguration.Planner.Name));
            return true;
        }

        internal bool IsPredefinedPlannerTaskSelected()
        {
            if (string.IsNullOrEmpty(SelectedPredefinedPlannerTask.Key))
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage,
                    Properties.Resources.PredefinedTaskName));
                return false;
            }

            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage,
                Properties.Resources.PredefinedTaskName,
                SelectedPredefinedPlannerTask.Key));
            return true;
        }

        internal bool IsTasksFormValid()
        {
            if (string.IsNullOrEmpty(Title))
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage,
                    Properties.Resources.TitleName));
                return false;
            }

            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage,
                Properties.Resources.TitleName,
                Title));
            logger?.WriteInformation(string.Format(Properties.Resources.IsSelectedMessage,
                Properties.Resources.ProductName,
                ProductName));
            return true;
        }
        #endregion

    }
}
