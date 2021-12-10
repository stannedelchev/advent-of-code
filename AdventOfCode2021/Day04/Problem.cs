using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day04
{
    internal class Problem : IProblem
    {
        private static class Board
        {
            public const int IsBingoIndex = 0;
            public const int IsBingoValue = 1;
            public const int RowMarksIndex = IsBingoIndex + 1;
            public const int ColumnMarksIndex = RowMarksIndex + Size;
            public const int DataIndex = ColumnMarksIndex + Size;
            public const int DataLength = 1 + Size + Size + Size * Size;
            public const int Size = 5;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool TryMark(ref int[] board, int draw)
            {
                if (board[IsBingoIndex] == IsBingoValue)
                {
                    return false;
                }

                var index = Array.IndexOf(board, draw, DataIndex);
                if (index == -1)
                {
                    return false;
                }
                index -= DataIndex;

                var row = index / Size;
                var column = index - (row * Size);

                board[DataIndex + index] = -1;

                board[RowMarksIndex + row]++;
                board[ColumnMarksIndex + column]++;

                if (board[RowMarksIndex + row] == Size ||
                    board[ColumnMarksIndex + column] == Size)
                {
                    board[IsBingoIndex] = IsBingoValue;
                    return true;
                }

                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static int SumUnmarkedNumbers(ref int[] board)
            {
                var sum = 0;
                var length = board.Length;
                for (var i = DataIndex; i < length; i++)
                {
                    if (board[i] != -1)
                    {
                        sum += board[i];
                    }
                }
                return sum;
            }
        }

        public string Part1(string[] input)
        {
            var draws = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var boards = new List<int[]>(input.Length);
            {
                var board = new int[Board.DataLength];
                var rowIndex = 0;
                for (var lineIndex = 2; lineIndex < input.Length; lineIndex++)
                {
                    var line = input[lineIndex];
                    if (string.IsNullOrEmpty(line))
                    {
                        boards.Add(board);
                        board = new int[Board.DataLength];
                        rowIndex = 0;
                        continue;
                    }

                    var row = new int[Board.Size];
                    {
                        for (var i = 0; i < 5; i++)
                        {
                            var digit = line[i * 3 + 1] - '0';
                            if (line[i * 3] != ' ')
                            {
                                digit += 10 * (line[i * 3] - '0');
                            }
                            row[i] = digit;
                        }
                    }

                    for (var i = 0; i < Board.Size; i++)
                    {
                        board[Board.DataIndex + rowIndex * Board.Size + i] = row[i];
                    }

                    rowIndex++;
                }

                boards.Add(board);
            }

            for (var drawIndex = 0; drawIndex < draws.Length; drawIndex++)
            {
                var draw = draws[drawIndex];
                for (var i = 0; i < boards.Count; i++)
                {
                    var board = boards[i];
                    var bingo = Board.TryMark(ref board, draw);
                    if (bingo)
                    {
                        var sum = Board.SumUnmarkedNumbers(ref board);
                        return (sum * draw).ToString();
                    }
                }
            }

            throw new NotImplementedException();
        }

        public string Part2(string[] input)
        {
            var draws = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var boards = new List<int[]>(input.Length);
            {
                var board = new int[Board.DataLength];
                var rowIndex = 0;
                for (var lineIndex = 2; lineIndex < input.Length; lineIndex++)
                {
                    var line = input[lineIndex];
                    if (string.IsNullOrEmpty(line))
                    {
                        boards.Add(board);
                        board = new int[Board.DataLength];
                        rowIndex = 0;
                        continue;
                    }

                    var row = new int[Board.Size];
                    {
                        for (var i = 0; i < 5; i++)
                        {
                            var digit = line[i * 3 + 1] - '0';
                            if (line[i * 3] != ' ')
                            {
                                digit += 10 * (line[i * 3] - '0');
                            }
                            row[i] = digit;
                        }
                    }

                    for (var i = 0; i < Board.Size; i++)
                    {
                        board[Board.DataIndex + rowIndex * Board.Size + i] = row[i];
                    }

                    rowIndex++;
                }

                boards.Add(board);
            }

            int[] lastWinningBoard = Array.Empty<int>();
            int lastWinningDraw = 0;

            for (var drawIndex = 0; drawIndex < draws.Length; drawIndex++)
            {
                var draw = draws[drawIndex];
                for (var i = 0; i < boards.Count; i++)
                {
                    var board = boards[i];
                    var bingo = Board.TryMark(ref board, draw);
                    if (bingo)
                    {
                        lastWinningBoard = board;
                        lastWinningDraw = draw;
                    }
                }
            }

            var sum = Board.SumUnmarkedNumbers(ref lastWinningBoard);
            return (sum * lastWinningDraw).ToString();

        }
    }
}
