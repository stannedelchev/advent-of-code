using AdventOfCode2019.Intcode;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode2019.Day11
{
    internal class RobotPainter
    {
        public Dictionary<Point, int> Grid { get; }
        private readonly IntCodeComputer computer;

        private int direction = 0;
        private int outputsCounted = 0;

        private int x;
        private int y;


        public RobotPainter(string program)
        {
            this.Grid = new Dictionary<Point, int>();
            this.computer = new IntCodeComputer();

            this.computer.Initialize(this.computer.CreateProgram(program));
            this.computer.Output += this.OnOutputReceived;
        }

        public void Run()
        {
            this.x = 0;
            this.y = 0;

            do
            {
                if (!this.Grid.TryGetValue(new Point(this.x, this.y), out var panelPaint))
                {
                    panelPaint = 0;
                }

                this.computer.Input.Enqueue(panelPaint);
                this.computer.ExecuteProgram();
            } while (this.computer.State != IntCodeComputerState.Halted);
        }

        private void OnOutputReceived(object sender, long value)
        {
            this.outputsCounted++;
            switch (this.outputsCounted)
            {
                case 1:
                    this.Grid[new Point(this.x, this.y)] = (int)value;
                    break;
                case 2:
                    this.outputsCounted = 0;
                    this.direction = value == 0 ? this.direction - 1 : this.direction + 1;
                    switch (this.direction)
                    {
                        case -1:
                            this.direction = 3;
                            break;
                        case 4:
                            this.direction = 0;
                            break;
                    }

                    switch (this.direction)
                    {
                        case 0:
                            this.y -= 1;
                            break;
                        case 1:
                            this.x += 1;
                            break;
                        case 2:
                            this.y += 1;
                            break;
                        case 3:
                            this.x -= 1;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
            }
        }
    }
}
