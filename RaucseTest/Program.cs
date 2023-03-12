using System;
using Raucse;

namespace RaucseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Option<string> test = "Hello World!";
            Console.WriteLine(test.Value);
        }
    }
}
