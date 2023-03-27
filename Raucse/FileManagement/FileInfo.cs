using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Raucse.FileManagement
{
	class FileInfo
	{
		private readonly string m_Path;

		public string FullPath => m_Path;
		public string Name => Path.GetFileName(m_Path);
		public string Extension => Path.GetExtension(m_Path);

		public FileInfo(string path)
		{
			if (!File.Exists(path))
				throw new ArgumentException($"File at path {path} does not exist");

			m_Path = path;
		}

		public void WriteAllLines(string[] lines)
		{
			CheckPath();
			File.WriteAllLines(m_Path, lines);
		}

		public void Clear()
		{
			CheckPath();
			File.WriteAllText(m_Path, string.Empty);
		}

		public void Delete()
		{
			CheckPath();
			File.Delete(m_Path);
		}

		public static Option<FileInfo> GetOrCreate(string path)
		{
			if(!File.Exists(path))
			{
				FileStream stream = File.Create(path);
				stream.Dispose();
				return new FileInfo(path);
			}

			return new FileInfo(path);
		}

		private void CheckPath()
		{
			if (!File.Exists(m_Path))
				throw new ResourceDoesNotExistException();
		}
	}
}
