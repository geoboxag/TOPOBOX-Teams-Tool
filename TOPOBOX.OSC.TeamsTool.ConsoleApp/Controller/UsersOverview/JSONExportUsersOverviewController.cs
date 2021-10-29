using GEOBOX.OSC.Common.Logging;
using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Domain;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.UsersOverview
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

        public ILogger Logger { get; set; }

        #region Constructor and Dispose
        public JSONExportUsersOverviewController(BatchRuntimeSettings batchRuntimeSettings, ILogger logger)
        {
            runtimeSettings = batchRuntimeSettings;
            Logger = logger;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

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

            return true;
        }
    }
}
