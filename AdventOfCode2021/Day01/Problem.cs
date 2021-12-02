using AdventOfCode.Shared;

namespace AdventOfCode2021.Day01
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var length = input.Length;
            var numbers = new int[length];
            for (var i = 0; i < length; i++)
            {
                numbers[i] = int.Parse(input[i]);
            }
            
            var count = 0;

            var previousNumber = numbers[0];
            for (var i = 1; i < length; i++)
            {
                var number = numbers[i];
                if (number > previousNumber)
                {
                    count++;
                }
                previousNumber = number;
            }

            return count.ToString();
        }

        public string Part2(string[] input)
        {
            var length = input.Length;
            var numbers = new int[length];
            for (var i = 0; i < length; i++)
            {
                numbers[i] = int.Parse(input[i]);
            }

            var count = 0;
            
            var lastSum = numbers[0] + numbers[1] + numbers[2];
            var end = length - 2;

            for(var i = 1; i < end; i++)
            {
                var previous = numbers[i - 1];
                var next = numbers[i + 2];
                var sum = lastSum - previous + next;
                if (lastSum < sum)
                {
                    count++;
                }
                lastSum = sum;
            }

            return count.ToString();
        }
    }
}
