using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOS2DE_SaveEditor.Source.Main
{
    public struct SaveGame
    {
        public string WorkspaceFolderLocation;
        public string SaveName;
        public string XmlContents;

        public SaveGame(string workspaceFolderLocation, string saveName)
        {
            WorkspaceFolderLocation = workspaceFolderLocation;
            SaveName = saveName;

            const string globalsFileName = "globals.lsf";
            XmlContents = $"{globalsFileName} contents go here.";
        }

        public bool LoadedSuccessfully()
        {
            return !string.IsNullOrWhiteSpace(WorkspaceFolderLocation)
                && !string.IsNullOrWhiteSpace(SaveName)
                && !string.IsNullOrWhiteSpace(XmlContents);
        }

        public void LoadContentsToTreeView(TreeView view)
        {
            
        }
    }
}
