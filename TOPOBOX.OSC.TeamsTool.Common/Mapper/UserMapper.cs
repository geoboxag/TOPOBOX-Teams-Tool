using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// UserMapper
    /// </summary>
    public sealed class UserMapper : BaseObjectMapper
    {
        public static User MapFrom(Graph.User graphUser)
        {
            var user = new User();
            user.Id = graphUser.Id;
            user.DisplayName = graphUser.DisplayName;
            user.Surname = graphUser.Surname;
            user.Firstname = graphUser.GivenName;
            user.Email = graphUser.Mail;
            
            if(graphUser.BusinessPhones != null && graphUser.BusinessPhones.Any())
            {
                user.BusinessPhones = graphUser.BusinessPhones;
            }

            return user;
        }

        public static User MapFrom(Graph.IdentitySet graphIdentitySet)
        {
            object userIdentityType = string.Empty;
            if (graphIdentitySet.AdditionalData != null && graphIdentitySet.AdditionalData.Any())
            {
                graphIdentitySet.AdditionalData.TryGetValue("userIdentityType", out userIdentityType);
            }

            return new User()
            {
                Id = graphIdentitySet.User.Id,
                IdentityType = userIdentityType.ToString(),
                DisplayName = graphIdentitySet.User.DisplayName               
            };
        }
    }
}
