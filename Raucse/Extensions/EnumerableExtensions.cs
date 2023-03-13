using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool SequenceEquals<T>(this IEnumerable<T> enumerable, IEnumerable<T> other, Func<T, T, bool> checker)
        {
            if (enumerable.Count() != other.Count())
                return false;

            for (int i = 0; i < enumerable.Count(); i++)
            {
                if (!checker(enumerable.ElementAt(i), other.ElementAt(i)))
                    return false;
            }

            return true;
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> self)
        {
            return self.SelectMany(s => s);
        }
    }
}
