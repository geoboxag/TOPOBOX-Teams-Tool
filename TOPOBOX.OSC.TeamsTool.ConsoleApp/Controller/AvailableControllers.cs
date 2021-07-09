using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.PlannersOverview;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.TeamsOverview;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller
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

            availableControllers.Add(JSONExportTeamsOverviewController.GetCommandName(), typeof(JSONExportTeamsOverviewController));
            availableControllers.Add(HTMLExportTeamsOverviewController.GetCommandName(), typeof(HTMLExportTeamsOverviewController));
            availableControllers.Add(PDFExportUsersOverviewController.GetCommandName(), typeof(PDFExportUsersOverviewController));
            availableControllers.Add(JSONExportPlannersOverviewController.GetCommandName(), typeof(JSONExportPlannersOverviewController));

            return availableControllers;
        }
    }
}
