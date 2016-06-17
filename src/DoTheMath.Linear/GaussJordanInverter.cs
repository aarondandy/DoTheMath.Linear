using System.Runtime.CompilerServices;
using static DoTheMath.Linear.Utilities.MathEx;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    internal struct GaussJordanInverter<TMatrix, TElement> where TMatrix : IMatrixMutable<TElement>
    {
        private TMatrix _scratch;

        private TMatrix _inverse;

        /// <summary>
        /// Sets up the inverter.
        /// </summary>
        /// <param name="scratch">A scratch space used to calculate the inverse. Must be populated with the matrix to be inverted. The matrix will be destroyed.</param>
        /// <param name="inverse">An identity matrix that will be converted into the inverse if successful.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public GaussJordanInverter(TMatrix scratch, TMatrix inverse)
        {
#if HAS_CODECONTRACTS
            Requires(scratch != null);
            Requires(inverse != null);
            Requires(scratch.Rows == scratch.Columns);
            Requires(inverse.Rows == inverse.Columns);
            Requires(scratch.Rows == inverse.Rows && scratch.Columns == inverse.Columns);
#endif

            _scratch = scratch;
            _inverse = inverse;
        }

        public TMatrix Inverse
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return _inverse;
            }
        }

        public bool Invert()
        {
            for (int column = 0; column < _scratch.Columns; column++)
            {
                if (!ForceDiagonalToOne(column))
                {
                    return false;
                }

#if HAS_CODECONTRACTS
                Assume(column <= _scratch.Rows);
#endif

                for (int row = 0; row < column; row++)
                {
                    if (!ForceColumnElementToZero(row, column))
                    {
                        return false;
                    }
                }

                for (int row = column + 1; row < _scratch.Rows; row++)
                {
                    if (!ForceColumnElementToZero(row, column))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ForceDiagonalToOne(int ordinal)
        {
            var currentElementValue = _scratch[ordinal, ordinal];
            if (IsOne(currentElementValue))
            {
                return true;
            }

            int rowIndexSearch;
            var rowSearchStart = ordinal + 1;

            // attempt to find a row after this one that already has a 1.0 value
            for(rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if (IsOne(_scratch[rowIndexSearch, ordinal]))
                {
                    SwapRows(ordinal, rowIndexSearch);
                    return true;
                }
            }
            
            for (rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if (IsOne(Add(_scratch[rowIndexSearch, ordinal], currentElementValue)))
                {
                    AddRow(rowIndexSearch, ordinal);
                    return true;
                }
                else if (IsOne(Subtract(_scratch[rowIndexSearch, ordinal], currentElementValue)))
                {
                    SubtractRow(rowIndexSearch, ordinal);
                    return true;
                }
            }

            // attempt to find a scalar value that can be applied to the row
            if (!IsZero(currentElementValue))
            {
                DivideRow(ordinal, currentElementValue);
                return true;
            }

            // if the current element is zero and we still have not found a way to get it to a 1 value
            for (rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if (!IsZero(_scratch[rowIndexSearch, ordinal]))
                {
                    SwapRows(ordinal, rowIndexSearch);
                    return ForceDiagonalToOne(ordinal); // this should only ever be called recusively once
                }
            }

            return false;
        }

        private bool ForceColumnElementToZero(int row, int column)
        {
#if HAS_CODECONTRACTS
            Requires(row != column);
#endif
            var currentElementValue = _scratch[row, column];
            if (IsZero(currentElementValue))
            {
                return true;
            }

            for (var searchRow = column; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                var searchElementValue = _scratch[searchRow, column];

                if (IsZero(Add(searchElementValue, currentElementValue)))
                {
                    // find a value where currentElementValue + searchElementValue == 0
                    AddRow(searchRow, row);
                    return true;
                }
                else if (IsZero(Subtract(searchElementValue, currentElementValue)))
                {
                    // find a value where currentElementValue - searchElementValue == 0
                    SubtractRow(searchRow, row);
                    return true;
                }
            }

            for (var searchRow = column; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                // find a value where currentElementValue + (searchElementValue * factor) == 0
                var searchElementValue = _scratch[searchRow, column];
                if (!IsZero(searchElementValue))
                {
                    AddScaledRow(searchRow, row, Divide(Negate(currentElementValue), searchElementValue));
                    return true;
                }
            }

            return false;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void AddRow(int sourceRow, int targetRow)
        {
            _scratch.AddRow(sourceRow, targetRow);
            _inverse.AddRow(sourceRow, targetRow);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SubtractRow(int sourceRow, int targetRow)
        {
            _scratch.SubtractRow(sourceRow, targetRow);
            _inverse.SubtractRow(sourceRow, targetRow);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void AddScaledRow(int sourceRow, int targetRow, TElement scalar)
        {
            _scratch.AddScaledRow(sourceRow, targetRow, scalar);
            _inverse.AddScaledRow(sourceRow, targetRow, scalar);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SwapRows(int rowA, int rowB)
        {
#if HAS_CODECONTRACTS
            Requires(rowA != rowB);
#endif
            _scratch.SwapRows(rowA, rowB);
            _inverse.SwapRows(rowA, rowB);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void DivideRow(int row, TElement denominator)
        {
            _scratch.DivideRow(row, denominator);
            _inverse.DivideRow(row, denominator);
        }
    }
}
