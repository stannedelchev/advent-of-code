using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Day10
{
    internal class AsteroidComparer : IComparer<AsteroidRelationship>
    {
        public int Compare(AsteroidRelationship a, AsteroidRelationship b)
        {
            var xAngle = a.RadianAngleFromOriginToDestination;
            var yAngle = b.RadianAngleFromOriginToDestination;
            if (ReferenceEquals(a, b) ||
                ReferenceEquals(a.Destination, b.Destination) && ReferenceEquals(a.Origin, b.Origin))
            {
                return 0;
            }

            if (Math.Abs(xAngle - yAngle) < double.Epsilon)
            {
                return a.Distance < b.Distance ? -1 : 1;
            }

            var xAngleDistanceFromPiOver2 = xAngle - Math.PI / 2;
            var yAngleDistanceFromPiOver2 = yAngle - Math.PI / 2;

            if (xAngleDistanceFromPiOver2 <= 0 && yAngleDistanceFromPiOver2 <= 0 ||
                xAngleDistanceFromPiOver2 > 0 && yAngleDistanceFromPiOver2 > 0)
            {
                return xAngle > yAngle ? -1 : 1;
            }

            if (xAngleDistanceFromPiOver2 <= 0)
            {
                return -1;
            }

            return 1;
        }
    }
}