using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for MigrateForm.xaml
    /// </summary>
    public partial class MigrateForm : UserControl, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;
        public MigrateForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
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
                OnPropertyChanged("Teams");
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
                OnPropertyChanged("SelectedTeam");
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
                OnPropertyChanged("Channels");
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
                OnPropertyChanged("SelectedChannel");
            }
        }

        private string sourcePath;
        public string SourcePath
        {
            get
            {
                return sourcePath;
            }
            set
            {
                sourcePath = value;
                OnPropertyChanged("SourcePath");
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
                OnPropertyChanged("LogMessages");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TeamCmb_OnDropDownOpened(object sender, System.EventArgs e)
        {
            if (!mainViewModel.IsAuthTokenSet())
            {
                return;
            }
            Teams = GetTeamsOverviewFromConfig();
        }

        private void TeamCmb_OnDropDownClosed(object sender, EventArgs e)
        {
            Channels = null;
            LoadChannelsOfSelectedTeam();
        }

        private void LoadChannelsOfSelectedTeam()
        {
            if (SelectedTeam is null)
            {
                return;
            }

            LogMessages.Insert(0,$"Laden der Kanäle des Teams: \"{SelectedTeam.Team.Name}\".");
            //var graphClient = new GraphServiceClient(mainViewModel.AuthenticationProvider);

            //Task task = new Task(() =>
            //{
            //    var answer = graphClient.Teams[SelectedTeam.Team.Id].Channels.Request().GetAsync().Result;

            //    if (answer != null && answer.Any())
            //    {
            //        List<Channel> list = new List<Channel>();
            //        answer.ForEach(c => list.Add(c));
            //        Dispatcher.Invoke(() =>
            //        {
            //            Channels = list;
            //            LogMessages.Insert(0, "Kanäle geladen.");
            //        });
            //    }

            //});
            //task.Start();
        }

        private void MigrateChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mainViewModel.IsAuthTokenSet() ||
                !IsMigrateFormValid())
            {
                return;
            }
            //Todo: Remove after Testing
            CopyDriveItem("textdokument im ordner2.txt");
            //MigrateChatMessages();
        }

        private void CopyDriveItem(string itemName)
        {
            // TODO: for copy target team and channel (properties with Binding on UI)
            //DriveItemsController driveItemsController = new DriveItemsController(mainViewModel.AuthenticationProvider);
            //driveItemsController.CopyDriveItem(SelectedTeam.Team.Id, SelectedChannel.Id,
            //    itemName, SelectedTeam.Team.Id, SelectedChannel.Id);

        }

        private void SearchDirBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result is DialogResult.OK)
                {
                    SourcePath = dialog.SelectedPath;
                    LogMessages.Insert(0, $"{Properties.Resources.SelectedSavePathMessage}{SourcePath}.");
                }
                else
                {
                    SourcePath = string.Empty;
                    LogMessages.Insert(0, Properties.Resources.SavePathIsEmptyMessage);
                }
            }
        }


        private List<TeamOverview> GetTeamsOverviewFromConfig()
        {
            //string filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
            //        Common.Properties.Settings.Default.RelFilePathTeamsJson);

            //var jsonSerialization = new JsonSerialization();
            //var teamsOverview = jsonSerialization.GetFromFile<GbxTeamOverview>(filePath);

            //if (!teamsOverview.Any())
            //{
            //    LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            //}
            //return teamsOverview;
            return null;
        }


        private bool MigrateChatMessages()
        {
            //if (ChangeMigrationModeOfChannel(SelectedTeam.Team.Id, SelectedChannel.Id))
            //{
            //    LogMessages.Insert(0, Properties.Resources.FaultedChangeMigrationModeMessage.Replace("{0}", $"{SelectedChannel.DisplayName}"));
            //    return false;
            //}

            LogMessages.Insert(0, Properties.Resources.BeginMigrateChannelItemMessage.Replace("{0}", "Kanal-Nachrichten"));

            //var chatMessagesController = new ChatMessagesController(mainViewModel.AuthenticationProvider);

            //if (chatMessagesController.MigrateChatMessages(SourcePath, SelectedTeam.Team.Id, SelectedChannel.Id))
            //{
            //    LogMessages.Insert(0, $"Kanal-Nachrichten archiviert: {SourcePath}");
            //    return true;
            //}

            return false;
        }

        private bool ChangeMigrationModeOfChannel(string teamId, string channelId)
        {
            //GraphConnector graphConnector = new GraphConnector(mainViewModel.AuthenticationProvider);
            //var migrationModeTask = graphConnector.ChangeMigrationStateOfChannel(teamId, channelId);
            //migrationModeTask.Start();
            //return migrationModeTask.Result;
            return false;
        }


        private bool IsMigrateFormValid()
        {
            if (SelectedTeam is null || SelectedChannel is null || string.IsNullOrEmpty(SourcePath))
            {
                LogMessages.Insert(0, Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }


    }
}
