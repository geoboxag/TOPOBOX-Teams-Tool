namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal Object Attachment, which partially corresponds to the object <see cref="Microsoft.Graph.ChatMessageAttachment"/>
    /// </summary>
    public class Attachment : BaseData
    {
        /// <summary>
        /// Name of the attachment <see cref="Microsoft.Graph.ChatMessageAttachment.Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ContentType of the attachment <see cref="Microsoft.Graph.ChatMessageAttachment.ContentType"/>
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Content of the attachment <see cref="Microsoft.Graph.ChatMessageAttachment.Content"/>
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ContentUrl of the attachment <see cref="Microsoft.Graph.ChatMessageAttachment.ContentUrl"/>
        /// </summary>
        public string ContentUrl { get; set; }
        
    }
}
