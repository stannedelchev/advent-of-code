using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day6
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var points = input.Select((l, i) =>
            {
                var match = new Regex(@"(?<x>\d+)\, (?<y>\d+)").Match(l);

                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                return new GridPoint(x, y, i);
            }).ToArray();

            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            var gridWidth = maxX + 2;
            var gridHeight = maxY + 2;
            var grid = new int[gridWidth, gridHeight];

            for (var x = 0; x < gridWidth; x++)
            {
                for (var y = 0; y < gridHeight; y++)
                {
                    var distances = new (int distance, int pointIndex)[points.Length];
                    for (var pi = 0; pi < points.Length; pi++)
                    {
                        var distance = points[pi].DistanceTo(x, y);
                        distances[pi] = (distance, pi);
                    }

                    Array.Sort(distances);

                    if(distances[0].distance == distances[1].distance)
                    {
                        grid[x, y] = points.Length;
                        goto next;
                    }
                    else
                    {
                        grid[x, y] = points[distances[0].pointIndex].Index;
                    }

                    points[grid[x,y]].Size++;
                    next:;
                }
            }

            for (var y = 0; y < gridHeight; y++)
            {
                for (var x = 0; x < gridWidth; x++)
                {
                    if (y == 0 || x == 0 || y == gridHeight - 1 || x == gridWidth - 1)
                    {
                        var owner = grid[x, y];
                        if(owner != points.Length)
                        {
                            points[owner].Size = 0;
                        }

                    }
                }
            }

            var res = points.Max(p => p.Size);
            return res.ToString();
        }

        public string Part2(string[] input)
        {
            var points = input.Select((l, i) =>
            {
                var match = new Regex(@"(?<x>\d+)\, (?<y>\d+)").Match(l);

                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                return new GridPoint(x, y, i);
            }).ToArray();

            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            var gridWidth = maxX + 2;
            var gridHeight = maxY + 2;
            var grid = new int[gridWidth, gridHeight];

            int total = 0;
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    var distanceSum = points.Sum(p => p.DistanceTo(x, y));
                    if(distanceSum < 10000)
                    {
                        total++;
                    }
                }
            }

            return total.ToString();
        }
    }

    internal struct GridPoint
    {
        public GridPoint(int x, int y, int index)
        {
            this.Y = y;
            this.X = x;
            this.Size = 0;
            this.Index = index;
        }

        public int X { get; }
        public int Y { get; }
        public int Index { get; }
        public int Size { get; set; }

        public int DistanceTo(int x, int y)
        {
            return Math.Abs(x - this.X) + Math.Abs(y - this.Y);
        }
    }
}
