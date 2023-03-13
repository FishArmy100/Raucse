using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key) where TValue : new()
        {
            if (!self.ContainsKey(key))
                self.Add(key, new TValue());

            return self[key];
        }

        public static string GetOrCreate<TKey>(this Dictionary<TKey, string> self, TKey key)
        {
            if (!self.ContainsKey(key))
                self.Add(key, "");

            return self[key];
        }
    }
}
