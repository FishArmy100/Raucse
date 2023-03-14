using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
    /// <summary>
    /// Represents one of two possible values, TA or TB
    /// </summary>
    /// <typeparam name="TA"></typeparam>
    /// <typeparam name="TB"></typeparam>
    public struct Either<TA, TB>
    {
        private readonly object Value;

        /// <summary>
        /// Returns true if is option A
        /// </summary>
        public bool IsOptionA => Value is TA;
        /// <summary>
        /// Returns true if is option B
        /// </summary>
        public bool IsOptionB => Value is TB;

        /// <summary>
        /// Returns the A value if is the A value, otherwise throws an InvalidOperationExeption
        /// </summary>
        public TA AValue
        {
            get
            {
                if (IsOptionA)
                    return (TA)Value;
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Returns the B value if is the B value, otherwise throws an InvalidOperationExeption
        /// </summary>
        public TB BValue
        {
            get
            {
                if (IsOptionB)
                    return (TB)Value;
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Constructs a TA Either
        /// </summary>
        /// <param name="value"></param>
        public Either(TA value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructs a TB Either
        /// </summary>
        /// <param name="value"></param>
        public Either(TB value)
        {
            Value = value;
        }

        /// <summary>
        /// Constructs a TA Either
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator Either<TA, TB>(TA a)
        {
            return new Either<TA, TB>(a);
        }

        /// <summary>
        /// Constructs a TB Either
        /// </summary>
        /// <param name="b"></param>
        public static implicit operator Either<TA, TB>(TB b)
        {
            return new Either<TA, TB>(b);
        }

        /// <summary>
        /// Used to visit a Either
        /// </summary>
        /// <param name="aFunc">Called if is the A value</param>
        /// <param name="bFunc">Called if is the B value</param>
        public void Match(Action<TA> aFunc, Action<TB> bFunc)
        {
            if (Value is TA a)
                aFunc(a);
            else if (Value is TB b)
                bFunc(b);
            else
                throw new ArgumentException("This should never be called, and if it is, you've done messed up.");
        }

        /// <summary>
        /// Used to visit a Either
        /// </summary>
        /// <param name="aFunc">Called if is the A value</param>
        /// <param name="bFunc">Called if is the B value</param>
        public TReturn Match<TReturn>(Func<TA, TReturn> aFunc, Func<TB, TReturn> bFunc)
        {
            if (Value is TA a)
                return aFunc(a);
            else if (Value is TB b)
                return bFunc(b);
            else
                throw new ArgumentException("This should never be called, and if it is, you've done messed up.");
        }

        public override bool Equals(object obj)
        {
            return obj is Either<TA, TB> either &&
                   EqualityComparer<object>.Default.Equals(Value, either.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}