using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2022.Day03;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        var totalSum = 0;
        var priorities = new[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52,
            0, 0, 0, 0, 0, 0,
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
        };

        foreach (var rucksack in input)
        {
            var compartmentSeparatorIndex = rucksack.Length / 2;
            for (var leftItemIndex = 0; leftItemIndex < compartmentSeparatorIndex; leftItemIndex++)
            {
                var leftItem = rucksack[leftItemIndex];
                if (rucksack.IndexOf(leftItem, compartmentSeparatorIndex) != -1)
                {
                    totalSum += priorities[leftItem];
                    break;
                }
            }
        }

        return totalSum.ToString();
    }

    public string Part2(string[] input)
    {
        return input.Batch(3, elfGroup => elfGroup.Select(Enumerable.ToHashSet)
                                                  .Aggregate((acc, h) =>
                                                  {
                                                      acc.IntersectWith(h);
                                                      return acc;
                                                  }))
                    .Select(h => h.First())
                    .Select(c => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) + 1)
                    .Sum()
                    .ToString();
    }
}
