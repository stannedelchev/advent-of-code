using System.Collections.Generic;
using AdventOfCode.Shared;

namespace AdventOfCode2018.Day5
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var polymer = this.ReactPolymer(input[0]);
            return polymer.ToString();
        }

        public string Part2(string[] input)
        {
            var polymer = input[0];
            var minLen = int.MaxValue;
            foreach (var ch in "abcdefghijklmnopqrstuvwxyz")
            {
                var len = this.ReactPolymer(polymer.Replace(ch.ToString(), "").Replace(char.ToUpper(ch).ToString(), ""));
                if (len < minLen)
                {
                    minLen = len;
                }
            }

            return minLen.ToString();
        }

        private int ReactPolymer(string polymer)
        {
            var polymerLength = polymer.Length;
            var stack = new Stack<char>();

            for(int polymerIndex = 0; polymerIndex < polymerLength; polymerIndex++)
            {
                var nextChar = polymer[polymerIndex];

                if (stack.Count == 0)
                {
                    stack.Push(nextChar);
                    continue;
                }

                var peekedChar = stack.Peek();
                if (peekedChar == nextChar)
                {
                    stack.Push(nextChar);
                    continue;
                }

                var charDifference = nextChar - peekedChar;
                if (charDifference == 32 || charDifference == -32)
                {
                    stack.Pop();
                    continue;
                }

                stack.Push(nextChar);
            }

            return stack.Count;
        }
    }
}
