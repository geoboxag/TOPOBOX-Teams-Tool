using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.PlannersOverview;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.TeamsOverview;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller.UsersOverview;

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

            // Teams
            availableControllers.Add(JSONExportTeamsOverviewController.GetCommandName(), typeof(JSONExportTeamsOverviewController));
            availableControllers.Add(HTMLExportTeamsOverviewController.GetCommandName(), typeof(HTMLExportTeamsOverviewController));
            // Users
            availableControllers.Add(HTMLExportUsersOverviewController.GetCommandName(), typeof(HTMLExportUsersOverviewController));
            availableControllers.Add(JSONExportUsersOverviewController.GetCommandName(), typeof(JSONExportUsersOverviewController));
            // Planners
            availableControllers.Add(JSONExportPlannersOverviewController.GetCommandName(), typeof(JSONExportPlannersOverviewController));
            availableControllers.Add(HTMLExportPlannersOverviewController.GetCommandName(), typeof(HTMLExportPlannersOverviewController));

            return availableControllers;
        }
    }
}
