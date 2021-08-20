using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Contains Tables to creating Html-Content
    /// </summary>
    public sealed class InformationTable : InformationBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="tables"></param>
        public InformationTable(string title, string description, IEnumerable<Table> tables)
        {
            Title = title;
            Description = description;
            Tables = tables;
        }


        /// <summary>
        /// A Collection of Tables
        /// </summary>
        public IEnumerable<Table> Tables { get; private set; }
    }
}

