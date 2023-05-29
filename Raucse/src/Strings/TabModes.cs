using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Strings
{
    public static class TabModes
    {
        /// <summary>
        /// For numbered lists
        /// </summary>
        public static readonly Func<int, string> Number = (i) => $"{i + 1}: ";

        /// <summary>
        /// For bullited lists
        /// </summary>
        public static readonly Func<int, string> Bulleted = (_) => "- ";
    }
}
