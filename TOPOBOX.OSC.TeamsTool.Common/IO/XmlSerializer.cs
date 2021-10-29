using GEOBOX.OSC.Common.Logging;
using System;
using System.IO;

namespace TOPOBOX.OSC.TeamsTool.Common.IO
{
    /// <summary>
    /// XmlSerializer serialize and deserialize ExportConvertRule to text file
    /// </summary>
    public static class XmlSerializer
    {
        /// <summary>
        /// Serializes the Object to xml and saves it to a file
        /// </summary>
        /// <param name="path">path to save the file</param>
        /// <param name="objectToSerialize">object to serialize as xml</param>
        /// <param name="logger">ILogger</param>
        /// <returns></returns>
        public static bool WriteXml<T>(string path, T objectToSerialize, ILogger logger)
        {
            try
            {
                logger?.WriteInformation(string.Format(Properties.Resources.CreateFileMessage, path));
                Stream stream = new FileStream(path, FileMode.Create);
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                xmlSerializer.Serialize(stream, objectToSerialize);

                stream.Flush();
                stream.Close();
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
        ///  object of T is the target for read the xml data
        /// </summary>
        /// <param name="path">filepath and filename for read</param>
        /// <param name="logger">ILogger</param>
        /// <returns>object of T or null</returns>
        public static T ReadXml<T>(string path, ILogger logger)
        {
            Stream stream = new FileStream(path, FileMode.Open);
            try
            {
                logger?.WriteInformation(string.Format(Properties.Resources.ReadFromFileMessage, path));
                System.Xml.Serialization.XmlSerializer xmlSerializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));

                T deserializedObject = (T)xmlSerializer.Deserialize(stream);

                logger?.WriteInformation(string.Format(Properties.Resources.ReadFromFileSuccessMessage, path));
                return deserializedObject;
            }
            catch (Exception ex)
            {
                logger?.WriteError(
                    string.Format(Properties.Resources.ReadFromFileErrorMessage, path) + $" {ex.Message}");
                return Activator.CreateInstance<T>();
            }
            finally
            {
                stream.Flush();
                stream.Close();
            }
        }
    }
}
