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
            FileInfo file = new FileInfo(path);
            file.Write(text);
        }

        /// <summary>
        /// Gets or or creates a file if it does not exist, and reads all the text from it
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFromFile(string path)
        {
            FileInfo info = new FileInfo(path);
            return info.Read();
        }
    }
}
