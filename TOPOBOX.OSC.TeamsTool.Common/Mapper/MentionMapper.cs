using System.Collections.Generic;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// MentionMapper
    /// </summary>
    public sealed class MentionMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static List<Mention> MapFrom(IEnumerable<Graph.ChatMessageMention> graphMentions)
        {
            List<Mention> mentions = new List<Mention>();
            foreach (var graphMention in graphMentions)
            {
                mentions.Add(new Mention()
                {
                    Id = graphMention.Id,
                    Text = graphMention.MentionText,
                    User = UserMapper.MapFrom(graphMention.Mentioned.User)
                });

            }
            return mentions;
        }

        public static IEnumerable<Graph.ChatMessageMention> MapTo(List<Mention> mentions)
        {
            List<Graph.ChatMessageMention> chatMessageMentions = new List<Graph.ChatMessageMention>();
            foreach (var mention in mentions)
            {
                chatMessageMentions.Add(new Graph.ChatMessageMention()
                {
                    Id = mention.Id,
                    MentionText = mention.Text,
                    Mentioned = UserMapper.MapToChatMessageMentionedIdentitySet(mention.User)
                });
            }
            return chatMessageMentions;
        }
#pragma warning restore CS1591

    }
}
