using AdventOfCode.Shared;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.Day12
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var moons = ParseMoons(input);
            var simulator = new MoonSimulator(moons);
            simulator.Simulate(1000);
            return simulator.TotalEnergy.ToString();
        }

        public string Part2(string[] input)
        {
            var moons = ParseMoons(input);
            var simulator = new MoonSimulator(moons);
            var ticks = 0L;
            var xCycles = 0L;
            var yCycles = 0L;
            var zCycles = 0L;

            var moonMappings = simulator.MoonsToOriginalMoons;

            while (true)
            {
                ticks++;

                simulator.Step();

                if (moons.All(m => Moon.XComparer.Equals(m, moonMappings[m]) && xCycles == 0))
                {
                    xCycles = ticks;
                }

                if (moons.All(m => Moon.YComparer.Equals(m, moonMappings[m]) && yCycles == 0))
                {
                    yCycles = ticks;
                }

                if (moons.All(m => Moon.ZComparer.Equals(m, moonMappings[m]) && zCycles == 0))
                {
                    zCycles = ticks;
                }

                if (xCycles * yCycles * zCycles != 0)
                {
                    var result = MathEx.LCM(new long[] { xCycles + 1, yCycles + 1, zCycles + 1 });
                    return result.ToString();
                }
            }
        }
        private static Moon[] ParseMoons(string[] input)
        {
            var parseRegex = new Regex("<x=(?<x>-?\\d+), y=(?<y>-?\\d+), z=(?<z>-?\\d+)");
            var moons = input.Select(l =>
            {
                var match = parseRegex.Match(l).Groups;
                var result = new Moon()
                {
                    X = int.Parse(match["x"].Value),
                    Y = int.Parse(match["y"].Value),
                    Z = int.Parse(match["z"].Value)
                };
                return result;
            }).ToArray();
            return moons;
        }
    }
}
