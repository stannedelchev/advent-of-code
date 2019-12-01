using System;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day13
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var grid = this.ParseInput(input);

            var ticker = new Ticker(grid);
            while (true)
            {
                var crashes = ticker.Tick(false);
                if (crashes.Count > 0)
                {
                    return $"{crashes[0].X},{crashes[0].Y}";
                }
            }
        }

        public string Part2(string[] input)
        {
            var grid = this.ParseInput(input);
            var ticker = new Ticker(grid);
            while (true)
            {
                if (grid.Carts.Count == 1)
                {
                    return $"{grid.Carts[0].X},{grid.Carts[0].Y}";
                }

                var crashes = ticker.Tick(true);
            }
        }

        private Grid ParseInput(string[] input)
        {
            var grid = new Grid(input.Length, input[0].Length);
            var roadFactory = new RoadFactory(grid);
            for (var l = 0; l < input.Length; l++)
            {
                for (var c = 0; c < input[l].Length; c++)
                {
                    grid.Roads[l, c] = roadFactory.Create(input[l][c], out var cart, l, c);

                    if (cart != null)
                    {
                        grid.Carts.Add(cart);
                    }
                }
            }
            return grid;
        }
    }
}
