using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day03
{
    internal class Problem : IProblem
    {
        internal readonly struct GridPoint
        {
            public int X { get; }
            public int Y { get; }
            public int Steps { get; }

            public GridPoint(int x, int y, int steps)
            {
                X = x;
                Y = y;
                Steps = steps;
            }

            private sealed class XYEqualityComparer : IEqualityComparer<GridPoint>
            {
                public bool Equals(GridPoint x, GridPoint y)
                {
                    return x.X == y.X && x.Y == y.Y;
                }

                public int GetHashCode(GridPoint obj)
                {
                    unchecked
                    {
                        return (obj.X * 397) ^ obj.Y;
                    }
                }
            }

            public static IEqualityComparer<GridPoint> XYComparer { get; } = new XYEqualityComparer();
        }


        public string Part1(string[] input)
        {
            var wire1 = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Aggregate(new List<GridPoint>(250000), GetPoints)
                                .ToArray();

            var wire2 = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Aggregate(new List<GridPoint>(250000), GetPoints)
                                .ToArray();


            var intersections = wire1.Intersect(wire2, GridPoint.XYComparer).ToArray();
            var closestIntersection = intersections.Min(point => Math.Abs(point.X) + Math.Abs(point.Y));
            return closestIntersection.ToString();
        }

        public string Part2(string[] input)
        {
            var wire1 = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Aggregate(new List<GridPoint>(250000), GetPoints)
                                .ToHashSet(GridPoint.XYComparer);

            var wire2 = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Aggregate(new List<GridPoint>(250000), GetPoints)
                                .ToHashSet(GridPoint.XYComparer);

            wire1.IntersectWith(wire2);
            wire2.IntersectWith(wire1);

            var res = wire1.Select(w1p =>
            {
                wire2.TryGetValue(w1p, out var w2p);
                return w1p.Steps + w2p.Steps;
            })
                .Min();
            

            return res.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static List<GridPoint> GetPoints(List<GridPoint> acc, string cur)
        {
            var oldPoint = acc.Count == 0 ? new GridPoint() : acc[^1];
            var steps = oldPoint.Steps;
            var (direction, length) = (cur[0], int.Parse(cur[1..]));

            switch (direction)
            {
                case 'R':
                    for (var i = 1; i <= length; i++)
                    {
                        acc.Add(new GridPoint(oldPoint.X + i, oldPoint.Y, steps += 1));
                    }

                    break;
                case 'L':
                    for (var i = 1; i <= length; i++)
                    {
                        acc.Add(new GridPoint(oldPoint.X - i, oldPoint.Y, steps += 1));
                    }

                    break;
                case 'U':
                    for (var i = 1; i <= length; i++)
                    {
                        acc.Add(new GridPoint(oldPoint.X, oldPoint.Y + i, steps += 1));
                    }

                    break;
                case 'D':
                    for (var i = 1; i <= length; i++)
                    {
                        acc.Add(new GridPoint(oldPoint.X, oldPoint.Y - i, steps += 1));
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return acc;
        }
    }
}
