using Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// MessageBody
    /// </summary>
    public class MessageBody
    {
        public BodyType? ContentType { get; set; }
        public string Content { get; set; }
    }
}
