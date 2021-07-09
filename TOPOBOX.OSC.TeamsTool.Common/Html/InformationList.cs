using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public sealed class InformationList : InformationBase
    {
        public InformationList(string title, string description, IEnumerable<Paragraph> paragraphs)
        {
            Title = title;
            Description = description;
            Paragraphs = paragraphs;
        }


        public IEnumerable<Paragraph> Paragraphs { get; private set; }
    }
}

