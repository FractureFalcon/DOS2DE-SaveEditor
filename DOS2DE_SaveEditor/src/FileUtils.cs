using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS2DE_SaveEditor.src
{
    public static class FileUtils
    {
        public static bool IsLsv(string path)
        {
            try
            {
                string extension = Path.GetExtension(path);
                if (string.Equals(extension.ToLower(), ".lsv"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static string GetFullPathIfPathRooted(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                return Path.GetFullPath(path);
            }

            return path;
        }
    }
}
