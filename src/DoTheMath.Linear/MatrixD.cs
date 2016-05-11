using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;
using static DoTheMath.Linear.Utilities.Duplicator;

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

        public MatrixD(MatrixD source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Rows = source.Rows;
            Columns = source.Columns;

            elements = Clone(source.elements);
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

            for (var rowAndColumn = 0; rowAndColumn < order; rowAndColumn++)
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

        public void SwapRows(int rowA, int rowB)
        {
            if (rowA < 0 || rowA >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(rowA));
            }
            if (rowB < 0 || rowB >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(rowB));
            }

            if (rowA == rowB)
            {
                return;
            }

            var rowOffetA = Columns * rowA;
            var rowOffsetB = Columns * rowB;

            for (var column = 0; column < Columns; column++)
            {
                Swap(ref elements[rowOffetA + column], ref elements[rowOffsetB + column]);
            }
        }

        public void SwapColumns(int columnA, int columnB)
        {
            if (columnA < 0 || columnA >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(columnA));
            }
            if (columnB < 0 || columnB >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(columnB));
            }

            if (columnA == columnB)
            {
                return;
            }

            for (var rowOffset = 0; rowOffset < elements.Length; rowOffset += Columns)
            {
                Swap(ref elements[columnA + rowOffset], ref elements[columnB + rowOffset]);
            }
        }

        public void ScaleRow(int row, double value)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            var elementIndex = Columns * row;
            var elementIndexUpperBound = Columns + elementIndex;

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(elementIndexUpperBound <= elements.Length);
#endif

            for (; elementIndex < elementIndexUpperBound; elementIndex++)
            {
                elements[elementIndex] *= value;
            }
        }

        public void ScaleColumn(int column, double value)
        {
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (var elementIndex = column; elementIndex < elements.Length; elementIndex += Columns)
            {
                elements[elementIndex] *= value;
            }
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(MatrixD other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            if (Rows != other.Rows || Columns != other.Columns)
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
                return 2203 + elements.Length * 23 + Rows;
            }
        }
    }
}
