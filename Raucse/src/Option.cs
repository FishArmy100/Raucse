using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
    public class NullOptionExeption : Exception { }

    /// <summary>
    /// A object that can have a valid or invalid representation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Option<T>
    {
        private readonly T m_Value;
        private readonly bool m_HasValue;

        /// <summary>
        /// Construct a valid Option from an object. If value is null, will defult to a invalid option.
        /// </summary>
        /// <param name="value">If null, will construct a null option.</param>
        public Option(T value)
        {
            if (typeof(T).IsValueType)
            {
                m_Value = value;
                m_HasValue = true;
            }
            else
            {
                m_Value = value;
                m_HasValue = value != null;
            }
        }

        /// <summary>
        /// Implicit conversion to a option from a value
        /// </summary>
        /// <param name="value">If null, will construct a invalid option</param>
        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }

        public static explicit operator T(Option<T> option)
        {
            return option.Value;
        }

        /// <summary>
        /// Gets the value of the option
        /// <exeption cref = "NullOptionExeption">Thrown if accessed when the value is accessed when the Option is invalid</exeption>
        /// </summary>
        public T Value
        {
            get
            {
                if (HasValue())
                    return m_Value;
                else
                    throw new NullOptionExeption();
            }
        }

        /// <summary>
        /// Returns if the option is valid or not.
        /// </summary>
        /// <returns></returns>
        public bool HasValue() => m_HasValue;

        /// <summary>
        /// Used to visit an option
        /// </summary>
        /// <param name="okFunc">Called if the option is valid</param>
        /// <param name="failFunc">Called if is an invalid option</param>
        public void Match(Action<T> okFunc, Action failFunc)
        {
            if (HasValue())
                okFunc(Value);
            else
                failFunc();
        }

        /// <summary>
        /// Used to visit an option
        /// </summary>
        /// <param name="okFunc">Called if the option is valid</param>
        public void Match(Action<T> okFunc)
        {
            if (HasValue())
                okFunc(Value);
        }

        /// <summary>
        /// Used to visit an option
        /// </summary>
        /// <param name="okFunc">Called if the option is valid</param>
        /// <param name="failFunc">Called if is an invalid option</param>
        public TReturn Match<TReturn>(Func<T, TReturn> okFunc, Func<TReturn> failFunc)
        {
            if (HasValue())
                return okFunc(Value);
            else
                return failFunc();
        }

        /// <summary>
        /// If valid, returns the value, otherwise will default construct one
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="okFunc"></param>
        /// <returns></returns>
        public TReturn MatchOrConstruct<TReturn>(Func<T, TReturn> okFunc) where TReturn : new()
        {
            return Match(okFunc, () => new TReturn());
        }

        /// <summary>
        /// Converts this option to a result
        /// </summary>
        /// <typeparam name="TError">The type of the error</typeparam>
        /// <param name="onError">Called if the this option does not have a value</param>
        /// <returns></returns>
        public Result<T, TError> ToResult<TError>(Func<TError> onError)
        {
            return Match(ok => new Result<T, TError>(ok), () => new Result<T, TError>(onError()));
        }

        public override bool Equals(object obj)
        {
            return obj is Option<T> option &&
                   EqualityComparer<T>.Default.Equals(m_Value, option.m_Value) &&
                   m_HasValue == option.m_HasValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_Value, m_HasValue);
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !(left == right);
        }
    }

    public static class OptionExtensions
    {
        /// <summary>
        /// If the option is valid, returns the string, otherwise, returns an empty string
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string MatchOrEmpty(this Option<string> option)
        {
            return option.Match(ok => ok, () => "");
        }

        /// <summary>
        /// Returns the first option that has a valid value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Option<T> FirstValid<T>(this IEnumerable<Option<T>> options)
        {
            foreach(var option in options)
            {
                if (option.HasValue())
                    return option.Value;
            }

            return new Option<T>();
        }

        /// <summary>
        /// Returns all of the valid options in the enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<T> AllValids<T>(this IEnumerable<Option<T>> options)
        {
            List<T> valids = new List<T>();
            foreach(var option in options)
            {
                option.Match(ok => valids.Add(ok));
            }

            return valids;
        }

        /// <summary>
        /// Returns true if any of the options is valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool AnyValid<T>(this IEnumerable<Option<T>> options)
        {
            return options.FirstValid().HasValue();
        }

        /// <summary>
        /// returns true if any of the options is invalid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool AnyInvalid<T>(this IEnumerable<Option<T>> options)
        {
            return !options.AnyValid();
        }
    }
}