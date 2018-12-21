using System;
using System.Linq;

namespace AdventOfCode2018.Day13
{
    class GridPrinter
    {
        private readonly Grid grid;

        public GridPrinter(Grid grid)
        {
            this.grid = grid;
        }

        public char DisplaySymbol(int row, int column)
        {
            var road = this.grid.Roads[row, column];
            var cart = this.grid.Carts.FirstOrDefault(c => c.Y == row && c.X == column);
            if (cart != null)
            {
                return cart.FacingDirectionChar;
            }

            switch (road)
            {
                case Road.Empty:
                    return ' ';
                case Road.StraightUpDown:
                    return '|';
                case Road.StraightLeftRight:
                    return '-';
                case Road.Crossroad:
                    return '+';
                case Road.TurnBottomLeftTopRight:
                    return '/';
                case Road.TurnTopLeftBottomRight:
                    return '\\';
                default:
                    throw new InvalidOperationException("Cannot return visual for unknown road type");
            }
        }

        public void Print()
        {
            for (var row = 0; row < this.grid.Rows; row++)
            {
                for (var column = 0; column < this.grid.Columns; column++)
                {
                    Console.Write(this.DisplaySymbol(row, column));
                }

                Console.WriteLine();
            }
        }
    }
}
