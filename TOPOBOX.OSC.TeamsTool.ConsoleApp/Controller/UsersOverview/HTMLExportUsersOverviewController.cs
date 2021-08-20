using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Controller;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;
using Logger = TOPOBOX.OSC.TeamsTool.Common.Logging.Logger;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.TeamsOverview
{
    internal sealed class PDFExportUsersOverviewController : IController
    {
        // DEBUG Command: --functiontype exportUsersOverviewHTML --folderpath "C:\Temp\TeamsTool" --filename "UsersOverview"
        /// <summary>
        /// Name of FunctionType
        /// Key: must be unique
        /// </summary>
        private const string FUNCTIONTYPE = "exportUsersOverviewHTML";

        /// <summary>
        /// Get the name
        /// </summary>
        /// <returns>name aka functionstype</returns>
        public static string GetCommandName()
        {
            return FUNCTIONTYPE;
        }

        private BatchRuntimeSettings runtimeSettings;

        #region Constructor and Dispose
        public PDFExportUsersOverviewController(BatchRuntimeSettings batchRuntimeSettings)
        {
            runtimeSettings = batchRuntimeSettings;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        public Logger Logger { get; set; } = new Logger();

        /// <summary>
        /// Run Command
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            if (!runtimeSettings.CheckSettings(Logger))
            {
                return false;
            }

            UsersOverviewHelper usersOverviewHelper = new UsersOverviewHelper(runtimeSettings.GraphConnectorHelper, Logger);
            var htmlFilePath = runtimeSettings.GetHTMLOutputFilePath();
            if (usersOverviewHelper.SaveAsHTMLFile(htmlFilePath))
            {
                Logger.WriteInformation($"Datei erstellt: {htmlFilePath}");
            }

            if (Logger.WriteLogFile(runtimeSettings.LogFilePath))
            {
                Console.WriteLine($"Log-Datei erstellt: {runtimeSettings.LogFilePath}");
            }
            Logger.Dispose();

            return true;
        }
    }
}
