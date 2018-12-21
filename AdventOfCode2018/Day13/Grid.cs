using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day13
{
    internal class Grid
    {
        public Grid(int rows, int columns)
        {
            this.Roads = new Road[rows, columns];
            this.Rows = rows;
            this.Columns = columns;
            this.Carts = new List<Cart>();
        }

        public Road[,] Roads { get; }
        public int Rows { get; }
        public int Columns { get; }
        public IList<Cart> Carts { get; }

        public IReadOnlyList<Crash> MoveCarts(bool removeCartsOnCrashes)
        {
            var carts = this.Carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();
            var result = new List<Crash>();
            foreach (var cart in carts)
            {
                cart.Move();

                var crashes = carts.GroupBy(c => (c.X, c.Y))
                    .Where(g => g.Count() > 1)
                    .Select(g => new Crash(g.Key.X, g.Key.Y, g.ToList()))
                    .ToList();

                foreach (var crash in crashes)
                {
                    if (removeCartsOnCrashes)
                    {
                        this.Carts.Remove(crash.Participants[0]);
                        this.Carts.Remove(crash.Participants[1]);
                    }
                }

                result.AddRange(crashes);
            }

            return result;
        }
    }
}
