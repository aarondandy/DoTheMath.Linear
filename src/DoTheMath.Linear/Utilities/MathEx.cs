using System.Runtime.CompilerServices;
using System;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear.Utilities
{
    internal static class MathEx
    {
#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Negate(ref double value)
        {
            value = -value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Negate(ref float value)
        {
            value = -value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Negate(ref int value)
        {
            value = -value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static double Square(double value)
        {
            return value * value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static float Square(float value)
        {
            return value * value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static int Square(int value)
        {
            return value * value;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static bool IsZero<TValue>(TValue value)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (double)(object)value == 0.0d;
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (float)(object)value == 0.0f;
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (int)(object)value == 0;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static bool IsOne<TValue>(TValue value)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (double)(object)value == 1.0d;
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (float)(object)value == 1.0f;
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (int)(object)value == 1;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue GetZero<TValue>()
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)0.0d;
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)0.0f;
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)0;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Add<TValue>(TValue left, TValue right)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)left + (double)(object)right);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)left + (float)(object)right);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)left + (int)(object)right);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Subtract<TValue>(TValue left, TValue right)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)left - (double)(object)right);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)left - (float)(object)right);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)left - (int)(object)right);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Negate<TValue>(TValue value)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)(-((double)(object)value));
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)(-((float)(object)value));
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)(-((int)(object)value));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Multiply<TValue>(TValue left, TValue right)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)left * (double)(object)right);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)left * (float)(object)right);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)left * (int)(object)right);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Multiply<TValue>(TValue a, TValue b, TValue c)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)a * (double)(object)b * (double)(object)c);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)a * (float)(object)b * (float)(object)c);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)a * (int)(object)b * (int)(object)c);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Multiply<TValue>(TValue a, TValue b, TValue c, TValue d)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)a * (double)(object)b * (double)(object)c * (double)(object)d);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)a * (float)(object)b * (float)(object)c * (float)(object)d);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)a * (int)(object)b * (int)(object)c * (int)(object)d);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static TValue Divide<TValue>(TValue left, TValue right)
        {
            if (typeof(TValue) == typeof(double))
            {
                return (TValue)(object)((double)(object)left / (double)(object)right);
            }
            else if (typeof(TValue) == typeof(float))
            {
                return (TValue)(object)((float)(object)left / (float)(object)right);
            }
            else if (typeof(TValue) == typeof(int))
            {
                return (TValue)(object)((int)(object)left / (int)(object)right);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
