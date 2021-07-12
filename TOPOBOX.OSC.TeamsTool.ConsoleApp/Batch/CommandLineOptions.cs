using CommandLine;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Batch
{
    public sealed class CommandLineOptions
    {
        [Option("functiontype", HelpText = "Angabe welche Funktion ausgeführt werden soll", Required = true)]
        public string FunctionType { get; set; }

        [Option("folderpath", HelpText = "Angabe des Verzeichnispfades", Required = true)]
        public string FolderPath { get; set; }

        [Option("filename", HelpText = "Angabe des Datei-Namens (ohne Dateiendung)", Required = false)]
        public string FileName { get; set; }

        [Option("logfilepath", HelpText = "Angabe des Verzeichnisses mit Datei-Namen (ohne Dateiendung)", Required = false)]
        public string LogFilePath { get; set; }
    }
}
