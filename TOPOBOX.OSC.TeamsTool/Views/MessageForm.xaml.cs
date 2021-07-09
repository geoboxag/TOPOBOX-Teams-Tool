using TOPOBOX.OSC.TeamsTool.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for MessageForm.xaml
    /// </summary>
    public partial class MessageForm : UserControl, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;

        private string subject;
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string caller;
        public string Caller
        {
            get
            {
                return caller;
            }
            set
            {
                caller = value;
                OnPropertyChanged("Caller");
            }
        }

        private string company;
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }

        private string phonenumber;
        public string Phonenumber
        {
            get
            {
                return phonenumber;
            }
            set
            {
                phonenumber = value;
                OnPropertyChanged("Phonenumber");
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }


        public MessageForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Implement via Graph API Nuget
            if (mainViewModel.IsAuthTokenSet())
            {
                return;
            }

            // ToDo
            //RuntimeSettings settings = this.SetupRuntimeSettings();
            //if (!this.SettingsValid(settings))
            //{
            //    return;
            //}

            //HttpClientController httpClientController = new HttpClientController(settings);
            //if (!this.SendIsSuccessful(httpClientController))
            //{
            //    return;
            //}

            //exitCode = ExitCode.Success;
            //MessageBox.Show($"{exitCode} Erfolgreich gesendet");
            return;
        }

        //private RuntimeSettings SetupRuntimeSettings()
        //{
        //    // ToDo : set specific runtimeSettings for every endpoint by selecting tab (call-notice, tasks, backup data, etc.)
        //    //RuntimeSettings settings = Config.ConfigCombinator.CombineConfigAndParams(commandLineParams, xmlConfig);
        //    RuntimeSettings settings = new RuntimeSettings();

        //    // ToDo : settings are to define dynamicly
        //    settings.HTTPEndpointAPIKey = mainViewModel.AuthenticationToken;

        //    if (!mainViewModel.IsChannelSelected() || 
        //        !CallerFormIsValid())
        //    {
        //        return settings;
        //    }

        //    string groupId = mainViewModel.SelectedTeam.Team.Id;
        //    string channelId = mainViewModel.SelectedChannel.Id;
        //    settings.HTTPEndpointUrl = $@"https://graph.microsoft.com/v1.0/teams/{groupId}/channels/{channelId}/messages";

        //    // ToDo : define dynamicly
        //    MessageBuilder messageBuilder = new MessageBuilder();
        //    string phoneNoticeMessage = messageBuilder.CreatePhoneNoticeMessage(Caller, Company, Phonenumber, Message);

        //    settings.RequestPayload = new BodyMessage();
        //    settings.RequestPayload.Subject = Subject;

        //    settings.RequestPayload.Body = new Body();
        //    settings.RequestPayload.Body.Content = phoneNoticeMessage;
        //    return settings;
        //}

        private bool CallerFormIsValid()
        {
            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Caller) || string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(Phonenumber))
            {
                MessageBox.Show(Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }

        //private bool SettingsValid(RuntimeSettings runtimeSettings)
        //{
        //    if (!runtimeSettings.IsValid())
        //    {
        //        exitCode = ExitCode.Error;
        //        return false;
        //    }
        //    return true;
        //}

        //private bool SendIsSuccessful(HttpClientController httpClientController)
        //{
        //    if (httpClientController.SendPost() == null)
        //    {
        //        MessageBox.Show($"Fehler! {httpClientController.SendMessage}");
        //        return false;
        //    }
        //    return true;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
