using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day12
{
    internal class MoonSimulator
    {
        private readonly Moon[] moons;
        private readonly Moon[] originalMoons;
        private readonly MoonRelationship[] relationships;

        public MoonSimulator(Moon[] moons)
        {
            this.moons = moons;
            this.originalMoons = moons.Select(m => m.Clone()).ToArray();
            this.relationships = moons
                                 .Cartesian(moons, (m1, m2) => ReferenceEquals(m1, m2) ? null : new MoonRelationship(m1, m2))
                                 .Where(mr => mr != null)
                                 .Distinct()
                                 .ToArray();
            this.MoonsToOriginalMoons = moons.ToDictionary(m => m, m => this.OriginalMoons.Single(mm => mm.Equals(m)));
        }

        public IReadOnlyList<Moon> OriginalMoons => this.originalMoons;
        public IReadOnlyList<Moon> Moons => this.moons;
        public IDictionary<Moon, Moon> MoonsToOriginalMoons { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Simulate(int ticks)
        {
            for (var tick = 1; tick <= ticks; tick++)
            {
                this.Step();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Step()
        {
            for (var i = 0; i < this.relationships.Length; i++)
            {
                var moonRelationship = this.relationships[i];
                moonRelationship.AdjustGravity();
            }

            for (var i = 0; i < this.moons.Length; i++)
            {
                var moon = this.moons[i];
                moon.ApplyVelocity();
            }
        }

        public int TotalEnergy => this.moons.Sum(m => m.TotalEnergy);
    }
}