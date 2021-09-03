using Microsoft.Identity.Client;
using System.Windows;
using TOPOBOX.OSC.TeamsTool.Helpers;
using TOPOBOX.OSC.TeamsTool.Settings;

namespace TOPOBOX.OSC.TeamsTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            publicClientApplication = PublicClientApplicationBuilder
                .Create(SettingsController.ApplicationSettings.ClientId)
                .WithTenantId(SettingsController.ApplicationSettings.TenantId)
                .WithDefaultRedirectUri()
                .Build();

            TokenCacheHelper.EnableSerialization(publicClientApplication.UserTokenCache);
        }

        private static SettingsController SettingsController = new SettingsController();
        
        private static IPublicClientApplication publicClientApplication;
        internal static IPublicClientApplication PublicClientApp { get { return publicClientApplication; } }
    }
}
