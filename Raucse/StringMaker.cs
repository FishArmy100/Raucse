using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raucse.Extensions;

namespace Raucse
{
    public class StringMaker
    {
        public readonly StringBuilder Builder;
        private readonly string m_Tab;

        private Stack<Pair<TabMode, int>> m_TabModes = new Stack<Pair<TabMode, int>>();
        private Stack<int> m_LineIndexStack = new Stack<int>();
        private bool m_LineStarted = false;

        public int TabLine => m_LineIndexStack.Peek() + 1;
        public int LineCount => m_LineIndexStack.Sum();

        public int Tabs => m_LineIndexStack.Count - 1;

        public StringMaker(StringBuilder builder, string tab = "\t")
        {
            Builder = builder;
            m_Tab = tab;
            m_TabModes.Push(new Pair<TabMode, int>(TabMode.None, 0));
            m_LineIndexStack.Push(0);
        }

        public StringMaker(string tab = "\t") : this(new StringBuilder(), tab)
        {
            
        }

        public void AppendLine(string src)
        {
            if(m_LineStarted)
            {
                Builder.Append(src + "\n");
            }
            else
            {
                Builder.Append(GetFormatedString(src) + "\n");
            }

            m_LineStarted = false;
            m_LineIndexStack.PeekUpdate(l => l + 1);
        }

        public void AppendLines(IEnumerable<string> lines)
        {
            foreach (string line in lines)
                AppendLine(line);
        }

        public void AppendLines(params string[] lines)
        {
            AppendLines((IEnumerable<string>)lines);
        }

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

        public void TabIn(TabMode mode = TabMode.None) 
        {
            if(m_TabModes.Peek().First == mode)
            {
                int count = m_TabModes.Peek().Second;
                m_TabModes.PeekUpdate(new Pair<TabMode, int>(mode, count + 1));
            }
            else
            {
                m_TabModes.Push(new Pair<TabMode, int>(mode, 0));
            }

            m_LineIndexStack.Push(0);
        }
        public void TabOut() 
        {
            if (Tabs == 0)
                return;

            Pair<TabMode, int> current = m_TabModes.Peek();
            int count = current.Second - 1;
            if (count == 0)
            {
                m_TabModes.Pop();
            }
            else
            {
                m_TabModes.PeekUpdate(new Pair<TabMode, int>(current.First, count));
            }

            m_LineIndexStack.Pop();
        }

        public override string ToString()
        {
            return Builder.ToString();
        }

        private string Indent => m_Tab.Repeat(Tabs);

        private string GetFormatedString(string str)
        {
            string untabbed = m_TabModes.Peek().First switch
            {
                TabMode.Bulleted => $"- {str}",
                TabMode.Numbered => $"{TabLine}: {str}",
                TabMode.None => str,
                _ => throw new NotImplementedException(),
            };

            return Indent + untabbed;
        }

        public enum TabMode
        {
            Bulleted,
            Numbered,
            None
        }
    }
}
