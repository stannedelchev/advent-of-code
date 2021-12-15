using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode.Shared;

namespace AdventOfCode2021.Day05
{
    internal class Problem : IProblem
    {
        private const int BoardRowLength = 1000;

        private static class Line
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static LineOrientation GetOrientation(int x1, int y1, int x2, int y2)
            {
                return x1 == x2 ? LineOrientation.Vertical
                                : y1 == y2 ? LineOrientation.Horizontal
                                           : LineOrientation.Diagonal;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static void Mark(ref int[] board, ref int overlappedPoints, int row, int column)
            {
                var index = row * BoardRowLength + column;
                var now = board[index];
                board[index] = now + 1;
                if (now == 1)
                {
                    overlappedPoints++;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static void HorizontalMark(int X1, int Y1, int X2, int Y2, ref int[] board, ref int overlappedPoints)
            {
                var x1 = Math.Min(X1, X2);
                var x2 = Math.Max(X1, X2);

                for (var x = x1; x <= x2; x++)
                {
                    Line.Mark(ref board, ref overlappedPoints, Y1, x);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static void VerticalMark(int X1, int Y1, int X2, int Y2, ref int[] board, ref int overlappedPoints)
            {
                var y1 = Math.Min(Y1, Y2);
                var y2 = Math.Max(Y1, Y2);
                for (var y = y1; y <= y2; y++)
                {
                    Line.Mark(ref board, ref overlappedPoints, y, X1);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static void DiagonalMark(int X1, int Y1, int X2, int Y2, ref int[] board, ref int overlappedPoints)
            {
                var xDelta = X2 > X1 ? 1 : -1;
                var yDelta = Y2 > Y1 ? 1 : -1;

                var x = X1;
                var y = Y1;
                do
                {
                    Line.Mark(ref board, ref overlappedPoints, y, x);
                    x += xDelta;
                    y += yDelta;
                } while (x != X2 && y != Y2);

                Line.Mark(ref board, ref overlappedPoints, y, x);
            }
        }

        private enum LineOrientation
        {
            Horizontal,
            Vertical,
            Diagonal,
        }

        public string Part1(string[] input)
        {
            var board = ArrayPool<int>.Shared.Rent(BoardRowLength * BoardRowLength);
            var overlappedPoints = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var line = input[i];
                ReadOnlySpan<char> span = line;
                var commaPos = line.IndexOf(',');
                var lastCommaPos = line.LastIndexOf(',');
                var x1 = int.Parse(span.Slice(0, commaPos));
                var y1 = int.Parse(span.Slice(commaPos + 1, 3));
                var x2 = int.Parse(span.Slice(lastCommaPos - 3, 3));
                var y2 = int.Parse(span.Slice(lastCommaPos + 1));

                var orientation = Line.GetOrientation(x1, y1, x2, y2);
                if (orientation == LineOrientation.Horizontal)
                {
                    Line.HorizontalMark(x1, y1, x2, y2, ref board, ref overlappedPoints);
                }
                else if (orientation == LineOrientation.Vertical)
                {
                    Line.VerticalMark(x1, y1, x2, y2, ref board, ref overlappedPoints);
                }
            }

            ArrayPool<int>.Shared.Return(board, true);
            return overlappedPoints.ToString();
        }

        private string PrintBoard(int[,] board)
        {
            var result = new StringBuilder();
            for (var row = 0; row < board.GetLength(1); row++)
            {
                for (var column = 0; column < board.GetLength(0); column++)
                {
                    const int space = 1;
                    result.Append(board[row, column] > 0 ? $"{board[row, column],space}" : $"{'.',space}");
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public string Part2(string[] input)
        {
            var board = ArrayPool<int>.Shared.Rent(BoardRowLength * BoardRowLength);
            var overlappedPoints = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var line = input[i];
                ReadOnlySpan<char> span = line;
                var commaPos = line.IndexOf(',');
                var lastCommaPos = line.LastIndexOf(',');
                var x1 = int.Parse(span.Slice(0, commaPos));
                var y1 = int.Parse(span.Slice(commaPos + 1, 3));
                var x2 = int.Parse(span.Slice(lastCommaPos - 3, 3));
                var y2 = int.Parse(span.Slice(lastCommaPos + 1));

                var orientation = Line.GetOrientation(x1, y1, x2, y2);

                if (orientation == LineOrientation.Horizontal)
                {
                    Line.HorizontalMark(x1, y1, x2, y2, ref board, ref overlappedPoints);
                }
                else if (orientation == LineOrientation.Vertical)
                {
                    Line.VerticalMark(x1, y1, x2, y2, ref board, ref overlappedPoints);
                }
                else
                {
                    Line.DiagonalMark(x1, y1, x2, y2, ref board, ref overlappedPoints);
                }
            }

            ArrayPool<int>.Shared.Return(board, true);
            return overlappedPoints.ToString();
        }
    }
}
