using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    public static class HtmlWriterReplacements
    {
        public static Dictionary<string, string> DefaultReplacements(string reportTitle)
        {
            var replacements = new Dictionary<string, string>();
            replacements.Add("[APP_NAME]", Assembly.GetExecutingAssembly().GetName().FullName);
            replacements.Add("[APP_VERSION]", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            replacements.Add("[GEN_TITLE]", reportTitle);
            replacements.Add("[GEN_DATE]", System.DateTime.Now.ToString(@"dd.MM.yyyy", CultureInfo.InvariantCulture));
            replacements.Add("[GEN_TIME]", System.DateTime.Now.ToString(@"HH:mm", CultureInfo.InvariantCulture));
            return replacements;
        }
    }
}
