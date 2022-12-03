using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2022.Day03;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        return input.Select(rucksack =>
             {
                 var leftCompartment = rucksack[..(rucksack.Length / 2)];
                 var rightCompartment = rucksack[(rucksack.Length / 2)..];

                 var leftItems = Enumerable.ToHashSet(leftCompartment);
                 var rightItems = Enumerable.ToHashSet(rightCompartment);

                 leftItems.IntersectWith(rightItems);
                 return leftItems.Select(c => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) + 1).Sum();
             })
             .Sum()
             .ToString();
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
