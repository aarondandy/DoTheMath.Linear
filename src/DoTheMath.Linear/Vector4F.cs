using System;
using System.Runtime.CompilerServices;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector4F :
        IVector4<float>,
        IVectorMutable<float>,
        IEquatable<Vector4F>
    {
        public float X;

        public float Y;

        public float Z;

        public float W;

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4F(Vector4F source)
        {
            X = source.X;
            Y = source.Y;
            Z = source.Z;
            W = source.W;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4F(IVector4<float> source)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4F(IVector<float> source)
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
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 4; }
        }

        public float this[int index]
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
                if (index == 3)
                {
                    return W;
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

        float IVector4<float>.X
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        float IVector4<float>.Y
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

        float IVector4<float>.Z
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Z; }
        }

        float IVector4<float>.W
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return W; }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F operator +(Vector4F left, Vector4F right)
        {
            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F operator -(Vector4F left, Vector4F right)
        {
            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static float operator *(Vector4F left, Vector4F right)
        {
            return left.GetDot(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F operator *(Vector4F vector, float scalar)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F operator *(float scalar, Vector4F vector)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F operator /(Vector4F vector, float divisor)
        {
            return vector.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F CreateXUnit()
        {
            return new Vector4F
            {
                X = 1.0f
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F CreateYUnit()
        {
            return new Vector4F
            {
                Y = 1.0f
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F CreateZUnit()
        {
            return new Vector4F
            {
                Z = 1.0f
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector4F CreateWUnit()
        {
            return new Vector4F
            {
                W = 1.0f
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float Get(int dimension)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int dimension, float value)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetSum(Vector4F right)
        {
            return new Vector4F
            {
                X = X + right.X,
                Y = Y + right.Y,
                Z = Z + right.Z,
                W = W + right.W
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Add(Vector4F right)
        {
            X += right.X;
            Y += right.Y;
            Z += right.Z;
            W += right.W;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetDifference(Vector4F right)
        {
            return new Vector4F
            {
                X = X - right.X,
                Y = Y - right.Y,
                Z = Z - right.Z,
                W = W - right.W
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Subtract(Vector4F right)
        {
            X -= right.X;
            Y -= right.Y;
            Z -= right.Z;
            W -= right.W;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetScaled(float scalar)
        {
            return new Vector4F
            {
                X = X * scalar,
                Y = Y * scalar,
                Z = Z * scalar,
                W = W * scalar
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Scale(float scalar)
        {
            X *= scalar;
            Y *= scalar;
            Z *= scalar;
            W *= scalar;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(float divisor)
        {
            X /= divisor;
            Y /= divisor;
            Z /= divisor;
            W /= divisor;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetQuotient(float divisor)
        {
            return new Vector4F
            {
                X = X / divisor,
                Y = Y / divisor,
                Z = Z / divisor,
                W = W / divisor
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
            W = -W;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetNegative()
        {
            return new Vector4F
            {
                X = -X,
                Y = -Y,
                Z = -Z,
                W = -W
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDot(Vector4F right)
        {
            return (X * right.X) + (Y * right.Y) + (Z * right.Z) + (W * right.W);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetMagnitude()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetAngleBetween(Vector4F other)
        {
            return (float)Math.Acos(
                GetDot(other)
                    / (float)Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDistance(Vector4F other)
        {
            return (float)Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z) + MathEx.Square(W - other.W));
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDistanceSquared(Vector4F other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y) + MathEx.Square(Z - other.Z) + MathEx.Square(W - other.W);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector4F GetProjected(Vector4F other)
        {
            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0f
                ? other
                : GetScaled(GetDot(other) / scalarDenominator);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Normalize()
        {
            var magnitude = GetMagnitude();
            if (magnitude != 0.0f)
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
        public Vector4F GetNormal()
        {
            var magnitude = GetMagnitude();
            return magnitude == 0.0f ? this : GetQuotient(magnitude);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Vector4F other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z)
                && W.Equals(other.W);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public override bool Equals(object obj)
        {
            return obj is Vector4F && Equals((Vector4F)obj);
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