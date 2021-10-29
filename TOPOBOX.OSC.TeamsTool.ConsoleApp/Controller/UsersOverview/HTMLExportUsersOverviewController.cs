using GEOBOX.OSC.Common.Logging;
using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Domain;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.UsersOverview
{
    internal sealed class HTMLExportUsersOverviewController : IController
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
        public HTMLExportUsersOverviewController(BatchRuntimeSettings batchRuntimeSettings, ILogger logger)
        {
            runtimeSettings = batchRuntimeSettings;
            Logger = logger;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        public ILogger Logger { get; set; }

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

            return true;
        }
    }
}
