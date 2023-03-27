using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Raucse.FileManagement
{
    public class FolderInfo
    {
        private readonly string m_Path;

        public FolderInfo(string path)
        {
            _ = Directory.CreateDirectory(path);
            m_Path = path;
        }

        public string FullPath => CheckPath(m_Path);
        public string Name => new DirectoryInfo(FullPath).Name;

        public IEnumerable<FileInfo> Files => Directory.GetFiles(FullPath).Select(p => new FileInfo(p));
        public IEnumerable<FolderInfo> SubFolders => Directory.GetDirectories(FullPath).Select(p => new FolderInfo(p));
        public void Delete() => Directory.Delete(FullPath);

        public FileInfo AddFile(string name)
        {
            string path = $"{FullPath}\\{name}";
            return new FileInfo(path);
        }

        public FolderInfo AddFolder(string name)
        {
            string path = $"{FullPath}\\{name}";
            return new FolderInfo(path);
        }

        private static string CheckPath(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            return path;
        }

        public override bool Equals(object obj)
        {
            return obj is FolderInfo info &&
                   m_Path == info.m_Path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_Path);
        }

        public static bool operator ==(FolderInfo left, FolderInfo right)
        {
            return EqualityComparer<FolderInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(FolderInfo left, FolderInfo right)
        {
            return !(left == right);
        }
    }
}
