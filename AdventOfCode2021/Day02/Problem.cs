using AdventOfCode.Shared;

namespace AdventOfCode2021.Day02
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            int x = 0;
            int depth = 0;

            foreach (var line in input)
            {
                switch (line[0])
                {
                    case 'f':
                        x += int.Parse(line.Substring(8));
                        break;
                    case 'u':
                        depth -= int.Parse(line.Substring(3));
                        break;
                    case 'd':
                        depth += int.Parse(line.Substring(5));
                        break;

                }
            }

            return (x * depth).ToString();
        }

        public string Part2(string[] input)
        {
            int x = 0;
            int depth = 0;
            int aim = 0;

            foreach (var line in input)
            {
                switch (line[0])
                {
                    case 'f':
                        var delta = int.Parse(line.Substring(8));
                        x += delta;
                        depth += aim * delta;
                        break;
                    case 'u':
                        aim -= int.Parse(line.Substring(3));
                        break;
                    case 'd':
                        aim += int.Parse(line.Substring(5));
                        break;

                }
            }

            return (x * depth).ToString();
        }
    }
}
