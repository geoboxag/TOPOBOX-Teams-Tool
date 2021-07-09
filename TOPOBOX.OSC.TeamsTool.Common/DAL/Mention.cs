namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Mention
    /// </summary>
    public class Mention
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
    }
}
