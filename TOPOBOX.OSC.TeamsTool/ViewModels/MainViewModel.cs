using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Helpers;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties and Attributes
        //default scopes
        string[] scopes = new string[] { "user.read", "channelmessage.send", "group.read.all", "channelsettings.readwrite.all" };

        private string userLabelText = "Login";
        public string UserLabelText
        {
            get
            {
                return userLabelText;
            }
            set
            {
                userLabelText = value;
                OnPropertyChanged(nameof(UserLabelText));
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

        private List<User> users;
        public List<User> Users
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

        private User selectedUser;
        public User SelectedUser
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string AuthenticationToken { get; set; }
        public IPublicClientApplication ClientApplication { get; private set; }

        #endregion

        #region Constructor
        public MainViewModel(IPublicClientApplication clientApplication)
        {
            LoadConfigFiles();
            ClientApplication = clientApplication;
        }
        #endregion

        #region Methods for Loading Data from Files
        private void LoadTeams()
        {           
            //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //            Common.Properties.Settings.Default.RelFilePathTeamsJson);

            //var jsonSerialization = new JsonSerialization();
            //Teams = jsonSerialization.GetFromFile<GbxTeamOverview>(filePath);
            //SetSelectedTeamToDefault();
        }

        private void LoadChannels()
        {
            //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //                        Common.Properties.Settings.Default.RelFilePathChannelJson);

            //var jsonSerialization = new JsonSerialization();
            //Channels = jsonSerialization.GetFromFile<GbxChannel>(filePath);
        }

        private void LoadUsers()
        {
            //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //                        Common.Properties.Settings.Default.RelFilePathUsersJson);

            //var jsonSerialization = new JsonSerialization();
            //Users = jsonSerialization.GetFromFile<GbxUser>(filePath);
        }

        #endregion

        #region Help-Methods
        private void LoadConfigFiles()
        {
            LoadTeams();
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

        internal bool IsUserSelected()
        {
            if (SelectedUser is null)
            {
                return false;
            }
            return true;
        }

        internal bool IsAuthTokenSet()
        {
            if (AuthenticationToken is null)
            {
                MessageBox.Show(Properties.Resources.LoginFirstBeforeExecuteMessage);
                return false;
            }
            return true;
        }

        internal bool IsChannelSelected()
        {
            if (SelectedChannel is null)
            {
                string message = Properties.Resources.SelectAnyItemFromListMessage.Replace("{0}", "Kanal");
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        #endregion

        internal async Task LoginUserAsync()
        {
            var authHelper = new GraphUserAuthenticationHelper();
            if(await authHelper.InitUserClientAsync(ClientApplication, scopes))
            {
                AuthenticationProvider = authHelper.AuthenticationProvider;
                UserLabelText = authHelper.Username;
            }
        }

        internal async void TryAutoLoginUserAsync()
        {
            var authHelper = new GraphUserAuthenticationHelper();
            if (await authHelper.InitUserClientAsync(ClientApplication, scopes, true))
            {
                AuthenticationProvider = authHelper.AuthenticationProvider;
                UserLabelText = authHelper.Username;
            }
        }

        internal async Task LogoutUser()
        {
            var accounts = await ClientApplication.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    await ClientApplication.RemoveAsync(accounts.FirstOrDefault());

                    AuthenticationProvider = null;
                    UserLabelText = "Login";
                }
                catch (MsalException ex)
                {
                    // TODO Log
                    MessageBox.Show("Fehler beim Auslogen des Benutzers.", "Logout nicht erfolgreich", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
