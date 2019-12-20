using System.Collections.Generic;

namespace AdventOfCode2019.Day10
{
    internal class AsteroidRelationship
    {
        public Asteroid Origin { get; set; }
        public Asteroid Destination { get; set; }
        public double RadianAngleFromOriginToDestination { get; set; }
        public double Distance { get; set; }

        private sealed class AngleEqualityComparer : IEqualityComparer<AsteroidRelationship>
        {
            public bool Equals(AsteroidRelationship x, AsteroidRelationship y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.RadianAngleFromOriginToDestination.Equals(y.RadianAngleFromOriginToDestination);
            }

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