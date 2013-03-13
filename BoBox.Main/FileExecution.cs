using BoBox.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoBox.Main
{

    public class Console : IConsole
    {
        private TextBox console;
        // Snad nemusim textbox zamykat
        private System.Object consoleLock = new System.Object();

        public Console(TextBox console)
        {
            this.console = console;
        }
        public void Log(string line)
        {
            // VM?
            lock (consoleLock)
            {
                console.Text += line;
            }
        }
    }

    public class FileExecution
    {
        public IConsole Console { get; set; }

        public FileExecution()
        {            
        }

        public void Run(string executable, string args = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.Arguments = args;
            startInfo.FileName = executable;
               
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += process_OutputDataReceived;
            process.Exited += process_Exited;            
            Console.Log(string.Format("Starting {0}", executable));
            process.Start();            
            process.BeginOutputReadLine();           
            
            //Console.Log(e);
        }

        /// <summary>
        /// NEFUNGUJE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void process_Exited(object sender, EventArgs e)
        {
            var process = sender as Process;            
            Console.Log(string.Format("Process exit with {0} in {1} ms", process.ExitCode, process.TotalProcessorTime.Milliseconds));
            process.CancelOutputRead();
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {            
            Console.Log("=" + e.Data);
        }
    }
}
