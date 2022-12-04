using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2022.Day04;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        var assignments = input.Select(x => x.Split(",-".ToCharArray()))
                               .Select(p => (Start1: int.Parse(p[0]),
                                           End1: int.Parse(p[1]),
                                           Start2: int.Parse(p[2]),
                                           End2: int.Parse(p[3])))
                               .ToArray();
        var pairsWithFullOverlap = assignments.Count(a => (a.Start1 >= a.Start2 && a.End1 <= a.End2) || (a.Start2 >= a.Start1 && a.End2 <= a.End1));
        return pairsWithFullOverlap.ToString();
    }

    public string Part2(string[] input)
    {
        var assignments = input.Select(x => x.Split(",-".ToCharArray()))
                               .Select(p => (Start1: int.Parse(p[0]),
                                           End1: int.Parse(p[1]),
                                           Start2: int.Parse(p[2]),
                                           End2: int.Parse(p[3])))
                               .ToArray();
        var pairsWithPartialOverlap = assignments.Count(a => (a.Start1 <= a.Start2 && a.End1 >= a.Start2) ||
                                                             (a.Start2 <= a.Start1 && a.End2 >= a.Start1));
        return pairsWithPartialOverlap.ToString();
    }
}
