namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object ChannelIdentity, which partially corresponds to the object <see cref="Microsoft.Graph.ChannelIdentity"/>
    /// </summary>
    public class ChannelIdentity
    {
        /// <summary>
        /// TeamId <see cref="Microsoft.Graph.ChannelIdentity.TeamId"/>
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// ChannelId <see cref="Microsoft.Graph.ChannelIdentity.ChannelId"/>
        /// </summary>
        public string ChannelId { get; set; }
    }
}
