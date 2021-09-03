using Microsoft.Graph.Auth;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.IO;
using Channel = TOPOBOX.OSC.TeamsTool.Common.DAL.Channel;
using User = TOPOBOX.OSC.TeamsTool.Common.DAL.User;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class TasksViewModel
    {
        #region Properties and Attributes

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

        private ObservableCollection<string> logMessages = new ObservableCollection<string>();
        public ObservableCollection<string> LogMessages
        {
            get
            {
                return logMessages;
            }
            set
            {
                logMessages = value;
                OnPropertyChanged(nameof(LogMessages));
            }
        }

        private InteractiveAuthenticationProvider authenticationProvider;
        internal InteractiveAuthenticationProvider AuthenticationProvider
        {
            get
            {
                return authenticationProvider;
            }
            private set
            {
                authenticationProvider = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructor
        public TasksViewModel(InteractiveAuthenticationProvider authenticationProvider)
        {
            LoadConfigFiles();
            AuthenticationProvider = authenticationProvider;
        }

        #endregion

        #region Methods for Loading Data from Files
        private void LoadTeams()
        {
            string filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                    Common.Properties.Settings.Default.RelFilePathTeamsJson);
            var teamsOverview = JSONSerializer.ReadJson<List<TeamOverview>>(filePath);

            if (!teamsOverview.Any())
            {
                LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            }

            Teams = teamsOverview;
        }


        private void LoadChannels()
        {
            var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                                    Common.Properties.Settings.Default.RelFilePathChannelJson);

            var channels = JSONSerializer.ReadJson<List<Channel>>(filePath);

            if (!channels.Any())
            {
                LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            }

            Channels = channels;
        }

        private void LoadUsers()
        {
            var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                                    Common.Properties.Settings.Default.RelFilePathUsersJson);

            var users = JSONSerializer.ReadJson<List<UserOverview>>(filePath);

            if (!users.Any())
            {
                LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            }

            Users = users;
        }

        #endregion

        #region Help-Methods
        private void LoadConfigFiles()
        {
            LoadTeams();
            SetSelectedTeamToDefault();
            LoadChannels();
            LoadUsers();
        }

        private void SetSelectedTeamToDefault()
        {
            SelectedTeam = Teams.Find(team => team.Team.Name.Equals(
                Common.Properties.Settings.Default.DefaultSelectedTeamName));
        }
        #endregion


        #region Validate-Methods

        internal bool IsTeamSelected()
        {
            if (SelectedTeam is null)
            {
                string message = Properties.Resources.SelectAnyItemFromListMessage.Replace("{0}", "Team");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        #endregion


    }
}
