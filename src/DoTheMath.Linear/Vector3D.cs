using System;
using System.Runtime.CompilerServices;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector3D :
        IVector3<double>,
        IVectorMutable<double>,
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(Vector3D source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
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

        double IVector3<double>.X
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        double IVector3<double>.Y
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

        double IVector3<double>.Z
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Z; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D CreateXUnit()
        {
            return new Vector3D
            {
                X = 1.0
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D CreateYUnit()
        {
            return new Vector3D
            {
                Y = 1.0
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D CreateZUnit()
        {
            return new Vector3D
            {
                Z = 1.0
            };
        }

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

            throw new ArgumentOutOfRangeException(nameof(dimension));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int dimension, double value)
        {
            if (dimension == 0)
            {
                X = value;
            }
            else if (dimension == 1)
            {
                Y = value;
            }
            else if (dimension == 2)
            {
                Z = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D Add(Vector3D right)
        {
            return new Vector3D
            {
                X = X + right.X,
                Y = Y + right.Y,
                Z = Z + right.Z
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void AddTo(Vector3D right)
        {
            X += right.X;
            Y += right.Y;
            Z += right.Z;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D Subtract(Vector3D right)
        {
            return new Vector3D
            {
                X = X - right.X,
                Y = Y - right.Y,
                Z = Z - right.Z
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void SubtractFrom(Vector3D right)
        {
            X -= right.X;
            Y -= right.Y;
            Z -= right.Z;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetScaled(double scalar)
        {
            return new Vector3D
            {
                X = X * scalar,
                Y = Y * scalar,
                Z = Z * scalar
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
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(double divisor)
        {
            X /= divisor;
            Y /= divisor;
            Z /= divisor;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetQuotient(double divisor)
        {
            return new Vector3D
            {
                X = X / divisor,
                Y = Y / divisor,
                Z = Z / divisor
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Negate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetNegative()
        {
            return new Vector3D
            {
                X = -X,
                Y = -Y,
                Z = -Z
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double Dot(Vector3D right)
        {
            return (X * right.X) + (Y * right.Y) + (Z * right.Z);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetAngleBetween(Vector3D other)
        {
            return Math.Acos(
                Dot(other)
                    / Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistance(Vector3D other)
        {
            return Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistanceSquared(Vector3D other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Vector3D other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector3D && Equals((Vector3D)obj);
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
