using CommandLine;
using GEOBOX.OSC.Common.Logging;
using System;
using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Batch;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Controller;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Domain;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Properties;
using TOPOBOX.OSC.TeamsTool.ConsoleApp.Settings;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// All Available Controllers
        /// </summary>
        private static Dictionary<string, Type> availableControllers = AvailableControllers.Get();

        static ExitCode exitCode = ExitCode.Error;

        [STAThread]
        static int Main(string[] args)
        {
            if (args != null && args.Length > 0)
            // run as command line app
            {
                var commandLineOptions = Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
#if DEBUG
                Console.ReadKey();
#endif          
            }
            return (int)exitCode;
        }


        /// <summary>
        /// Run Console Application with parsed Command Line Options
        /// </summary>
        /// <param name="commandLineOptions">Options from Command Line Input</param>
        static void RunOptions(CommandLineOptions commandLineOptions)
        {
            string logFilePath = $"{commandLineOptions.LogFilePath}.log";

            ILogger logger = new CustomerFriendlyLogger(
                FileLogger.Create(logFilePath), true);

            // Check is type available
            if (!availableControllers.ContainsKey(commandLineOptions.FunctionType))
            {
                var message = "Die angegebene Funktion wurde nicht gefunden.";
                logger?.WriteWarning(message);
                Console.WriteLine(message);
                exitCode = ExitCode.Error;
                return;
            }
            else
            {
                logger?.WriteInformation("Die angegebene Funktion wurde gefunden.");
            }

            // Init Console Settings
            SettingsController settingsController = new SettingsController();

            if (!settingsController.ConfigIsOk)
            {
                var message1 = "Problem beim lesen der Einstellungsdatei:";
                logger?.WriteWarning(message1);
                Console.WriteLine(message1);
                foreach(string message in settingsController.Messages)
                {
                    logger?.WriteWarning(message);
                    Console.WriteLine(message);
                }
                exitCode = ExitCode.Error;
                return;
            }
            else
            {
                logger?.WriteInformation("Einstellungsdatei wurde erfolgreich gelesen.");
            }

            try
            {
                // ToDo make better for getters from ConsoleAppSettings
                var batchRuntimeSettings = new BatchRuntimeSettings(
                    settingsController.ConsoleAppSettings.ClientId, 
                    settingsController.ConsoleAppSettings.ClientSecret,
                    settingsController.ConsoleAppSettings.TenantId)
                {
                    OutputDirectory = commandLineOptions.FolderPath,
                    OutputFileName = commandLineOptions.FileName,
                    LogFilePath = commandLineOptions.LogFilePath
                };


                using (IController controller = (IController)Activator.CreateInstance(availableControllers[commandLineOptions.FunctionType], new object[] { batchRuntimeSettings, logger }))
                {
                    logger?.WriteInformation($"Starte die Funktion: {commandLineOptions.FunctionType}");

                    if (controller.Execute())
                    {
                        var message = $"Erfolgreich abgeschlossen: {commandLineOptions.FunctionType}";
                        logger?.WriteInformation(message);
                        Console.WriteLine(message);
                        exitCode = ExitCode.Success;
                    }
                    else
                    {
                        var message = $"Funktion fehlgeschlagen: {commandLineOptions.FunctionType}";
                        logger?.WriteError(message);
                        Console.WriteLine(message);
                        exitCode = ExitCode.Error;
                    }

                    logger?.Dispose();
                    return;
                }
            }
            catch (Exception ex)
            {
                var message = "Hopperla, da ging etwas nicht so wie es sollte....";
                logger?.WriteError(message);
                Console.WriteLine(message);
                logger?.WriteError(ex.Message);
                Console.WriteLine(ex.Message);
                exitCode = ExitCode.Error;
                logger?.Dispose();
                return;
            }
        }

        /// <summary>
        /// Run Error Case
        /// </summary>
        /// <param name="errors"></param>
        static void HandleParseError(IEnumerable<CommandLine.Error> errors)
        {
            Console.WriteLine(Resources.CMDCallWithError);

            exitCode = ExitCode.Error;
        }
    }
}