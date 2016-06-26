using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;
using static DoTheMath.Linear.Utilities.Duplicator;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

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
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public MatrixD(IMatrix<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _rows = source.Rows;
            _columns = source.Columns;
            _elements = new double[checked(_rows * _columns)];

            for (int row = 0; row < _rows; row++)
            {
                var rowOffset = row * _columns;
                for (int column = 0; column < _columns; column++)
                {
                    _elements[rowOffset + column] = source[row, column];
                }
            }
        }

        public int Rows
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get { return _rows; }
        }

        public int Columns
        {
#if HAS_CODECONTRACTS
            [Pure]
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
            [Pure]
#endif
            get
            {
                return Rows == Columns;
            }
        }

        public double this[int row, int column]
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return _elements[(Columns * row) + column];
            }
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            set
            {
                _elements[(Columns * row) + column] = value;
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool CheckIdentity()
        {
            if (!IsSquare)
            {
                return false;
            }

            for (var row = 0; row < Rows; row++)
            {
                var rowOffset = (Columns * row);
                if (_elements[rowOffset + row] != 1.0)
                {
                    return false;
                }

#if HAS_CODECONTRACTS
                Assume(row < Columns);
#endif

                for (var column = 0; column < row; column++)
                {
                    if (_elements[rowOffset + column] != 0.0)
                    {
                        return false;
                    }
                    if (_elements[(Columns * column) + row] != 0.0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static MatrixD CreateIdentity(int order)
        {
            var result = new MatrixD(order, order);

            for (var rowAndColumn = 0; rowAndColumn < order; rowAndColumn++)
            {
                result[rowAndColumn, rowAndColumn] = 1.0;
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
        [Pure]
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
            Assume(elementIndexUpperBound <= _elements.Length);
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
            Assume(elementIndexUpperBound <= _elements.Length);
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

        public void AddRow(int sourceRow, int targetRow)
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
                _elements[targetIndex] = _elements[sourceRowOffset + column] + _elements[targetIndex];
            }
        }

        public void SubtractRow(int sourceRow, int targetRow)
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
                _elements[targetIndex] = _elements[targetIndex] - _elements[sourceRowOffset + column];
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

        public void AddColumn(int sourceColumn, int targetColumn)
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
                _elements[targetIndex] = _elements[rowOffset + sourceColumn] + _elements[targetIndex];
            }
        }

        public void SubtractColumn(int sourceColumn, int targetColumn)
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
                _elements[targetIndex] = _elements[targetIndex] - _elements[rowOffset + sourceColumn];
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
        [Pure]
#endif
        public MatrixD GetSum(MatrixD other)
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
            Assume(_elements.Length == other._elements.Length);
            Assume(_elements.Length == sum._elements.Length);
#endif

            for (var elementIndex = 0; elementIndex < sum._elements.Length; elementIndex++)
            {
                sum._elements[elementIndex] = _elements[elementIndex] + other._elements[elementIndex];
            }

            return sum;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetDifference(MatrixD other)
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
            Assume(_elements.Length == other._elements.Length);
            Assume(_elements.Length == sum._elements.Length);
#endif

            for (var elementIndex = 0; elementIndex < sum._elements.Length; elementIndex++)
            {
                sum._elements[elementIndex] = _elements[elementIndex] - other._elements[elementIndex];
            }

            return sum;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetScaled(double scalar)
        {
            var scaled = new MatrixD(Rows, Columns);
            var scaledElements = scaled._elements;

#if HAS_CODECONTRACTS
            Assume(_elements.Length == scaledElements.Length);
#endif

            for (var elementIndex = 0; elementIndex < scaledElements.Length; elementIndex++)
            {
                scaledElements[elementIndex] = _elements[elementIndex] * scalar;
            }

            return scaled;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetQuotient(double denominator)
        {
            var divided = new MatrixD(Rows, Columns);
            var dividedElements = divided._elements;

#if HAS_CODECONTRACTS
            Assume(_elements.Length == dividedElements.Length);
#endif

            for (var elementIndex = 0; elementIndex < dividedElements.Length; elementIndex++)
            {
                dividedElements[elementIndex] = _elements[elementIndex] / denominator;
            }

            return divided;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetProduct(MatrixD right)
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
            Assume(Columns == right.Rows);
            Assume(product.Rows == Rows);
            Assume(product.Columns == right.Columns);
#endif

            var rightElements = right._elements;
            var rightColumns = right.Columns;
            var productElements = product._elements;

            for (var row = 0; row < product.Rows; row++)
            {
                var leftRowOffset = Columns * row;
                var productRowOffset = product.Columns * row;

                for (var column = 0; column < product.Columns; column++)
                {
                    double sum = _elements[leftRowOffset] * rightElements[column];
                    for (var innerIndex = 1; innerIndex < Columns; innerIndex++)
                    {
                        sum += _elements[leftRowOffset + innerIndex] * rightElements[(rightColumns * innerIndex) + column];
                    }

                    productElements[productRowOffset + column] = sum;
                }
            }

            return product;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetTranspose()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<MatrixD>() != null);
            Ensures(Result<MatrixD>().Rows == Columns);
            Ensures(Result<MatrixD>().Columns == Rows);
#endif

            var transposed = new MatrixD(Columns, Rows);
            var transposedElements = transposed._elements;

#if HAS_CODECONTRACTS
            Assume(transposed._elements.Length == _elements.Length);
            Assume(transposedElements.Length == _elements.Length);
#endif

            for (int row = 0; row < Rows; row++)
            {
                var selfRowOffset = Columns * row;

                for (int column = 0; column < Columns; column++)
                {
                    transposedElements[(column * Rows) + row] = _elements[selfRowOffset + column];
                }
            }

            return transposed;
        }

        public void Transpose()
        {
            var newElements = new double[_elements.Length];

            for (var row = 0; row < Rows; row++)
            {
                var oldRowOffset = Columns * row;

                for (var column = 0; column < Columns; column++)
                {
                    newElements[(Rows * column) + row] = _elements[oldRowOffset + column];
                }
            }

            _elements = newElements;
            Swap(ref _rows, ref _columns);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDeterminant()
        {
            if (Rows != Columns)
            {
                throw new NoDeterminantException();
            }

            var evaluator = new DeterminantEvaluator<MatrixD, double>(new MatrixD(this));
            return evaluator.Evaluate();
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public MatrixD GetInverse()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<MatrixD>() != null);
#endif

            if (!IsSquare)
            {
                throw new NotSquareMatrixException();
            }

            var inverter = new GaussJordanInverter<MatrixD, double>(new MatrixD(this), CreateIdentity(Rows));
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            return inverter.Inverse;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool TryGetInverse(out MatrixD inverse)
        {
            if (!IsSquare)
            {
                inverse = null;
                return false;
            }

            var inverter = new GaussJordanInverter<MatrixD, double>(new MatrixD(this), CreateIdentity(Rows));
            var successful = inverter.Invert();
            inverse = inverter.Inverse;
            return successful;
        }

        public void Invert()
        {
            if (!IsSquare)
            {
                throw new NotSquareMatrixException();
            }

            var inverter = new GaussJordanInverter<MatrixD, double>(new MatrixD(this), CreateIdentity(Rows));
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            CopyFrom(inverter.Inverse);
        }

        public bool TryInvert()
        {
            if (!IsSquare)
            {
                return false;
            }

            var inverter = new GaussJordanInverter<MatrixD, double>(new MatrixD(this), CreateIdentity(Rows));
            var successful = inverter.Invert();
            if (successful)
            {
                CopyFrom(inverter.Inverse);
            }

            return successful;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetTrace()
        {
            if (!IsSquare)
            {
                throw new NotSquareMatrixException();
            }

            var sum = _elements[0];
            for (int ordinal = 1; ordinal < Rows; ordinal++)
            {
                sum += _elements[(Columns * ordinal) + ordinal];
            }
            return sum;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(MatrixD other)
        {
#if HAS_CODECONTRACTS
            Ensures(other != null || !Result<bool>());
#endif

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
            Assume(_elements.Length == other._elements.Length);
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
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return Equals(obj as MatrixD);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override int GetHashCode()
        {
            unchecked
            {
                return 2203 + _elements.Length * 23;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CopyFrom(MatrixD source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
            Requires(source.Rows == Rows);
            Requires(source.Columns == Columns);

            Assume(source._elements.Length == _elements.Length);
#endif

            CopyTo(source._elements, _elements);
        }
    }
}
