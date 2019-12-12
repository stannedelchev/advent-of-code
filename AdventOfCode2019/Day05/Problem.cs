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
            var result = 0L;

            computer.Initialize(program);
            computer.Input.Enqueue(1);
            computer.Output = l => { result = l; };

            // Ignore .WaitingForOutput state
            while (computer.State != IntCodeComputerState.Halted)
            {
                computer.ExecuteProgram(true);
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var computer = new IntCodeComputer(input[0].Length);
            var program = computer.CreateProgram(input[0]);
            var result = 0L;

            computer.Initialize(program);
            computer.Input.Enqueue(5);
            computer.Output = l => result = l;
            // Ignore .WaitingForOutput state
            while (computer.State != IntCodeComputerState.Halted)
            {
                computer.ExecuteProgram(true);
            }

            return result.ToString();
        }
    }
}
