using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;
using static DoTheMath.Linear.Utilities.Duplicator;

namespace DoTheMath.Linear
{
    public sealed class MatrixD :
        IMatrixMutable<double>,
        IEquatable<MatrixD>
    {
        private double[] _elements;
        private int _rows;
        private int _columns;

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

            _rows = rows;
            _columns = columns;
            _elements = new double[checked(rows * columns)];
        }

        public MatrixD(MatrixD source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _rows = source.Rows;
            _columns = source.Columns;

            _elements = Clone(source._elements);
        }

        public int Rows
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get { return _rows; }
        }

        public int Columns
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get { return _columns; }
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
                        if (_elements[(Columns * row) + column] != ((row == column) ? 1.0 : 0.0))
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

            return _elements[(Columns * row) + column];
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

            _elements[(Columns * row) + column] = value;
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
                Swap(ref _elements[rowOffetA + column], ref _elements[rowOffsetB + column]);
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

            for (var rowOffset = 0; rowOffset < _elements.Length; rowOffset += Columns)
            {
                Swap(ref _elements[columnA + rowOffset], ref _elements[columnB + rowOffset]);
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
            System.Diagnostics.Contracts.Contract.Assume(elementIndexUpperBound <= _elements.Length);
#endif

            for (; elementIndex < elementIndexUpperBound; elementIndex++)
            {
                _elements[elementIndex] *= value;
            }
        }

        public void ScaleColumn(int column, double value)
        {
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (var elementIndex = column; elementIndex < _elements.Length; elementIndex += Columns)
            {
                _elements[elementIndex] *= value;
            }
        }

        public void DivideRow(int row, double denominator)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            var elementIndex = Columns * row;
            var elementIndexUpperBound = Columns + elementIndex;

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(elementIndexUpperBound <= _elements.Length);
#endif

            for (; elementIndex < elementIndexUpperBound; elementIndex++)
            {
                _elements[elementIndex] /= denominator;
            }
        }

        public void DivideColumn(int column, double denominator)
        {
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (var elementIndex = column; elementIndex < _elements.Length; elementIndex += Columns)
            {
                _elements[elementIndex] /= denominator;
            }
        }

        public void AddScaledRow(int sourceRow, int targetRow, double scalar)
        {
            if (sourceRow < 0 || sourceRow >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }
            if (targetRow < 0 || targetRow >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(targetRow));
            }

            var sourceRowOffset = Columns * sourceRow;
            var targetRowOffset = Columns * targetRow;
            for (int column = 0; column < Columns; column++)
            {
                var targetIndex = targetRowOffset + column;
                _elements[targetIndex] = (_elements[sourceRowOffset + column] * scalar) + _elements[targetIndex];
            }
        }

        public void AddScaledColumn(int sourceColumn, int targetColumn, double scalar)
        {
            if (sourceColumn < 0 || sourceColumn >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }
            if (targetColumn < 0 || targetColumn >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }

            for (int row = 0; row < Rows; row++)
            {
                var rowOffset = (Columns * row);
                var targetIndex = rowOffset + targetColumn;
                _elements[targetIndex] = (_elements[rowOffset + sourceColumn] * scalar) + _elements[targetIndex];
            }
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public MatrixD Add(MatrixD other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            if (other.Rows != Rows || other.Columns != Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(other));
            }

            var sum = new MatrixD(Rows, Columns);

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(_elements.Length == other._elements.Length);
            System.Diagnostics.Contracts.Contract.Assume(_elements.Length == sum._elements.Length);
#endif

            for (var elementIndex = 0; elementIndex < sum._elements.Length; elementIndex++)
            {
                sum._elements[elementIndex] = _elements[elementIndex] + other._elements[elementIndex];
            }

            return sum;
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public MatrixD Multiply(double scalar)
        {
            var scaled = new MatrixD(Rows, Columns);

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(_elements.Length == scaled._elements.Length);
#endif

            for (var elementIndex = 0; elementIndex < scaled._elements.Length; elementIndex++)
            {
                scaled._elements[elementIndex] = _elements[elementIndex] * scalar;
            }

            return scaled;
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public MatrixD Multiply(MatrixD right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            if (Columns != right.Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            var product = new MatrixD(Rows, right.Columns);

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(Columns == right.Rows);
            System.Diagnostics.Contracts.Contract.Assume(product.Rows == Rows);
            System.Diagnostics.Contracts.Contract.Assume(product.Columns == right.Columns);
#endif

            for (int row = 0; row < product.Rows; row++)
            {
                int leftRowOffset = Columns * row;
                int productRowOffset = product.Columns * row;

                for (int column = 0; column < product.Columns; column++)
                {
                    double sum = 0.0;
                    for (int innerIndex = 0; innerIndex < Columns; innerIndex++)
                    {
                        sum += _elements[leftRowOffset + innerIndex] * right.Get(innerIndex, column);
                    }

                    product._elements[productRowOffset + column] = sum;
                }
            }

            return product;
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public MatrixD Transposed()
        {
            var transposed = new MatrixD(Columns, Rows);

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(transposed._elements.Length == _elements.Length);
#endif

            for (int row = 0; row < Rows; row++)
            {
                var selfRowOffset = Columns * row;

                for (int column = 0; column < Columns; column++)
                {
                    transposed._elements[(column * Rows) + row] = _elements[selfRowOffset + column];
                }
            }

            return transposed;
        }

        public void Transpose()
        {
            var newElements = new double[_elements.Length];

            for(int elementIndex = 0; elementIndex < _elements.Length; elementIndex++)
            {
                var newIndex = ((elementIndex % Columns) * Rows) + (elementIndex / Columns);

                newElements[newIndex] = _elements[elementIndex];
            }
            
            _elements = newElements;
            Swap(ref _rows, ref _columns);
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
            System.Diagnostics.Contracts.Contract.Assume(_elements.Length == other._elements.Length);
#endif

            for (int elementIndex = 0; elementIndex < _elements.Length; elementIndex++)
            {
                if (!_elements[elementIndex].Equals(other._elements[elementIndex]))
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
                return 2203 + _elements.Length * 23;
            }
        }
    }
}
