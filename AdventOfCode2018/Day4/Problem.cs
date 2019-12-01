using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day4
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var guardSleepPeriods = CreateGuardSleepPeriods(ref input);

            var sleepiestGuardKvp = guardSleepPeriods.OrderByDescending(kvp => kvp.Value.Sum()).First();

            var sleepiestGuard = sleepiestGuardKvp.Key;
            var minuteMostSlept = sleepiestGuardKvp.Value.ToList().IndexOf(sleepiestGuardKvp.Value.Max());
            var result = sleepiestGuard * minuteMostSlept;
            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var guardSleepPeriods = CreateGuardSleepPeriods(ref input);
            var guardId = -1;
            var minuteIndex = -1;
            var maxMinuteValue = -1;

            foreach(var kvp in guardSleepPeriods)
            {
                for (int j = 0; j < 60; j++)
                {
                    if (maxMinuteValue < kvp.Value[j])
                    {
                        maxMinuteValue = kvp.Value[j];
                        guardId = kvp.Key;
                        minuteIndex = j;
                    }
                }

            }
            var result = minuteIndex * guardId;
            return result.ToString();
        }

        private Dictionary<int, int[]> CreateGuardSleepPeriods(ref string[] input)
        {
            input = input.OrderBy(l => l).ToArray();

            var currentGuardId = 0;

            var guardSleepPeriods = new Dictionary<int, int[]>();

            var currentSleepStart = -1;
            var currentSleepEnd = -1;
            for (var i = 0; i < input.Length; i++)
            {
                var line = input[i];
                var regex = new Regex(@"\[(?<date>\d{4}-\d{2}-\d{2} \d{2}:\d{2})\] (?<event>.*)");
                var match = regex.Match(line);
                var date = DateTime.Parse(match.Groups["date"].Value);
                var type = match.Groups["event"].Value;

                if (type.Contains("Guard"))
                {
                    var rg = new Regex(@"Guard #(?<id>\d*)");
                    currentGuardId = int.Parse(rg.Match(type).Groups["id"].Value);
                    guardSleepPeriods.TryAdd(currentGuardId, new int[60]);
                    currentSleepStart = -1;
                    currentSleepEnd = -1;
                }

                if (type.Contains("wake"))
                {
                    currentSleepEnd = date.Minute;
                    for (var minute = currentSleepStart; minute < currentSleepEnd; minute++)
                    {
                        guardSleepPeriods[currentGuardId][minute]++;
                    }
                }

                if (type.Contains("falls"))
                {
                    currentSleepStart = date.Minute;
                }
            }

            return guardSleepPeriods;
        }
    }
}
