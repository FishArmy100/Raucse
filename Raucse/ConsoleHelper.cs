using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
    public static class ConsoleHelper
    {
        public const ConsoleColor ERROR_COLOR = ConsoleColor.Red;
        public const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor MESSAGE_COLOR = ConsoleColor.Green;

        public static void WriteError(string message)
        {
            WriteLine(message, ERROR_COLOR);
        }

        public static void WriteWarning(string message)
        {
            WriteLine(message, WARNING_COLOR);
        }

        public static void WriteMessage(string message)
        {
            WriteLine(message, MESSAGE_COLOR);
        }

        public static void WriteLine(string message, ConsoleColor forground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            Write(message + "\n", forground, background);
        }

        public static void Write(string message, ConsoleColor forground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor currentForground = Console.ForegroundColor;
            ConsoleColor currentBackground = Console.BackgroundColor;
            Console.ForegroundColor = forground;
            Console.BackgroundColor = background;

            Console.Write(message);

            Console.ForegroundColor = currentForground;
            Console.BackgroundColor = currentBackground;
        }
    }
}
