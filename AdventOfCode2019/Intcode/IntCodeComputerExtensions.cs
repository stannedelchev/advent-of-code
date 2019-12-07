using System;
using System.Linq;

namespace AdventOfCode2019.Intcode
{
    internal static class IntCodeComputerExtensions
    {
        public static long[] CreateProgram(this IntCodeComputer self, string input)
        {
            var program = input.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Select(i => (long)i)
                .ToArray();
            return program;
        }
    }
}
