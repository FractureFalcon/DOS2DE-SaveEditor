using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor.src
{
    static class Program
    {
        static string testSave = "C:\\Users\\K8\\Documents\\Larian Studios\\Divinity Original Sin 2 Definitive Edition\\PlayerProfiles\\Fracture\\Savegames\\Story\\Autosave_8_Testing\\Autosave_8_Testing.lsv";

        [STAThread]
        static void Main()
        {
            // Open the save game
            var exportToolWrapper = new ExportToolWrapper();
            SaveGame test = exportToolWrapper.OpenSaveGameLSV(testSave);
            exportToolWrapper.CloseSaveGame(test);

            // IDK C# Visual shit?
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
