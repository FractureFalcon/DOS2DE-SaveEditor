using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS2DE_SaveEditor.Source.Utils
{
    public struct ExecuteCommandsArgs
    {
        const int TenMinutesInMs = 600000;

        public List<string> Commands;
        public string WorkingDirectory;
        public int TimeoutInMs;

        public ExecuteCommandsArgs(List<string> commands, string workingDirectory = null, int timeoutInMs = TenMinutesInMs)
        {
            Commands = commands;

            WorkingDirectory = workingDirectory;

            if (string.IsNullOrWhiteSpace(workingDirectory))
            {
                WorkingDirectory = Directory.GetCurrentDirectory();
            }
            
            TimeoutInMs = timeoutInMs;
        }
    }

    public struct ExecuteCommandsAsyncArgs
    {
        const int OneMinuteInMs = 60000;
        const int TenMinutesInMs = 600000;

        public List<string> Commands;
        public string WorkingDirectory;
        public int TimeoutInMs;

        public EventHandler ExitedCallback;
        public DataReceivedEventHandler OutputDataCallback;
        public DataReceivedEventHandler ErrorDataCallback;

        public ExecuteCommandsAsyncArgs(List<string> commands, EventHandler exitedCallback, 
            string workingDirectory = null, int timeoutInMs = OneMinuteInMs, DataReceivedEventHandler outputDataCallback = null,
            DataReceivedEventHandler errorDataCallback = null)
        {
            Commands = commands;
            ExitedCallback = exitedCallback;

            if (ExitedCallback == null)
            {
                ExitedCallback = (sender, e) => (sender as Process)?.Close();
            }

            WorkingDirectory = workingDirectory;

            if (string.IsNullOrWhiteSpace(workingDirectory))
            {
                WorkingDirectory = Directory.GetCurrentDirectory();
            }

            TimeoutInMs = timeoutInMs;
            OutputDataCallback = outputDataCallback;
            ErrorDataCallback = errorDataCallback;

            if (OutputDataCallback == null)
            {
                OutputDataCallback = (sender, e) => Console.WriteLine(e.Data);
            }

            if (ErrorDataCallback == null)
            {
                OutputDataCallback = (sender, e) => Console.WriteLine(e.Data);
            }
        }
    }

    class ProcessWrapper
    {
        public static Process ExecuteCommands(ExecuteCommandsAsyncArgs args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = args.WorkingDirectory;

            cmd.EnableRaisingEvents = true;
            cmd.OutputDataReceived += args.OutputDataCallback;
            cmd.ErrorDataReceived += args.ErrorDataCallback;
            cmd.Exited += args.ExitedCallback;

            cmd.Start();

            cmd.BeginOutputReadLine();
            cmd.BeginErrorReadLine();

            cmd.StandardInput.AutoFlush = true;
            foreach (string command in args.Commands)
            {
                cmd.StandardInput.WriteLine(command);
            }
            cmd.StandardInput.Close();
            cmd.WaitForExit(args.TimeoutInMs);
            return cmd;
        }

        public static void ExecuteCommandsAsync(ExecuteCommandsAsyncArgs args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = args.WorkingDirectory;

            cmd.EnableRaisingEvents = true;
            cmd.OutputDataReceived += args.OutputDataCallback;
            cmd.ErrorDataReceived += args.ErrorDataCallback;
            cmd.Exited += args.ExitedCallback;

            cmd.Start();

            cmd.BeginOutputReadLine();
            cmd.BeginErrorReadLine();

            cmd.StandardInput.AutoFlush = true;
            foreach (string command in args.Commands)
            {
                cmd.StandardInput.WriteLine(command);
            }
            cmd.StandardInput.Close();
        }
    }
}
