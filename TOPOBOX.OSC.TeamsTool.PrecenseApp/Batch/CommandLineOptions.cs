using CommandLine;

namespace TOPOBOX.OSC.TeamsTool.PrecenseApp.Batch
{
    public sealed class CommandLineOptions
    {
        [Option("setPresence", HelpText = "Angabe welcher Status gesetzt werden soll", Required = true)]
        public string SetPresence { get; set; }

        [Option("userid", HelpText = "Benutzer Identifikator (ID oder E-Mail Adresse)", Required = true)]
        public string UserID { get; set; }
    }
}
