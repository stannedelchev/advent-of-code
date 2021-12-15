using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day05
{
    internal class Problem : IProblem
    {
        [DebuggerDisplay("{X1},{Y1} -> {X2}, {Y2}")]
        private record Line(int X1, int Y1, int X2, int Y2)
        {
            public LineOrientation Orientation =>
                X1 == X2 ? LineOrientation.Vertical
                         : Y1 == Y2 ? LineOrientation.Horizontal
                                    : LineOrientation.Diagonal;

            public IEnumerable<(int, int)> Points()
            {
                switch (Orientation)
                {
                    case LineOrientation.Horizontal:
                        {
                            var x1 = Math.Min(X1, X2);
                            var x2 = Math.Max(X1, X2);
                            for (var x = x1; x <= x2; x++)
                            {
                                yield return (x, Y1);
                            }
                        }
                        break;
                    case LineOrientation.Vertical:
                        {
                            var y1 = Math.Min(Y1, Y2);
                            var y2 = Math.Max(Y1, Y2);
                            for (var y = y1; y <= y2; y++)
                            {
                                yield return (X1, y);
                            }
                        }
                        break;
                    case LineOrientation.Diagonal:
                        {
                            var xDelta = X2 > X1 ? 1 : -1;
                            var yDelta = Y2 > Y1 ? 1 : -1;

                            var x = X1;
                            var y = Y1;
                            do
                            {
                                yield return (x, y);
                                x += xDelta;
                                y += yDelta;
                            } while (x != X2 && y != Y2);

                            yield return (x, y);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private enum LineOrientation
        {
            Horizontal,
            Vertical,
            Diagonal,
        }

        public string Part1(string[] input)
        {
            var lines = input.Select(l =>
            {
                var split = l.Split(" -> ");
                var leftParts = split[0].Split(',');
                var rightParts = split[1].Split(',');
                return new Line(int.Parse(leftParts[0]),
                                int.Parse(leftParts[1]),
                                int.Parse(rightParts[0]),
                                int.Parse(rightParts[1]));
            })
                             .Where(l => l.Orientation is LineOrientation.Horizontal or LineOrientation.Vertical)
                             .ToArray();

            var board = new int[1000, 1000];

            var overlappingPoints = 0;

            foreach (var line in lines)
            {
                foreach (var (x, y) in line.Points())
                {
                    board[y, x]++;
                    if (board[y, x] == 2)
                    {
                        overlappingPoints++;
                    }
                }
            }

            return overlappingPoints.ToString();
        }

        private string PrintBoard(int[,] board)
        {
            var result = new StringBuilder();
            for (var row = 0; row < board.GetLength(1); row++)
            {
                for (var column = 0; column < board.GetLength(0); column++)
                {
                    const int space = 1;
                    result.Append(board[row, column] > 0 ? $"{board[row, column],space}" : $"{'.',space}");
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var lines = input.Select(l =>
                             {
                                 var split = l.Split(" -> ");
                                 var leftParts = split[0].Split(',');
                                 var rightParts = split[1].Split(',');
                                 return new Line(int.Parse(leftParts[0]),
                                                 int.Parse(leftParts[1]),
                                                 int.Parse(rightParts[0]),
                                                 int.Parse(rightParts[1]));
                             })
                             .ToArray();

            var board = new int[1000, 1000];

            var overlappingPoints = 0;

            foreach (var line in lines)
            {
                foreach (var (x, y) in line.Points())
                {
                    board[y, x]++;
                    if (board[y, x] == 2)
                    {
                        overlappingPoints++;
                    }
                }
            }

            return overlappingPoints.ToString();
        }
    }
}
