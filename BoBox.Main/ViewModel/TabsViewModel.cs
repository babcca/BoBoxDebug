using BoBox.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoBox.Utils;
using BoBox.Console;

namespace BoBox.Main.ViewModel
{
    class MainViewModel : NotifyProperty
    {
        public IConsole Console { get; set; }

        public MainViewModel()
        {
            Console = new GUIConsole();
        }

        public ObservableCollection<BoBox.Main.Editor.BoBolangEditor> o = new ObservableCollection<Editor.BoBolangEditor>();
        
    }

    //class GUIConsol : NotifyProperty, IConsole
    //{
    //    private StringBuilder logs_ = new StringBuilder();

    //    public string Logs { get { return logs_.ToString(); } set { } }

    //    public void Log(string format, params object[] args)
    //    {
    //        var msg = string.Format("[Log] " + format, args);
    //        logs_.AppendLine(string.Format("[{0}] {1}", System.Threading.Thread.CurrentThread.ManagedThreadId, msg));
    //        RaisePropertyChanged("Logs");
    //    }

    //    public void Error(string format, params object[] args)
    //    {
    //        var msg = string.Format("[Error] " + format, args);
    //        logs_.AppendLine(string.Format("[{0}] {1}", System.Threading.Thread.CurrentThread.ManagedThreadId, msg));
    //        RaisePropertyChanged("Logs");
    //    }
    //}

    //class NotifyProperty : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected void RaisePropertyChanged(string propertyName)
    //    {
    //        // take a copy to prevent thread issues
    //        PropertyChangedEventHandler handler = PropertyChanged;
    //        if (handler != null)
    //        {
    //            handler(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //}
}
