using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Raucse.Diagnostics
{
    public static class Debug
    {
        /// <summary>
        /// Throws an PanicException with the given message
        /// </summary>
        /// <param name="message"></param>
        public static void Panic(string message) => throw new PanicException(message);

        /// <summary>
        /// If the condition is not true, panics with the message
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Assert(bool condition, string message)
        {
            if (!condition)
                Panic($"Assertion failed: {message}");
        }
    }
}
