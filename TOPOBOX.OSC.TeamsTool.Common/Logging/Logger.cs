using System;
using System.Collections.Generic;
using System.IO;

namespace TOPOBOX.OSC.TeamsTool.Common.Logging
{
    /// <summary>
    /// Logger for logging the run of an executed function 
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Messages of the Logger
        /// </summary>
        public List<string> Messages { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Logger()
        {
            Messages = new List<string>();
        }

        /// <summary>
        /// Add Errormessage: ERR: [message]
        /// </summary>
        /// <param name="message"></param>
        public void WriteError(string message)
        {
            Messages.Add($"ERR: {message}");
        }

        /// <summary>
        /// Add Informationmessage: INF: [message]
        /// </summary>
        /// <param name="message"></param>
        public void WriteInformation(string message)
        {
            Messages.Add($"INF: {message}");
        }

        /// <summary>
        /// Add simple Message: [message]
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            Messages.Add(message);
        }

        /// <summary>
        /// Add Warningmessage: WRN: [message]
        /// </summary>
        /// <param name="message"></param>
        public void WriteWarning(string message)
        {
            Messages.Add($"WRN: {message}");
        }

        /// <summary>
        /// Save the loggermessages in a file
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Clears the Messages
        /// </summary>
        public void Dispose()
        {
            Messages.Clear();
        }


    }
}
