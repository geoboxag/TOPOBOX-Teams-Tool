using System.Collections.Generic;
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
            return new ChatMessage()
            {
                Id = graphChatMessage.Id,
                ReplyToId = graphChatMessage.ReplyToId,
                MessageType = graphChatMessage.MessageType,
                CreatedDateTime = graphChatMessage.CreatedDateTime,
                LastModifiedDateTime = graphChatMessage.LastModifiedDateTime,
                LastEditedDateTime = graphChatMessage.LastEditedDateTime,
                Subject = graphChatMessage.Subject,
                Summary = graphChatMessage.Summary,
                ChatId = graphChatMessage.ChatId,
                Importance = graphChatMessage.Importance,
                User = UserMapper.MapFrom(graphChatMessage.From),
                MessageBody = ItemBodyMapper.MapFrom(graphChatMessage.Body),
                ChannelIdentity = ChannelIdentityMapper.MapFrom(graphChatMessage.ChannelIdentity),
                Mentions = MentionMapper.MapFrom(graphChatMessage.Mentions),
                Attachments = ChatMessageAttachmentsMapper.MapFrom(graphChatMessage.Attachments),
                MessageReplies = MapFrom(graphChatMessage.Replies)
            };
        }

        public static List<ChatMessage> MapFrom(IEnumerable<Graph.ChatMessage> graphChatMessages)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            foreach (var graphChatMessage in graphChatMessages)
            {
                chatMessages.Add(MapFrom(graphChatMessage));
            }

            return chatMessages;
        }

        public static Graph.ChatMessage MapTo(ChatMessage chatMessage)
        {
            return new Graph.ChatMessage()
            {
                Id = chatMessage.Id,
                ReplyToId = chatMessage.ReplyToId,
                MessageType = chatMessage.MessageType,
                CreatedDateTime = chatMessage.CreatedDateTime,
                LastModifiedDateTime = chatMessage.LastModifiedDateTime,
                LastEditedDateTime = chatMessage.LastEditedDateTime,
                Subject = chatMessage.Subject,
                Summary = chatMessage.Summary,
                ChatId = chatMessage.ChatId,
                Importance = chatMessage.Importance,
                From = UserMapper.MapToChatMessageFromIdentitySet(chatMessage.User),
                Body = ItemBodyMapper.MapTo(chatMessage.MessageBody),
                ChannelIdentity = ChannelIdentityMapper.MapTo(chatMessage.ChannelIdentity),
                Mentions = MentionMapper.MapTo(chatMessage.Mentions),
                Attachments = ChatMessageAttachmentsMapper.MapTo(chatMessage.Attachments),
                Replies = MapToCollectionPage(chatMessage.MessageReplies)
            };
        }

        public static List<Graph.ChatMessage> MapTo(List<ChatMessage> chatMessages)
        {
            List<Graph.ChatMessage> graphChatMessages = new List<Graph.ChatMessage>();

            foreach (var chatMessage in chatMessages)
            {
                graphChatMessages.Add(MapTo(chatMessage));
            }

            return graphChatMessages;
        }

        private static Graph.IChatMessageRepliesCollectionPage MapToCollectionPage(List<ChatMessage> messageReplies)
        {
            var collPages = new Graph.ChatMessageRepliesCollectionPage();
            foreach (var messageReply in messageReplies)
            {
                collPages.Add(MapTo(messageReply));
            }

            return collPages;
        }

#pragma warning restore CS1591

    }
}
