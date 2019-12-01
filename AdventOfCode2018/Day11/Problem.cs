using System.Threading.Tasks;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day11
{
    internal class Problem : IProblem
    {
        private const int ProblemGridSize = 300;

        public string Part1(string[] input)
        {
            var gridSerialNumber = int.Parse(input[0]);

            var grid = this.CreateSummedAreaTable(ProblemGridSize, gridSerialNumber);

            var largestTotalPower = this.CalculateTotalPower(grid, ProblemGridSize, 3);

            return $"{largestTotalPower.X + 1},{largestTotalPower.Y + 1}";
        }

        public string Part2(string[] input)
        {
            var gridSerialNumber = int.Parse(input[0]);
            var grid = this.CreateSummedAreaTable(ProblemGridSize, gridSerialNumber);

            var largestSquarePower = new TotalSquarePower();
            var lockObj = new object();

            Parallel.For(1, ProblemGridSize + 1, squareSize =>
            {
                var squarePower = this.CalculateTotalPower(grid, ProblemGridSize, squareSize);
                lock (lockObj)
                {
                    if (squarePower.Sum > largestSquarePower.Sum)
                    {
                        largestSquarePower = squarePower;
                    }
                }
            });

            var result = $"{largestSquarePower.X + 1},{largestSquarePower.Y + 1},{largestSquarePower.SquareSize}";
            return result;
        }

        private int[,] CreateSummedAreaTable(int gridSize, int gridSerialNumber)
        {
            // Include always-zero top and left edges.
            var extendedGrid = new int[gridSize + 1, gridSize + 1];
            for (var y = 1; y <= gridSize; y++)
            {
                for (var x = 1; x <= gridSize; x++)
                {
                    var power = this.CalculatePowerLevel(x, y, gridSerialNumber);
                    extendedGrid[y, x] = power + extendedGrid[y, x - 1] + extendedGrid[y - 1, x] - extendedGrid[y - 1, x - 1];
                }
            }

            return extendedGrid;
        }

        private TotalSquarePower CalculateTotalPower(int[,] extendedGrid, int gridSize, int squareSize)
        {
            var largestSum = 0;
            var largestX = 0;
            var largestY = 0;
            for (var y = 1; y <= gridSize - squareSize; y++)
            {
                for (var x = 1; x <= gridSize - squareSize; x++)
                {
                    var d = extendedGrid[y + squareSize, x + squareSize];
                    var a = extendedGrid[y, x];
                    var b = extendedGrid[y, x + squareSize];
                    var c = extendedGrid[y + squareSize, x];
                    var squarePower = d + a - b - c;

                    if (squarePower > largestSum)
                    {
                        largestSum = squarePower;
                        largestX = x;
                        largestY = y;
                    }
                }
            }

            return new TotalSquarePower() { X = largestX, Y = largestY, Sum = largestSum, SquareSize = squareSize };
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
}
