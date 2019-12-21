using AdventOfCode.Shared;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day11
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var painter = new RobotPainter(input[0]);
            painter.Run();

            return painter.Grid.Count.ToString();
        }

        public string Part2(string[] input)
        {
            var painter = new RobotPainter(input[0]);
            painter.Grid[new Point(0, 0)] = 1;
            painter.Run();
            var grid = painter.Grid;

            var gridHeight = grid.Max(kvp => kvp.Key.Y) + grid.Min(kvp => kvp.Key.Y);
            var gridWidth = grid.Max(kvp => kvp.Key.X) + grid.Min(kvp => kvp.Key.X);

            return PrintGrid(gridHeight, gridWidth, grid);
        }

        private static string PrintGrid(int gridHeight, int gridWidth, Dictionary<Point, int> grid)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            for (var y = 0; y < gridHeight + 1; y++)
            {
                for (var x = 0; x < gridWidth + 1; x++)
                {
                    if (!grid.TryGetValue(new Point(x, y), out var paint))
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append(paint > 0 ? "X" : " ");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
