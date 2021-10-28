using GEOBOX.OSC.Common.Logging;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Helpers;
using TOPOBOX.OSC.TeamsTool.Logging;

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

        // TODO: Try with ICollectionView for Sorting
        //public ICollectionView LogMessagesView { get; internal set; }

        public UILogger Logger { get; set; }


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

        private GraphConnectorHelper graphConnectorHelper;
        public GraphConnectorHelper GraphConnectorHelper
        {
            internal get
            {
                return graphConnectorHelper;
            }
            set
            {
                graphConnectorHelper = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IPublicClientApplication ClientApplication { get; private set; }

        #endregion

        #region Constructor
        public MainViewModel(IPublicClientApplication clientApplication)
        {
            ClientApplication = clientApplication;

            var logFilePath = Path.Combine(Path.GetTempPath(), string.Format(Common.Properties.Resources.LogFileName, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));
            Logger = new UILogger(FileLogger.Create(logFilePath), true);
        }
        #endregion

        #region Methods for Loading Data from Files
        //private void LoadTeams()
        //{           
        //    //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
        //    //            Common.Properties.Settings.Default.RelFilePathTeamsJson);

        //    //var jsonSerialization = new JsonSerialization();
        //    //Teams = jsonSerialization.GetFromFile<GbxTeamOverview>(filePath);
        //    //SetSelectedTeamToDefault();
        //}

        //private void LoadChannels()
        //{
        //    //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
        //    //                        Common.Properties.Settings.Default.RelFilePathChannelJson);

        //    //var jsonSerialization = new JsonSerialization();
        //    //Channels = jsonSerialization.GetFromFile<GbxChannel>(filePath);
        //}

        //private void LoadUsers()
        //{
        //    //var filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
        //    //                        Common.Properties.Settings.Default.RelFilePathUsersJson);

        //    //var jsonSerialization = new JsonSerialization();
        //    //Users = jsonSerialization.GetFromFile<GbxUser>(filePath);
        //}

        //#endregion

        //#region Help-Methods
        //private void LoadConfigFiles()
        //{
        //    LoadTeams();
        //    LoadChannels();
        //    LoadUsers();
        //}

        //private void SetSelectedTeamToDefault()
        //{
        //    SelectedTeam = Teams.Find(team => team.Team.Name.Equals(
        //        Common.Properties.Settings.Default.DefaultSelectedTeamName));
        //}
        //#endregion

        //#region Validate-Methods

        //internal bool IsTeamSelected()
        //{
        //    if (SelectedTeam is null)
        //    {
        //        string message = Properties.Resources.SelectAnyItemFromListMessage.Replace("{0}", "Team");
        //        MessageBox.Show(message);
        //        return false;
        //    }
        //    return true;
        //}

        //internal bool IsUserSelected()
        //{
        //    if (SelectedUser is null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        internal bool IsAuthTokenSet()
        {
            if (ClientApplication.UserTokenCache is null)
            {
                Logger.WriteInformation(Properties.Resources.LoginFirstBeforeExecuteMessage);
                return false;
            }
            return true;
        }

        #endregion

        internal async Task LoginUserAsync()
        {
            var authHelper = new GraphUserAuthenticationHelper();
            if (await authHelper.InitUserClientAsync(ClientApplication, scopes))
            {
                AuthenticationProvider = authHelper.AuthenticationProvider;
                GraphConnectorHelper = new GraphConnectorHelper();
                GraphConnectorHelper.InitUserServiceClient(AuthenticationProvider);
                UserLabelText = authHelper.Username;
            }
        }

        internal async void TryAutoLoginUserAsync()
        {
            var authHelper = new GraphUserAuthenticationHelper();
            if (await authHelper.InitUserClientAsync(ClientApplication, scopes, true))
            {
                AuthenticationProvider = authHelper.AuthenticationProvider;
                GraphConnectorHelper = new GraphConnectorHelper();
                GraphConnectorHelper.InitUserServiceClient(AuthenticationProvider);
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
                    Logger.WriteWarning("Logout nicht erfolgreich - Fehler beim Auslogen des Benutzers.");
                }
            }
        }
    }
}
