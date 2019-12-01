using System;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2019.Day01
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var totalFuel = input.Select(int.Parse).Sum(CalculateFuel);
            return totalFuel.ToString();
        }

        public string Part2(string[] input)
        {
            var totalFuel = input.Select(int.Parse).Sum(moduleMass =>
            {
                var fuel = CalculateFuel(moduleMass);
                var result = fuel;

                while (fuel > 0)
                {
                    fuel = CalculateFuel(fuel);
                    result += fuel;
                }

                return result;
            });

            return totalFuel.ToString();
        }

        private static int CalculateFuel(int mass)
        {
            var fuel = mass / 3 - 2;
            return Math.Max(fuel, 0);
        }
    }
}
