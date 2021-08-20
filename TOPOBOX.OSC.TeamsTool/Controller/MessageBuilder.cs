using System.Text;

namespace TOPOBOX.OSC.TeamsTool.Controller
{
    /// <summary>
    /// Specific functions to build messages, for ex. to send in a channel or chat
    /// </summary>
    public class MessageBuilder
    {
        /// <summary>
        /// Returns a phonenotice with the given parameters
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="company"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string CreatePhoneNoticeMessage(string caller, string company, string phoneNumber, string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Anrufer: <b>{caller}</b><br />");
            stringBuilder.Append($"Firma: {company}<br />");
            stringBuilder.Append($"Tel: {phoneNumber}<br />");
            stringBuilder.Append($"Notiz:<br />{message}");
            return stringBuilder.ToString();
        }
    }
}
