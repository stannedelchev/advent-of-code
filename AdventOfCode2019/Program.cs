using System;
using AdventOfCode.Shared;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var problems = new IProblem[] {
                new Day01.Problem(),
            };

            var runner = new ProblemRunner(problems);
            runner.Run();
        }
    }
}
