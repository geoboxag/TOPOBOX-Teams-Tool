using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TOPOBOX.OSC.TeamsTool.Helpers
{
    /// <summary>
    /// Helper Class for the user authentication
    /// </summary>
    internal class GraphUserAuthenticationHelper
    {
        /// <summary>
        /// InteractiveAuthenticationProvider
        /// </summary>
        public InteractiveAuthenticationProvider AuthenticationProvider { get; private set; }

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
        /// <param name="clientApplication"></param>
        /// <param name="scopes"></param>
        /// <param name="silentOnly">Do not promt for the user login</param>
        /// <returns></returns>
        public async Task<bool> InitUserClientAsync(IPublicClientApplication clientApplication, string[] scopes, bool silentOnly = false)
        {
            // Enable token Serialization
            TokenCacheHelper.EnableSerialization(clientApplication.UserTokenCache);

            // Get Account
            var accounts = await clientApplication.GetAccountsAsync();
            var account = accounts.FirstOrDefault();

            // Acquire Token
            var authenticationResult = await AquireTokenAsync(clientApplication, account, scopes, silentOnly);
            if (authenticationResult is null)
            {
                IsInitOK = false;
                return IsInitOK;
            }

            AuthenticationProvider = new InteractiveAuthenticationProvider(clientApplication, authenticationResult.Scopes);
            Username = authenticationResult.Account.Username;

            IsInitOK = true;
            return IsInitOK;
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
