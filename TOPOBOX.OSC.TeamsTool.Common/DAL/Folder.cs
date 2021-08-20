using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Representing one folder
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// FolderName
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// SubFolders
        /// </summary>
        public List<Folder> Folders { get; set; }

        /// <summary>
        /// Files in Folder
        /// </summary>
        public List<FileInfo> Files { get; set; }
    }
}
