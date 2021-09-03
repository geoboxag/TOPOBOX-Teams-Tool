using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Controller;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;
using Logger = TOPOBOX.OSC.TeamsTool.Common.Logging.Logger;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.TeamsOverview
{
    internal sealed class JSONExportUsersOverviewController : IController
    {
        // DEBUG Command: --functiontype exportUsersOverviewJSON --folderpath "C:\Temp\TeamsTool" --filename "UsersOverview"
        /// <summary>
        /// Name of FunctionType
        /// Key: must be unique
        /// </summary>
        private const string FUNCTIONTYPE = "exportUsersOverviewJSON";

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
        public JSONExportUsersOverviewController(BatchRuntimeSettings batchRuntimeSettings)
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
            var jsonFilePath = runtimeSettings.GetJSONOutputFilePath();
            if (usersOverviewHelper.SaveAsJsonFile(jsonFilePath))
            {
                Logger.WriteInformation($"Datei erstellt: {jsonFilePath}");
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
