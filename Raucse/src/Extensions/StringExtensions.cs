using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Concatinates a enumerable of strings with a given seperator
        /// </summary>
        /// <param name="list"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> list, string seperator = " ")
        {
            string str = "";
            for (int i = 0; i < list.Count(); i++)
            {
                if (i != 0)
                    str += seperator;

                str += list.ElementAt(i);
            }

            return str;
        }

        /// <summary>
        /// Sets the first char of the string to lower case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstCharToLowerCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        /// <summary>
        /// Removes all whitespace of a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Parses a string to a short
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<short> ToShort(this string str)
        {
            if (short.TryParse(str, out short result))
                return result;

            return new Option<short>();
        }

        /// <summary>
        /// Parses a string to an int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<int> ToInt(this string str)
        {
            if (int.TryParse(str, out int result))
                return result;

            return new Option<int>();
        }

        /// <summary>
        /// Parses a string to a long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<long> ToLong(this string str)
        {
            if (long.TryParse(str, out long result))
                return result;

            return new Option<long>();
        }

        /// <summary>
        /// Parses a string to a ushort
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<ushort> ToUShort(this string str)
        {
            if (ushort.TryParse(str, out ushort result))
                return result;

            return new Option<ushort>();
        }

        /// <summary>
        /// Parses a string to a uint
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<uint> ToUInt(this string str)
        {
            if (uint.TryParse(str, out uint result))
                return result;

            return new Option<uint>();
        }

        /// <summary>
        /// Parses a string to a ulong
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<ulong> ToULong(this string str)
        {
            if (ulong.TryParse(str, out ulong result))
                return result;

            return new Option<ulong>();
        }

        /// <summary>
        /// Parses a string to a float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<float> ToFloat(this string str)
        {
            if (float.TryParse(str, out float result))
                return result;

            return new Option<float>();
        }

        /// <summary>
        /// Parses a string to a double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<double> ToDouble(this string str)
        {
            if (double.TryParse(str, out double result))
                return result;

            return new Option<double>();
        }

        /// <summary>
        /// Parses a string to a decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Option<decimal> ToDecimal(this string str)
        {
            if (decimal.TryParse(str, out decimal result))
                return result;

            return new Option<decimal>();
        }

        /// <summary>
        /// Repeats a string n number of times.
        /// From: https://www.codingame.com/playgrounds/5113/implementing-repeat-method-for-strings-in-c
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Repeat(this string instr, int n)
        {
            if (n <= 0)
            {
                return null;
            }

            if (string.IsNullOrEmpty(instr) || n == 1)
            {
                return instr;
            }

            return new StringBuilder(instr.Length * n)
                                    .Insert(0, instr, n)
                                    .ToString();
        }

        /// <summary>
        /// Surrounds a string with charactors of the given StringSurroundMode
        /// </summary>
        /// <param name="str"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string Surround(this string str, StringSurroundMode mode)
        {
            return mode switch
            {
                StringSurroundMode.Braces => "{" + str + "}",
                StringSurroundMode.Brackets => "[" + str + "]",
                StringSurroundMode.Parens => "(" + str + ")",
                StringSurroundMode.Angles => "<" + str + ">",
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Adds the surrounder string on each side to the source string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="surrounder"></param>
        /// <returns></returns>
        public static string Surround(this string str, string surrounder)
        {
            return surrounder + str + surrounder;
        }

        /// <summary>
        /// Checks to see if a string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    }
}
