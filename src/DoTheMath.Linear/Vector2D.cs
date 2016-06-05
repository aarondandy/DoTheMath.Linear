﻿using System;
using System.Runtime.CompilerServices;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public struct Vector2D :
        IVector2<double>,
        IVectorMutable<double>,
        IEquatable<Vector2D>
    {
        public double X;

        public double Y;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2D(Vector2D source)
        {
            X = source.X;
            Y = source.Y;
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return 2; }
        }

        double IVector2<double>.X
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return X; }
        }

        double IVector2<double>.Y
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return Y; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Vector2D CreateXUnit()
        {
            return new Vector2D
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
        public static Vector2D CreateYUnit()
        {
            return new Vector2D
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
        public Vector2D Add(Vector2D right)
        {
            return new Vector2D
            {
                X = X + right.X,
                Y = Y + right.Y
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void AddTo(Vector2D right)
        {
            X += right.X;
            Y += right.Y;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2D Subtract(Vector2D right)
        {
            return new Vector2D
            {
                X = X - right.X,
                Y = Y - right.Y
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void SubtractFrom(Vector2D right)
        {
            X -= right.X;
            Y -= right.Y;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2D GetScaled(double scalar)
        {
            return new Vector2D
            {
                X = X * scalar,
                Y = Y * scalar
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Scale(double scalar)
        {
            X *= scalar;
            Y *= scalar;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Divide(double divisor)
        {
            X /= divisor;
            Y /= divisor;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2D GetQuotient(double divisor)
        {
            return new Vector2D
            {
                X = X / divisor,
                Y = Y / divisor
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2D GetNegative()
        {
            return new Vector2D
            {
                X = -X,
                Y = -Y
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double Dot(Vector2D right)
        {
            return (X * right.X) + (Y * right.Y);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetMagnitudeSquared()
        {
            return (X * X) + (Y * Y);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetAngleBetween(Vector2D other)
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
        public double GetDistance(Vector2D other)
        {
            return Math.Sqrt(MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDistanceSquared(Vector2D other)
        {
            return MathEx.Square(X - other.X) + MathEx.Square(Y - other.Y);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Vector2D GetProjected(Vector2D other)
        {
            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0
                ? other
                : GetScaled((Dot(other)) / scalarDenominator);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Vector2D other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is Vector2D && Equals((Vector2D)obj);
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
