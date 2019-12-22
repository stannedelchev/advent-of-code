using System;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Day12
{
    internal class MoonRelationship
    {
        public Moon A { get; }
        public Moon B { get; }

        public MoonRelationship(Moon a, Moon b)
        {
            this.A = a;
            this.B = b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AdjustGravity()
        {
            var axDiff = Math.Sign(this.A.X - this.B.X);
            var ayDiff = Math.Sign(this.A.Y - this.B.Y);
            var azDiff = Math.Sign(this.A.Z - this.B.Z);

            A.VelocityX += -axDiff;
            B.VelocityX += axDiff;

            A.VelocityY += -ayDiff;
            B.VelocityY += ayDiff;

            A.VelocityZ += -azDiff;
            B.VelocityZ += azDiff;
        }

        protected bool Equals(MoonRelationship other)
        {
            return this.A.Equals(other.A) && this.B.Equals(other.B) ||
                   this.A.Equals(other.B) && this.B.Equals(other.A);
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

            return this.Equals((MoonRelationship)obj);
        }

        public override int GetHashCode()
        {
            return this.A.GetHashCode() + this.B.GetHashCode();
        }

        public static bool operator ==(MoonRelationship left, MoonRelationship right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MoonRelationship left, MoonRelationship right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{this.A} <-> {this.B}";
        }
    }
}