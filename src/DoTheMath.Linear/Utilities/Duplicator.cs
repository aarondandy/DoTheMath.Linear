using System;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
#endif

namespace DoTheMath.Linear.Utilities
{
    internal static class Duplicator
    {
        public static double[] Clone(double[] source)
        {
#if HAS_CODECONTRACTS
            Contract.Requires(source != null);
            Contract.Ensures(Contract.Result<double[]>() != null);
            Contract.Ensures(Contract.Result<double[]>().Length == source.Length);
            Contract.Ensures(Contract.ForAll(0, source.Length, i => Contract.Result<double[]>()[i] == source[i]));
#endif

            var result = new double[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(double)));
            return result;
        }

        public static float[] Clone(float[] source)
        {
#if HAS_CODECONTRACTS
            Contract.Requires(source != null);
            Contract.Ensures(Contract.Result<float[]>() != null);
            Contract.Ensures(Contract.Result<float[]>().Length == source.Length);
            Contract.Ensures(Contract.ForAll(0, source.Length, i => Contract.Result<float[]>()[i] == source[i]));
#endif

            var result = new float[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(float)));
            return result;
        }

        public static int[] Clone(int[] source)
        {
#if HAS_CODECONTRACTS
            Contract.Requires(source != null);
            Contract.Ensures(Contract.Result<int[]>() != null);
            Contract.Ensures(Contract.Result<int[]>().Length == source.Length);
            Contract.Ensures(Contract.ForAll(0, source.Length, i => Contract.Result<int[]>()[i] == source[i]));
#endif

            var result = new int[source.Length];
            Buffer.BlockCopy(source, 0, result, 0, checked(result.Length * sizeof(int)));
            return result;
        }
    }
}
