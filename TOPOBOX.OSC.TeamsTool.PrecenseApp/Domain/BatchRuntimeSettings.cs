using System.IO;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp.Domain
{
    internal class BatchRuntimeSettings
    {

        internal string UserID { get; set; }
        
        internal string Activity { get; set; }
        
        internal string SessionID { get; private set; }

        internal string ExpirationDuration { get; set; } = "PT1H";

        internal GraphConnectorHelper GraphConnectorHelper { get; private set; }


        internal BatchRuntimeSettings(string clientId, string clientSecret, string tenantId)
        {
            // ToDo Check Init
            GraphConnectorHelper = new GraphConnectorHelper();
            GraphConnectorHelper.InitServiceClient(clientId, clientSecret, tenantId);

            SessionID = clientId;
        }
    }
}
