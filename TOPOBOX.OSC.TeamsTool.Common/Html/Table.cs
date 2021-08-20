using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Represents a Table with Title, HeaderRows and Rows
    /// </summary>
    public sealed class Table
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="headerRows"></param>
        /// <param name="rows"></param>
        public Table(string title, IEnumerable<IEnumerable<string>> headerRows, IEnumerable<IEnumerable<string>> rows)
        {
            Title = title;
            HeaderRows = headerRows;
            Rows = rows;
        }

        /// <summary>
        /// TableTitle
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Titles of the Columns
        /// </summary>
        public IEnumerable<IEnumerable<string>> HeaderRows { get; private set; }

        /// <summary>
        /// Values of the Rows
        /// </summary>
        public IEnumerable<IEnumerable<string>> Rows { get; private set; }
    }
}

