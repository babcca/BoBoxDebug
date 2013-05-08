using BoBox.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public void Log(string format, params object[] args)
        {
            string msg = string.Format("[Log] " + format, args);
            lock (consoleLock)
            {
                console.Text += msg;
            }
        }

        public void Error(string format, params object[] args)
        {
            string msg = string.Format("[Error] " + format, args);
            lock (consoleLock)
            {
                console.Text += msg;
            }
        }
    }

    public class FileExecution
    {
        public IConsole Console { get; set; }

        public FileExecution()
        {
        }

        public void Run(string executable, string args)
        {
            if (File.Exists(executable))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.Arguments = args;
                startInfo.FileName = executable;

                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                startInfo.WorkingDirectory = Path.GetDirectoryName(executable);

                Process process = new Process();
                process.StartInfo = startInfo;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += process_OutputDataReceived;
                process.ErrorDataReceived += process_ErrorDataReceived;
                process.Exited += process_Exited;

                try
                {
                    Console.Log(string.Format("Starting {0} {1}", executable, args));
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                }
                catch (Exception ex)
                {
                    Console.Error("Process {0} not started", executable);

                }
            }
            else
            {
                Console.Error("File {0} not exists", executable);
            }
        }



        /// <summary>
        /// NEFUNGUJE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void process_Exited(object sender, EventArgs e)
        {
            var process = sender as Process;
            if (process.ExitCode != 0)
            {
                Console.Error("Process exited with {0}", process.ExitCode); 
            }
            else
            {
                Console.Log(string.Format("Process exit in {0} ms", process.TotalProcessorTime.Milliseconds));                
            }

            process.CancelOutputRead();
            process.CancelErrorRead();
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.Log("{0}", e.Data);
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.Error("{0}", e.Data);
        }
    }
}
