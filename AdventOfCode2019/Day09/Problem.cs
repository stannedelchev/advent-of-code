using System.Linq;
using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day09
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program);
            computer.Input.Enqueue(1);
            computer.ExecuteProgram();
            return string.Join(",", computer.Outputs.ToArray());
        }


        public string Part2(string[] input)
        {
            return "";
        }
    }
}
