﻿using System.Runtime.CompilerServices;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    internal struct GaussJordanInverter<TMatrix> where TMatrix : IMatrixMutable<double>
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
            var currentElementValue = _scratch.Get(ordinal, ordinal);
            if (currentElementValue.Equals(1.0))
            {
                return true;
            }

            int rowIndexSearch;
            var rowSearchStart = ordinal + 1;

            // attempt to find a row after this one that already has a 1.0 value
            for(rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if (_scratch.Get(rowIndexSearch, ordinal).Equals(1.0))
                {
                    SwapRows(ordinal, rowIndexSearch);
                    return true;
                }
            }
            
            for (rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if ((_scratch.Get(rowIndexSearch, ordinal) + currentElementValue).Equals(1.0))
                {
                    // TODO: optimize to avoid the multiplication
                    AddScaledRow(rowIndexSearch, ordinal, 1.0);
                    return true;
                }
                else if ((_scratch.Get(rowIndexSearch, ordinal) - currentElementValue).Equals(1.0))
                {
                    // TODO: optimize to avoid the multiplication
                    AddScaledRow(rowIndexSearch, ordinal, -1.0);
                    return true;
                }
            }

            // attempt to find a scalar value that can be applied to the row
            if (!currentElementValue.Equals(0.0))
            {
                DivideRow(ordinal, currentElementValue);
                return true;
            }

            // if the current element is zero and we still have not found a way to get it to a 1 value
            for (rowIndexSearch = rowSearchStart; rowIndexSearch < _scratch.Rows; rowIndexSearch++)
            {
                if (!_scratch.Get(rowIndexSearch, ordinal).Equals(0.0))
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
            var currentElementValue = _scratch.Get(row, column);
            if (currentElementValue.Equals(0.0))
            {
                return true;
            }

            for (var searchRow = column; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                var searchElementValue = _scratch.Get(searchRow, column);

                if ((searchElementValue + currentElementValue).Equals(0.0))
                {
                    // find a value where currentElementValue + searchElementValue == 0

                    // TODO: optimize to avoid the multiplication
                    AddScaledRow(searchRow, row, 1.0);
                    return true;
                }
                else if ((searchElementValue - currentElementValue).Equals(0.0))
                {
                    // find a value where currentElementValue - searchElementValue == 0

                    // TODO: optimize to avoid the multiplication
                    AddScaledRow(searchRow, row, -1.0);
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
                var searchElementValue = _scratch.Get(searchRow, column);
                if (!searchElementValue.Equals(0.0))
                {
                    AddScaledRow(searchRow, row, -currentElementValue / searchElementValue);
                    return true;
                }
            }

            return false;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void AddScaledRow(int sourceRow, int targetRow, double scalar)
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

        private void DivideRow(int row, double denominator)
        {
            _scratch.DivideRow(row, denominator);
            _inverse.DivideRow(row, denominator);
        }
    }
}
