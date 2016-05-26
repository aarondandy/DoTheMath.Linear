using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Duplicator;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

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
            [Pure]
#endif
            get { return _components.Length; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
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
        [Pure]
#endif
        public VectorD Add(VectorD right)
        {
            if(right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if(_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var sum = new VectorD(_components.Length);
            var sumComponents = sum._components;
            var rightComponents = right._components;

            for(int i = 0; i < _components.Length; i++)
            {
                sumComponents[i] = _components[i] + rightComponents[i];
            }

            return sum;
        }

        public void AddTo(VectorD right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var rightComponents = right._components;

            for (int i = 0; i < _components.Length; i++)
            {
                _components[i] += rightComponents[i];
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorD Subtract(VectorD right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var difference = new VectorD(_components.Length);
            var diffComponents = difference._components;
            var rightComponents = right._components;

            for (int i = 0; i < _components.Length; i++)
            {
                diffComponents[i] = _components[i] - rightComponents[i];
            }

            return difference;
        }

        public void SubtractFrom(VectorD right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var rightComponents = right._components;

            for (int i = 0; i < _components.Length; i++)
            {
                _components[i] -= rightComponents[i];
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorD GetScaled(double scalar)
        {
            var scaled = new VectorD(_components.Length);
            var scaledComponents = scaled._components;

            for(int i = 0; i < scaledComponents.Length; i++)
            {
                scaledComponents[i] = _components[i] * scalar;
            }

            return scaled;
        }

        public void Scale(double scalar)
        {
            for(int i = 0; i < _components.Length; i++)
            {
                _components[i] *= scalar;
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
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

            var otherComponents = other._components;
            if (_components.Length != otherComponents.Length)
            {
                return false;
            }

#if HAS_CODECONTRACTS
            Assume(_components.Length == otherComponents.Length);
#endif

            for (int dimension = 0; dimension < _components.Length; dimension++)
            {
                if (!_components[dimension].Equals(otherComponents[dimension]))
                {
                    return false;
                }
            }

            return true;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return obj is VectorD && Equals((VectorD)obj);
        }

#if HAS_CODECONTRACTS
        [Pure]
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
