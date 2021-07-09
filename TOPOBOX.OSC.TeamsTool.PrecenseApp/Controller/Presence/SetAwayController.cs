using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Domain;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp.Controller.TeamsOverview
{
    internal sealed class SetAwayController : IController
    {
        // DEBUG Command: --setPresence away --userid {userid}

        /// <summary>
        /// Name of FunctionType
        /// Key: must be unique
        /// </summary>
        private const string FUNCTIONTYPE = "away";

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
        public SetAwayController(BatchRuntimeSettings batchRuntimeSettings)
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
            SetPrecense setPrecense = new SetPrecense(runtimeSettings);
            setPrecense.SetAway(runtimeSettings.UserID);
            return true;
        }
    }
}
