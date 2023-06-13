using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class CharExtensions
    {
        /// <summary>
        /// Checks to see if a charactor is a digit: 0..9
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        /// <summary>
        /// Checks to see if a charactor is a letter or '_'
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsAlpha(this char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        /// <summary>
        /// Checks to see if a charactor is a letter: a..z or A..Z
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }

        /// <summary>
        /// Checks to see if a charactor is a letter, number, or '_'
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this char c)
        {
            return c.IsAlpha() || c.IsDigit();
        }

        /// <summary>
        /// Checks to see if a charactor is whitespace
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this char c)
        {
            return char.IsWhiteSpace(c);
        }

        /// <summary>
        /// Checks to see if a charactor is upper case: A..Z
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsUpperCase(this char c)
        {
            return char.IsUpper(c);
        }

        /// <summary>
        /// Checks to see if a charactor is lower case: a..z
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsLowerCase(this char c)
        {
            return char.IsLower(c);
        }

        /// <summary>
        /// Checks to see if the charactor is one of the chars
        /// </summary>
        /// <param name="c"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static bool IsChar(this char c, params char[] chars)
        {
            return chars.Contains(c);
        }
    }
}
