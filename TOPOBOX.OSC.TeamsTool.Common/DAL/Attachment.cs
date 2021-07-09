namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Attacchment
    /// </summary>
    public class Attachment : BaseData
    {
        public string Name { get; set; }

        public string ContentType { get; set; }
        public string Content { get; set; }
        public string ContentUrl { get; set; }
        
    }
}
