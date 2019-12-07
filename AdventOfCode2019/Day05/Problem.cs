using System.Linq;
using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day05
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var computer = new IntCodeComputer(input[0].Length);
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program, 1);
            computer.ExecuteProgram();

            return computer.Output.Last().ToString();
        }

        public string Part2(string[] input)
        {
            var computer = new IntCodeComputer(input[0].Length);
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program, 5);
            computer.ExecuteProgram();

            return computer.Output.Last().ToString();
        }
    }
}
