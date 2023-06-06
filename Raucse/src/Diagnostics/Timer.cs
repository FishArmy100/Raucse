using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Diagnostics
{
    public static class Timer
    {
        public static TimeSpan Time(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public static Pair<TimeSpan, T> Time<T>(Func<T> func)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            T value = func();
            stopwatch.Stop();

            return new Pair<TimeSpan, T>(stopwatch.Elapsed, value);
        }
    }
}
