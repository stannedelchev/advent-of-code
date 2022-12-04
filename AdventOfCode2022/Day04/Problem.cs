using AdventOfCode.Shared;
using MoreLinq;

namespace AdventOfCode2022.Day04;

internal class Problem : IProblem
{
    public string Part1(string[] input)
    {
        var fullOverlaps = 0;
        foreach (var l in input)
        {
            var line = l.AsSpan();
            var start1EndIndex = 0;
            var end1StartIndex = 0;
            var end1EndIndex = 0;

            var start2StartIndex = 0;
            var start2EndIndex = 0;
            var end2StartIndex = 0;

            while (line[start1EndIndex] != '-') start1EndIndex++;
            end1StartIndex = start1EndIndex + 1;
            end1EndIndex = end1StartIndex;
            while (line[end1EndIndex] != ',') end1EndIndex++;

            start2StartIndex = end1EndIndex + 1;
            start2EndIndex = start2StartIndex;
            while (line[start2EndIndex] != '-') start2EndIndex++;
            end2StartIndex = start2EndIndex + 1;

            var start1 = uint.Parse(line[..start1EndIndex]);
            var end1 = uint.Parse(line[end1StartIndex..end1EndIndex]);
            var start2 = uint.Parse(line[start2StartIndex..start2EndIndex]);
            var end2 = uint.Parse(line[end2StartIndex..]);

            if ((start1 >= start2 && end1 <= end2) ||
                (start2 >= start1 && end2 <= end1))
            {
                fullOverlaps++;
            }
        }

        return fullOverlaps.ToString();
    }

    public string Part2(string[] input)
    {

        var partialOverlaps = 0;
        foreach (var l in input)
        {
            var line = l.AsSpan();
            var start1EndIndex = 0;
            var end1StartIndex = 0;
            var end1EndIndex = 0;

            var start2StartIndex = 0;
            var start2EndIndex = 0;
            var end2StartIndex = 0;

            while (line[start1EndIndex] != '-') start1EndIndex++;
            end1StartIndex = start1EndIndex + 1;
            end1EndIndex = end1StartIndex;
            while (line[end1EndIndex] != ',') end1EndIndex++;

            start2StartIndex = end1EndIndex + 1;
            start2EndIndex = start2StartIndex;
            while (line[start2EndIndex] != '-') start2EndIndex++;
            end2StartIndex = start2EndIndex + 1;

            var start1 = uint.Parse(line[..start1EndIndex]);
            var end1 = uint.Parse(line[end1StartIndex..end1EndIndex]);
            var start2 = uint.Parse(line[start2StartIndex..start2EndIndex]);
            var end2 = uint.Parse(line[end2StartIndex..]);

            if ((start1 <= start2 && end1 >= start2) ||
                (start2 <= start1 && end2 >= start1))
            {
                partialOverlaps++;
            }
        }

        return partialOverlaps.ToString();
    }
}
