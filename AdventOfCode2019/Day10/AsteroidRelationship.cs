using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day10
{
    internal class AsteroidRelationship
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AsteroidRelationship(in Asteroid origin, in Asteroid destination, in double radianAngleFromOriginToDestination, in double distance)
        {
            Origin = origin;
            Destination = destination;
            RadianAngleFromOriginToDestination = radianAngleFromOriginToDestination;
            Distance = distance;
        }

        public Asteroid Origin { get; }
        public Asteroid Destination { get; }
        public double RadianAngleFromOriginToDestination { get; }
        public double Distance { get; }

        private sealed class AngleEqualityComparer : IEqualityComparer<AsteroidRelationship>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public bool Equals(AsteroidRelationship x, AsteroidRelationship y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.RadianAngleFromOriginToDestination.Equals(y.RadianAngleFromOriginToDestination);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public int GetHashCode(AsteroidRelationship obj)
            {
                return obj.RadianAngleFromOriginToDestination.GetHashCode();
            }
        }

        public static IEqualityComparer<AsteroidRelationship> AngleComparer { get; } = new AngleEqualityComparer();

        public override string ToString()
        {
            return $"{Origin.Position.X},{Origin.Position.Y} [{Origin.OriginalPosition}] -> {Destination.Position.X},{Destination.Position.Y} [{Destination.OriginalPosition}]: {RadianAngleFromOriginToDestination} : {Distance}";
        }
    }
}