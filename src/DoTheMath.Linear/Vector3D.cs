using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector3D :
        IVector<double>,
        IEquatable<Vector3D>
    {
        public double X;

        public double Y;

        public double Z;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get { return 3; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double Get(int dimension)
        {
            if (dimension == 0)
            {
                return X;
            }
            if (dimension == 1)
            {
                return Y;
            }
            if (dimension == 2)
            {
                return Z;
            }

            throw new ArgumentOutOfRangeException(nameof(dimension));
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(Vector3D other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector3D && Equals((Vector3D)obj);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Dimensions;
        }
    }
}
