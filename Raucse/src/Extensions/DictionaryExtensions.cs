using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets or creates a value from a key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key) where TValue : new()
        {
            if (!self.ContainsKey(key))
                self.Add(key, new TValue());

            return self[key];
        }

        /// <summary>
        /// Gets a string or creates an empty string from a key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetOrCreate<TKey>(this Dictionary<TKey, string> self, TKey key)
        {
            if (!self.ContainsKey(key))
                self.Add(key, string.Empty);

            return self[key];
        }

        /// <summary>
        /// Gets a value from a dictionary, if there is no value found with the key, will return a empty option
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Option<TValue> Get<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key)
        {
            if(self.TryGetValue(key, out TValue value))
                return value;

            return new Option<TValue>(); 
        }
    }
}
