using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day10
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var points = this.ParsePoints(input);
            var iterations = this.PartCore(points);

            this.PrintPoints(points);

            return "CZKPNARN";
        }

        public string Part2(string[] input)
        {
            var points = this.ParsePoints(input);
            var iterations = this.PartCore(points);

            return iterations.ToString();
        }

        private int PartCore(Point[] points)
        {
            var lengthX = int.MaxValue;
            var iteration = 0;
            while (lengthX >= 70)
            {
                iteration++;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].X += points[i].Dx;
                    points[i].Y += points[i].Dy;
                }

                lengthX = points.Max(p => p.X) - points.Min(p => p.X);
            }

            return iteration;
        }

        private Point[] ParsePoints(string[] input)
        {
            return input.Select(l =>
            {
                var regex = new Regex(@"position\=\<(?<x>\s*-?\d+), (?<y>\s*-?\d+)> velocity=<(?<dx>\s*-?\d+), (?<dy>\s*-?\d+)>");
                var match = regex.Match(l);
                var (x, y, dx, dy) = (match.Groups["x"].Value, match.Groups["y"].Value, match.Groups["dx"].Value, match.Groups["dy"].Value);
                return new Point(int.Parse(x), int.Parse(y), int.Parse(dx), int.Parse(dy));
            }).ToArray();
        }

        private void PrintPoints(Point[] points)
        {
            var copy = new Point[points.Length];
            Array.Copy(points.ToArray(), copy, points.Length);

            var maxX = points.Max(p => p.X);
            var minX = points.Min(p => p.X);
            var maxY = points.Max(p => p.Y);
            var minY = points.Min(p => p.Y);
            for (var i = 0; i < copy.Length; i++)
            {
                copy[i].X = points[i].X - minX;
                copy[i].Y = points[i].Y - minY;
            }

            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 80; x++)
                {
                    if (Array.Find(copy, p => p.X == x && p.Y == y) != null)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
