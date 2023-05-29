using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raucse.Extensions;

namespace Raucse.Strings
{
    public class StringMaker
    {
        public readonly StringBuilder Builder;
        private readonly string m_Tab;

        private Stack<Pair<Option<Func<int, string>>, int>> m_TabModes = new Stack<Pair<Option<Func<int, string>>, int>>();
        private bool m_LineStarted = false;

        /// <summary>
        /// The number of lines that have been appended
        /// </summary>
        public int LineCount { get; private set; } = 1;

        /// <summary>
        /// The current tab count
        /// </summary>
        public int Tabs => m_TabModes.Count - 1;

        /// <summary>
        /// Creates a string maker with a string builder, and a tab mode
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="tab"></param>
        public StringMaker(StringBuilder builder, string tab = "\t")
        {
            Builder = builder;
            m_Tab = tab;
            m_TabModes.Push(new Pair<Option<Func<int, string>>, int>(null, 0));
        }

        /// <summary>
        /// Creates a string maker with a tab mode, and an empty string builder
        /// </summary>
        /// <param name="tab"></param>
        public StringMaker(string tab = "\t") : this(new StringBuilder(), tab)
        {
            
        }

        /// <summary>
        /// Appends a line to the string maker
        /// </summary>
        /// <param name="src"></param>
        public void AppendLine(string src = "")
        {
            if(m_LineStarted)
            {
                Builder.Append(src + "\n");
            }
            else
            {
                Builder.Append(GetFormatedString(src) + "\n");
            }

            m_TabModes.PeekUpdate(pair =>
            {
                return new Pair<Option<Func<int, string>>, int>(pair.First, pair.Second + 1);
            });

            LineCount++;
            m_LineStarted = false;
        }

        /// <summary>
        /// Appends multiple lines to the string maker
        /// </summary>
        /// <param name="lines"></param>
        public void AppendLines(IEnumerable<string> lines)
        {
            foreach (string line in lines)
                AppendLine(line);
        }

        /// <summary>
        /// Appends multiple lines to the string maker
        /// </summary>
        /// <param name="lines"></param>
        public void AppendLines(params string[] lines)
        {
            AppendLines((IEnumerable<string>)lines);
        }

        /// <summary>
        /// Appends a string to the string maker
        /// </summary>
        /// <param name="src"></param>
        public void Append(string src)
        {
            if (m_LineStarted)
            {
                Builder.Append(src);
            }
            else
            {
                Builder.Append(GetFormatedString(src));
            }

            m_LineStarted = true;
        }

        /// <summary>
        /// Tabs the string maker in, allowing for item lists
        /// </summary>
        /// <param name="mode"></param>
        public void TabIn(Func<int, string> appender = null)
        {
            if (m_LineStarted)
                AppendLine();

            m_TabModes.Push(new Pair<Option<Func<int, string>>, int>(appender, 0));
        }

        /// <summary>
        /// Tabs out the string maker
        /// </summary>
        public void TabOut() 
        {
            if (m_LineStarted)
                AppendLine();

            if (Tabs == 0)
                return;

            m_TabModes.Pop();
        }

        /// <summary>
        /// Returns the constructed string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Builder.ToString();
        }

        private string Indent => m_Tab.Repeat(Tabs);

        private string GetFormatedString(string str)
        {
            var tabInfo = m_TabModes.Peek();
            string untabbed = tabInfo.First.Match(ok => ok(tabInfo.Second), () => "");
            return Indent + untabbed + str;
        }
    }
}
