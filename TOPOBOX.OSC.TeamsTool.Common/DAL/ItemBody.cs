using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object MessageBody, which partially corresponds to the object <see cref="Microsoft.Graph.ItemBody"/>
    /// </summary>
    public class ItemBody
    {
        /// <summary>
        /// ContentType <see cref="Microsoft.Graph.ItemBody.ContentType"/>
        /// </summary>
        public BodyType? ContentType { get; set; }

        /// <summary>
        /// Content <see cref="Microsoft.Graph.ItemBody.Content"/>
        /// </summary>
        public string Content { get; set; }
    }
}
