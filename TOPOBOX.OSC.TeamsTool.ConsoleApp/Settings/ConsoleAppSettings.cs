using System;
using System.Xml.Serialization;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Settings
{
    [Serializable]
    public sealed class ConsoleAppSettings
    {
        [XmlElement("ClientId")]
        public string ClientId { get; set; }

        [XmlElement("ClientSecret")]
        public string ClientSecret { get; set; }

        [XmlElement("TenantId")]
        public string TenantId { get; set; }
    }
}
