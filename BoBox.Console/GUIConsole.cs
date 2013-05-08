using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BoBox.Utils;

namespace BoBox.Console
{
    public class GUIConsole : NotifyProperty, IConsole
    {
        private StringBuilder logs_ = new StringBuilder();

        public string Logs { get { return logs_.ToString(); } set {} }

        public void Log(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                logs_.AppendLine(string.Format("[{0}] [Log] {1}", System.Threading.Thread.CurrentThread.ManagedThreadId, msg));
                RaisePropertyChanged("Logs");
            }
        }

        public void Error(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                logs_.AppendLine(string.Format("[{0}] [Error] {1}", System.Threading.Thread.CurrentThread.ManagedThreadId, msg));
                RaisePropertyChanged("Logs");
            }
        }
    }
}
