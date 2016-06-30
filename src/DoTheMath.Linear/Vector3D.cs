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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(Vector3D source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(IVector3<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            X = source.X;
            Y = source.Y;
            Z = source.Z;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(IVector<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Dimensions != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            X = source[0];
            Y = source[1];
            Z = source[2];
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 3; }
        }

        public double this[int index]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                if (index == 0)
                {
                    return X;
                }
                if (index == 1)
                {
                    return Y;
                }
                if (index == 2)
                {
                    return Z;
                }

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            set
            {
                if (index == 0)
                {
                    X = value;
                }
                else if (index == 1)
                {
                    Y = value;
                }
                else if (index == 2)
                {
                    Z = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        double IVector3<double>.X
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        double IVector3<double>.Y
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

        double IVector3<double>.Z
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Z; }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static double operator *(Vector3D left, Vector3D right)
        {
            return left.GetDot(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector3D operator /(Vector3D vector, double divisor)
        {
            return vector.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetSum(Vector3D right)
        {
            return new Vector3D
            {
                X = X + right.X,
                Y = Y + right.Y,
                Z = Z + right.Z
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Add(Vector3D right)
        {
            X += right.X;
            Y += right.Y;
            Z += right.Z;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetDifference(Vector3D right)
        {
            return new Vector3D
            {
                X = X - right.X,
                Y = Y - right.Y,
                Z = Z - right.Z
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Subtract(Vector3D right)
        {
            X -= right.X;
            Y -= right.Y;
            Z -= right.Z;
        }

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Scale(double scalar)
        {
            X *= scalar;
            Y *= scalar;
            Z *= scalar;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(double divisor)
        {
            X /= divisor;
            Y /= divisor;
            Z /= divisor;
        }

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Negate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

#if !PRE_NETSTANDARD && !DEBUG
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDot(Vector3D right)
        {
            return (X * right.X) + (Y * right.Y) + (Z * right.Z);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetAngleBetween(Vector3D other)
        {
            return Math.Acos(
                GetDot(other)
                    / Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistance(Vector3D other)
        {
            return Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistanceSquared(Vector3D other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetProjected(Vector3D other)
        {
            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0d
                ? other
                : GetScaled(GetDot(other) / scalarDenominator);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0.0d)
            {
                Divide(magnitude);
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector3D GetNormal()
        {
            var magnitude = GetMagnitude();
            return magnitude == 0.0d ? this : GetQuotient(magnitude);
        }

        public Vector3D GetCrossProduct(Vector3D right)
        {
            return new Vector3D
            {
                X = (Y * right.Z) - (Z * right.Y),
                Y = (Z * right.X) - (X * right.Z),
                Z = (X * right.Y) - (Y * right.X)
            };
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
        public override bool Equals(object obj)
        {
            return obj is Vector3D && Equals((Vector3D)obj);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public override int GetHashCode()
        {
            return Dimensions;
        }
    }
}
