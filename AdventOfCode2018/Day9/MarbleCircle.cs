using System.Collections.Generic;

namespace AdventOfCode2018.Day9
{
    internal class MarbleCircle
    {
        private readonly LinkedList<int> marbles = new LinkedList<int>();

        public LinkedListNode<int> currentMarbleNode;

        private int currentMarble;

        public void PutTwoAfterCurrent(int marble)
        {
            if (this.marbles.Count <= 1)
            {
                this.marbles.AddLast(marble);
                this.currentMarble = marble;
                this.currentMarbleNode = this.marbles.Last;
                return;
            }

            if (this.marbles.Last.Value == this.currentMarble)
            {
                this.marbles.AddAfter(this.marbles.First.Next, marble);
                this.currentMarble = marble;
                this.currentMarbleNode = this.marbles.First.Next;
                return;
            }

            if (this.marbles.Last.Previous.Value == this.currentMarble)
            {
                this.marbles.AddLast(marble);
                this.currentMarble = marble;
                this.currentMarbleNode = this.marbles.Last;
                return;
            }

            this.marbles.AddAfter(this.currentMarbleNode.Next, marble);
            this.currentMarble = marble;
            this.currentMarbleNode = this.currentMarbleNode.Next.Next;
        }

        internal int RemoveSeventhCounterClockwise()
        {
            var currentNode = this.currentMarbleNode;

            for (var i = 0; i < 7; i++)
            {
                currentNode = currentNode.Previous ?? this.marbles.Last;
            }

            this.currentMarbleNode = currentNode.Next;
            this.marbles.Remove(currentNode);
            this.currentMarble = this.currentMarbleNode.Value;
            return currentNode.Value;
        }
    }
}
