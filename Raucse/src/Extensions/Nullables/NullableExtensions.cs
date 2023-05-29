using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions.Nullables
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Converts the nullable value to a Option
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Option<T> ToOption<T>(this T? self) where T : struct
        {
            if (self is null)
                return new Option<T>();

            return new Option<T>(self.Value);
        }

        /// <summary>
        /// Used to visit a nullable value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="ok">Called if the value is not null</param>
        public static void Match<T>(this T? self, Action<T> ok) where T : struct
        {
            if (self is T value)
                ok(value);
        }

        /// <summary>
        /// Used to visit a nullable value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="ok">Called if the value is not null</param>
        /// <param name="fail">Called if the value is null</param>
        public static void Match<T>(this T? self, Action<T> ok, Action fail) where T : struct
        {
            if (self is T value)
                ok(value);
            else
                fail();
        }

        /// <summary>
        /// Used to visit an nullable and return a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturned"></typeparam>
        /// <param name="self"></param>
        /// <param name="ok"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public static TReturned Match<T, TReturned>(this T? self, Func<T, TReturned> ok, Func<TReturned> fail) where T : struct
        {
            if (self is T value)
                return ok(value);
            
           return fail();
        }

        /// <summary>
        /// Converts a nullable value to an Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <param name="onError">Called if the nullable is null</param>
        /// <returns></returns>
        public static Result<T, TError> ToResult<T, TError>(this T? self, Func<TError> onError) where T : struct
        {
            if (self is null)
                return new Result<T, TError>(self.Value);

            return new Result<T, TError>(onError());
        }
    }
}
