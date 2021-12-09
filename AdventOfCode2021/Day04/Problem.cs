using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day04
{
    internal class Problem : IProblem
    {
        class Board
        {
            private readonly int[] _data;

            public Board(int[] data)
            {
                _data = data;
                RowMarks = new int[Size];
                ColumnMarks = new int[Size];
            }

            public int[] RowMarks { get; }
            public int[] ColumnMarks { get; }

            public const int Size = 5;

            public bool IsBingo { get; private set; }

            public bool TryMark(int draw)
            {
                if (IsBingo)
                {
                    return false;
                }

                var index = Array.IndexOf(_data, draw);
                if (index == -1)
                {
                    return false;
                }

                var row = index / Size;
                var column = index - (row * Size);

                _data[index] = -1;

                RowMarks[row]++;
                ColumnMarks[column]++;

                if (RowMarks[row] == Size || ColumnMarks[column] == Size)
                {
                    IsBingo= true;
                    return true;
                }

                return false;
            }

            public int SumUnmarkedNumbers()
            {
                var sum = 0;
                for (int i = 0; i < _data.Length; i++)
                {
                    if (_data[i] != -1)
                    {
                        sum += _data[i];
                    }
                }
                return sum;
            }
        }

        public string Part1(string[] input)
        {
            var draws = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var boards = new List<Board>();
            {
                var board = new int[Board.Size * Board.Size];
                var rowIndex = 0;
                foreach (var line in input.Skip(2))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        boards.Add(new Board((int[])board.Clone()));
                        Array.Clear(board);
                        rowIndex = 0;
                        continue;
                    }

                    var row = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    for (var i = 0; i < Board.Size; i++)
                    {
                        board[rowIndex * Board.Size + i] = row[i];
                    }

                    rowIndex++;
                }

                boards.Add(new Board((int[])board.Clone()));
            }

            foreach (var draw in draws)
            {
                foreach (var board in boards)
                {
                    var bingo = board.TryMark(draw);
                    if (bingo)
                    {
                        var sum = board.SumUnmarkedNumbers();
                        return (sum * draw).ToString();
                    }
                }
            }

            throw new NotImplementedException();
        }

        public string Part2(string[] input)
        {
            var draws = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var boards = new List<Board>();
            {
                var board = new int[Board.Size * Board.Size];
                var rowIndex = 0;
                foreach (var line in input.Skip(2))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        boards.Add(new Board((int[])board.Clone()));
                        Array.Clear(board);
                        rowIndex = 0;
                        continue;
                    }

                    var row = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    for (var i = 0; i < Board.Size; i++)
                    {
                        board[rowIndex * Board.Size + i] = row[i];
                    }

                    rowIndex++;
                }
                boards.Add(new Board((int[])board.Clone()));
            }

            var lastWinningBoard = boards[0];
            var lastWinningDraw = 0;
            foreach (var draw in draws)
            {
                foreach (var board in boards)
                {
                    var bingo = board.TryMark(draw);
                    if (bingo)
                    {
                        lastWinningBoard = board;
                        lastWinningDraw = draw;
                    }
                }
            }

            var sum = lastWinningBoard.SumUnmarkedNumbers();
            return (sum * lastWinningDraw).ToString();
        }
    }
}
