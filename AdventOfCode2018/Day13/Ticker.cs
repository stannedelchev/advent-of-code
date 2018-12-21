using System.Collections.Generic;

namespace AdventOfCode2018.Day13
{
    internal class Ticker
    {
        private readonly Grid grid;

        public Ticker(Grid grid)
        {
            this.grid = grid;
        }

        public int Ticks { get; private set; }

        public IReadOnlyList<Crash> Tick(bool removeCartsOnCrashes)
        {
            var crashes = this.grid.MoveCarts(removeCartsOnCrashes);
            this.Ticks++;
            return crashes;
        }
    }
}
