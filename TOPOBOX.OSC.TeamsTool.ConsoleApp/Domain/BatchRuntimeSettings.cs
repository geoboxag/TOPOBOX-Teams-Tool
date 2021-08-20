using System.IO;
using TOPOBOX.OSC.TeamsTool.Common.GraphHelper;
using TOPOBOX.OSC.TeamsTool.Common.Logging;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain
{
    internal class BatchRuntimeSettings
    {

        internal string OutputDirectory { get; set; }
        
        internal string OutputFileName { get; set; }
        
        internal string LogFilePath { get; set; }

        internal GraphConnectorHelper GraphConnectorHelper { get; private set; }

        private bool IsInitOK = false;

        internal BatchRuntimeSettings(string clientId, string clientSecret, string tenantId)
        {
            GraphConnectorHelper = new GraphConnectorHelper();
            GraphConnectorHelper.InitServiceClient(clientId, clientSecret, tenantId);
        }

        public bool CheckSettings(Logger logger)
        {
            bool isInitOk = IsInitOk(logger);

            if (!isInitOk)
            {
                logger.WriteWarning($"{nameof(BatchRuntimeSettings)} ist nicht initialisiert.");
            }
            return isInitOk;
        }

        private bool IsInitOk(Logger logger)
        {
            if (string.IsNullOrEmpty(OutputDirectory))
            {
                IsInitOK = false;
                logger.WriteWarning($"{nameof(OutputDirectory)} enthält keine Angabe.");
                return IsInitOK;
            }
            if (string.IsNullOrEmpty(OutputFileName))
            {
                IsInitOK = false;
                logger.WriteWarning($"{nameof(OutputFileName)} enthält keine Angabe.");
                return IsInitOK;
            }
            if (string.IsNullOrEmpty(LogFilePath))
            {
                IsInitOK = false;
                logger.WriteWarning($"{nameof(LogFilePath)} enthält keine Angabe.");
                return IsInitOK;
            }
            if (GraphConnectorHelper is null)
            {
                IsInitOK = false;
                logger.WriteWarning($"{nameof(GraphConnectorHelper)} ist null.");
                return IsInitOK;
            }

            IsInitOK = true;
            return IsInitOK;
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
