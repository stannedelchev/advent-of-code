using System;
using System.Linq;

namespace AdventOfCode2019.Day12
{
    internal static class MathEx
    {
        public static long LCM(long[] numbers)
        {
            return numbers.Aggregate(LCM);
        }
        public static long LCM(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        public static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
