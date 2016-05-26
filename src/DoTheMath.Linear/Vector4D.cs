using System;
using System.Runtime.CompilerServices;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector4D :
        IVector4<double>,
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4D(Vector4D source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 3; }
        }

        double IVector4<double>.X { get { return X; } }

        double IVector4<double>.Y { get { return Y; } }

        double IVector4<double>.Z { get { return Z; } }

        double IVector4<double>.W { get { return W; } }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D Add(Vector4D right)
        {
            return new Vector4D
            {
                X = X + right.X,
                Y = Y + right.Y,
                Z = Z + right.Z,
                W = W + right.W
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void AddTo(Vector4D right)
        {
            X += right.X;
            Y += right.Y;
            Z += right.Z;
            W += right.W;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D Subtract(Vector4D right)
        {
            return new Vector4D
            {
                X = X - right.X,
                Y = Y - right.Y,
                Z = Z - right.Z,
                W = W - right.W
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void SubtractFrom(Vector4D right)
        {
            X -= right.X;
            Y -= right.Y;
            Z -= right.Z;
            W -= right.W;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D GetScaled(double scalar)
        {
            return new Vector4D
            {
                X = X * scalar,
                Y = Y * scalar,
                Z = Z * scalar,
                W = W * scalar
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Scale(double scalar)
        {
            X *= scalar;
            Y *= scalar;
            Z *= scalar;
            W *= scalar;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Vector4D other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z)
                && W.Equals(other.W);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector4D && Equals((Vector4D)obj);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Dimensions;
        }
    }
}