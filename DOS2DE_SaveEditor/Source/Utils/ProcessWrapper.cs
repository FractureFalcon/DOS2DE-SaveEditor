using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS2DE_SaveEditor.Source.Utils
{
    public struct ExecuteCommandArgs
    {
        const int TenMinutesInMs = 600000;
        
        public List<string> Commands;
        public string WorkingDirectory;
        public int TimeoutInMs;

        public ExecuteCommandArgs(List<string> commands, string workingDirectory = null, int timeoutInMs = TenMinutesInMs)
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

    class ProcessWrapper
    {
        public static Process ExecuteCommand(ExecuteCommandArgs args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = args.WorkingDirectory;
            cmd.Start();

            cmd.StandardInput.AutoFlush = true;
            foreach (string command in args.Commands)
            {
                cmd.StandardInput.WriteLine(command);
            }
            cmd.StandardInput.Close();
            cmd.WaitForExit(10 * 60 * 1000);
            return cmd;
        }
    }
}
