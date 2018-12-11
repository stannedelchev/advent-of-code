using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day11
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var gridSerialNumber = int.Parse(input[0]);
            const int GridSize = 300;
            var grid = new int[GridSize, GridSize];
            for (var y = 1; y <= GridSize; y++)
            {
                for (var x = 1; x <= GridSize; x++)
                {
                    var powerLevel = this.CalculatePowerLevel(x, y, gridSerialNumber);
                    grid[x - 1, y - 1] = powerLevel;
                }
            }

            (long sum, int x, int y) largestTotalPower = (0, 0, 0);

            const int SquareSize = 3;
            for (var y = 0; y < GridSize - SquareSize; y++)
            {
                for (var x = 0; x < GridSize - SquareSize; x++)
                {
                    var sum = 0L;
                    for (var dy = 0; dy < SquareSize; dy++)
                    {
                        for (var dx = 0; dx < SquareSize; dx++)
                        {
                            sum += grid[x + dx, y + dy];
                        }
                    }

                    if (sum > largestTotalPower.sum)
                    {
                        largestTotalPower = (sum, x, y);
                    }
                }
            }

            var result = $"{largestTotalPower.x + 1},{largestTotalPower.y + 1}";
            return result;
        }

        public string Part2(string[] input)
        {
            var gridSerialNumber = int.Parse(input[0]);
            const int GridSize = 300;
            var grid = new int[GridSize, GridSize];
            for (var y = 1; y <= GridSize; y++)
            {
                for (var x = 1; x <= GridSize; x++)
                {
                    var powerLevel = this.CalculatePowerLevel(x, y, gridSerialNumber);
                    grid[x - 1, y - 1] = powerLevel;
                }
            }

            var allTotalPowers = new ConcurrentQueue<TotalPower>();

            Parallel.For(1, GridSize + 1, (squareSize) =>
            {
                var largestPowerForSquare = new TotalPower()
                {
                    SquareSize = squareSize
                };

                for (var y = 0; y <= GridSize - squareSize; y++)
                {
                    for (var x = 0; x <= GridSize - squareSize; x++)
                    {
                        var sum = 0L;
                        for (var dy = 0; dy < squareSize; dy++)
                        {
                            for (var dx = 0; dx < squareSize; dx++)
                            {
                                sum += grid[x + dx, y + dy];
                            }
                        }

                        if (sum > largestPowerForSquare.Sum)
                        {
                            largestPowerForSquare.X = x;
                            largestPowerForSquare.Y = y;
                            largestPowerForSquare.Sum = sum;
                        }
                    }
                }

                allTotalPowers.Enqueue(largestPowerForSquare);
            });

            var maxTotalPower = allTotalPowers.OrderByDescending(p => p.Sum).First();
            var result = $"{maxTotalPower.X + 1},{maxTotalPower.Y + 1},{maxTotalPower.SquareSize}";
            return result;
        }

        private int CalculatePowerLevel(int x, int y, int gridSerialNumber)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += gridSerialNumber;
            powerLevel *= rackId;
            powerLevel = (powerLevel / 100) % 10;
            powerLevel -= 5;
            return powerLevel;
        }
    }

    public struct TotalPower
    {
        public long Sum { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int SquareSize { get; set; }
    }
}
