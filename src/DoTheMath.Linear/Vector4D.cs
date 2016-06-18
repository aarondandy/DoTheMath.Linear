using System;
using System.Runtime.CompilerServices;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector4D :
        IVector4<double>,
        IVectorMutable<double>,
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4D(IVector4<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4D(IVector<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Dimensions != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            X = source[0];
            Y = source[1];
            Z = source[2];
            W = source[3];
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 4; }
        }

        public double this[int index]
        {
#if !PRE_NETSTANDARD
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
                if (index == 3)
                {
                    return W;
                }

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD
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
                else if (index == 3)
                {
                    W = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        double IVector4<double>.X
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        double IVector4<double>.Y
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

        double IVector4<double>.Z
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Z; }
        }

        double IVector4<double>.W
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return W; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D operator +(Vector4D left, Vector4D right)
        {
            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D operator -(Vector4D left, Vector4D right)
        {
            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static double operator *(Vector4D left, Vector4D right)
        {
            return left.GetDot(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D operator *(Vector4D vector, double scalar)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D operator *(double scalar, Vector4D vector)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D operator /(Vector4D vector, double divisor)
        {
            return vector.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4D CreateXUnit()
        {
            return new Vector4D
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
        public static Vector4D CreateYUnit()
        {
            return new Vector4D
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
        public static Vector4D CreateZUnit()
        {
            return new Vector4D
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
        public static Vector4D CreateWUnit()
        {
            return new Vector4D
            {
                W = 1.0
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
            if (dimension == 3)
            {
                return W;
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
            else if (dimension == 3)
            {
                W = value;
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
        public Vector4D GetSum(Vector4D right)
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
        public void Add(Vector4D right)
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
        public Vector4D GetDifference(Vector4D right)
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
        public void Subtract(Vector4D right)
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(double divisor)
        {
            X /= divisor;
            Y /= divisor;
            Z /= divisor;
            W /= divisor;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D GetQuotient(double divisor)
        {
            return new Vector4D
            {
                X = X / divisor,
                Y = Y / divisor,
                Z = Z / divisor,
                W = W / divisor
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
            W = -W;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D GetNegative()
        {
            return new Vector4D
            {
                X = -X,
                Y = -Y,
                Z = -Z,
                W = -W
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDot(Vector4D right)
        {
            return (X * right.X) + (Y * right.Y) + (Z * right.Z) + (W * right.W);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetAngleBetween(Vector4D other)
        {
            return Math.Acos(
                GetDot(other)
                    / Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistance(Vector4D other)
        {
            return Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z) + MathEx.Square(W - other.W));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistanceSquared(Vector4D other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z) + MathEx.Square(W - other.W);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D GetProjected(Vector4D other)
        {
            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0
                ? other
                : GetScaled((GetDot(other)) / scalarDenominator);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0.0)
            {
                Divide(magnitude);
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4D GetNormal()
        {
            var magnitude = GetMagnitude();
            return magnitude == 0.0 ? this : GetQuotient(magnitude);
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