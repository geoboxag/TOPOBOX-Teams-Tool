using System.Text;

namespace TOPOBOX.OSC.TeamsTool.Controller
{
    public class MessageBuilder
    {
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
