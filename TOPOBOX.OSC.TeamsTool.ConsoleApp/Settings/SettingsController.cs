using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace TOPOBOX.OSC.TeamsTool.ConsoleApp.Settings
{
    public class SettingsController
    {
        private const string DEFAULTAPPLICATIONTITLE = "TOPOBOX TeamsTool - Console";
        internal bool ConfigIsOk = false;

        internal List<string> Messages = new List<string>();

        internal ConsoleAppSettings ConsoleAppSettings;

        internal SettingsController()
        {
            ReadConfigurationFile();
        }

        #region Read XML-File with Configurations
        private void ReadConfigurationFile()
        {
            var xmlFilePath = Properties.Settings.Default.ConsoleAppSettingsPath;

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
                    var result = new XmlSerializer(typeof(ConsoleAppSettings)).Deserialize(xmlTextReader);
                    ConsoleAppSettings = (ConsoleAppSettings)result;
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

        #region Get Application Title
        // Return the Windows Title from Properties
        internal string GetAssemblyWindowTitle()
        {
            try
            {
                return Properties.Resources.genModulName ?? DEFAULTAPPLICATIONTITLE;
            }
            catch
            {
                return DEFAULTAPPLICATIONTITLE;
            }
        }
        #endregion

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