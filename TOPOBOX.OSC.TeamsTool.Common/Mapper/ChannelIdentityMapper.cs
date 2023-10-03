using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph.Models;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// ChannelIdentityMapper
    /// </summary>
    public sealed class ChannelIdentityMapper : BaseObjectMapper
    {
#pragma warning disable CS1591
        public static ChannelIdentity MapFrom(Graph.ChannelIdentity graphChannelIdentity)
        {
            return new ChannelIdentity()
            {
                ChannelId = graphChannelIdentity.ChannelId,
                TeamId = graphChannelIdentity.TeamId
            };
        }

        public static Graph.ChannelIdentity MapTo(ChannelIdentity channelIdentity)
        {
            return new Graph.ChannelIdentity()
            {
                ChannelId = channelIdentity.ChannelId,
                TeamId = channelIdentity.TeamId
            };
        }
#pragma warning restore CS1591

    }
}
