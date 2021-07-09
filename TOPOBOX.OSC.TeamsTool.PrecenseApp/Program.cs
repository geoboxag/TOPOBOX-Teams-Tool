using CommandLine;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Batch;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Controller;
using TOPOBOX.OSC.TeamsTool.Common.Batch;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Properties;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Domain;
using TOPOBOX.OSC.TeamsTool.PrecenseApp.Settings;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp
{
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
        /// <param name="CommandLineOptions">Options from Command Line Input</param>
        static void RunOptions(CommandLineOptions commandLineOptions)
        { 
            // Check is type available
            if (!availableControllers.ContainsKey(commandLineOptions.SetPresence))
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
                    UserID = commandLineOptions.UserID
                };

                using (IController controller = (IController)Activator.CreateInstance(availableControllers[commandLineOptions.SetPresence], new[] { batchRuntimeSettings }))
                {
                    if (controller.Execute())
                    {
                        Console.WriteLine($"Erfolgreich abgeschlossen: {commandLineOptions.SetPresence}");
                        exitCode = ExitCode.Success;
                    }
                    else
                    {
                        Console.WriteLine($"Funktion fehlgeschlagen: {commandLineOptions.SetPresence}");
                        exitCode = ExitCode.Error;
                    }
                    // ToDo - gut so? besser möglichkeit? - Runtime Settings
                    string tempPath = Path.GetTempPath();
                    string fileName = $"{tempPath}TeamsTool_{DateTime.Now.ToString("yyyyMMddhhmmss")}.txt";
                    System.IO.File.AppendAllLines(fileName, controller.LoggerMessages);
                    Console.WriteLine($"Log-Datei erstellt: {fileName}");
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