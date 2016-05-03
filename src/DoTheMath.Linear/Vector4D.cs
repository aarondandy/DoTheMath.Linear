using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector4D :
        IVector<double>,
        IEquatable<Vector4D>
    {
        public double X;

        public double Y;

        public double Z;

        public double W;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
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
            if (dimension == 3)
            {
                return W;
            }

            throw new ArgumentOutOfRangeException(nameof(dimension));
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(Vector4D other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z)
                && W.Equals(other.W);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector4D && Equals((Vector4D)obj);
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