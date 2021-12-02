using System;
using System.Diagnostics;

namespace AdventOfCode.Shared
{
    public struct PartRun
    {
        public int Runs { get; set; }
        public TimeSpan AverageDuration => TotalDuration / Runs;
        public TimeSpan TotalDuration { get; set; }
        public TimeSpan MinimumDuration { get; set; }
        public TimeSpan MaximumDuration { get; set; }


        public decimal AverageDurationSeconds => (decimal)AverageDuration.Ticks / Stopwatch.Frequency;
        public decimal TotalDurationSeconds => (decimal)TotalDuration.Ticks / Stopwatch.Frequency;
        public decimal MinimumDurationSeconds => (decimal)MinimumDuration.Ticks / Stopwatch.Frequency;
        public decimal MaximumDurationSeconds => (decimal)MaximumDuration.Ticks / Stopwatch.Frequency;

        public string Result { get; set; }
    }
}
