using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Experimental.Debugging
{
    class PanicException : Exception
    {
        public PanicException(string message) : base(message) { }
    }
}
