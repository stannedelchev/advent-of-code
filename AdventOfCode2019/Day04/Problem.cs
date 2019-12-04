using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2019.Day04
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var min = int.Parse(input[0][..6]);
            var max = int.Parse(input[0][^6..]);

            var count = 0;
            for (var i = min; i < max; i++)
            {
                var digits = GetDigits(i);
                if (!IsOrderedNumber(digits))
                {
                    continue;
                }

                if (digits.RunLengthEncode().Any(g => g.Value >= 2))
                {
                    count += 1;
                }
            }

            return count.ToString();
        }

        public string Part2(string[] input)
        {
            var min = int.Parse(input[0][..6]);
            var max = int.Parse(input[0][7..]);

            return Enumerable.Range(min, max - min + 1)
                             .Select(GetDigits)
                             .Where(IsOrderedNumber)
                             .Count(i => i.RunLengthEncode().Any(g => g.Value == 2))
                             .ToString();
        }

        private static readonly int[] Digits = new int[6];

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static int[] GetDigits(int i)
        {
            Digits[0] = i / 100000;
            Digits[1] = i / 10000 % 10;
            Digits[2] = i / 1000 % 10;
            Digits[3] = i / 100 % 10;
            Digits[4] = i / 10 % 10;
            Digits[5] = i % 10;

            return Digits;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool IsOrderedNumber(int[] i)
        {
            return i[0] <= i[1] && i[1] <= i[2] && i[2] <= i[3] && i[3] <= i[4] && i[4] <= i[5];
        }
    }
}
