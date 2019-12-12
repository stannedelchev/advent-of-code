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

            computer.Initialize(program);
            computer.Input.Enqueue(1);
            computer.ExecuteProgram(true);

            return computer.Outputs.Last().ToString();
        }

        public string Part2(string[] input)
        {
            var computer = new IntCodeComputer(input[0].Length);
            var program = computer.CreateProgram(input[0]);

            computer.Initialize(program);
            computer.Input.Enqueue(5);
            computer.ExecuteProgram(true);

            return computer.Outputs.Last().ToString();
        }
    }
}
