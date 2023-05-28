using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Raucse.FileManagement
{
    public static class FileUtils
    {
        /// <summary>
        /// Gets the directory of the executable
        /// </summary>
        public static string ApplicationDirectoryPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Gets or or creates a file if it does not exist, and writes all the text to it.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public static void WriteToFile(string path, string text)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Dispose();
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// Reads from a file, if it exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Option<string> ReadFromFile(string path)
        {
            if (File.Exists(path))
                return File.ReadAllText(path);

            return new Option<string>();
        }

        /// <summary>
        /// Returns all files in the specified directory, and all sub directories
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllFiles(string dir)
        {
            foreach (string path in Directory.GetFiles(dir))
                yield return path;

            foreach (string subdir in Directory.GetDirectories(dir))
            {
                foreach (string path in GetAllFiles(subdir))
                    yield return path;
            }
        }

        /// <summary>
        /// Returns the directory, and all subdirectories of a given path
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllDirectories(string dir)
        {
            yield return dir;
            foreach (string subdir in Directory.GetDirectories(dir))
            {
                foreach (string d in GetAllDirectories(subdir))
                    yield return d;
            }
        }
    }
}
