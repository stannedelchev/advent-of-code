using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        private static List<Asteroid> ParseAsteroids(in IReadOnlyList<string> input, out Asteroid asteroidWithMostVisibleOthers)
        {
            var asteroids = new List<Asteroid>(1024);

            for (var row = input.Count - 1; row >= 0; row--)
            {
                for (var column = 0; column < input[row].Length; column++)
                {
                    var x = column;
                    var y = input.Count - 1 - row;
                    if (input[row][column] == '#')
                    {
                        asteroids.Add(new Asteroid(new Point(x, y), new Point(x, row)));
                    }
                }
            }

            asteroidWithMostVisibleOthers = asteroids[0];
            foreach (var asteroid in asteroids)
            {
                foreach (var asteroid1 in asteroids)
                {
                    if (asteroid.Equals(asteroid1))
                    {
                        continue;
                    }

                    var dx = asteroid1.Position.X - asteroid.Position.X;
                    var dy = asteroid1.Position.Y - asteroid.Position.Y;
                    var angle = Math.Atan2(dy, dx);

                    var asteroidRelationship = new AsteroidRelationship
                    {
                        RadianAngleFromOriginToDestination = angle,
                        Origin = asteroid,
                        Destination = asteroid1,
                        Distance = Math.Sqrt(Math.Pow(asteroid.Position.X - asteroid1.Position.X, 2) +
                                             Math.Pow(asteroid.Position.Y - asteroid1.Position.Y, 2))
                    };

                    asteroid.VisibleAsteroidAngles.Add(asteroidRelationship);
                    asteroid.AllAsteroidAngles.Add(asteroidRelationship);

                    if (asteroid.VisibleAsteroidAngles.Count >
                        asteroidWithMostVisibleOthers.VisibleAsteroidAngles.Count)
                    {
                        asteroidWithMostVisibleOthers = asteroid;
                    }
                }
            }

            return asteroids;
        }
    }
}