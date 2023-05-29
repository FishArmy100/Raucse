using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks to see if two sequences are Equal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="other"></param>
        /// <param name="checker"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Selects only the items that the selector returns a valid option for
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="self"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> self, Func<T, Option<TOut>> selector)
        {
            foreach(var value in self)
            {
                var selected = selector(value);
                if (selected.HasValue())
                    yield return selected.Value;
            }
        }

        /// <summary>
        /// Itterates through the enumerable and performs the action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (T item in self)
                action(item);
        }

        /// <summary>
        /// Calls the visitor for each item, but does not mutate the Enumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public static IEnumerable<T> Visit<T>(this IEnumerable<T> self, Action<T> visitor)
        {
            foreach(T item in self)
            {
                visitor(item);
                yield return item;
            }
        }

        /// <summary>
        /// Returns an enumerable of all options which where valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<T> Valid<T>(this IEnumerable<Option<T>> self)
        {
            foreach(Option<T> item in self)
            {
                if (item.HasValue())
                    yield return item.Value;
            }
        }

        /// <summary>
        /// Returns an enumerable of all the items in the collection that are not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> self) where T : struct
        {
            foreach(T? item in self)
            {
                if (item is T t)
                    yield return t;
            }
        }

        /// <summary>
        /// Returns an enumerable of all the items in the collection that are not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> self) where T : class
        {
            foreach (T item in self)
            {
                if (item != null)
                    yield return item;
            }
        }

        /// <summary>
        /// Returns an enumerable of all the Ok value of the results
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> Valid<TValue, TError>(this IEnumerable<Result<TValue, TError>> self)
        {
            foreach(Result<TValue, TError> result in self)
            {
                if (result.IsOk())
                    yield return result.Value;
            }
        }

        /// <summary>
        /// Returns an enumerable of all the Fail value of the results
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<TError> Errors<TValue, TError>(this IEnumerable<Result<TValue, TError>> self)
        {
            foreach (Result<TValue, TError> result in self)
            {
                if (result.IsError())
                    yield return result.Error;
            }
        }

        /// <summary>
        /// Merges an enumerable of enumerables together into one enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> self)
        {
            return self.SelectMany(s => s);
        }
    }
}
