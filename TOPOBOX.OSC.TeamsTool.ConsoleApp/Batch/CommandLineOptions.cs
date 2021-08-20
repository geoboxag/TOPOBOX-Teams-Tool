using CommandLine;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Batch
{
    /// <summary>
    /// Contains the properties of the command line arguments
    /// </summary>
    public sealed class CommandLineOptions
    {
        /// <summary>
        /// Type of function to be executed
        /// </summary>
        [Option("functiontype", HelpText = "Angabe welche Funktion ausgeführt werden soll", Required = true)]
        public string FunctionType { get; set; }

        /// <summary>
        /// The root directory in which the files are to be created
        /// </summary>
        [Option("folderpath", HelpText = "Angabe des Verzeichnispfades", Required = true)]
        public string FolderPath { get; set; }

        /// <summary>
        /// Name of the file without file extension
        /// </summary>
        [Option("filename", HelpText = "Angabe des Datei-Namens (ohne Dateiendung)", Required = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Full path and filename without file extension
        /// </summary>
        [Option("logfilepath", HelpText = "Angabe des Verzeichnisses mit Datei-Namen (ohne Dateiendung)", Required = false)]
        public string LogFilePath { get; set; }
    }
}
