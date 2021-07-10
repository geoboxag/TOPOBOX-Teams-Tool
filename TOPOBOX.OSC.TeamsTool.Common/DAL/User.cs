namespace TOPOBOX.OSC.TeamsTool.Common.DAL
{
    /// <summary>
    /// Internal object User, which partially corresponds to the object <see cref="Microsoft.Graph.User"/> / <see cref="Microsoft.Graph.AadUserConversationMember"/>
    /// </summary>
    public class User : BaseData
    {
        /// <summary>
        /// DisplayName <see cref="Microsoft.Graph.User.DisplayName"/>
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// FullName ([Firstname] [Surname]
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Surname <see cref="Microsoft.Graph.User.Surname"/>
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Firstname <see cref="Microsoft.Graph.User.GivenName"/>
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Email <see cref="Microsoft.Graph.User.Mail"/>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// IdentityType <see cref="Microsoft.Graph.IdentitySet.AdditionalData"/>
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// CompanyName <see cref="Microsoft.Graph.User.CompanyName"/>
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// BusinessPhone <see cref="Microsoft.Graph.User.BusinessPhones"/>
        /// </summary>
        public string BusinessPhone { get; set; }
    }
}