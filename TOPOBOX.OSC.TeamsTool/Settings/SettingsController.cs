using System;
using System.Collections.Generic;

namespace TOPOBOX.OSC.TeamsTool.Settings
{
    public class SettingsController
    {
        private const string DEFAULTAPPLICATIONTITLE = "TOPOBOX TeamsTool - Application";
        internal bool ConfigIsOk = false;

        internal List<string> Messages = new List<string>();

        internal SettingsController()
        {
        }

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
