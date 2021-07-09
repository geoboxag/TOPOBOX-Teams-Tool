using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.Common.Controller;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;

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
            UsersOverviewHelper usersOverviewHelper = new UsersOverviewHelper(runtimeSettings.GraphConnectorHelper);
            usersOverviewHelper.SaveAsHTMLFile(runtimeSettings.GetHTMLOutputFilePath());

            return true;
        }
    }
}
