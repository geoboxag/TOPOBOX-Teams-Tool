using Microsoft.Identity.Client;
using System.Windows;
using TOPOBOX.OSC.TeamsTool.Helpers;

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
                .Create(ClientId)
                .WithTenantId(Tenant)
                .WithDefaultRedirectUri()
                .Build();

            TokenCacheHelper.EnableSerialization(publicClientApplication.UserTokenCache);
        }

        // Below are the clientId (Application Description) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Description for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use c6cea225-6253-4fdd-9a98-16bfa375fb54
        //   - for Microsoft Personal account, use consumers
        private static string ClientId = "hoppelhase";

        // Note: Tenant is important for the quickstart. We'd need to check with Andre/Portal if we
        // want to change to the AadAuthorityAudience.
        // TODO: maybe Tenant ID move to AppSettings
        private static string Tenant = "tenantHASE";
        //private static string Instance = "https://login.microsoftonline.com/";
        private static IPublicClientApplication publicClientApplication;

        public static IPublicClientApplication PublicClientApp { get { return publicClientApplication; } }
    }
}
