using System;
using System.Collections.Generic;
using System.IO;

namespace TOPOBOX.OSC.TeamsTool.Common.Logger
{
    public class Logger
    {
        public List<string> Messages { get; private set; }

        public Logger()
        {
            Messages = new List<string>();
        }

        public void WriteError(string message)
        {
            Messages.Add($"ERR: {message}");
        }

        public void WriteInformation(string message)
        {
            Messages.Add($"INF: {message}");
        }

        public void WriteLine(string message)
        {
            Messages.Add(message);
        }

        public void WriteWarning(string message)
        {
            Messages.Add($"WRN: {message}");
        }

        public bool WriteLogFile(string logFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(logFilePath))
                {
                    var logFilePathWithExtension = $"{logFilePath}.tmp";
                    File.AppendAllLines(logFilePathWithExtension, Messages);
                }
                return true;
            }
            catch (Exception ex)
            {
                // TODO: exception
                return false;               
            }
        }

        public void Dispose()
        {
            Messages.Clear();
        }


    }
}
