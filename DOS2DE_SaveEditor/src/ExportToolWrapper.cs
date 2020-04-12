using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor.src
{
    public class ExportToolWrapper
    {
        private const string exportTool = "ExportTool-v1.14.1\\divine.exe";
        private readonly string _workspaceFolder;

        public ExportToolWrapper()
        {
            InitializeWorkspace(Application.LocalUserAppDataPath);
            _workspaceFolder = Application.LocalUserAppDataPath;
        }

        public ExportToolWrapper(string workspaceFolder)
        {
            InitializeWorkspace(workspaceFolder);
            _workspaceFolder = workspaceFolder;
        }

        private void InitializeWorkspace(string workspaceFolder)
        {
            workspaceFolder = FileUtils.GetFullPathIfPathRooted(workspaceFolder);

            if (Directory.Exists(workspaceFolder))
            {
                Directory.Delete(workspaceFolder, true);
            }

            Directory.CreateDirectory(workspaceFolder);
        }

        public SaveGame OpenSaveGameLSV(string fullFileName)
        {
            fullFileName = FileUtils.GetFullPathIfPathRooted(fullFileName);

            if (!File.Exists(fullFileName))
            {
                Console.WriteLine($"[ERROR] Save game LSV could not be found, input parameter was: \"{fullFileName}\"");
                return new SaveGame();
            }

            string filename = Path.GetFileNameWithoutExtension(fullFileName);
            string outputFolder = Path.Combine(_workspaceFolder, filename);

            Console.WriteLine($"[INFO] Extracting {fullFileName} to {outputFolder}...");
            string args = $"-s \"{fullFileName}\" -a extract-package -d \"{outputFolder}\"";

            string commandLineArgs = $".\\{exportTool} {args}";
            Console.WriteLine($"[DEBUG] Using args {commandLineArgs}");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            cmd.Start();

            //cmd.StandardInput.WriteLine("cd");
            cmd.StandardInput.WriteLine(commandLineArgs);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit(10 * 60 * 1000);
            Console.WriteLine($"[DEBUG] Console output:\n{cmd.StandardOutput.ReadToEnd()}");

            return new SaveGame(outputFolder);
        }

        public void CloseSaveGame(SaveGame saveGame)
        {
            CloseSaveGame(saveGame, _workspaceFolder);
        }


        public void CloseSaveGame(SaveGame saveGame, string outputLocation)
        {
            string saveFolder = saveGame.WorkspaceFolderLocation;

            if (!Directory.Exists(saveGame.WorkspaceFolderLocation))
            {
                Console.WriteLine($"[ERROR] Save game folder could not be found, looking for: \"{saveFolder}\"");
                return;
            }

            string outputFilePath = FileUtils.GetFullPathIfPathRooted(outputLocation);

            if (!FileUtils.IsLsv(outputLocation))
            {
                string outputFileName = Path.GetFileName(saveFolder);
                outputFilePath = Path.Combine(outputFilePath, $"{outputFileName}.lsv");
            }

            Console.WriteLine($"[INFO] Compressing {saveFolder} to {outputFilePath}...");
            string args = $"-s \"{saveFolder}\" -a create-package -d \"{outputFilePath}\"";

            string commandLineArgs = $".\\{exportTool} {args}";
            Console.WriteLine($"[DEBUG] Using args {commandLineArgs}");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            cmd.Start();

            //cmd.StandardInput.WriteLine("cd");
            cmd.StandardInput.WriteLine(commandLineArgs);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine($"[DEBUG] Console output:\n{cmd.StandardOutput.ReadToEnd()}");
        }
    }
}
