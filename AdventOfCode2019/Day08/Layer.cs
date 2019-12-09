using System;
using System.Text;

namespace AdventOfCode2019.Day08
{
    internal class Layer
    {
        public int[,] Data { get; set; }

        public int[] Digits { get; set; }

        public Layer(in int width, in int height)
        {
            this.Data = new int[height, width];
            this.Digits = new int[10];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var row = 0; row < this.Data.GetLength(0); row++)
            {
                for (var column = 0; column < this.Data.GetLength(1); column++)
                {
                    sb.Append(this.Data[row, column] == 1 ? "X" : " ");
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}