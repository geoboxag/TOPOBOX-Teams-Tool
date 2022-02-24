using System;
using System.Windows;
using System.Windows.Forms;
using TOPOBOX.OSC.TeamsTool.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for ArchiveForm.xaml
    /// </summary>
    public partial class ArchiveForm : UserControl
    {
        private MainViewModel mainViewModel;

        private ArchiveViewModel archiveViewModel;

        public ArchiveForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
            archiveViewModel = new ArchiveViewModel(mainViewModel);
            DataContext = archiveViewModel;
        }

        private void TeamCmb_OnDropDownOpened(object sender, System.EventArgs e)
        {
            archiveViewModel.LoadTeams();
        }

        private void TeamCmb_OnDropDownClosed(object sender, EventArgs e)
        {
            archiveViewModel.LoadChannels();
        }

        private void SearchDirBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result is DialogResult.OK)
                {
                    archiveViewModel.SetRootSavePath(dialog.SelectedPath);
                }
                else
                {
                    archiveViewModel.SetRootSavePath(string.Empty);
                }
            }
        }

        private void ArchiveChannelButton_Click(object sender, RoutedEventArgs e)
        {
            archiveViewModel.ArchivingChannel();
        }
    }
}
