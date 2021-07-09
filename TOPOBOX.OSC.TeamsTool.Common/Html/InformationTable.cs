using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public sealed class InformationTable : InformationBase
    {
        public InformationTable(string title, string description, IEnumerable<Table> tables)
        {
            Title = title;
            Description = description;
            Tables = tables;
        }


        public IEnumerable<Table> Tables { get; private set; }
    }
}

