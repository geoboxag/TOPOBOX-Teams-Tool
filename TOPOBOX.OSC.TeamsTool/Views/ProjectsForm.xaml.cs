using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for ProjectsForm.xaml
    /// </summary>
    public partial class ProjectsForm : UserControl, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;


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
                OnPropertyChanged("Channels");
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

        private string description;
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


        public ProjectsForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
            Channels = mainViewModel.Channels;
        }

        private void CreateChannelButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement
            // if (!mainViewModel.IsAuthTokenSet() || 
            //     !mainViewModel.IsGroupSelected() ||
            //     !IsChannelFormValid())
            // {
            //     return;
            // }
            //

            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("@microsoft.graph.channelCreationMode", "migration");

            //var channel = new Channel()
            //{
            //    DisplayName = "TestName (Cedric)",
            //    Description = "Zum Test für das erstellen eines Kanals",
            //    MembershipType = ChannelMembershipType.Standard,
            //    CreatedDateTime = new DateTimeOffset(new DateTime(2019, 12, 01)),
            //    AdditionalData = dic
            //};

            //GraphConnector graphConnector = new GraphConnector(mainViewModel.AuthenticationProvider);

            //var createChannelTask = graphConnector.CreateChannel("654551c6-0fb1-4a2e-90c3-e8b60b77aa29", channel);
            //createChannelTask.Start();
            //var resultChannel = createChannelTask.Result;

            //return;

            // RuntimeSettings settings = new RuntimeSettings();
            // settings.HTTPEndpointAPIKey = mainViewModel.AuthenticationToken;
            // string teamId = mainViewModel.SelectedTeam.Id;
            // settings.HTTPEndpointUrl = $@"https://graph.microsoft.com/v1.0/teams/{teamId}/channels";
            // settings.ChannelRequestPayload = new ChannelBodyMessage();
            // settings.ChannelRequestPayload.Title = Title;
            // settings.ChannelRequestPayload.Description = Description;
            //
            // HttpClientController httpClientController = new HttpClientController(settings);
            // HttpResponseMessage httpResponseMessage = GetChannelHttpResponseMessage(httpClientController, "channel");
            //
            // if (httpResponseMessage == null)
            // {
            //     return;
            // }
            //
            // //CreateTabInChannel();
            //
            // exitCode = ExitCode.Success;
            // MessageBox.Show($"{exitCode} Erfolgreich gesendet");
            // return;

        }

        // Load all channels of the selected Team/Group (only Admins)
        //private void GetChannelsFromSelectedGroupsGroup()
        //{
        //    if (!AuthTokenIsSet())
        //    {
        //        return;
        //    }

        //    RuntimeSettings settings = new RuntimeSettings();
        //    settings.HTTPEndpointAPIKey = mainViewModel.AuthenticationToken;
        //    string teamId = mainViewModel.SelectedTeam.Description;
        //    settings.HTTPEndpointUrl = $@"https://graph.microsoft.com/v1.0/teams/{teamId}/channels";

        //    HttpClientController httpClientController = new HttpClientController(settings);
        //    var httpResponse = httpClientController.SendGet();
        //    if (httpResponse == null)
        //    {
        //        MessageBox.Show($"Fehler! {httpClientController.SendMessage}");
        //        return;
        //    }

        //    string jsonString = GetContentString(httpResponse);
        //    dynamic obj = JsonConvert.DeserializeObject(jsonString);
        //    mainViewModel.ChannelsOfSelectedGroupsGroup = JsonConvert.DeserializeObject<List<Channel>>(JsonConvert.SerializeObject(obj["value"]));
        //}


        private void CreateTabInChannel(string channelId)
        {
            // TODO: Implement
            // RuntimeSettings settings = new RuntimeSettings();
            // settings.HTTPEndpointAPIKey = mainViewModel.AuthenticationToken;
            // string teamId = mainViewModel.SelectedTeam.Id;
            // settings.HTTPEndpointUrl = $@"https://graph.microsoft.com/v1.0/teams/{teamId}/channels/{channelId}/tabs";
            // settings.TabRequestPayload = new TabBodyMessage();
            // settings.TabRequestPayload.Name = "Wiki";
            // settings.TabRequestPayload.TeamsAppBind = Description;
            //
            // HttpClientController httpClientController = new HttpClientController(settings);
            // HttpResponseMessage httpResponseMessage = GetChannelHttpResponseMessage(httpClientController, "tab");
            // if (httpResponseMessage == null)
            // {
            //     return;
            // }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool IsChannelFormValid()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
            {
                MessageBox.Show(Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }

    }
}
