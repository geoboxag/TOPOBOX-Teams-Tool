using Newtonsoft.Json;
using System;
using System.IO;
using GEOBOX.OSC.Common.Logging;

namespace TOPOBOX.OSC.TeamsTool.Common.IO
{
    /// <summary>
    /// JsonSerializer serialize and deserialize ExportConvertRule to text file
    /// </summary>
    public static class JSONSerializer
    {
        /// <summary>
        /// Serializes the Object to json and saves it to a file
        /// </summary>
        /// <param name="path">path to save the file</param>
        /// <param name="objectToSerialize">object to serialize as json</param>
        /// <param name="logger">ILogger</param>
        /// <returns></returns>
        public static bool WriteJson(string path, object objectToSerialize, ILogger logger)
        {
            try
            {
                logger?.WriteInformation(string.Format(Properties.Resources.CreateFileMessage, path));
                string jsonExportContent = JsonConvert.SerializeObject(objectToSerialize);
                File.WriteAllText(path, jsonExportContent);

                logger?.WriteInformation(string.Format(Properties.Resources.CreatingFileSuccessMessage, path));
                return true;
            }
            catch (Exception ex)
            {
                logger?.WriteError(string.Format(Properties.Resources.CreatingFileErrorMessage, path) + $" {ex.Message}");
                return false;
            }
        }

        /// <summary>
        ///  Read a file and deserializes this content to object
        ///  object of T is the target for read the json data
        /// </summary>
        /// <param name="path">filepath and filename for read</param>
        /// <param name="logger">ILogger</param>
        /// <returns>object of T or null</returns>
        public static T ReadJson<T>(string path, ILogger logger)
        {
            try
            {
                logger?.WriteInformation(string.Format(Properties.Resources.ReadFromFileMessage, path));

                var jsonExportConvertRule = File.ReadAllText(path);
                var serializedContent = JsonConvert.DeserializeObject<T>(jsonExportConvertRule);

                logger?.WriteInformation(string.Format(Properties.Resources.ReadFromFileSuccessMessage, path));
                return serializedContent;
            }
            catch (Exception ex)
            {
                logger?.WriteError(
                    string.Format(Properties.Resources.ReadFromFileErrorMessage, path) + $" {ex.Message}");
                return Activator.CreateInstance<T>();
            }
        }
    }
}
