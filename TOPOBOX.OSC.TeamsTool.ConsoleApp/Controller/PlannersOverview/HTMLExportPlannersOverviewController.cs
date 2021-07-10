using System;
using System.Collections.Generic;
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
        public HTMLExportPlannersOverviewController(BatchRuntimeSettings batchRuntimeSettings)
        {
            runtimeSettings = batchRuntimeSettings;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        public List<string> LoggerMessages { get; private set; } = new List<string>();

        /// <summary>
        /// Run Command
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            LoggerMessages.Clear();

            // ToDo Check runtime settings
            // ToDo Check return an messages
            PlannerOverviewHelper overviewHelper = new PlannerOverviewHelper(runtimeSettings.GraphConnectorHelper);
            overviewHelper.SaveAsHTMLFile(runtimeSettings.GetHTMLOutputFilePath());

            return true;
        }
    }
}
