using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Represents a Paragraph with its Title
    /// </summary>
    public sealed class Paragraph
    {
        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="rows"></param>
        public Paragraph(string title, IEnumerable<string> rows)
        {
            Title = title;
            Rows = rows;
        }

        /// <summary>
        /// ParagraphTitle
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// ParagraphLines
        /// </summary>
        public IEnumerable<string> Rows { get; private set; }
    }
}

