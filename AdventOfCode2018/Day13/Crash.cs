using System.Collections.Generic;

namespace AdventOfCode2018.Day13
{
    internal class Crash
    {
        public Crash(int x, int y, IReadOnlyList<Cart> participators)
        {
            this.X = x;
            this.Y = y;
            this.Participants = participators;
        }

        public int X { get; }
        public int Y { get; }
        public IReadOnlyList<Cart> Participants { get; }
    }
}
