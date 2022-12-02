using AdventOfCode.Shared;

namespace AdventOfCode2022.Day02;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        var results = new Dictionary<string, int>
        {
            ["A X"] = 1 + 3,
            ["A Y"] = 2 + 6,
            ["A Z"] = 3 + 0,

            ["B X"] = 1 + 0,
            ["B Y"] = 2 + 3,
            ["B Z"] = 3 + 6,

            ["C X"] = 1 + 6,
            ["C Y"] = 2 + 0,
            ["C Z"] = 3 + 3,
        };

        return input.Select(l => results[l]).Sum().ToString();
    }

    public string Part2(string[] input)
    {
        var results = new Dictionary<string, int>
        {
            ["A X"] = 3 + 0,
            ["A Y"] = 1 + 3,
            ["A Z"] = 2 + 6,

            ["B X"] = 1 + 0,
            ["B Y"] = 2 + 3,
            ["B Z"] = 3 + 6,

            ["C X"] = 2 + 0,
            ["C Y"] = 3 + 3,
            ["C Z"] = 1 + 6,
        };

        return input.Select(l => results[l]).Sum().ToString();
    }
}