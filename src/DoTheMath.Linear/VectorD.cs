using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Duplicator;

namespace DoTheMath.Linear
{
    public sealed class VectorD :
        IVector<double>,
        IEquatable<VectorD>
    {
        private double[] _components;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public VectorD(int dimensions)
        {
            if (dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            _components = new double[dimensions];
        }

        public VectorD(VectorD source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _components = Clone(source._components);
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get { return _components.Length; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double Get(int dimension)
        {
            if (dimension < 0 || dimension >= _components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            return _components[dimension];
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int dimension, double value)
        {
            if (dimension < 0 || dimension >= _components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            _components[dimension] = value;
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
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            if (_components.Length != other._components.Length)
            {
                return false;
            }

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(_components.Length == other._components.Length);
#endif

            for (int dimension = 0; dimension < _components.Length; dimension++)
            {
                if (!_components[dimension].Equals(other._components[dimension]))
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
                return 4099 + _components.Length * 23;
            }
        }
    }
}
