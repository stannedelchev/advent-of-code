using System;

namespace AdventOfCode2018.Day13
{
    internal class RoadFactory
    {
        private readonly Grid grid;

        public RoadFactory(Grid grid)
        {
            this.grid = grid;
        }

        public Road Create(char text, out Cart cart, int row, int column)
        {
            cart = null;
            switch (text)
            {
                case '|': return Road.StraightUpDown;
                case '-': return Road.StraightLeftRight;
                case '/': return Road.TurnBottomLeftTopRight;
                case '\\': return Road.TurnTopLeftBottomRight;
                case '+': return Road.Crossroad;
                case '<':
                    cart = new Cart(this.grid, FacingDirection.Left, column, row);
                    return Road.StraightLeftRight;
                case '>':
                    cart = new Cart(this.grid, FacingDirection.Right, column, row);
                    return Road.StraightLeftRight;
                case '^':
                    cart = new Cart(this.grid, FacingDirection.Up, column, row);
                    return Road.StraightUpDown;
                case 'v':
                    cart = new Cart(this.grid, FacingDirection.Down, column, row);
                    return Road.StraightUpDown;
                case ' ': return Road.Empty;
                default:
                    throw new InvalidOperationException("Unknown character for road");
            }
        }

    }
}
