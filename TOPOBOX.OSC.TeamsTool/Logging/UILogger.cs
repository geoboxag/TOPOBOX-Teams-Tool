using System;
using System.Collections.ObjectModel;
using GEOBOX.OSC.Common.Logging;

namespace TOPOBOX.OSC.TeamsTool.Logging
{
    /// <summary>
    /// CustomerFriendlyLogger. Logs messages and remembers statistics
    /// to make summaries for the end user (or "log reader").
    /// </summary>
    public class UILogger : CustomerFriendlyLogger
    {
        public ObservableCollection<string> LogMessages { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="toLogger">Logger to write to.</param>
        /// <param name="disposeLogger">if true, ToLogger will be disposed if this object is disposed.</param>
        public UILogger(ILogger toLogger, bool disposeLogger) : base(toLogger, disposeLogger)
        {
            LogMessages = new ObservableCollection<string>();
        }

        /// <summary>
        /// Writes message as information to the logger.
        /// </summary>
        /// <param name="message">Message</param>
        public override void WriteInformation(string message)
        {
            base.WriteInformation(message);
            LogMessages.Add($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - INF: {message}");
        }

        /// <summary>
        /// Writes message as warning to the logger.
        /// </summary>
        /// <param name="message">Message</param>
        public override void WriteWarning(string message)
        {
            base.WriteWarning(message);
            LogMessages.Add($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - WRN: {message}");
        }

        /// <summary>
        /// Writes message as error to the logger.
        /// </summary>
        /// <param name="message">Message</param>
        public override void WriteError(string message)
        {
            base.WriteError(message);
            LogMessages.Add($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - ERR: {message}");
        }

        /// <summary>
        /// Writes a time entry to the log (format 0:dd.MM.yyyy HH:mm:ss)
        /// </summary>
        public override void WriteTime()
        {
            base.WriteTime();
            LogMessages.Add($"TIM: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
        }

    }
}
