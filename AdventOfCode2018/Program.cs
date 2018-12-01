using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var problems = new[] { new Day1.Problem() };

            foreach (var problem in problems)
            {
                var day = problem.GetType().Namespace.Split(".", StringSplitOptions.RemoveEmptyEntries).Last();
                var result1 = problem.Part1(File.ReadAllLines($"{day}\\input.txt"));
                var result2 = problem.Part2(File.ReadAllLines($"{day}\\input.txt"));
                Console.WriteLine($"{day}.1 - {result1}");
                Console.WriteLine($"{day}.2 - {result2}");
            }
        }
    }
}
