using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph.Models;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// ChatMessageAttachmentsMapper
    /// </summary>
    public sealed class ChatMessageAttachmentsMapper : BaseObjectMapper
    {
#pragma warning disable CS1591
        public static List<Attachment> MapFrom(IEnumerable<Graph.ChatMessageAttachment> graphAttachments)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach (var graphAttachment in graphAttachments)
            {
                attachments.Add(new Attachment()
                {
                    Id = graphAttachment.Id,
                    Name = graphAttachment.Name,
                    ContentType = graphAttachment.ContentType,
                    ContentUrl = graphAttachment.ContentUrl,
                    Content = graphAttachment.Content
                });
            }
            return attachments;
        }

        public static List<Graph.ChatMessageAttachment> MapTo(List<Attachment> attachments)
        {
            List<Graph.ChatMessageAttachment> chatMessageAttachments = new List<Graph.ChatMessageAttachment>();
            foreach (var attachment in attachments)
            {
                chatMessageAttachments.Add(new Graph.ChatMessageAttachment()
                {
                    Id = attachment.Id,
                    Name = attachment.Name,
                    ContentType = attachment.ContentType,
                    ContentUrl = attachment.ContentUrl,
                    Content = attachment.Content
                });
            }
            return chatMessageAttachments;
        }
#pragma warning restore CS1591

    }
}
