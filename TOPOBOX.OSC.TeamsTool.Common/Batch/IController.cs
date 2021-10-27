using GEOBOX.OSC.Common.Logging;
using System;

namespace TOPOBOX.OSC.TeamsTool.Common.Batch
{
    /// <summary>
    /// Interface for all Commandline implementations
    /// </summary>
    public interface IController : IDisposable
    {
        /// <summary>
        /// Get the name (function type)
        /// </summary>
        /// <returns>function type</returns>
        // ToDo: C#8
        //static string GetCommandName();

        /// <summary>
        /// Execute Command
        /// </summary>
        /// <returns>true is runnig without errors</returns>
        bool Execute();

        /// <summary>
        /// Logger with all Messages from execute
        /// </summary>
        ILogger Logger { get; }

        // ToDo after refactoring
        /// <summary>
        /// Path and filename for log-file
        /// </summary>
        //string LogPath { get; }

        
    }
}
