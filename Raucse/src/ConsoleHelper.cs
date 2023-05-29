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

        /// <summary>
        /// Writes a message with the ERROR_COLOR
        /// </summary>
        /// <param name="message"></param>
        public static void WriteError(string message)
        {
            WriteLine(message, ERROR_COLOR);
        }

        /// <summary>
        /// Writes a message with the WARNING_COLOR
        /// </summary>
        /// <param name="message"></param>
        public static void WriteWarning(string message)
        {
            WriteLine(message, WARNING_COLOR);
        }

        /// <summary>
        /// Writes a message with the MESSAGE_COLOR
        /// </summary>
        /// <param name="message"></param>
        public static void WriteMessage(string message)
        {
            WriteLine(message, MESSAGE_COLOR);
        }

        /// <summary>
        /// Writes a line to the console with a optional forground and background colors
        /// </summary>
        /// <param name="message"></param>
        /// <param name="forground"></param>
        /// <param name="background"></param>
        public static void WriteLine(string message, ConsoleColor forground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            Write(message + "\n", forground, background);
        }

        /// <summary>
        /// Writes to teh console with a optional forground and background colors
        /// </summary>
        /// <param name="message"></param>
        /// <param name="forground"></param>
        /// <param name="background"></param>
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
