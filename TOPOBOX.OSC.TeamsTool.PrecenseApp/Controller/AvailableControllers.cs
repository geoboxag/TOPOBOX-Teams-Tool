using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Controller.TeamsOverview;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp.Controller
{
    /// <summary>
    /// All available controlles for execute with command line
    /// </summary>
    internal class AvailableControllers
    {
        /// <summary>
        /// Register for all Controllers
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string /*Key: FunctionType*/, Type /*Value: Controller-Class-Type*/> Get()
        {
            var availableControllers = new Dictionary<string, Type>();

            availableControllers.Add(SetAvailableController.GetCommandName(), typeof(SetAvailableController));
            availableControllers.Add(SetAwayController.GetCommandName(), typeof(SetAwayController));
            availableControllers.Add(SetBusyInCallController.GetCommandName(), typeof(SetBusyInCallController));
            availableControllers.Add(ResetAvailableController.GetCommandName(), typeof(ResetAvailableController));

            return availableControllers;
        }
    }
}
