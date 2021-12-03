using System;
using System.Buffers;
using System.Collections;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day03
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var inputLength = input.Length;
            int wordLength = input[0].Length;
            var wordLengthZeroBased = wordLength - 1;
            var numbers = ArrayPool<int>.Shared.Rent(inputLength);

            for (var i = 0; i < inputLength; i++)
            {
                var line = input[i];
                var result = 0;
                result += (line[0] - 48) * 2048;
                result += (line[1] - 48) * 1024;
                result += (line[2] - 48) * 512;
                result += (line[3] - 48) * 256;
                result += (line[4] - 48) * 128;
                result += (line[5] - 48) * 64;
                result += (line[6] - 48) * 32;
                result += (line[7] - 48) * 16;
                result += (line[8] - 48) * 8;
                result += (line[9] - 48) * 4;
                result += (line[10] - 48) * 2;
                result += (line[11] - 48) * 1;
                numbers[i] = result;
            }

            var ones = ArrayPool<int>.Shared.Rent(wordLength);
            for (var i = 0; i < inputLength; i++)
            {
                var number = numbers[i];
                for (var bitsLeftFromMsb = 0; bitsLeftFromMsb < wordLength; bitsLeftFromMsb++)
                {
                    ones[bitsLeftFromMsb] += number >> (wordLengthZeroBased - bitsLeftFromMsb) & 1;
                }
            }

            var gammaRate = 0;
            for (var i = 0; i < wordLength; i++)
            {
                var current = ones[i];
                if (inputLength - current <= current)
                {
                    gammaRate += (int)Math.Pow(2, wordLengthZeroBased - i);
                }
            }

            var epsilonRate = ~gammaRate & 0xFFF;
            ArrayPool<int>.Shared.Return(ones, true);
            ArrayPool<int>.Shared.Return(numbers, true);
            return (gammaRate * epsilonRate).ToString();
        }

        public string Part2(string[] input)
        {
            //input = new[] {
            //    "00100",
            //    "11110",
            //    "10110",
            //    "10111",
            //    "10101",
            //    "01111",
            //    "00111",
            //    "11100",
            //    "10000",
            //    "11001",
            //    "00010",
            //    "01010",
            //    };

            var inputLength = input.Length;
            int wordLength = input[0].Length;
            var wordLengthLoopUpperBound = wordLength - 1;

            var numbers = input;
            for (int bit = 0; bit < wordLength; bit++)
            {
                var ones = numbers.Count(l => l[bit] == '1');
                var zeroes = numbers.Count(l => l[bit] == '0');

                if (ones >= zeroes)
                {
                    numbers = numbers.Where(l => l[bit] == '1').ToArray();
                }
                else
                {
                    numbers = numbers.Where(l => l[bit] == '0').ToArray();
                }

                if (numbers.Length == 1)
                {
                    break;
                }
            }

            var oxygenGeneratorRating = Convert.ToInt32(numbers[0], 2);

            numbers = input;
            for (int bit = 0; bit < wordLength; bit++)
            {
                var ones = numbers.Count(l => l[bit] == '1');
                var zeroes = numbers.Count(l => l[bit] == '0');

                if (zeroes <= ones)
                {
                    numbers = numbers.Where(l => l[bit] == '0').ToArray();
                }
                else
                {
                    numbers = numbers.Where(l => l[bit] == '1').ToArray();
                }

                if (numbers.Length == 1)
                {
                    break;
                }
            }
            var co2ScrubberRating = Convert.ToInt32(numbers[0], 2);

            return (oxygenGeneratorRating * co2ScrubberRating).ToString();
        }
    }
}
