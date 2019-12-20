using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode2019.Day10
{
    internal class Asteroid
    {
        public Asteroid(in Point position, in Point originalPosition)
        {
            this.Position = position;
            this.OriginalPosition = originalPosition;
        }

        /// <summary>
        /// Position in a bottom-left 0,0 coordinate system.
        /// </summary>
        public Point Position { get; }

        /// <summary>
        /// Position in a top-left 0,0 coordinate system - same as problem's string input.
        /// </summary>
        public Point OriginalPosition { get; }

        public HashSet<AsteroidRelationship> VisibleAsteroidAngles { get; } = new HashSet<AsteroidRelationship>(1024, AsteroidRelationship.AngleComparer);
        public HashSet<AsteroidRelationship> AllAsteroidAngles { get; } = new HashSet<AsteroidRelationship>(1024);

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}