using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Strings
{
    /// <summary>
    /// A class that can visit through a string, and has helper methods to check the current charactor, advance through the string
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    public class StringReader<TError>
    {
        /// <summary>
        /// The passed source string
        /// </summary>
        public readonly string Src;
        private readonly Func<StringReaderLocation, string, Option<string>, TError> m_ConsumeErrorFunc;
        /// <summary>
        /// The current index
        /// </summary>
        public int Index { get; private set; } = 0;
        /// <summary>
        /// The current line
        /// </summary>
        /// <remarks>
        /// Starts at 0
        /// </remarks>
        public int Line { get; private set; } = 0;
        /// <summary>
        /// The current column (ie: index of the line)
        /// </summary>
        /// <remarks>
        /// Starts at 0
        /// </remarks>
        public int Column { get; private set; } = 0;

        /// <summary>
        /// The current location of the string reader
        /// </summary>
        public StringReaderLocation CurrentLocation => new StringReaderLocation(Line, Column, Index);

        public StringReader(string src, Func<StringReaderLocation, string, Option<string>, TError> errorFunc)
        {
            Src = src;
            m_ConsumeErrorFunc = errorFunc;
        }

        /// <summary>
        /// The current charactor of the reader
        /// </summary>
        /// <returns></returns>
        public char Current()
        {
            if (IsAtEnd())
                throw new InvalidOperationException("Cannot access the current charactor, because the reader has reached the end of the string.");

            return Src[Index];
        }

        /// <summary>
        /// Will advance through the string. If the reader is already at the end, will throw an InvalidOperatorExeption
        /// </summary>
        /// <remarks>
        /// If the reader is on the last charactor, you can advance past it
        /// </remarks>
        /// <returns></returns>
        public char Advance()
        {
            if (IsAtEnd())
                throw new InvalidOperationException("Cannot advance passed the end of the string.");

            char c = Current();
            Column++;
            if (c == '\n')
            {
                Line++;
                Column = 0;
            }
            Index++;
            return c;

        }

        /// <summary>
        /// Will return the previous charactor
        /// </summary>
        /// <returns></returns>
        public char Previous()
        {
            if (Index == 0)
                throw new InvalidOperationException("Cannot get the previous charactor from the beginning of a string.");

            return Src[Index - 1];
        }

        /// <summary>
        /// Can be used to peek forwards, or backwards from the current charactor
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public char? Peek(int offset = 1) => Index + offset < Src.Length ? Src[Index + offset] : null;

        /// <summary>
        /// If the current charactor is equal to one of the given charactors, returns true and advances, otherwise, returns false
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool Match(params char[] cs)
        {
            if (IsAtEnd())
                return false;

            if (cs.Contains(Current()))
            {
                Advance();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Starting at the current charactor, if it and the following chractors equal the string, returns true and advances, else returns false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckString(string str)
        {
            return CheckSequence(str.ToArray());
        }

        /// <summary>
        /// Starting at the current charactor, if it and the following chractors equal the array of charactors, returns true and advances, else returns false
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool CheckSequence(params char[] cs)
        {
            for (int i = 0; i < cs.Length; i++)
            {
                if (Peek(i) is char c)
                {
                    if (c != cs[i])
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if the reader has advanced past the last chractor
        /// </summary>
        /// <returns></returns>
        public bool IsAtEnd() { return Index >= Src.Length; }

        /// <summary>
        /// If the current charactor equals the given charactor, returns the charactor and advances, else returns an error generated by the given generator funciton
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Result<char, TError> Consume(char c)
        {
            if (IsAtEnd())
                return m_ConsumeErrorFunc(CurrentLocation, c.ToString(), null);

            if (!Match(c))
                return m_ConsumeErrorFunc(CurrentLocation, c.ToString(), Current().ToString());

            return Previous();
        }

        /// <summary>
        /// tarting at the current charactor, if it and the following chractors equal the string, returns the string and advances, else returns an error generated by the given error generator
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Result<string, TError> ConsumeString(string str)
        {
            if (IsAtEnd() || Index + str.Length >= Src.Length)
                return m_ConsumeErrorFunc(CurrentLocation, str, null);

            if (!CheckString(str))
                return m_ConsumeErrorFunc(CurrentLocation, str, Src[Index..(Index + str.Length)]);

            string consumed = str[Index..(Index + str.Length)];
            Index += str.Length;
            return consumed;
        }
    }

    public class StringReader
    {
        private readonly StringReader<Exception> m_Reader;
        /// <summary>
        /// The passed source string
        /// </summary>
        public string Src => m_Reader.Src;
        /// <summary>
        /// The current index
        /// </summary>
        public int Index => m_Reader.Index;
        /// <summary>
        /// The current line
        /// </summary>
        /// <remarks>
        /// Starts at 0
        /// </remarks>
        public int Line => m_Reader.Line;
        /// <summary>
        /// The current column (ie: index of the line)
        /// </summary>
        /// <remarks>
        /// Starts at 0
        /// </remarks>
        public int Column => m_Reader.Column;

        /// <summary>
        /// The current location of the string reader
        /// </summary>
        public StringReaderLocation CurrentLocation => m_Reader.CurrentLocation;
        public StringReader(string src)
        {
            m_Reader = new StringReader<Exception>(src, ErrorFunc);
        }
        /// <summary>
        /// The current charactor of the reader
        /// </summary>
        /// <returns></returns>
        public char Current() => m_Reader.Current();
        /// <summary>
        /// Will advance through the string. If the reader is already at the end, will throw an InvalidOperatorExeption
        /// </summary>
        /// <remarks>
        /// If the reader is on the last charactor, you can advance past it
        /// </remarks>
        /// <returns></returns>
        public char Advance() => m_Reader.Advance();
        /// <summary>
        /// Will return the previous charactor
        /// </summary>
        /// <returns></returns>
        public char Previous() => m_Reader.Previous();
        /// <summary>
        /// Can be used to peek forwards, or backwards from the current charactor
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public char? Peek(int offset = 1) => m_Reader.Peek(offset);
        /// <summary>
        /// If the current charactor is equal to one of the given charactors, returns true and advances, otherwise, returns false
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool Match(params char[] cs) => m_Reader.Match(cs);
        /// <summary>
        /// Starting at the current charactor, if it and the following chractors equal the string, returns true and advances, else returns false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckString(string str) => m_Reader.CheckString(str);
        /// <summary>
        /// Starting at the current charactor, if it and the following chractors equal the array of charactors, returns true and advances, else returns false
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool CheckSequence(params char[] cs) => m_Reader.CheckSequence(cs);
        /// <summary>
        /// Returns true if the reader has advanced past the last chractor
        /// </summary>
        /// <returns></returns>
        public bool IsAtEnd() => m_Reader.IsAtEnd();
        /// <summary>
        /// If the current charactor equals the given charactor, returns the charactor and advances, else throws an InvalidOperationException
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public char Consume(char c) => m_Reader.Consume(c).MatchOrThrow(ok => ok);
        /// <summary>
        /// tarting at the current charactor, if it and the following chractors equal the string, returns the string and advances, else throws an InvalidOperationException
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ConsumeString(string str) => m_Reader.ConsumeString(str).MatchOrThrow(ok => ok);

        private static Exception ErrorFunc(StringReaderLocation location, string expected, Option<string> got)
        {
            if(expected.Length == 1)
            {
                return got.Match(
                    ok =>
                    {
                        return new InvalidOperationException($"Expected the charactor '{expected[0]}', but got '{ok[0]}'.");
                    },
                    () =>
                    {
                        return new InvalidOperationException("Cannot consume past the end of the string.");
                    });
            }
            else
            {
                return got.Match(
                    ok =>
                    {
                        return new InvalidOperationException($"Expected the string '{expected}', but got '{ok}'.");
                    },
                    () =>
                    {
                        return new InvalidOperationException($"Expected string advanced past the end of the source string.");
                    });
            }
        }
    }

}
