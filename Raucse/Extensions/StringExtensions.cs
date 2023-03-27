using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class StringExtensions
    {
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

        public static string FirstCharToLowerCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        public static Option<int> ToInt(this string str)
        {
            if (int.TryParse(str, out int result))
                return result;

            return new Option<int>();
        }

        public static Option<long> ToLong(this string str)
        {
            if (long.TryParse(str, out long result))
                return result;

            return new Option<long>();
        }

        public static Option<float> ToFloat(this string str)
        {
            if (float.TryParse(str, out float result))
                return result;

            return new Option<float>();
        }

        public static Option<double> ToDouble(this string str)
        {
            if (double.TryParse(str, out double result))
                return result;

            return new Option<double>();
        }

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

        public static string Surround(this string str, string surrounder)
        {
            return surrounder + str + surrounder;
        }

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    }
}
