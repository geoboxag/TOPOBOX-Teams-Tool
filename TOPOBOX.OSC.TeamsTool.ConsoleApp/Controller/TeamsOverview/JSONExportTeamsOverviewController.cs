using GEOBOX.OSC.Common.Logging;
using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Domain;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.TeamsOverview
{
    internal sealed class JSONExportTeamsOverviewController : IController
    {
        // DEBUG Command: --functiontype exportTeamsOverviewJSON --folderpath "C:\Temp\TeamsTool" --filename "TeamsOverview"
        /// <summary>
        /// Name of FunctionType
        /// Key: must be unique
        /// </summary>
        private const string FUNCTIONTYPE = "exportTeamsOverviewJSON";

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
        public JSONExportTeamsOverviewController(BatchRuntimeSettings batchRuntimeSettings, ILogger logger)
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

            TeamsOverviewHelper teamsOverviewHelper = new TeamsOverviewHelper(runtimeSettings.GraphConnectorHelper, Logger);
            var htmlFilePath = runtimeSettings.GetJSONOutputFilePath();
            if (teamsOverviewHelper.SaveAsJsonFile(htmlFilePath))
            {
                Logger.WriteInformation($"Datei erstellt: {htmlFilePath}");
            }

            return true;
        }
    }
}
