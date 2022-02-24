using Azure.Identity;
using Microsoft.Identity.Client;
using System;
using System.IO;
using System.Threading.Tasks;
using TOPOBOX.OSC.TeamsTool.Settings;

namespace TOPOBOX.OSC.TeamsTool.Helpers
{
    /// <summary>
    /// Helper Class for the user authentication
    /// </summary>
    internal class AzureAuthenticationHelper
    {
        private string AUTH_RECORD_PATH = $@"{Environment.SpecialFolder.LocalApplicationData}\TOPOBOX\TeamsTool\";
        private string AUTH_RECORD_FILE_NAME = "azuretokencache.bin";

        /// <summary>
        /// InteractiveBrowserCredential
        /// </summary>
        public InteractiveBrowserCredential InteractiveBrowserCredential { get; private set; }

        /// <summary>
        /// Username of the connected user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// IsInitOK
        /// </summary>
        public bool IsInitOK = false;


        /// <summary>
        /// Initialize a UserClient
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitUserClientAsync(bool silentOnly = false)
        {
            try
            {
                SettingsController settingsController = new SettingsController();
                AuthenticationRecord authRecord = null;

                if (File.Exists(Path.Combine(AUTH_RECORD_PATH, AUTH_RECORD_FILE_NAME)))
                {
                    using (var authRecordStream = new FileStream(Path.Combine(AUTH_RECORD_PATH, AUTH_RECORD_FILE_NAME), FileMode.Open, FileAccess.Read))
                    {
                        authRecord = await AuthenticationRecord.DeserializeAsync(authRecordStream);
                    }

                    if (authRecord != null)
                    {
                        InteractiveBrowserCredential = new InteractiveBrowserCredential(
                            new InteractiveBrowserCredentialOptions
                            {
                                TokenCachePersistenceOptions = new TokenCachePersistenceOptions() { Name = "TOPOBOX TeamsTool" },
                                AuthenticationRecord = authRecord
                            });

                        Username = authRecord.Username;

                        IsInitOK = true;
                        return IsInitOK;
                    }
                }

                if (authRecord is null && !silentOnly)
                {
                    InteractiveBrowserCredential = new InteractiveBrowserCredential(
                        new InteractiveBrowserCredentialOptions
                        {
                            TokenCachePersistenceOptions = new TokenCachePersistenceOptions() { Name = "TOPOBOX TeamsTool" },
                            AuthenticationRecord = authRecord,
                            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                            TenantId = settingsController.ApplicationSettings.TenantId,
                            RedirectUri = new Uri("http://localhost"),
                        });

                    authRecord = await InteractiveBrowserCredential.AuthenticateAsync();
                    Username = authRecord.Username;

                    if (authRecord != null)
                    {
                        if(!Directory.Exists(AUTH_RECORD_PATH))
                        {
                            Directory.CreateDirectory(AUTH_RECORD_PATH);
                        }

                        using (var authRecordStream = new FileStream(Path.Combine(AUTH_RECORD_PATH, AUTH_RECORD_FILE_NAME), FileMode.Create, FileAccess.Write))
                        {
                            await authRecord.SerializeAsync(authRecordStream);
                        }

                        IsInitOK = true;
                        return IsInitOK;
                    }
                }


                IsInitOK = false;
                return IsInitOK;
            }
            catch (Exception ex)
            {
                IsInitOK = false;
                return IsInitOK;
            }
        }

        internal bool DeleteAuthenticationRecordCache()
        {
            if (File.Exists(Path.Combine(AUTH_RECORD_PATH, AUTH_RECORD_FILE_NAME)))
            {
                File.Delete(Path.Combine(AUTH_RECORD_PATH, AUTH_RECORD_FILE_NAME));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to aquire a token silent otherwise with user login.
        /// </summary>
        /// <param name="clientApplication"></param>
        /// <param name="account"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        private async Task<AuthenticationResult> AquireTokenAsync(IPublicClientApplication clientApplication, IAccount account, string[] scopes,
            bool silentOnly)
        {
            AuthenticationResult authResult;

            try
            {
                authResult = await clientApplication.AcquireTokenSilent(scopes, account).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent. 
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"Silent Login fehlgeschlagen: {ex.Message}");

                if (silentOnly) return null;
                
                try
                {
                    authResult = await clientApplication.AcquireTokenInteractive(scopes)
                        .WithAccount(account)
                        //.WithParentActivityOrWindow(new WindowInteropHelper(this).Handle) // optional, used to center the browser on the window
                        .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    // TODO write error: $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                    return null;
                }
            }
            catch (Exception ex)
            {
                // TODO write error: $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                return null;
            }

            return authResult;
        }
    }
}
