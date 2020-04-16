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

        private const string GlobalsCompressedFile = "globals.lsf";
        private const string GlobalsDecompressedFile = "globals.lsx";

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

            string exportToolExtractSaveArgs = $"-s \"{fullFileName}\" -d \"{outputFolder}\" -a extract-package";
            string exportToolDecompressContentsArgs = $"-s \"{outputFolder}\" -d \"{outputFolder}\" -a convert-resources -i lsf -o lsx";

            string exportToolExtractSaveCommand = $".\\{ExportTool} {exportToolExtractSaveArgs}";
            string exportToolDecompressContentsCommand = $".\\{ExportTool} {exportToolDecompressContentsArgs}";

            ExecuteCommandsAsyncArgs commandArgs = new ExecuteCommandsAsyncArgs(new List<string> {
                    exportToolExtractSaveCommand,
                    exportToolDecompressContentsCommand
                }, null);

            Process cmd = ProcessWrapper.ExecuteCommands(commandArgs);

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

            string duplicateSaveFolder = $"{saveGame.WorkspaceFolderLocation}_TEMP";
            string outputFilePath = FileUtils.UseDefaultPathIfNotRooted(outputLocation, WorkspaceFolder);

            if (!FileUtils.IsLsv(outputLocation))
            {
                string outputFileName = Path.GetFileName(saveFolder);
                outputFilePath = Path.Combine(outputFilePath, $"{outputFileName}.lsv");
            }

            Console.WriteLine($"[INFO] Compressing {saveFolder} to {outputFilePath}...");

            string duplicateSaveFolderCommand = $"xcopy /s /i \"{saveFolder}\" \"{duplicateSaveFolder}\"";

            string exportToolCompressContentsArgs = $"-s \"{duplicateSaveFolder}\" -d \"{duplicateSaveFolder}\" -a convert-resources -i lsx -o lsf";
            string exportToolCreateSaveLsvArgs = $"-s \"{duplicateSaveFolder}\" -d \"{outputFilePath}\" -a create-package";

            string exportToolCompressContentsCommand = $".\\{ExportTool} {exportToolCompressContentsArgs}";
            string lsxFilesGlob = Path.Combine(duplicateSaveFolder, "*.lsx");
            string wipeLsxFilesCommand = $"del /s /q \"{lsxFilesGlob}\"";
            string exportToolCreateSaveLsvCommand = $".\\{ExportTool} {exportToolCreateSaveLsvArgs}";

            ExecuteCommandsAsyncArgs commandArgs = new ExecuteCommandsAsyncArgs(new List<string> {
                    duplicateSaveFolderCommand,
                    exportToolCompressContentsCommand,
                    wipeLsxFilesCommand,
                    exportToolCreateSaveLsvCommand
                }, null);

            ProcessWrapper.ExecuteCommands(commandArgs);
        }
    }
}
