using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS2DE_SaveEditor.src
{
    public struct SaveGame
    {
        public string WorkspaceFolderLocation;
        public string SaveName;

        public SaveGame(string workspaceFolderLocation, string saveName)
        {
            WorkspaceFolderLocation = workspaceFolderLocation;
            SaveName = saveName;
        }
    }
}
