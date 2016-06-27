using System;
using System.Runtime.CompilerServices;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector2F :
        IVector2<float>,
        IVectorMutable<float>,
        IEquatable<Vector2F>
    {
        public float X;

        public float Y;

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F(Vector2F source)
        {
            X = source.X;
            Y = source.Y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F(IVector2<float> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            X = source.X;
            Y = source.Y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F(IVector<float> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Dimensions != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            X = source[0];
            Y = source[1];
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD && RELEASE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 2; }
        }

        public float this[int index]
        {
#if !PRE_NETSTANDARD && RELEASE
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

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD && RELEASE
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
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        float IVector2<float>.X
        {
#if !PRE_NETSTANDARD && RELEASE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        float IVector2<float>.Y
        {
#if !PRE_NETSTANDARD && RELEASE
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F operator +(Vector2F left, Vector2F right)
        {
            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F operator -(Vector2F left, Vector2F right)
        {
            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static float operator *(Vector2F left, Vector2F right)
        {
            return left.GetDot(right);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F operator *(Vector2F vector, float scalar)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F operator *(float scalar, Vector2F vector)
        {
            return vector.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F operator /(Vector2F vector, float divisor)
        {
            return vector.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F CreateXUnit()
        {
            return new Vector2F
            {
                X = 1.0f
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2F CreateYUnit()
        {
            return new Vector2F
            {
                Y = 1.0f
            };
        }

#if !PRE_NETSTANDARD && RELEASE
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

            throw new ArgumentOutOfRangeException(nameof(dimension));
        }

#if !PRE_NETSTANDARD && RELEASE
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
            else
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetSum(Vector2F right)
        {
            return new Vector2F
            {
                X = X + right.X,
                Y = Y + right.Y
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Add(Vector2F right)
        {
            X += right.X;
            Y += right.Y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetDifference(Vector2F right)
        {
            return new Vector2F
            {
                X = X - right.X,
                Y = Y - right.Y
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Subtract(Vector2F right)
        {
            X -= right.X;
            Y -= right.Y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetScaled(float scalar)
        {
            return new Vector2F
            {
                X = X * scalar,
                Y = Y * scalar
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Scale(float scalar)
        {
            X *= scalar;
            Y *= scalar;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(float divisor)
        {
            X /= divisor;
            Y /= divisor;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetQuotient(float divisor)
        {
            return new Vector2F
            {
                X = X / divisor,
                Y = Y / divisor
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetNegative()
        {
            return new Vector2F
            {
                X = -X,
                Y = -Y
            };
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDot(Vector2F right)
        {
            return (X * right.X) + (Y * right.Y);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetPerpendicularDot(Vector2F right)
        {
            return (X * right.Y) - (Y * right.X);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetMagnitude()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetAngleBetween(Vector2F other)
        {
            return (float)Math.Acos(
                GetDot(other)
                    / (float)Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDistance(Vector2F other)
        {
            return (float)Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y));
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDistanceSquared(Vector2F other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetProjected(Vector2F other)
        {
            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0f
                ? other
                : GetScaled(GetDot(other) / scalarDenominator);
        }

#if !PRE_NETSTANDARD && RELEASE
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

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetNormal()
        {
            var magnitude = GetMagnitude();
            return magnitude == 0.0f ? this : GetQuotient(magnitude);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void RotatePerpendicularClockwise()
        {
            var temp = Y;
            Y = -X;
            X = temp;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetPerpendicularClockwise()
        {
            return new Vector2F(Y, -X);
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void RotatePerpendicularCounterclockwise()
        {
            var temp = X;
            X = -Y;
            Y = temp;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2F GetPerpendicularCounterclockwise()
        {
            return new Vector2F(-Y, X);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Vector2F other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector2F && Equals((Vector2F)obj);
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
