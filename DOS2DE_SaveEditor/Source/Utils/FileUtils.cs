using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS2DE_SaveEditor.Source.Utils
{
    public static class FileUtils
    {
        public static bool IsLsv(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

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

        public static string UseDefaultPathIfNotRooted(string path, string defaultPathIfNotRooted = "")
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return defaultPathIfNotRooted;
            }

            path = path.Replace("\"", string.Empty);

            if (!Path.IsPathRooted(path))
            {
                return Path.Combine(defaultPathIfNotRooted, path);
            }

            return path;
        }
    }
}
