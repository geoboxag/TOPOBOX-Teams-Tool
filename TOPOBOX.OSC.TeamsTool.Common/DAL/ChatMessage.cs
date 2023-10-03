using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object ChatMessage, which partially corresponds to the object <see cref="Microsoft.Graph.ChatMessage"/>
    /// </summary>
    public class ChatMessage : BaseData
    {
        /// <summary>
        /// Id of the other ChatMessage, where this message is a reply, or null
        /// </summary>
        public string ReplyToId { get; set; }

        /// <summary>
        /// ChatMessageType <see cref="ChatMessageType"/>
        /// </summary>
        public ChatMessageType? MessageType { get; set; }

        /// <summary>
        /// CreatedDateTime <see cref="Microsoft.Graph.ChatMessage.CreatedDateTime"/>
        /// </summary>
        public DateTimeOffset? CreatedDateTime { get; set; }

        /// <summary>
        /// LastModifiedDateTime <see cref="Microsoft.Graph.ChatMessage.LastModifiedDateTime"/>
        /// </summary>
        public DateTimeOffset? LastModifiedDateTime { get; set; }

        /// <summary>
        /// LastEditedDateTime <see cref="Microsoft.Graph.ChatMessage.LastEditedDateTime"/>
        /// </summary>
        public DateTimeOffset? LastEditedDateTime { get; set; }

        /// <summary>
        /// Subject <see cref="Microsoft.Graph.ChatMessage.Subject"/>
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Summary <see cref="Microsoft.Graph.ChatMessage.Summary"/>
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// ChatId <see cref="Microsoft.Graph.ChatMessage.ChatId"/>
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// Importance <see cref="Microsoft.Graph.ChatMessageImportance"/>
        /// </summary>
        public ChatMessageImportance? Importance { get; set; }

        /// <summary>
        /// Internal User 
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Internal MessageBody
        /// </summary>
        public ItemBody MessageBody { get; set; }

        /// <summary>
        /// Internal ChannelIdentity
        /// </summary>
        public ChannelIdentity ChannelIdentity { get; set; }

        /// <summary>
        /// A List of internal Attachments
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// A List of internal Mentions
        /// </summary>
        public List<Mention> Mentions { get; set; }

        /// <summary>
        /// A List of internal ChatMessages
        /// </summary>
        public List<ChatMessage> MessageReplies { get; set; }
    }
}
