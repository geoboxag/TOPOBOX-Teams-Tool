using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// ChatMessageMapper
    /// </summary>
    public sealed class ChatMessageMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static ChatMessage MapFrom(Graph.ChatMessage graphChatMessage)
        {
            // TODO: Implement

            // var graphChatMessageReplies = new List<GbxChatMessage>();
            // if (graphChatMessage.Replies != null && graphChatMessage.Replies.Any())
            // {
            //     graphChatMessageReplies = ChatMessageMapper.MapToGbxChatMessages(graphChatMessage.Replies.ToList());
            // }

            return new ChatMessage()
            {
                Id = graphChatMessage.Id,
                ReplyToId = graphChatMessage.ReplyToId,
                MessageType = graphChatMessage.MessageType.Value,
                CreatedDateTime = graphChatMessage.CreatedDateTime,
                LastModifiedDateTime = graphChatMessage.LastModifiedDateTime,
                LastEditedDateTime = graphChatMessage.LastEditedDateTime,
                Subject = graphChatMessage.Subject,
                Summary = graphChatMessage.Summary,
                ChatId = graphChatMessage.ChatId,
                Importance = graphChatMessage.Importance,
                // User = MapToGbxUser(graphChatMessage.From),
                // GbxMessageBody = MapToGbxMessageBody(graphChatMessage.Body),
                // GbxChannelIdentity = MapToGbxChannelIdentity(graphChatMessage.ChannelIdentity),
                // GbxMentions = MapToGbxMentions(graphChatMessage.Mentions),
                // GbxAttachments = MapToGbxAttachments(graphChatMessage.Attachments),
                // MessageReplies = graphChatMessageReplies
            };

        }



        public static Graph.ChatMessage MapFrom(ChatMessage chatMessage)
        {
            // TODO: Implement
            // var chatMessageReplies = new List<GbxChatMessage>();
            // if (chatMessage.Replies != null && chatMessage.Replies.Any())
            // {
            //     chatMessageReplies = ChatMessageMapper.MapToGbxChatMessages(chatMessage.Replies.ToList());
            // }

            return new Graph.ChatMessage()
            {
                Id = chatMessage.Id,
                ReplyToId = chatMessage.ReplyToId,
                MessageType = chatMessage.MessageType.Value,
                CreatedDateTime = chatMessage.CreatedDateTime,
                LastModifiedDateTime = chatMessage.LastModifiedDateTime,
                LastEditedDateTime = chatMessage.LastEditedDateTime,
                Subject = chatMessage.Subject,
                Summary = chatMessage.Summary,
                ChatId = chatMessage.ChatId,
                Importance = chatMessage.Importance,
                // User = MapToGbxUser(chatMessage.From),
                // GbxMessageBody = MapToGbxMessageBody(chatMessage.Body),
                // GbxChannelIdentity = MapToGbxChannelIdentity(chatMessage.ChannelIdentity),
                // GbxMentions = MapToGbxMentions(chatMessage.Mentions),
                // GbxAttachments = MapToGbxAttachments(chatMessage.Attachments),
                // MessageReplies = chatMessageReplies
            };

        }
# pragma warning restore CS1591

    }
}
