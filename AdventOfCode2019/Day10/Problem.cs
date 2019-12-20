using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day10
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var asteroids = ParseAsteroids(input, out var asteroidWithMostVisibleOthers);
            return asteroidWithMostVisibleOthers.VisibleAsteroidAngles.Count.ToString();
        }

        public string Part2(string[] input)
        {
            var asteroids = ParseAsteroids(input, out var asteroidWithMostVisibleOthers);
            var result = asteroidWithMostVisibleOthers.VisibleAsteroidAngles.OrderBy(kvp => kvp, new AsteroidComparer()).ToArray();

            return $"{result[199].Destination.OriginalPosition.X * 100 + result[199].Destination.OriginalPosition.Y}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static List<Asteroid> ParseAsteroids(in IReadOnlyList<string> input, out Asteroid asteroidWithMostVisibleOthers)
        {
            var asteroids = new List<Asteroid>(1024);

            for (var row = input.Count - 1; row >= 0; row--)
            {
                for (var column = 0; column < input[row].Length; column++)
                {
                    var y = input.Count - 1 - row;
                    if (input[row][column] == '#')
                    {
                        asteroids.Add(new Asteroid(new Point(column, y), new Point(column, row)));
                    }
                }
            }

            asteroidWithMostVisibleOthers = asteroids[0];
            var asteroidsCount = asteroids.Count;
            for (var i = 0; i < asteroidsCount; i++)
            {
                var origin = asteroids[i];
                for (var j = 0; j < asteroidsCount; j++)
                {
                    var destination = asteroids[j];
                    if (origin.Equals(destination))
                    {
                        continue;
                    }

                    var asteroidPosition = origin.Position;
                    var asteroid1Position = destination.Position;
                    var asteroidX = asteroidPosition.X;
                    var asteroidY = asteroidPosition.Y;
                    var asteroid1X = asteroid1Position.X;
                    var asteroid1Y = asteroid1Position.Y;

                    var dx = asteroid1X - asteroidX;
                    var dy = asteroid1Y - asteroidY;
                    var angle = Math.Atan2(dy, dx);

                    var asteroidRelationship = new AsteroidRelationship(origin, destination, angle,
                                                                        Math.Sqrt(Math.Pow(asteroidX - asteroid1X, 2) +
                                                                                  Math.Pow(asteroidY - asteroid1Y, 2)));

                    origin.VisibleAsteroidAngles.Add(asteroidRelationship);

                    if (origin.VisibleAsteroidAngles.Count > asteroidWithMostVisibleOthers.VisibleAsteroidAngles.Count)
                    {
                        asteroidWithMostVisibleOthers = origin;
                    }
                }
            }

            return asteroids;
        }
    }
}