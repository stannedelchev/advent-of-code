using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal static class IntCodeComputerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static long[] CreateProgram(this IntCodeComputer self, string input)
        {
            var program = input.Split(",", StringSplitOptions.RemoveEmptyEntries)
                               .Select(long.Parse)
                               .ToArray();
            return program;
        }
    }
}