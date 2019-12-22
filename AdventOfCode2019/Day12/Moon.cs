using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day12
{
    internal class Moon
    {
        private readonly Guid guid;
        public Moon()
        {
            this.guid = Guid.NewGuid();
        }

        public int X;
        public int Y;
        public int Z;

        public int VelocityX;
        public int VelocityY;
        public int VelocityZ;

        public int PotentialEnergy => Math.Abs(this.X) + Math.Abs(this.Y) + Math.Abs(this.Z);
        public int KineticEnergy => Math.Abs(this.VelocityX) + Math.Abs(this.VelocityY) + Math.Abs(this.VelocityZ);
        public int TotalEnergy => this.PotentialEnergy * this.KineticEnergy;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void ApplyVelocity()
        {
            this.X += this.VelocityX;
            this.Y += this.VelocityY;
            this.Z += this.VelocityZ;
        }

        public static IEqualityComparer<Moon> XComparer { get; } = new XEqualityComparer();
        public static IEqualityComparer<Moon> YComparer { get; } = new YEqualityComparer();
        public static IEqualityComparer<Moon> ZComparer { get; } = new ZEqualityComparer();

        protected bool Equals(Moon other)
        {
            return this.X == other.X &&
                   this.Y == other.Y &&
                   this.Z == other.Z &&
                   this.VelocityX == other.VelocityX &&
                   this.VelocityY == other.VelocityY &&
                   this.VelocityZ == other.VelocityZ;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Moon)obj);
        }

        public override int GetHashCode()
        {
            return this.guid.GetHashCode();
        }

        public override string ToString()
        {
            return $"pos=<x= {this.X,3}, y= {this.Y,3}, z= {this.Z,3}>, vel=<x= {this.VelocityX,3}, y= {this.VelocityY,3}, z= {this.VelocityZ,3}>, pot={this.PotentialEnergy}, kin={this.KineticEnergy}, tot={this.TotalEnergy}";
        }

        public Moon Clone()
        {
            return new Moon()
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z,
                VelocityX = this.VelocityX,
                VelocityY = this.VelocityY,
                VelocityZ = this.VelocityZ
            };
        }

        private sealed class XEqualityComparer : IEqualityComparer<Moon>
        {
            public bool Equals(Moon x, Moon y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.X == y.X;
            }

            public int GetHashCode(Moon obj)
            {
                return obj.X;
            }
        }

        private sealed class YEqualityComparer : IEqualityComparer<Moon>
        {
            public bool Equals(Moon x, Moon y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.Y == y.Y;
            }

            public int GetHashCode(Moon obj)
            {
                return obj.Y;
            }
        }

        private sealed class ZEqualityComparer : IEqualityComparer<Moon>
        {
            public bool Equals(Moon x, Moon y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.Z == y.Z;
            }

            public int GetHashCode(Moon obj)
            {
                return obj.Z;
            }
        }
    }
}