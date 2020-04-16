using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor.Source.Main
{
    public class FormBindings
    {
        private SaveGame _currentSaveGame;

        public void RunNullSaveTest()
        {
            const string testSave = "C:\\Users\\K8\\Documents\\Larian Studios\\Divinity Original Sin 2 Definitive Edition\\PlayerProfiles\\Fracture\\Savegames\\Story\\Autosave_8_Testing\\Autosave_8_Testing.lsv";

            RunSaveTest(testSave);
        }

        public void RunSaveTest(string pathToSave, string pathToExport = "")
        {
            var exportToolWrapper = new ExportToolWrapper();
            SaveGame test = ExportToolWrapper.OpenSaveGameLSV(pathToSave);

            if (string.IsNullOrWhiteSpace(test.WorkspaceFolderLocation))
            {
                return;
            }

            ExportToolWrapper.CloseSaveGame(test, pathToExport);
        }

        public SaveGame ImportSaveGameLsv(string pathToSaveGameLsv)
        {
            _currentSaveGame = ExportToolWrapper.OpenSaveGameLSV(pathToSaveGameLsv);

            return _currentSaveGame;
        }

        public void ExportCurrentSaveGame(string outputPath)
        {
            ExportSaveGame(_currentSaveGame, outputPath);
        }

        private void ExportSaveGame(SaveGame saveGame, string outputPath)
        {
            if (saveGame.LoadedSuccessfully())
            {
                ExportToolWrapper.CloseSaveGame(saveGame, outputPath);
            }
        }
    }
}
