﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse
{
	public class Pair<TFirst, TSecond>
	{
		public readonly TFirst First;
		public readonly TSecond Second;

		public Pair(TFirst first, TSecond second)
		{
			First = first;
			Second = second;
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
	}
}