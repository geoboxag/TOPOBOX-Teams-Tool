using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace TOPOBOX.OSC.TeamsTool.Settings
{
    internal class SettingsController
    {
        private const string DEFAULTAPPLICATIONTITLE = "TOPOBOX TeamsTool - Application";
        internal bool ConfigIsOk = false;

        internal List<string> Messages = new List<string>();

        internal ApplicationSettings ApplicationSettings;

        internal SettingsController()
        {
            ReadConfigurationFile();
        }

        #region Read XML-File with Configurations
        private void ReadConfigurationFile()
        {
            var xmlFilePath = Properties.Settings.Default.ApplicationSettingsPath;

            if (!System.IO.File.Exists(xmlFilePath))
            {
                Messages.Add($"XML-Konfigurations-Datei wurde nicht gefunden. Gesuchte Datei [{xmlFilePath}]");
                ConfigIsOk = false;
                return;
            }

            try
            {
                using (var xmlTextReader = new XmlTextReader(xmlFilePath))
                {
                    var result = new XmlSerializer(typeof(ApplicationSettings)).Deserialize(xmlTextReader);
                    ApplicationSettings = (ApplicationSettings)result;
                }
                // ToDo Check Settings
                ConfigIsOk = true;
            }
            catch (Exception ex)
            {
                Messages.Add(ex.Message);
                ConfigIsOk = false;
            }
        }
        #endregion

        internal bool IsCmdParameterExists(string cmdParamter)
        {
            return false;
        }

        #region Get Assembly-Version

        // Return the actual version from EXE to Display in Form
        internal string GetAssemblyVersionString()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("Version: {0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        #endregion Get Assembly-Version
    }
}
