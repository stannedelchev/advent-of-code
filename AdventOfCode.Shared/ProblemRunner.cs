using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;

namespace AdventOfCode.Shared
{
    public class ProblemRunner
    {
        private readonly IEnumerable<IProblem> problems;

        public ProblemRunner(IEnumerable<IProblem> problems, int partRuns = 1, bool gcBetweenRuns = false, bool gcBetweenParts = false)
        {
            this.problems = problems;
            PartRuns = partRuns;
            GcBetweenRuns = gcBetweenRuns;
            GcBetweenParts = gcBetweenParts;
        }

        public int PartRuns { get; }
        public bool GcBetweenRuns { get; }
        public bool GcBetweenParts { get; }

        public void Run()
        {
            var wallClock = Stopwatch.StartNew();

            static string PrintPart(string day, int partNumber, PartRun part, TimeSpan wallTimeSoFar)
            {
                return $"{day}.{partNumber} - {part.Result} in {part.AverageDurationSeconds:N8}s " +
                    $"| Minimum: {part.MinimumDurationSeconds:N8} " +
                    $"| Maximum: {part.MaximumDurationSeconds:N8} " +
                    $"| Total time: {part.TotalDurationSeconds:N8}s " +
                    $"| Cumulative wall time: {wallTimeSoFar.TotalSeconds}";
            }

            foreach (var problem in this.problems)
            {
                var day = problem.GetType().Namespace!.Split(".", StringSplitOptions.RemoveEmptyEntries).Last();
                var input = File.ReadAllLines($"{day}\\input.txt");

                if (GcBetweenParts)
                {
                    Gc();
                }
                var part1 = RunPart(input, problem.Part1, PartRuns, GcBetweenRuns);
                Console.WriteLine(PrintPart(day, 1, part1, wallClock.Elapsed));

                if (GcBetweenParts)
                {
                    Gc();
                }
                var part2 = RunPart(input, problem.Part2, PartRuns, GcBetweenRuns);
                Console.WriteLine(PrintPart(day, 2, part2, wallClock.Elapsed));
            }
        }

        private static PartRun RunPart(string[] input, Func<string[], string> part, int runs = 1, bool gcBetweenRuns = false)
        {
            var partResult = string.Empty;
            var totalDuration = TimeSpan.Zero;
            var minimumDuration = TimeSpan.MaxValue;
            var maximumDuration = TimeSpan.Zero;

            for (var i = 0; i < runs; i++)
            {
                if (gcBetweenRuns)
                {
                    Gc();
                }

                var partWatch = Stopwatch.StartNew();
                partResult = part(input);
                partWatch.Stop();

                totalDuration += partWatch.Elapsed;

                if (partWatch.Elapsed < minimumDuration)
                {
                    minimumDuration = partWatch.Elapsed;
                }

                if (partWatch.Elapsed > maximumDuration)
                {
                    maximumDuration = partWatch.Elapsed;
                }
            }

            return new PartRun
            {
                Runs = runs,
                TotalDuration = totalDuration,
                MinimumDuration = minimumDuration,
                MaximumDuration = maximumDuration,
                Result = partResult
            };
        }

        private static void Gc()
        {
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
