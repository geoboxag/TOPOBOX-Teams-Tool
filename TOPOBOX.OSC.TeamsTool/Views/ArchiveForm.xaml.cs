using TOPOBOX.OSC.TeamsTool.Common.Controller;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using TOPOBOX.OSC.TeamsTool.Common.IO;
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
    /// Interaction logic for ArchiveForm.xaml
    /// </summary>
    public partial class ArchiveForm : UserControl, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;
        public ArchiveForm(MainViewModel mainViewModel)
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
                OnPropertyChanged("SavePath");
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
            //Teams = GetTeamsOverviewFromConfig();
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

        private void ArchiveChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mainViewModel.IsAuthTokenSet() ||
                !IsArchiveFormValid())
            {
                return;
            }

            ArchivingChatMessages();
            ArchiveDriveItems();

        }


        private void SearchDirBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result is DialogResult.OK)
                {
                    SavePath = dialog.SelectedPath;
                    LogMessages.Insert(0, $"{Properties.Resources.SelectedSavePathMessage}{SavePath}.");
                }
                else
                {
                    SavePath = string.Empty;
                    LogMessages.Insert(0, Properties.Resources.SavePathIsEmptyMessage);
                }
            }
        }


        private List<TeamOverview> GetTeamsOverviewFromConfig()
        {
            string filePath = Path.Combine(Common.Properties.Settings.Default.TeamsToolConfigRootPath,
                    Common.Properties.Settings.Default.RelFilePathTeamsJson);

            //var jsonSerialization = new JsonSerialization();
            //var teamsOverview = jsonSerialization.GetFromFile<GbxTeamOverview>(filePath);

            //if (!teamsOverview.Any())
            //{
            //    LogMessages.Insert(0, $"{Properties.Resources.ErrorReadingFileMessage}{filePath}");
            //}
            //return teamsOverview;
            return null;
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
            if (SelectedTeam is null || SelectedChannel is null || string.IsNullOrEmpty(SavePath))
            {
                LogMessages.Insert(0, Properties.Resources.FirstFillRequiredFieldsMessage);
                return false;
            }
            return true;
        }

 
    }
}
