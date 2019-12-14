using System.Linq;
using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2019
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var problems = new IProblem[] {
                //new Day01.Problem(),
                //new Day02.Problem(),
                //new Day03.Problem(),
                //new Day04.Problem(),
                //new Day05.Problem(),
                //new Day06.Problem(),
                //new Day07.Problem(),
                //new Day08.Problem(),
                new Day09.Problem()
            };

            //problems = problems.Repeat().Take(200).ToArray();

            var runner = new ProblemRunner(problems);
            runner.Run();
        }
    }
}
