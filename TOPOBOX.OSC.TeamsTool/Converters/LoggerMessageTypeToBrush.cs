using System.Windows.Data;

namespace TOPOBOX.OSC.TeamsTool.Converters
{
    internal sealed class LoggerMessageTypeToBrush : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string message = (string)value;

            if (message.Contains("ERR"))
            {
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }

            if (message.Contains("WRN"))
            {
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
            }

            return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
        }


        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Keine ConvertBack notwendig
            return false;
        }
    }
}
