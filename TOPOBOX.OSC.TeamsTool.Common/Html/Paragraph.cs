using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public sealed class Paragraph
    {
        public Paragraph(string title, IEnumerable<string> rows)
        {
            Title = title;
            Rows = rows;
        }

        public string Title { get; private set; }
        public IEnumerable<string> Rows { get; private set; }
    }
}

