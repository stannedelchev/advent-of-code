using System.Linq;
using System.Text;

namespace AdventOfCode2018.Day2
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            (int Twos, int Threes) result = (0, 0);
            var res = input.Aggregate(result, (acc, l) =>
             {
                 var characterGroups = l.GroupBy(c => c);
                 acc.Twos += characterGroups.Any(g => g.Count() == 2) ? 1 : 0;
                 acc.Threes += characterGroups.Any(g => g.Count() == 3) ? 1 : 0;
                 return acc;
             });

            return $"{res.Twos * res.Threes}";
        }

        public string Part2(string[] input)
        {
            var maxDifferenceSoFar = int.MaxValue;
            (string First, string Second) correctBoxIds = (string.Empty, string.Empty);
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = i + 1; j < input.Length; j++)
                {
                    var firstWord = input[i];
                    var secondWord = input[j];
                    var differences = 0;
                    for (var ci = 0; ci < firstWord.Length; ci++)
                    {
                        if (firstWord[ci] != secondWord[ci])
                        {
                            differences++;
                        }
                    }

                    if (differences < maxDifferenceSoFar)
                    {
                        maxDifferenceSoFar = differences;
                        correctBoxIds = (firstWord, secondWord);
                    }
                }
            }

            var result = string.Empty;
            for (int i = 0; i < correctBoxIds.First.Length; i++)
            {
                if (correctBoxIds.First[i] == correctBoxIds.Second[i])
                {
                    result += correctBoxIds.First[i];
                }
            }

            return result;
        }
    }
}
