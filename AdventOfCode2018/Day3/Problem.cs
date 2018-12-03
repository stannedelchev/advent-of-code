using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018.Day3
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var rects = this.ParseInputAsRectangles(input);

            // TODO: Replace with sweeper from https://stackoverflow.com/questions/244452/what-is-an-efficient-algorithm-to-find-area-of-overlapping-rectangles

            var maxWidth = 0;
            var maxHeight = 0;
            foreach (var rect in rects)
            {
                maxWidth = Math.Max(maxWidth, rect.X + rect.Width);
                maxHeight = Math.Max(maxHeight, rect.Y + rect.Height);
            }

            var result = 0;
            var rectsLength = rects.Length;

            for (var x = 0; x < maxWidth; x++)
            {
                for (var y = 0; y < maxHeight; y++)
                {
                    int rectanglesPointBelongsTo = 0;
                    for(int i = 0; i < rectsLength; i++)
                    {
                        rectanglesPointBelongsTo += rects[i].Contains(x, y) ? 1 : 0;
                        if (rectanglesPointBelongsTo >= 2)
                        {
                            result++;
                            break;
                        }
                    }
                }
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var rects = this.ParseInput(input);

            for (var i = 0; i < rects.Length - 1; i++)
            {
                var intersects = false;
                for (var j = 0; j < rects.Length; j++)
                {
                    if (rects[i].Rectangle.IntersectsWith(rects[j].Rectangle) && i != j)
                    {
                        intersects = true;
                    }
                }

                if (!intersects)
                {
                    return rects[i].Id.ToString();
                }
            }

            throw new InvalidOperationException();
        }

        private PositionedRectangle[] ParseInput(string[] input)
        {
            var rectangles = input.Select(l =>
            {
                var regex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d*)");
                var match = regex.Match(l);
                var id = int.Parse(match.Groups["id"].Value);
                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                var width = int.Parse(match.Groups["width"].Value);
                var height = int.Parse(match.Groups["height"].Value);

                return new PositionedRectangle
                {
                    Id = id,
                    Rectangle = new Rectangle(x, y, width, height)
                };
            });

            return rectangles.OrderBy(r => r.Rectangle.Location.X).ToArray();
        }

        private Rectangle[] ParseInputAsRectangles(string[] input)
        {
            return this.ParseInput(input).Select(r => r.Rectangle).ToArray();
        }
    }

    internal struct PositionedRectangle
    {
        public int Id { get; set; }

        public Rectangle Rectangle { get; set; }
    }
}
