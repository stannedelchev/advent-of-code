using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day10
{
    internal class Asteroid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Asteroid(in Point position, in Point originalPosition)
        {
            this.Position = position;
            this.OriginalPosition = originalPosition;
            this.VisibleAsteroidAngles = new HashSet<AsteroidRelationship>(1024, AsteroidRelationship.AngleComparer);
        }

        /// <summary>
        /// Position in a bottom-left 0,0 coordinate system.
        /// </summary>
        public Point Position { get; }

        /// <summary>
        /// Position in a top-left 0,0 coordinate system - same as problem's string input.
        /// </summary>
        public Point OriginalPosition { get; }

        public HashSet<AsteroidRelationship> VisibleAsteroidAngles { get; }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}