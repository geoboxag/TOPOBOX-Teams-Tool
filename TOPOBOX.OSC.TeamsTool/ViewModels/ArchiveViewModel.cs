using GEOBOX.OSC.Common.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.Domain;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class ArchiveViewModel : INotifyPropertyChanged
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

        private List<Channel> channels;
        public List<Channel> Channels
        {
            get
            {
                return channels;
            }
            set
            {
                channels = value;
                OnPropertyChanged(nameof(Channels));
            }
        }

        private Channel selectedChannel;
        public Channel SelectedChannel
        {
            get
            {
                return selectedChannel;
            }
            set
            {
                selectedChannel = value;
                OnPropertyChanged(nameof(SelectedChannel));
            }
        }

        private string savePath;
        public string SavePath
        {
            get
            {
                return savePath;
            }
            set
            {
                savePath = value;
                OnPropertyChanged(nameof(SavePath));
            }
        }

        private bool messagesCheckBoxIsChecked;
        public bool MessagesCheckBoxIsChecked
        {
            get
            {
                return messagesCheckBoxIsChecked;
            }
            set
            {
                messagesCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(MessagesCheckBoxIsChecked));
            }
        }

        private bool filesCheckBoxIsChecked;
        public bool FilesCheckBoxIsChecked
        {
            get
            {
                return filesCheckBoxIsChecked;
            }
            set
            {
                filesCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(FilesCheckBoxIsChecked));
            }
        }

        private bool tasksCheckBoxIsChecked;
        public bool TasksCheckBoxIsChecked
        {
            get
            {
                return tasksCheckBoxIsChecked;
            }
            set
            {
                tasksCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(TasksCheckBoxIsChecked));
            }
        }

        private bool membersCheckBoxIsChecked;
        public bool MembersCheckBoxIsChecked
        {
            get
            {
                return membersCheckBoxIsChecked;
            }
            set
            {
                membersCheckBoxIsChecked = value;
                OnPropertyChanged(nameof(MembersCheckBoxIsChecked));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructor
        public ArchiveViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            logger = mainViewModel.Logger;
        }
        #endregion


        internal void LoadTeams()
        {
            if (!mainViewModel.IsUserAuthTokenSet())
            {
                logger?.WriteError(Properties.Resources.UserIsNotLoggedInMessage);
                return;
            }
            TeamOverviews = GetTeams();
        }

        internal void LoadChannels()
        {
            Channels = LoadChannelsOfSelectedTeam(SelectedTeamOverview);
        }

        private List<Channel> LoadChannelsOfSelectedTeam(TeamOverview teamOverview)
        {
            if (teamOverview is null)
            {
                logger?.WriteWarning(string.Format(Properties.Resources.NotSelectedWarningMessage, nameof(Team)));
                return new List<Channel>();
            }

            logger?.WriteInformation(string.Format(Properties.Resources.LoadChannelsOfTeamMessage, SelectedTeamOverview.Team.Name));

            // TODO: Implement Load Channels
            logger?.WriteError($"Not Implemented: LoadChannelsOfSelectedTeam()");
            return new List<Channel>();
        }

        internal void ArchivingChannel()
        {
            if (!mainViewModel.IsUserAuthTokenSet() ||
                !IsArchiveFormValid())
            {
                return;
            }

            logger?.WriteError("Not Implemented");
            ArchivingChatMessages();
            ArchiveDriveItems();

        }


        internal void SetRootSavePath(string rootSavePath)
        {
            if (string.IsNullOrEmpty(rootSavePath))
            {
                SavePath = string.Empty;
                logger?.WriteWarning(Properties.Resources.SavePathIsEmptyMessage);
            }
            else
            {
                SavePath = rootSavePath;
                logger?.WriteInformation($"{Properties.Resources.SelectedSavePathMessage}{SavePath}.");
            }
        }


        private List<TeamOverview> GetTeams()
        {
            TeamsOverviewHelper teamsOverviewHelper =
                new TeamsOverviewHelper(mainViewModel.GraphConnectorHelper, logger);

            List<TeamOverview> teamsOverviews = new List<TeamOverview>();
            teamsOverviews.Add(null);
            teamsOverviews.AddRange(teamsOverviewHelper.CollectDataFromConfigFile());

            return teamsOverviews;
        }


        private bool ArchivingChatMessages()
        {
            //LogMessages.Insert(0, Properties.Resources.BeginArchivingChannelItemMessage.Replace("{0}", "Kanal-Nachrichten"));

            //var chatMessagesController = new ChatMessagesController(mainViewModel.AuthenticationProvider);
            //string savePath = Path.Combine(SavePath, SelectedTeam.Team.Name, SelectedChannel.DisplayName);

            //if (chatMessagesController.ArchiveChatMessages(savePath, SelectedTeam.Team.Id, SelectedChannel.Id))
            //{
            //    LogMessages.Insert(0, $"Kanal-Nachrichten archiviert: {SavePath}");
            //    return true;
            //}

            return false;
        }

        private bool ArchiveDriveItems()
        {
            //DriveItemsController driveItemsController = new DriveItemsController(mainViewModel.AuthenticationProvider);
            //return driveItemsController.ArchiveDriveItems(SavePath, SelectedTeam.Team.Id, SelectedChannel.Id);
            return false;

        }


        private bool IsArchiveFormValid()
        {
            if (SelectedTeamOverview is null || SelectedChannel is null || string.IsNullOrEmpty(SavePath))
            {
                logger?.WriteWarning(Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            logger?.WriteInformation(Properties.Resources.RequiredFieldsIsOkMessage);
            return true;
        }


    }
}
