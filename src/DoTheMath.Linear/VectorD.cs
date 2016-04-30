using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public sealed class VectorD : IVector<double>
    {
        private double[] components;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public VectorD(int dimensions)
        {
            if (dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            components = new double[dimensions];
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get { return components.Length; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double Get(int dimension)
        {
            if (dimension < 0 || dimension >= components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            return components[dimension];
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int dimension, double value)
        {
            if (dimension < 0 || dimension >= components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            components[dimension] = value;
        }
    }
}
