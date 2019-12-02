using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2019.Day02
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var (_, ram) = CreateMemory(input);

            SetInputs(12, 2, ref ram);
            ExecuteProgram(ref ram);

            return ram[0].ToString();
        }

        public string Part2(string[] input)
        {
            var (rom, ram) = CreateMemory(input);

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    ClearMemory(ref ram, ref rom);
                    SetInputs(noun, verb, ref ram);
                    ExecuteProgram(ref ram);

                    if (ram[0] == 19690720)
                    {
                        return $"{100 * noun + verb}";
                    }
                }
            }

            throw new InvalidOperationException();
        }

        public static (long[] rom, long[] ram) CreateMemory(string[] input)
        {
            var rom = input[0].Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Select(i => (long)i)
                .ToArray();
            var ram = new long[rom.Length];

            ClearMemory(ref ram, ref rom);

            return (rom, ram);
        }

        private static void ClearMemory(ref long[] ram, ref long[] rom)
        {
            Array.Copy(rom, ram, ram.Length);
        }

        private static void SetInputs(long noun, long verb, ref long[] ram)
        {
            ram[1] = noun;
            ram[2] = verb;
        }

        private static void ExecuteProgram(ref long[] memory)
        {
            for (var ip = 0; ip <= memory.Length; ip += 4)
            {
                switch (memory[ip])
                {
                    case 1:
                        {
                            var op1Index = memory[ip + 1];
                            var op2Index = memory[ip + 2];
                            var resultIndex = memory[ip + 3];
                            var op1 = memory[op1Index];
                            var op2 = memory[op2Index];

                            memory[resultIndex] = op1 + op2;
                        }
                        break;
                    case 2:
                        {
                            var op1Index = memory[ip + 1];
                            var op2Index = memory[ip + 2];
                            var resultIndex = memory[ip + 3];
                            var op1 = memory[op1Index];
                            var op2 = memory[op2Index];

                            memory[resultIndex] = op1 * op2;
                        }
                        break;
                    case 99:
                        return;
                    default:
                        throw new InvalidOperationException("Unknown opcode");
                }
            }
        }

        private void DumpCore(ref long[] data)
        {
            var result = "";
            for (int i = 0; i < data.Length; i++)
            {
                result += $"{data[i]} ";
            }

            result.TrimEnd();
            Console.WriteLine(result);
        }

    }
}
