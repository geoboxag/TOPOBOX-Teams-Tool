using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TOPOBOX.OSC.TeamsTool.Common.Properties;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Class with default-values and functions to create a Html-File
    /// </summary>
    public class BaseHTMLGenerator
    {
        private const string DEFAULTPAGETITLEH1 = "General Report";

        private Dictionary<string /*replacement key*/, string /*replacement content*/> replacements;

        /// <summary>
        /// Construtcor
        /// </summary>
        /// <param name="pageTitle"></param>
        public BaseHTMLGenerator(string pageTitle)
        {
            CreateDefaultReplacements(pageTitle);
        }

        private void CreateDefaultReplacements(string pageTitle)
        {
            if (string.IsNullOrEmpty(pageTitle)) pageTitle = DEFAULTPAGETITLEH1;
            replacements = HtmlWriterReplacements.DefaultReplacements(pageTitle);
        }

        internal string GetHTMLfromReplacement()
        {
            if (!replacements.Any()) return string.Empty;

            var result = StringTemplateReplacer.Replace
                (
                    Resources.htmlTemplate,
                    replacements
                );

            return result;
        }

        internal void AddContentAsReplacement(string htmlContent)
        {
            replacements.Add("[CONTENT]", htmlContent);
        }

        internal Control GetH2Title(string title)
        {
            return new HtmlGenericControl("h2") { InnerText = title };
        }

        internal Control GetH3Title(string title)
        {
            return new HtmlGenericControl("h3") { InnerText = title };
        }

        internal Control GetParagraph(string content)
        {
            return new HtmlGenericControl("p") { InnerText = content };
        }

        internal Control GetParagraphAndDescription(string content, string description)
        {
            var p = GetParagraph(content);
            p.Controls.Add(GetSpan($" {description}", true));

            return p;
        }

        internal Control GetSpan(string text, bool isDescription = false)
        {
            var control = new HtmlGenericControl("span") { InnerText = text };

            if (isDescription) control.Attributes.Add("class", "desc");

            return control;
        }

        internal Control GetInformationsDIV(string titleH2)
        {
            var divInformation = new HtmlGenericControl("div");
            divInformation.Controls.Add(GetH2Title(titleH2));
            return divInformation;
        }

        internal Control GetLineBreack()
        {
            return new HtmlGenericControl("br");
        }
                
    }
}