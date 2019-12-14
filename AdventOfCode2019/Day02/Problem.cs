using System;
using System.Runtime.CompilerServices;
using AdventOfCode.Shared;
using AdventOfCode2019.Intcode;

namespace AdventOfCode2019.Day02
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram(input[0]);
            computer.Initialize(program);
            SetInputs(12, 2, computer);
            computer.ExecuteProgram();

            return computer[0].ToString();
        }

        public string Part2(string[] input)
        {
            var computer = new IntCodeComputer();
            var program = computer.CreateProgram(input[0]);

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    computer.Initialize(program);
                    SetInputs(noun, verb, computer);
                    computer.ExecuteProgram();

                    if (computer[0] == 19690720)
                    {
                        return $"{100 * noun + verb}";
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private static void SetInputs(long noun, long verb, IntCodeComputer computer)
        {
            computer[1] = noun;
            computer[2] = verb;
        }
    }
}
