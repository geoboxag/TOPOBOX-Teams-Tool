using TOPOBOX.OSC.TeamsTool.ViewModels;
using System.ComponentModel;
using System.Windows.Controls;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for DataBackupForm.xaml
    /// </summary>
    public partial class DataBackupForm : UserControl, INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;
        public DataBackupForm(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.mainViewModel = mainViewModel;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
