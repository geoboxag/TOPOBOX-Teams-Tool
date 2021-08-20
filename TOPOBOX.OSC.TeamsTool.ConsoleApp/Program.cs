using CommandLine;
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
            // Check is type available
            if (!availableControllers.ContainsKey(commandLineOptions.FunctionType))
            {
                Console.WriteLine("Die angegebene Funktion wurde nicht gefunden.");
                exitCode = ExitCode.Error;
                return;
            }

            // Init Console Settings
            SettingsController settingsController = new SettingsController();

            if (!settingsController.ConfigIsOk)
            {
                Console.WriteLine("Problem beim lesen der Einstellungsdatei:");
                foreach(string message in settingsController.Messages)
                {
                    Console.WriteLine(message);
                }
                exitCode = ExitCode.Error;
                return;
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

                using (IController controller = (IController)Activator.CreateInstance(availableControllers[commandLineOptions.FunctionType], new[] { batchRuntimeSettings }))
                {
                    if (controller.Execute())
                    {
                        Console.WriteLine($"Erfolgreich abgeschlossen: {commandLineOptions.FunctionType}");
                        exitCode = ExitCode.Success;
                    }
                    else
                    {
                        Console.WriteLine($"Funktion fehlgeschlagen: {commandLineOptions.FunctionType}");
                        exitCode = ExitCode.Error;
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hopperla, da ging etwas nicht so wie es sollte....");
                Console.WriteLine(ex.Message);
                exitCode = ExitCode.Error;
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