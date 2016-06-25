using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Duplicator;
using DoTheMath.Linear.Utilities;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public sealed class VectorF :
        IVector<float>,
        IVectorMutable<float>,
        IEquatable<VectorF>
    {
        private float[] _components;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public VectorF(int dimensions)
        {
            if (dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            }

            _components = new float[dimensions];
        }

        public VectorF(VectorF source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _components = Clone(source._components);
        }

        public VectorF(float[] components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components));
            }

            _components = Clone(components);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public VectorF(IVector<float> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Dimensions < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            _components = new float[source.Dimensions];
            for (var i = 0; i < _components.Length; i++)
            {
                _components[i] = source[i];
            }
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

        public float this[int index]
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
#if HAS_CODECONTRACTS
                Requires(index >= 0);
                Requires(index < Dimensions);
                EndContractBlock();

                Assume(Dimensions == _components.Length);
#endif

                return _components[index];
            }
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            set
            {
#if HAS_CODECONTRACTS
                Requires(index >= 0);
                Requires(index < Dimensions);
                EndContractBlock();

                Assume(Dimensions == _components.Length);
#endif
                _components[index] = value;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static VectorF CreateUnit(int size, int dimension)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (dimension >= size)
            {
                throw new ArgumentOutOfRangeException(nameof(dimension));
            }

            var result = new VectorF(size);
            result.Set(dimension, 1.0f);
            return result;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float Get(int dimension)
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
        public void Set(int dimension, float value)
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
        public VectorF GetSum(VectorF right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var sum = new VectorF(_components.Length);
            var sumComponents = sum._components;
            var rightComponents = right._components;

            for (int i = 0; i < _components.Length; i++)
            {
                sumComponents[i] = _components[i] + rightComponents[i];
            }

            return sum;
        }

        public void Add(VectorF right)
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
        public VectorF GetDiffernce(VectorF right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (_components.Length != right._components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var difference = new VectorF(_components.Length);
            var diffComponents = difference._components;
            var rightComponents = right._components;

            for (int i = 0; i < _components.Length; i++)
            {
                diffComponents[i] = _components[i] - rightComponents[i];
            }

            return difference;
        }

        public void Subtract(VectorF right)
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
        public VectorF GetScaled(float scalar)
        {
            var scaled = new VectorF(_components.Length);
            var scaledComponents = scaled._components;

            for (int i = 0; i < scaledComponents.Length; i++)
            {
                scaledComponents[i] = _components[i] * scalar;
            }

            return scaled;
        }

        public void Scale(float scalar)
        {
            for (int i = 0; i < _components.Length; i++)
            {
                _components[i] *= scalar;
            }
        }

        public void Divide(float divisor)
        {
            for (int i = 0; i < _components.Length; i++)
            {
                _components[i] /= divisor;
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorF GetQuotient(float divisor)
        {
            var quotient = new VectorF(_components.Length);

            var quotientComponents = quotient._components;
            for (int i = 0; i < _components.Length; i++)
            {
                quotientComponents[i] = _components[i] / divisor;
            }

            return quotient;
        }

        public void Negate()
        {
            for (var i = 0; i < _components.Length; i++)
            {
                MathEx.Negate(ref _components[i]);
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorF GetNegative()
        {
            var negated = new VectorF(_components.Length);

            var negatedComponents = negated._components;
            for (var i = 0; i < _components.Length; i++)
            {
                negatedComponents[i] = -_components[i];
            }

            return negated;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDot(VectorF right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var rightComponents = right._components;
            if (_components.Length != rightComponents.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            if (_components.Length == 0)
            {
                return 0.0f;
            }

            var sum = _components[0] * rightComponents[0];
            for (int i = 1; i < _components.Length; i++)
            {
                sum += _components[i] * rightComponents[i];
            }

            return sum;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public float GetMagnitude()
        {
            return (float)Math.Sqrt(GetMagnitudeSquared());
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetMagnitudeSquared()
        {
            if (_components.Length == 0)
            {
                return 0.0f;
            }

            var sum = MathEx.Square(_components[0]);
            for (int i = 1; i < _components.Length; i++)
            {
                sum += MathEx.Square(_components[i]);
            }

            return sum;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetAngleBetween(VectorF other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (other._components.Length != _components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            return (float)Math.Acos(
                GetDot(other)
                    / (float)Math.Sqrt(GetMagnitudeSquared() * other.GetMagnitudeSquared()));
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public float GetDistance(VectorF other)
        {
            return (float)Math.Sqrt(GetDistanceSquared(other));
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float GetDistanceSquared(VectorF other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var otherComponenets = other._components;
            if (otherComponenets.Length != _components.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            if (_components.Length == 0)
            {
                return 0.0f;
            }

            var sum = MathEx.Square(_components[0] - otherComponenets[0]);
            for (var i = 1; i < _components.Length; i++)
            {
                sum += MathEx.Square(_components[i] - otherComponenets[i]);
            }

            return sum;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorF GetProjected(VectorF other)
        {
            if (other.Dimensions != Dimensions)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            var scalarDenominator = GetMagnitudeSquared();
            return scalarDenominator == 0.0f
                ? other
                : GetScaled(GetDot(other) / scalarDenominator);
        }

#if !PRE_NETSTANDARD
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public VectorF GetNormal()
        {
            var magnitude = GetMagnitude();
            return magnitude == 0.0f ? this : GetQuotient(magnitude);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(VectorF other)
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
            return obj is VectorF && Equals((VectorF)obj);
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
