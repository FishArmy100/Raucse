using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Strings
{
    public readonly struct StringReaderLocation
    {
        public readonly int Line;
        public readonly int Column;
        public readonly int Index;

        public StringReaderLocation(int line, int column, int index)
        {
            Line = line;
            Column = column;
            Index = index;
        }
    }
}
