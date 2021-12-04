using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day04
{
    internal class Problem : IProblem
    {
        public string Part1(string[] input)
        {
            var draws = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var boards = new List<List<List<int>>>();
            var board = new List<List<int>>();
            foreach (var line in input.Skip(2))
            {
                if (string.IsNullOrEmpty(line))
                {
                    boards.Add(board);
                    board = new List<List<int>>();
                    continue;
                }

                var row = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                board.Add(row);
            }
            boards.Add(board);

            foreach(var number in draws)
            {
            }

            return "";
        }

        public string Part2(string[] input)
        {
            return "";
        }
    }
}
