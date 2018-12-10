namespace AdventOfCode2018.Day9
{
    internal class Player
    {
        public Player(int number)
        {
            this.Number = number;
        }

        public int Number { get; }

        public long Score { get; private set; }

        public void PutMarble(int marble, MarbleCircle circle)
        {
            if (marble % 23 != 0)
            {
                circle.PutTwoAfterCurrent(marble);
            }
            else
            {
                this.Score += marble + circle.RemoveSeventhCounterClockwise();
            }
        }
    }
}
