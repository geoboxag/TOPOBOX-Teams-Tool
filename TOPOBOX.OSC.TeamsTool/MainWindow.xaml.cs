using System.Windows;
using System.Windows.Input;
using TOPOBOX.OSC.TeamsTool.ViewModels;
using TOPOBOX.OSC.TeamsTool.Views;

namespace TOPOBOX.OSC.TeamsTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Fonts für Icons: Segoe MDL2 Assets
    /// https://docs.microsoft.com/de-de/windows/uwp/design/style/segoe-ui-symbol-font?redirectedfrom=MSDN
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        private TasksForm tasksForm;
        private ArchiveForm archiveForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadAndShowMode();

            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;

            tasksForm = new TasksForm(mainViewModel);
            archiveForm = new ArchiveForm(mainViewModel);

            if (Properties.Settings.Default.EnableAutologin)
            {
                mainViewModel.TryAutoLoginUserAsync(LoadDataFromWeb);
            }

            ShowTasksForm();
        }

        private void LoadDataFromWeb()
        {
            tasksForm.LoadData();
        }

        private void LoadAndShowMode()
        {
            // TODO Cleanup
            //bool isDarkMode = DetectWindowsMode.UseAppDarkMode();

            //if (isDarkMode)
            //{
            //    this.LabelThemeMode.Content = "Theme is DARK";
            //}
            //else
            //{
            //    this.LabelThemeMode.Content = "Theme is LIGHT";
            //}
        }

        #region Register and Event for HotKey

        private void AddHotKeys()
        {
            try
            {
                //RoutedCommand hotKeyF5 = new RoutedCommand();
                //hotKeyF5.InputGestures.Add(new KeyGesture(Key.F5));
                //CommandBindings.Add(new CommandBinding(hotKeyF5, HotKeyF5_EventHandler));

                //RoutedCommand hotKeyF3 = new RoutedCommand();
                //hotKeyF3.InputGestures.Add(new KeyGesture(Key.F3));
                //CommandBindings.Add(new CommandBinding(hotKeyF3, HotKeyF3_EventHandler));
            }
            catch
            {

            }
        }

        private void HotKeyF3_EventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //if (showDetailsContentControl.Content.GetType() != typeof(AvailablePackagesView)) return;

            //mainViewModel.SelectedAvailablePackagesViewModel?.ServerSearch();
        }

        private void HotKeyF5_EventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //Refresh();
            e.Handled = true;
        }
        #endregion Register and Event for HotKey

        #region Window Events (Close, Minimized, Drag)
        // Info Window
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
            e.Handled = true;
        }

        // Close Window
        private void MainWindow_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            OnClosed(e);
        }

        // Minimize Window
        private void MainWindow_Minimize(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Drag Window (relocate)
        private void MainWindow_Drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        #endregion Window Events (Close, Minimized, Drag)

        private void ShowTasksForm()
        {
            MainContentControl.Content = tasksForm;
            HandleBorderNavigationLabels(NavigationTasksLabel.Name);
        }

        private void ShowArchiveForm()
        {
            MainContentControl.Content = archiveForm;
            HandleBorderNavigationLabels(NavigationArchiveLabel.Name);
        }

        private void HandleBorderNavigationLabels(string name)
        {
            NavigationTasksLabel.BorderThickness = GetBorderThickness(NavigationTasksLabel.Name == name);
            NavigationArchiveLabel.BorderThickness = GetBorderThickness(NavigationArchiveLabel.Name == name);
        }

        private Thickness GetBorderThickness(/* true = active */bool isActiv)
        {
            if (isActiv)
            {
                return new Thickness(0, 0, 0, 3);
            }

            return new Thickness(0, 0, 0, 0);
        }

        private async void User_Click(object sender, MouseButtonEventArgs e)
        {
            if (mainViewModel.InteractiveBrowserCredential is null)
            {
                await mainViewModel.LoginUserAsync(LoadDataFromWeb);
            }
            else
            {
                await mainViewModel.LogoutUser();
            }
        }

        private void NavigationTasks_Click(object sender, MouseButtonEventArgs e)
        {
            ShowTasksForm();
        }

        private void NavigationArchive_Click(object sender, MouseButtonEventArgs e)
        {
            ShowArchiveForm();
        }
    }
}
