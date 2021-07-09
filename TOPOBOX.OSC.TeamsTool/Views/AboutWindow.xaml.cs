using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TOPOBOX.OSC.TeamsTool.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// Splash Screen using ONLY without Dark-Theme
    /// Fonts für Icons: Segoe MDL2 Assets
    /// https://docs.microsoft.com/de-de/windows/uwp/design/style/segoe-ui-symbol-font?redirectedfrom=MSDN
    /// </summary>
    public partial class AboutWindow : Window
    {
        // set without Close and Minimize Buttons = true
        bool withoutWindowTitelBarButtons = false;
        public AboutWindow()
        {
            InitializeComponent();

            RemoveWindowTitelBarButtons();
        }

        private void RemoveWindowTitelBarButtons()
        {
            if (withoutWindowTitelBarButtons)
            {
                this.HeaderGrid.Children.Remove(this.TitleBarStackPanel);
            }
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
        private void MainWindow_Info(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        // Close Window
        private void MainWindow_Close(object sender, RoutedEventArgs e)
        {
            // ToDo - Application Shutdown or Dialog Close
            this.Close();
            //App.Current.Shutdown();
            base.OnClosed(e);
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

    }
}

