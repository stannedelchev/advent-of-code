using System.Collections.Generic;
using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;
using MoreLinq;
using System.Linq;

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
                var result = new IntCodeComputer();
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
                    computer.Input.Enqueue(phaseDistribution[i]);
                    computer.Input.Enqueue(previousOutput);
                    computer.Output += (_, l) => previousOutput = l;
                    computer.ExecuteProgram();
                }

                var thrusterSignal = previousOutput;
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }

            return maxThrusterSignal.ToString();
        }

        public string Part2(string[] input)
        {
            var phases = new[] { 5, 6, 7, 8, 9 }.Permutations().ToArray();
            var program = IntCodeComputerExtensions.CreateProgram(null, input[0]);
            var computers = Enumerable.Range(0, 5)
                                      .Select(i =>
                                      {
                                          var result = new IntCodeComputer();
                                          result.Initialize(program);
                                          return result;
                                      })
                                      .ToArray();

            var maxThrusterSignal = long.MinValue;
            foreach (var phaseDistribution in phases)
            {
                for (var i = 0; i < 5; i++)
                {
                    var computerIndex = i;
                    var computer = computers[computerIndex];
                    computer.Initialize(program);
                    computer.Input.Enqueue(phaseDistribution[i]);
                    computer.Output += (_, l) =>
                    {
                        var nextComputer = computers[(computerIndex + 1) % 5];
                        nextComputer.Input.Enqueue(l);
                        nextComputer.ExecuteProgram();
                    };
                }

                computers[0].Input.Enqueue(0);
                computers[0].ExecuteProgram();

                var thrusterSignal = computers[^1].Outputs.Last();
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }

            return maxThrusterSignal.ToString();
        }
    }
}
