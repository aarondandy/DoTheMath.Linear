using System;
using System.Runtime.CompilerServices;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear.Utilities
{
    internal static class Duplicator
    {
#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static double[] Clone(double[] source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Ensures(Result<double[]>() != null);
            Ensures(Result<double[]>().Length == source.Length);
            Ensures(ForAll(0, source.Length, i => Result<double[]>()[i] == source[i]));
#endif

            var result = new double[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(double)));
            return result;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static float[] Clone(float[] source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Ensures(Result<float[]>() != null);
            Ensures(Result<float[]>().Length == source.Length);
            Ensures(ForAll(0, source.Length, i => Result<float[]>()[i] == source[i]));
#endif

            var result = new float[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(float)));
            return result;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static int[] Clone(int[] source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Ensures(Result<int[]>() != null);
            Ensures(Result<int[]>().Length == source.Length);
            Ensures(ForAll(0, source.Length, i => Result<int[]>()[i] == source[i]));
#endif

            var result = new int[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(int)));
            return result;
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void CopyTo(double[] source, double[] destination)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Requires(destination != null);
            Requires(source.Length >= destination.Length);
#endif

            Buffer.BlockCopy(source, 0, destination, 0, checked(destination.Length * sizeof(double)));
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void CopyTo(float[] source, float[] destination)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Requires(destination != null);
            Requires(source.Length >= destination.Length);
#endif

            Buffer.BlockCopy(source, 0, destination, 0, checked(destination.Length * sizeof(float)));
        }

#if !PRE_NETSTANDARD && RELEASE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void CopyTo(int[] source, int[] destination)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Requires(destination != null);
            Requires(source.Length >= destination.Length);
#endif

            Buffer.BlockCopy(source, 0, destination, 0, checked(destination.Length * sizeof(int)));
        }
    }
}
