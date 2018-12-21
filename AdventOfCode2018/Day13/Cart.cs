using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day13
{
    internal class Cart
    {
        private readonly Grid roadGrid;
        private FacingDirection facingDirection;
        private int crossroadTurns = 0;

        public Cart(Grid roadGrid, FacingDirection facingDirection, int x, int y)
        {
            this.roadGrid = roadGrid;
            this.facingDirection = facingDirection;
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char FacingDirectionChar
        {
            get
            {
                var directionChars = new Dictionary<FacingDirection, char>
                {
                    { FacingDirection.Left, '<' },
                    { FacingDirection.Up, '^' },
                    { FacingDirection.Right, '>' },
                    { FacingDirection.Down, 'v' },
                };
                return directionChars[this.facingDirection];
            }
        }

        public void Move()
        {
            var turnOptions = new Action[]
            {
                this.TurnLeft,
                () => { },
                this.TurnRight
            };

            this.MoveForward();

            switch (this.roadGrid.Roads[this.Y, this.X])
            {
                case Road.Crossroad:
                    turnOptions[this.crossroadTurns % 3]();
                    this.crossroadTurns++;
                    break;
                case Road.Empty:
                    throw new InvalidOperationException("Cart should not be outside a road.");
                case Road.StraightLeftRight:
                case Road.StraightUpDown:
                    break;
                case Road.TurnBottomLeftTopRight:
                    switch (this.facingDirection)
                    {
                        case FacingDirection.Left:
                        case FacingDirection.Right:
                            this.TurnLeft();
                            break;
                        case FacingDirection.Up:
                        case FacingDirection.Down:
                            this.TurnRight();
                            break;
                        default:
                            throw new InvalidOperationException("Cannot turn from an unknown direction");
                    }
                    break;
                case Road.TurnTopLeftBottomRight:
                    switch (this.facingDirection)
                    {
                        case FacingDirection.Left:
                        case FacingDirection.Right:
                            this.TurnRight();
                            break;
                        case FacingDirection.Up:
                        case FacingDirection.Down:
                            this.TurnLeft();
                            break;
                        default:
                            throw new InvalidOperationException("Cannot turn from an unknown direction");
                    }
                    break;
            }
        }

        private void TurnLeft()
        {
            var directionsMap = new Dictionary<FacingDirection, FacingDirection> {
                { FacingDirection.Left ,FacingDirection.Down },
                { FacingDirection.Up,FacingDirection.Left },
                { FacingDirection.Right,FacingDirection.Up },
                { FacingDirection.Down,FacingDirection.Right },
            };

            this.facingDirection = directionsMap[this.facingDirection];
        }

        private void TurnRight()
        {
            var directionsMap = new Dictionary<FacingDirection, FacingDirection> {
                { FacingDirection.Left ,FacingDirection.Up },
                { FacingDirection.Up,FacingDirection.Right },
                { FacingDirection.Right,FacingDirection.Down },
                { FacingDirection.Down,FacingDirection.Left },
            };

            this.facingDirection = directionsMap[this.facingDirection];
        }

        private void MoveForward()
        {
            switch (this.facingDirection)
            {
                case FacingDirection.Left:
                    this.X--;
                    break;
                case FacingDirection.Up:
                    this.Y--;
                    break;
                case FacingDirection.Right:
                    this.X++;
                    break;
                case FacingDirection.Down:
                    this.Y++;
                    break;
                default:
                    throw new InvalidOperationException("Cart cannot head in an undefined direction");
            }
        }
    }
}
