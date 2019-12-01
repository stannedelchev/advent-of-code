using AdventOfCode.Shared;

namespace AdventOfCode2018
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var problems = new IProblem[] {
                new Day1.Problem(),
                new Day2.Problem(),
                new Day3.Problem(),
                new Day4.Problem(),
                new Day5.Problem(),
                new Day6.Problem(),
                new Day7.Problem(),
                new Day8.Problem(),
                new Day9.Problem(),
                new Day10.Problem(),
                new Day11.Problem(),
                new Day13.Problem()
            };

            var runner = new ProblemRunner(problems);
            runner.Run();
        }
    }
}
