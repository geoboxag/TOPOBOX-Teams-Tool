using System;
using System.Collections.Generic;

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
        /// List with all Messages from execute
        /// </summary>
        List<string> LoggerMessages { get; }

        // ToDo after refactoring
        /// <summary>
        /// Path and filename for log-file
        /// </summary>
        //string LogPath { get; }

        
    }
}
