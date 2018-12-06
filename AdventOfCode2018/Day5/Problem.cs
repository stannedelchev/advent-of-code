using System;

namespace AdventOfCode2018.Day5
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var polymer = this.ReactPolymer(input[0]);
            return polymer.Length.ToString();
        }

        public string Part2(string[] input)
        {
            var polymer = input[0];
            var minLen = int.MaxValue;
            foreach(var ch in "abcdefghijklmnopqrstuvwxyz")
            {
                var len = this.ReactPolymer(polymer.Replace(ch.ToString(), "").Replace(char.ToUpper(ch).ToString(), "")).Length;
                if(len < minLen)
                {
                    minLen = len;
                }
            }

            return minLen.ToString();
        }

        private string ReactPolymer(string polymer)
        {
            for (var i = 0; i < polymer.Length - 1; i++)
            {
                if (char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1])
                    && (char.IsUpper(polymer[i]) && char.IsLower(polymer[i + 1])
                        || char.IsLower(polymer[i]) && char.IsUpper(polymer[i + 1])))
                {
                    polymer = polymer.Remove(i, 2);
                    i = Math.Max(-1, i - 2);
                }
            }

            return polymer;
        }
    }
}
