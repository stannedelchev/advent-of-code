using AdventOfCode.Shared;
using MoreLinq;
using System.Globalization;

namespace AdventOfCode2022.Day01;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        return input.GroupAdjacent(string.IsNullOrEmpty)
                    .Where(l => !l.Key)
                    .Select(g => g.Select(decimal.Parse).Sum())
                    .Max()
                    .ToString(CultureInfo.InvariantCulture);
    }

    public string Part2(string[] input)
    {
        return input.GroupAdjacent(string.IsNullOrEmpty)
                    .Where(l => !l.Key)
                    .Select(g => g.Select(decimal.Parse).Sum())
                    .OrderByDescending(x => x)
                    .Take(3)
                    .Sum()
                    .ToString(CultureInfo.InvariantCulture);
    }
}