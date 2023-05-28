using System;
using Raucse;
using Raucse.Extensions;
using Raucse.FileManagement;

namespace RaucseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FileUtils.GetAllDirectories("C:\\dev\\Raucse\\Raucse").Concat("\n"));
        }
    }
}
