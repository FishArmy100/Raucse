using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
    /// <summary>
    /// Represents a Success or an Error
    /// </summary>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class Result<TSuccess, TError>
    {
        private readonly Either<TSuccess, TError> m_Value;

        /// <summary>
        /// Returns if is an error
        /// </summary>
        /// <returns></returns>
        public bool IsError() => m_Value.IsOptionB;

        /// <summary>
        /// Returns if is not a error
        /// </summary>
        /// <returns></returns>
        public bool IsOk() => m_Value.IsOptionA;

        /// <summary>
        /// If valid returns the value otherwise, throws an InvalidOperationException
        /// </summary>
        public TSuccess Value => m_Value.AValue;
        /// <summary>
        /// If invalid returns the error otherwise, throws an InvalidOperationException
        /// </summary>
        public TError Error => m_Value.BValue;

        /// <summary>
        /// Constructs a successful Result
        /// </summary>
        /// <param name="success"></param>
        public Result(TSuccess success)
        {
            m_Value = success;
        }

        /// <summary>
        /// Constructs a failed result
        /// </summary>
        /// <param name="error"></param>
        public Result(TError error)
        {
            m_Value = error;
        }

        /// <summary>
        /// Constructs a successful result
        /// </summary>
        /// <param name="success"></param>
        public static implicit operator Result<TSuccess, TError>(TSuccess success)
        {
            return new Result<TSuccess, TError>(success);
        }

        /// <summary>
        /// Constructs a failed result
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator Result<TSuccess, TError>(TError error)
        {
            return new Result<TSuccess, TError>(error);
        }

        /// <summary>
        /// Visits a Result
        /// </summary>
        /// <param name="okFunc">Called if is a successful result</param>
        /// <param name="failFunc">Called if is a failed result</param>
        public void Match(Action<TSuccess> okFunc, Action<TError> failFunc)
        {
            m_Value.Match(okFunc, failFunc);
        }

        /// <summary>
        /// Visits a Result
        /// </summary>
        /// <param name="okFunc">Called if is a successful result</param>
        /// <param name="failFunc">Called if is a failed result</param>
        public TReturn Match<TReturn>(Func<TSuccess, TReturn> okFunc, Func<TError, TReturn> failFunc)
        {
            return m_Value.Match(okFunc, failFunc);
        }

        /// <summary>
        /// Constructs an option from this result. The option is valid if the Result is successful, otherwise, returns an invalid option
        /// </summary>
        /// <returns></returns>
        public Option<TSuccess> ToOption()
        {
            return Match(ok => new Option<TSuccess>(ok), fail => new Option<TSuccess>());
        }

        public Option<TError> GetErrorOption()
        {
            return Match(ok => new Option<TError>(), fail => new Option<TError>(fail));
        }
    }

    public static class ResultExtensions
    {

        public static Result<List<TSuccess>, List<TError>> AggregateResults<TSuccess, TError>(this IEnumerable<Result<TSuccess, List<TError>>> self)
        {
            List<TSuccess> successes = new List<TSuccess>();
            List<TError> errors = new List<TError>();
            foreach (var result in self)
            {
                result.Match(
                    ok => successes.Add(ok),
                    fail => errors.AddRange(fail));
            }

            if (errors.Any())
                return errors;

            return successes;
        }
    }
}