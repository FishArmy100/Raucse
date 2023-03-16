using System;
using Raucse;

namespace RaucseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Pair<int, int> pair = (5, 6);
            (int, int) val = pair;
            Console.WriteLine(pair.Second);
        }
    }
}
