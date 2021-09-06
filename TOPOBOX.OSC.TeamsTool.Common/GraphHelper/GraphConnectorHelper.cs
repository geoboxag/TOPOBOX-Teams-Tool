using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace TOPOBOX.OSC.TeamsTool.Common.GraphHelper
{
    /// <summary>
    /// GraphConnectorHelper for creating a GraphserviceClient
    /// </summary>
    public class GraphConnectorHelper
    {
        /// <summary>
        /// GraphServiceClient
        /// </summary>
        public GraphServiceClient GraphServiceClient { get; private set; }

        /// <summary>
        /// IsInitOK
        /// </summary>
        public bool IsInitOK = false;

        /// <summary>
        /// Initialize a ServiceClient
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public bool InitServiceClient(string clientID, string clientSecret, string tenantID)
        {
            var clientApplication = CreateClientApplication(clientID, clientSecret, tenantID);
            if (clientApplication is null)
            {
                IsInitOK = false;
                return IsInitOK;
            }

            var clientProvider = CreateClientProvider(clientApplication);
            if (clientProvider is null)
            {
                IsInitOK = false;
                return IsInitOK;
            }

            GraphServiceClient = CreateServiceClient(clientProvider);
            if (GraphServiceClient is null)
            {
                IsInitOK = false;
                return IsInitOK;
            }

            IsInitOK = true;
            return IsInitOK;
        }

        /// <summary>
        /// Initialize a UserServiceClient
        /// </summary>
        /// <param name="authenticationProvider"></param>
        /// <returns></returns>
        public bool InitUserServiceClient(IAuthenticationProvider authenticationProvider)
        {
            GraphServiceClient = CreateServiceClient(authenticationProvider);
            if (GraphServiceClient is null)
            {
                IsInitOK = false;
                return IsInitOK;
            }

            IsInitOK = true;
            return IsInitOK;
        }

        /// <summary>
        /// Creates a ClientApplication
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        private IConfidentialClientApplication CreateClientApplication(string clientID, string clientSecret, string tenantID)
        {
            try
            {
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(clientID)
                    .WithTenantId(tenantID)
                    .WithClientSecret(clientSecret)
                    .Build();
                return confidentialClientApplication;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Create a ClientCredentialProvider
        /// </summary>
        /// <param name="clientApplication"></param>
        /// <returns></returns>
        private ClientCredentialProvider CreateClientProvider(IConfidentialClientApplication clientApplication) {
            try
            {
                return new ClientCredentialProvider(clientApplication);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a GraphServiceClient
        /// </summary>
        /// <param name="authProvider"></param>
        /// <returns></returns>
        private GraphServiceClient CreateServiceClient(IAuthenticationProvider authProvider)
        {
            
            try
            {
                return new GraphServiceClient(authProvider);
            }
            catch
            {
                return null;
            }
        }

    }
}