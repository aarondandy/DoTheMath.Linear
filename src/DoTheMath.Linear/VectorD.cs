using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public sealed class VectorD :
        IVector<double>,
        IEquatable<VectorD>
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

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(VectorD other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            if(object.ReferenceEquals(null, other))
            {
                return false;
            }
            if(components.Length != other.components.Length)
            {
                return false;
            }

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(components.Length == other.components.Length);
#endif

            for (int dimension = 0; dimension < components.Length; dimension++)
            {
                if (!components[dimension].Equals(other.components[dimension]))
                {
                    return false;
                }
            }

            return true;
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is VectorD && Equals((VectorD)obj);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override int GetHashCode()
        {
            unchecked
            {
                return 4099 + components.Length * 23;
            }
        }
    }
}
