using GEOBOX.OSC.Common.Logging;
using System;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Controller;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.PlannersOverview
{
    internal sealed class HTMLExportPlannersOverviewController : IController
    {
        // DEBUG Command: --functiontype exportPlannersOverviewHTML --folderpath "C:\Temp\TeamsTool" --filename "PlannerOverview"
        /// <summary>
        /// Name of FunctionType
        /// Key: must be unique
        /// </summary>
        private const string FUNCTIONTYPE = "exportPlannersOverviewHTML";

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
        public HTMLExportPlannersOverviewController(BatchRuntimeSettings batchRuntimeSettings, ILogger logger)
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

            PlannerOverviewHelper overviewHelper = new PlannerOverviewHelper(runtimeSettings.GraphConnectorHelper, Logger);
            var htmlFilePath = runtimeSettings.GetHTMLOutputFilePath();
            if (overviewHelper.SaveAsHTMLFile(htmlFilePath))
            {
                Logger.WriteInformation($"Datei erstellt: {htmlFilePath}");
            }

            Logger.Dispose();
            return true;
        }


    }
}
