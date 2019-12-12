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
                    computer.Input.Enqueue(phaseDistribution[i]);
                    computer.Input.Enqueue(previousOutput);
                    computer.Output = l => previousOutput = l;
                    computer.ExecuteProgram(true);
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
            var phases = new[] { 9, 8, 7, 6, 5 }.Permutations().ToArray();
            var program = IntCodeComputerExtensions.CreateProgram(null, input[0]);
            var computers = Enumerable.Range(0, 5)
                                      .Select(i =>
                                      {
                                          var result = new IntCodeComputer(input[0].Length);
                                          result.Initialize(program);
                                          return result;
                                      })
                                      .ToArray();

            var maxThrusterSignal = long.MinValue;
            foreach (var phaseDistribution in phases)
            {
                var outputs = new Dictionary<IntCodeComputer, List<long>>()
                {
                    {computers[0], new List<long>()},
                    {computers[1], new List<long>()},
                    {computers[2], new List<long>()},
                    {computers[3], new List<long>()},
                    {computers[4], new List<long>()}
                };

                for (var i = 0; i < 5; i++)
                {
                    var computerIndex = i;
                    var computer = computers[computerIndex];
                    computer.Initialize(program);
                    computer.Input.Enqueue(phaseDistribution[i]);
                    computer.Output = (l) =>
                    {
                        outputs[computer].Add(l);
                        computers[(computerIndex + 1) % 5].Input.Enqueue(l);
                        computers[(computerIndex + 1) % 5].ExecuteProgram(true);
                    };
                }

                computers[0].Input.Enqueue(0);
                computers[0].ExecuteProgram(true);

                var thrusterSignal = outputs[computers[^1]].Last();
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }

            return maxThrusterSignal.ToString();
        }
    }
}
