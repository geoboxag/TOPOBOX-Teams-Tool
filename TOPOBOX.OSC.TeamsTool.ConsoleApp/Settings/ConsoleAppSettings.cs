using System;
using System.Xml.Serialization;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Settings
{
    /// <summary>
    /// Contains the Id's and Secrets of the Graph API
    /// </summary>
    [Serializable]
    public sealed class ConsoleAppSettings
    {
        /// <summary>
        /// The Client-Id that was registered for this application
        /// </summary>
        [XmlElement("ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// The Client-Secret that was registered for this application
        /// </summary>
        [XmlElement("ClientSecret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Id of the Organisation in Microsoft.Graph
        /// </summary>
        [XmlElement("TenantId")]
        public string TenantId { get; set; }
    }
}
