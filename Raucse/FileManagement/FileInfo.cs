using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Raucse.FileManagement
{
    /// <summary>
    /// Represents a file on disk. When constructed, will automatically generate all subfolders and directories required and the file itself. If the file or directory is deleted, and functions are called on a FileInfo referencing that file, it will throw a FileNotFoundException.
    /// </summary>
    public class FileInfo
    {
        private readonly string m_Path;

        public FileInfo(string path)
        {
            _ = Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
                _ = File.Create(path);

            m_Path = path;
        }

        public string FullPath => CheckPath(m_Path);
        public string Name => Path.GetFileName(FullPath);
        public string Extension => Path.GetExtension(FullPath);
        public IEnumerable<string> Lines => Read().Split('\n');
        public FolderInfo ParentFolder => new FolderInfo(Path.GetDirectoryName(FullPath));


        public void Write(string text) => File.WriteAllText(FullPath, text);
        public string Read() => File.ReadAllText(FullPath);
        public void Append(string text) => File.AppendAllText(FullPath, text);
        public void Clear() => Write(string.Empty);
        public void Delete() => File.Delete(FullPath);

        public FileStream GetFileStream(FileMode mode) => new FileStream(FullPath, mode);
        public StreamWriter GetWriter() => new StreamWriter(FullPath);
        public StreamReader GetReader() => new StreamReader(FullPath);

        private static string CheckPath(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            return path;
        }

        public override bool Equals(object obj)
        {
            return obj is FileInfo info &&
                   m_Path == info.m_Path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_Path);
        }

        public static bool operator ==(FileInfo left, FileInfo right)
        {
            return EqualityComparer<FileInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(FileInfo left, FileInfo right)
        {
            return !(left == right);
        }
    }
}
