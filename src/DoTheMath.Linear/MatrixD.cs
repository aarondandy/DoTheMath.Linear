using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public sealed class MatrixD :
        IMatrix<double>,
        IEquatable<MatrixD>
    {
        private double[] elements;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
        public MatrixD(int rows, int columns)
        {
            if (rows < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }
            if (columns < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Rows = rows;
            Columns = columns;
            elements = new double[checked(rows * columns)];
        }

        public int Rows
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get;
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private set;
        }

        public int Columns
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get;
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            private set;
        }

        public bool IsSquare
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return Rows == Columns;
            }
        }

        public bool IsIdentity
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                if (!IsSquare)
                {
                    return false;
                }

                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        if (elements[(Columns * row) + column] != ((row == column) ? 1.0 : 0.0))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public static MatrixD CreateIdentity(int order)
        {
            var result = new MatrixD(order, order);

            for(var rowAndColumn = 0; rowAndColumn < order; rowAndColumn++)
            {
                result.Set(rowAndColumn, rowAndColumn, 1.0);
            }

            return result;
        }

        /// <summary>
        /// Retrieves the element value at the given row and column.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The element value from the given location.</returns>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double Get(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return elements[(Columns * row) + column];
        }

        /// <summary>
        /// Sets the element value at the given row and column.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="value">The new value.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int row, int column, double value)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            elements[(Columns * row) + column] = value;
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(MatrixD other)
        {
            if(object.ReferenceEquals(this, other))
            {
                return true;
            }
            if(object.ReferenceEquals(null, other))
            {
                return false;
            }
            if(Rows != other.Rows || Columns != other.Columns)
            {
                return false;
            }

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(elements.Length == other.elements.Length);
#endif

            for (int elementIndex = 0; elementIndex < elements.Length; elementIndex++)
            {
                if (!elements[elementIndex].Equals(other.elements[elementIndex]))
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
            return Equals(obj as MatrixD);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override int GetHashCode()
        {
            unchecked
            {
                return 2203 + elements.Length * 23 + Rows * 23 + Columns;
            }
        }
    }
}
