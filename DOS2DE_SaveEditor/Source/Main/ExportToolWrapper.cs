using DOS2DE_SaveEditor.Source.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor.Source.Main
{
    public class ExportToolWrapper
    {
        private const string ExportTool = "ExportTool-v1.14.1\\divine.exe";
        private static string WorkspaceFolder => Application.LocalUserAppDataPath;

        public static void InitializeWorkspace(string workspaceFolder)
        {
            workspaceFolder = FileUtils.UseDefaultPathIfNotRooted(workspaceFolder);

            if (Directory.Exists(workspaceFolder))
            {
                Directory.Delete(workspaceFolder, true);
            }

            Directory.CreateDirectory(workspaceFolder);
        }

        public static SaveGame OpenSaveGameLSV(string fullFileName)
        {
            fullFileName = FileUtils.UseDefaultPathIfNotRooted(fullFileName);

            if (!Path.IsPathRooted(fullFileName) && !File.Exists(fullFileName))
            {
                Console.WriteLine($"[ERROR] Save game LSV could not be found, input parameter was: \"{fullFileName}\"");
                return new SaveGame();
            }

            if (!FileUtils.IsLsv(fullFileName))
            {
                Console.WriteLine($"[ERROR] Inputted save game was not an LSV, it was: \"{fullFileName}\"");
                return new SaveGame();
            }

            string filename = Path.GetFileNameWithoutExtension(fullFileName);
            string outputFolder = Path.Combine(WorkspaceFolder, filename);

            Console.WriteLine($"[INFO] Extracting {fullFileName} to {outputFolder}...");
            string args = $"-s \"{fullFileName}\" -a extract-package -d \"{outputFolder}\"";

            string commandLineArgs = $".\\{ExportTool} {args}";
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

            return new SaveGame(outputFolder, Path.GetFileNameWithoutExtension(outputFolder));
        }

        public static void CloseSaveGame(SaveGame saveGame)
        {
            CloseSaveGame(saveGame, WorkspaceFolder);
        }

        public static void CloseSaveGame(SaveGame saveGame, string outputLocation)
        {
            string saveFolder = saveGame.WorkspaceFolderLocation;

            if (!Directory.Exists(saveGame.WorkspaceFolderLocation))
            {
                Console.WriteLine($"[ERROR] Save game folder could not be found, looking for: \"{saveFolder}\"");
                return;
            }

            string outputFilePath = FileUtils.UseDefaultPathIfNotRooted(outputLocation, WorkspaceFolder);

            if (!FileUtils.IsLsv(outputLocation))
            {
                string outputFileName = Path.GetFileName(saveFolder);
                outputFilePath = Path.Combine(outputFilePath, $"{outputFileName}.lsv");
            }

            Console.WriteLine($"[INFO] Compressing {saveFolder} to {outputFilePath}...");
            string args = $"-s \"{saveFolder}\" -a create-package -d \"{outputFilePath}\"";

            string commandLineArgs = $".\\{ExportTool} {args}";
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
