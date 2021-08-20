using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Contains Paragraphs to creating Html-Content
    /// </summary>
    public sealed class InformationList : InformationBase
    {
        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="paragraphs"></param>
        public InformationList(string title, string description, IEnumerable<Paragraph> paragraphs)
        {
            Title = title;
            Description = description;
            Paragraphs = paragraphs;
        }


        /// <summary>
        /// A Collection of Paragraphs
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs { get; private set; }
    }
}

