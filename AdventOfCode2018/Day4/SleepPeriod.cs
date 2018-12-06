namespace AdventOfCode2018.Day4
{
    internal class SleepPeriod
    {
        public SleepPeriod()
        {

        }

        public SleepPeriod(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public int Start { get; set; }
        public int End { get; set; }
    }
}
