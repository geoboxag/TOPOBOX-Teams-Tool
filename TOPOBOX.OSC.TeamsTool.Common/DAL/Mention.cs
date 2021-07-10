namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object Mention, which partially corresponds to the object <see cref="Microsoft.Graph.ChatMessageMention"/>
    /// </summary>
    public class Mention
    {
        /// <summary>
        /// Id <see cref="Microsoft.Graph.ChatMessageMention.Id"/>
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Text <see cref="Microsoft.Graph.ChatMessageMention.MentionText"/>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Internal User <see cref="DAL.User"/>
        /// </summary>
        public User User { get; set; }
    }
}
