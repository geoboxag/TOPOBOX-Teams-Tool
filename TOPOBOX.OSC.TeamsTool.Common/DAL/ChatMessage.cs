using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// ChatMessage
    /// </summary>
    public class ChatMessage : BaseData
    {
        public string ReplyToId { get; set; }

        public ChatMessageType? MessageType { get; set; }

        public DateTimeOffset? CreatedDateTime { get; set; }

        public DateTimeOffset? LastModifiedDateTime { get; set; }

        public DateTimeOffset? LastEditedDateTime { get; set; }

        public string Subject { get; set; }

        public string Summary { get; set; }

        public string ChatId { get; set; }

        public ChatMessageImportance? Importance { get; set; }

        public User User { get; set; }

        public MessageBody GbxMessageBody { get; set; }

        public ChannelIdentity GbxChannelIdentity { get; set; } 

        public List<Attachment> Attachments { get; set; }

        public List<Mention> GbxMentions { get; set; }

        public List<ChatMessage> MessageReplies { get; set; }
    }
}
