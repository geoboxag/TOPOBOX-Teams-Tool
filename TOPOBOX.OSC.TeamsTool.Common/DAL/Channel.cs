namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object Channel, which partially corresponds to the object <see cref="Microsoft.Graph.Channel"/>
    /// </summary>
    public class Channel : BaseData
    {
        /// <summary>
        /// Name <see cref="Microsoft.Graph.Channel.DisplayName"/>
        /// </summary>
        public string Name { get; set; }
    }
}
