using Azure.Identity;
using GEOBOX.OSC.Common.Logging;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Helpers;
using TOPOBOX.OSC.TeamsTool.Logging;

namespace TOPOBOX.OSC.TeamsTool.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties and Attributes

        private const string LOGINUSERLABELTEXT = "Login";
        private string userLabelText = LOGINUSERLABELTEXT;
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


        private InteractiveBrowserCredential interactiveBrowserCredential;
        public InteractiveBrowserCredential InteractiveBrowserCredential
        {
            internal get
            {
                return interactiveBrowserCredential;
            }
            set
            {
                interactiveBrowserCredential = value;
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

        #endregion

        #region Constructor
        public MainViewModel()
        {
            var logFilePath = Path.Combine(Path.GetTempPath(), string.Format(Common.Properties.Resources.LogFileName, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));
            Logger = new UILogger(FileLogger.Create(logFilePath), true);
        }
        #endregion

        internal bool IsUserAuthCredentialsSet()
        {
            if (InteractiveBrowserCredential is null)
            {
                Logger.WriteInformation(Properties.Resources.LoginFirstBeforeExecuteMessage);
                return false;
            }
            return true;
        }

        internal async Task LoginUserAsync(Action loadDataAction)
        {
            var authHelper = new AzureAuthenticationHelper();
            if (await authHelper.InitUserClientAsync())
            {
                InteractiveBrowserCredential = authHelper.InteractiveBrowserCredential;
                GraphConnectorHelper = new GraphConnectorHelper();
                GraphConnectorHelper.InitUserServiceClient(authHelper.InteractiveBrowserCredential);
                UserLabelText = authHelper.Username;

                loadDataAction.Invoke();
            }
            else
            {
                Logger.WriteInformation(Properties.Resources.UserIsNotLoggedInMessage);
            }
        }

        internal async Task TryAutoLoginUserAsync(Action loadDataAction)
        {
            var authHelper = new AzureAuthenticationHelper();
            if (await authHelper.InitUserClientAsync(true))
            {
                InteractiveBrowserCredential = authHelper.InteractiveBrowserCredential;
                GraphConnectorHelper = new GraphConnectorHelper();
                GraphConnectorHelper.InitUserServiceClient(authHelper.InteractiveBrowserCredential);
                UserLabelText = authHelper.Username;

                Logger.WriteInformation(string.Format(Properties.Resources.UserLoginSuccessMessage, UserLabelText));

                loadDataAction.Invoke();
            }
            else
            {
                Logger.WriteInformation(Properties.Resources.UserIsNotLoggedInMessage);
            }
        }

        internal async Task LogoutUser()
        {
            var authHelper = new AzureAuthenticationHelper();
            if(authHelper.DeleteAuthenticationRecordCache())
            {
                Logger.WriteInformation(string.Format(Properties.Resources.UserLogoutSuccessMessage, UserLabelText));
                InteractiveBrowserCredential = null;
                UserLabelText = LOGINUSERLABELTEXT;
            }
            else
            {
                Logger.WriteWarning(Properties.Resources.UserLogoutFailureMessage);
            }

        }
    }
}
