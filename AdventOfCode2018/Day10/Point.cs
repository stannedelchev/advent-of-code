namespace AdventOfCode2018.Day10
{
    internal class Point
    {
        public Point(int x, int y, int dx, int dy)
        {
            this.X = x;
            this.Y = y;
            this.Dx = dx;
            this.Dy = dy;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }
    }
}
