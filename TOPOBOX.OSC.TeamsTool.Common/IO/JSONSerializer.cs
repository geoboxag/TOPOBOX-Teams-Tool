using Newtonsoft.Json;
using System;
using System.IO;

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
        /// <returns></returns>
        // ToDO Logger
        //public static bool WriteJson(string path, object objectToSerialize, ILogger logger = null)
        public static bool WriteJson(string path, object objectToSerialize)
        {
            try
            {
                string jsonExportContent = JsonConvert.SerializeObject(objectToSerialize);
                File.WriteAllText(path, jsonExportContent);
                return true;
            }
            catch (Exception ex)
            {
                //logger?.WriteError($"Error writing json to file [{path}]: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        ///  Read a file and deserializes this content to object
        ///  object of T is the target for read the json data
        /// </summary>
        /// <param name="path">filepath and filename for read</param>
        /// <returns>object of T or null</returns>
        // ToDo Logger
        //public static T ReadJson<T>(string path, ILogger logger = null)
        public static T ReadJson<T>(string path)
        {
            try
            {
                var jsonExportConvertRule = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(jsonExportConvertRule);
            }
            catch (Exception ex)
            {
                //logger?.WriteError($"Error reading json from file [{path}]: {ex.Message}");
                return Activator.CreateInstance<T>();
            }
        }
    }
}
