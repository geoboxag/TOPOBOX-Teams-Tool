using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public sealed class Table
    {
        public Table(string title, IEnumerable<IEnumerable<string>> headerRows, IEnumerable<IEnumerable<string>> rows)
        {
            Title = title;
            HeaderRows = headerRows;
            Rows = rows;
        }

        public string Title { get; private set; }
        public IEnumerable<IEnumerable<string>> HeaderRows { get; private set; }
        public IEnumerable<IEnumerable<string>> Rows { get; private set; }
    }
}

