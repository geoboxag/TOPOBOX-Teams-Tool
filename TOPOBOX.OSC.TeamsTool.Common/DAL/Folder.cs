using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Folder
    /// </summary>
    public class Folder
    {
        public string Name { get; set; }
        public List<Folder> Folders { get; set; }
        public List<FileInfo> Files { get; set; }
    }
}
