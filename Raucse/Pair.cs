using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
	/// <summary>
	/// A pair of two different objects
	/// </summary>
	/// <typeparam name="TFirst"></typeparam>
	/// <typeparam name="TSecond"></typeparam>
	public struct Pair<TFirst, TSecond>
	{
		public readonly TFirst First;
		public readonly TSecond Second;

		/// <summary>
		/// Constructs a Pair from two values
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		public Pair(TFirst first, TSecond second)
		{
			First = first;
			Second = second;
		}

		/// <summary>
		/// Implicit conversion from a tuple, to a pair
		/// </summary>
		/// <param name="tuple"></param>
		public static implicit operator Pair<TFirst, TSecond>((TFirst, TSecond) tuple)
        {
			return new Pair<TFirst, TSecond>(tuple.Item1, tuple.Item2);
        }

		/// <summary>
		/// Implicit conversion from a pair to a tuple
		/// </summary>
		/// <param name="pair"></param>
		public static implicit operator (TFirst, TSecond)(Pair<TFirst, TSecond> pair)
		{
			return (pair.First, pair.Second);
		}

		public static implicit operator Pair<TFirst, TSecond>(KeyValuePair<TFirst, TSecond> pair)
        {
			return new Pair<TFirst, TSecond>(pair.Key, pair.Value);
        }

		public static implicit operator KeyValuePair<TFirst, TSecond>(Pair<TFirst, TSecond> pair)
        {
			return new KeyValuePair<TFirst, TSecond>(pair.First, pair.Second);
        }

		/// <summary>
		/// Allows for deconstruction, simmilar to Tuple deconstruction
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		public void Deconstruct(out TFirst first, out TSecond second)
		{
			first = First;
			second = Second;
		}

		public override bool Equals(object obj)
		{
			return obj is Pair<TFirst, TSecond> pair &&
				   EqualityComparer<TFirst>.Default.Equals(First, pair.First) &&
				   EqualityComparer<TSecond>.Default.Equals(Second, pair.Second);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(First, Second);
		}

        public static bool operator ==(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
        {
            return !(left == right);
        }
    }
}