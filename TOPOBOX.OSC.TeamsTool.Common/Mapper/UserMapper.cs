﻿using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Graph = Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    /// <summary>
    /// UserMapper
    /// </summary>
    public sealed class UserMapper : BaseObjectMapper
    {
# pragma warning disable CS1591
        public static User MapFrom(Graph.Models.User graphUser)
        {
            var user = new User();
            user.Id = graphUser.Id;
            user.DisplayName = graphUser.DisplayName;
            user.FullName = $"{graphUser.GivenName} {graphUser.Surname} ({graphUser.DisplayName})";
            user.Surname = graphUser.Surname;
            user.Firstname = graphUser.GivenName;
            user.Email = graphUser.Mail;
            
            if(graphUser.BusinessPhones != null && graphUser.BusinessPhones.Any())
            {
                user.BusinessPhones = graphUser.BusinessPhones;
            }

            return user;
        }

        public static User MapFrom(Graph.Models.IdentitySet graphIdentitySet)
        {
            object userIdentityType = string.Empty;
            if (graphIdentitySet.AdditionalData != null && graphIdentitySet.AdditionalData.Any())
            {
                graphIdentitySet.AdditionalData.TryGetValue("userIdentityType", out userIdentityType);
            }

            return new User()
            {
                Id = graphIdentitySet.User.Id,
                IdentityType = userIdentityType?.ToString(),
                DisplayName = graphIdentitySet.User.DisplayName,    
                FullName = $"({graphIdentitySet.User.DisplayName})"
            };
        }

        public static User MapFrom(Graph.Models.Identity graphIdentity)
        {
            object userIdentityType = string.Empty;
            if (graphIdentity.AdditionalData != null && graphIdentity.AdditionalData.Any())
            {
                graphIdentity.AdditionalData.TryGetValue("userIdentityType", out userIdentityType);
            }

            return new User()
            {
                Id = graphIdentity.Id,
                IdentityType = userIdentityType?.ToString(),
                DisplayName = graphIdentity.DisplayName,
                FullName = $"({graphIdentity.DisplayName})"
            };
        }
        
        public static Graph.Models.User MapTo(User user)
        {
            var graphUser = new Graph.Models.User();
            graphUser.Id = user.Id;
            graphUser.DisplayName = user.DisplayName;
            graphUser.Surname = user.Surname;
            graphUser.GivenName = user.Firstname;
            graphUser.Mail = user.Email;

            if (user.BusinessPhones != null && user.BusinessPhones.Any())
            {
                graphUser.BusinessPhones = (System.Collections.Generic.List<string>)user.BusinessPhones;
            }

            return graphUser;
        }

        public static Graph.Models.Identity MapToIdentity(User user)
        {
            var graphIdentity = new Graph.Models.Identity();
            graphIdentity.Id = user.Id;
            graphIdentity.DisplayName = user.DisplayName;
            return graphIdentity;
        }

        public static Graph.Models.ChatMessageFromIdentitySet MapToChatMessageFromIdentitySet(User user)
        {
            var chatMessageFromIdentitySet = new Graph.Models.ChatMessageFromIdentitySet();
            chatMessageFromIdentitySet.User = MapToIdentity(user);
            return chatMessageFromIdentitySet;
        }

        public static Graph.Models.ChatMessageMentionedIdentitySet MapToChatMessageMentionedIdentitySet(User user)
        {
            var chatMessageMentionedIdentitySet = new Graph.Models.ChatMessageMentionedIdentitySet();
            chatMessageMentionedIdentitySet.User = MapToIdentity(user);
            return chatMessageMentionedIdentitySet;
        }
# pragma warning restore CS1591
    }
}
