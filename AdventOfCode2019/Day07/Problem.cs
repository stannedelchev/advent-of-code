using System.Linq;
using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;
using MoreLinq;

namespace AdventOfCode2019.Day07
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var phases = new[] { 0, 1, 2, 3, 4 }.Permutations().ToArray();
            var program = IntCodeComputerExtensions.CreateProgram(null, input[0]);
            var computers = Enumerable.Range(0, 5).Select(i =>
            {
                var result = new IntCodeComputer(input[0].Length);
                result.Initialize(program);
                return result;
            }).ToArray();

            var maxThrusterSignal = long.MinValue;
            foreach (var phaseDistribution in phases)
            {
                var previousOutput = 0L;
                for (var i = 0; i < 5; i++)
                {
                    var computer = computers[i];
                    computer.Initialize(program);
                    computer.QueueInput(phaseDistribution[i]);
                    computer.QueueInput(previousOutput);
                    computer.ExecuteProgram();
                    previousOutput = computer.Output.Last();
                }

                var thrusterSignal = computers[^1].Output.Last();
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }

            return maxThrusterSignal.ToString();
        }

        public string Part2(string[] input)
        {
            return "";
        }
    }
}
