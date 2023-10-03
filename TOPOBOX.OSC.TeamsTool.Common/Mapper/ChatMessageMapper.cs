using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph.Models;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// ChatMessageMapper
    /// </summary>
    public sealed class ChatMessageMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static ChatMessage MapFrom(Microsoft.Graph.Models.ChatMessage graphChatMessage)
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

        public static List<ChatMessage> MapFrom(IEnumerable<Microsoft.Graph.Models.ChatMessage> graphChatMessages)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            foreach (var graphChatMessage in graphChatMessages)
            {
                chatMessages.Add(MapFrom(graphChatMessage));
            }

            return chatMessages;
        }

        public static Microsoft.Graph.Models.ChatMessage MapTo(ChatMessage chatMessage)
        {
            return new Microsoft.Graph.Models.ChatMessage()
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
                Mentions = (List<Microsoft.Graph.Models.ChatMessageMention>)MentionMapper.MapTo(chatMessage.Mentions),
                Attachments = ChatMessageAttachmentsMapper.MapTo(chatMessage.Attachments),
                Replies = MapToCollectionPage(chatMessage.MessageReplies)
            };
        }

        public static List<Microsoft.Graph.Models.ChatMessage> MapTo(List<ChatMessage> chatMessages)
        {
            List<Microsoft.Graph.Models.ChatMessage> graphChatMessages = new List<Microsoft.Graph.Models.ChatMessage>();

            foreach (var chatMessage in chatMessages)
            {
                graphChatMessages.Add(MapTo(chatMessage));
            }

            return graphChatMessages;
        }

        private static List<Microsoft.Graph.Models.ChatMessage> MapToCollectionPage(List<ChatMessage> messageReplies)
        //private static Microsoft.Graph.Models.IChatMessageRepliesCollectionPage MapToCollectionPage(List<ChatMessage> messageReplies)
        {
            //var collPages = new Microsoft.Graph.Models.ChatCollectionResponse();
            var e = new List<Microsoft.Graph.Models.ChatMessage>();
            //var collPages = new Microsoft.Graph.Models.MessageCollectionResponse();
            //var collPages = new Microsoft.Graph.Models.ChatMessageRepliesCollectionPage();
            foreach (var messageReply in messageReplies)
            {
                e.Add(MapTo(messageReply));
            }

            return e;
        }

#pragma warning restore CS1591

    }
}
