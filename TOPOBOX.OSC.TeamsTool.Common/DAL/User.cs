namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// User
    /// </summary>
    public class User : BaseData
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string IdentityType { get; set; }

        public string BusinessPhone { get; set; }
    }
}
