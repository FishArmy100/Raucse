using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Raucse.Experimental.Debugging
{
    public static class Debug
    {
        public static void Panic(string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "") => throw new PanicException(FormatMessage(message, lineNumber, filePath));

        public static void Assert(bool condition, string message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            if (!condition)
                Panic($"Assertion failed: {message}", lineNumber, filePath);
        }

        private static string FormatMessage(string message, int lineNumber, string filePath)
        {
            return $"[{filePath}:{lineNumber}]: \"{message}\"";
        }
    }
}
