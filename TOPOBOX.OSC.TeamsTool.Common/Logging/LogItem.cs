using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TOPOBOX.OSC.TeamsTool.Common.Logging
{
    public class LogItem : INotifyPropertyChanged
    {
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private DateTime logDateTime = DateTime.Now;
        public DateTime LogDateTime
        {
            get
            {
                return logDateTime;
            }
            set
            {
                logDateTime = value;
                OnPropertyChanged(nameof(LogDateTime));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
