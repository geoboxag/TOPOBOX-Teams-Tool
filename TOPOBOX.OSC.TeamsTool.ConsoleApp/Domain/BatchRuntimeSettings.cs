using System.IO;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain
{
    internal class BatchRuntimeSettings
    {

        internal string OutputDirectory { get; set; }
        
        internal string OutputFileName { get; set; }
        
        internal string LogFilePath { get; private set; }

        internal GraphConnectorHelper GraphConnectorHelper { get; private set; }


        internal BatchRuntimeSettings(string clientId, string clientSecret, string tenantId)
        {
            // ToDo Check Init
            GraphConnectorHelper = new GraphConnectorHelper();
            GraphConnectorHelper.InitServiceClient(clientId, clientSecret, tenantId);
        }

        public string GetJSONOutputFilePath()
        {
            return GetOutputFilePath("json");
        }

        public string GetHTMLOutputFilePath()
        {
            return GetOutputFilePath("html");
        }

        private string GetOutputFilePath(string fileExtension)
        {
            return Path.Combine(OutputDirectory, $"{OutputFileName}.{fileExtension}");
        }
    }
}
