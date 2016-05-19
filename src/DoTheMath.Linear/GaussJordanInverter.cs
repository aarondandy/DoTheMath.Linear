using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    internal struct GaussJordanInverter<TMatrix> where TMatrix : IMatrixMutable<double>
    {
        private TMatrix _scratch;

        private TMatrix _inverse;

        /// <summary>
        /// Sets up the inverter.
        /// </summary>
        /// <param name="scratch">A scratch space used to calculate the inverse. Must be populated with the matrix to be inverted. The data will be destroyed.</param>
        /// <param name="inverse">An identity matrix that will be converted into the inverse if successful.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public GaussJordanInverter(TMatrix scratch, TMatrix inverse)
        {
#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Requires(scratch != null);
            System.Diagnostics.Contracts.Contract.Requires(inverse != null);
            System.Diagnostics.Contracts.Contract.Requires(scratch.Rows == scratch.Columns);
            System.Diagnostics.Contracts.Contract.Requires(scratch.Rows == inverse.Rows);
            System.Diagnostics.Contracts.Contract.Requires(scratch.Columns == inverse.Columns);
#endif

            _scratch = scratch;
            _inverse = inverse;
        }

        public TMatrix Inverse
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            if (currentElementValue == 1.0)
            {
                return true;
            }

            int rowIndexSearch;

            // attempt to find a row after this one that already has a 1.0 value
            rowIndexSearch = LocateNextRowFromDiagonalWithValueOne(ordinal);
            if (rowIndexSearch > ordinal)
            {
                _scratch.SwapRows(ordinal, rowIndexSearch);
                _inverse.SwapRows(ordinal, rowIndexSearch);
                return true;
            }

            // attempt to find an additive value from the next rows that can give us a 1.0 value here
            rowIndexSearch = LocateNextRowFromDiagonalThatCanSumToOne(ordinal, currentElementValue);
            if (rowIndexSearch > ordinal)
            {
                // TODO: optimize to avoid the multiplication
                _scratch.AddScaledRow(rowIndexSearch, ordinal, 1.0);
                _inverse.AddScaledRow(rowIndexSearch, ordinal, 1.0);
                return true;
            }

            // attempt to find a scalar value that can be applied to the row
            if (currentElementValue != 0)
            {
                // TODO: consider using division
                var scalar = 1.0 / currentElementValue;
                _scratch.ScaleRow(ordinal, scalar);
                _inverse.ScaleRow(ordinal, scalar);
                return true;
            }

            // if the current element is zero and we still have not found a way to get it to a 1 value
            // we need to try something else
            rowIndexSearch = LocateNextRowFromDiagonalWithNonZeroValue(ordinal);
            if (rowIndexSearch > ordinal)
            {
#if HAS_CODECONTRACTS
                // it is really important that this value not be 0.0 or it will cause an infinite loop
                System.Diagnostics.Contracts.Contract.Assume(_scratch.Get(rowIndexSearch, ordinal) != 0.0);
#endif

                _scratch.SwapRows(ordinal, rowIndexSearch);
                _inverse.SwapRows(ordinal, rowIndexSearch);

                return ForceDiagonalToOne(ordinal); // this should only ever be called recusively once
            }

            return false;
        }

        private bool ForceColumnElementToZero(int row, int column)
        {
#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Requires(row != column);
#endif
            var currentElementValue = _scratch.Get(row, column);
            if (currentElementValue == 0.0)
            {
                return true;
            }

            for (var searchRow = column; searchRow < _scratch.Rows; searchRow++)
            {
#if HAS_CODECONTRACTS
                System.Diagnostics.Contracts.Contract.Assume(searchRow != row);
#endif

                // find a value where currentElementValue + (searchElementValue * factor) == 0
                var searchElementValue = _scratch.Get(searchRow, column);

                if (searchElementValue == currentElementValue)
                {
                    // TODO: optimize to avoid the multiplication
                    _scratch.AddScaledRow(searchRow, row, -1.0);
                    _inverse.AddScaledRow(searchRow, row, -1.0);
                    return true;
                }

                if (searchElementValue != 0.0)
                {
                    var scalar = -currentElementValue / searchElementValue;
                    _scratch.AddScaledRow(searchRow, row, scalar);
                    _inverse.AddScaledRow(searchRow, row, scalar);
                    return true;
                }
            }

            return false;
        }

        private int LocateNextRowFromDiagonalWithValueOne(int ordinal)
        {
            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                if (_scratch.Get(row, ordinal) == 1.0)
                {
                    return row;
                }
            }

            return -1;
        }

        private int LocateNextRowFromDiagonalWithNonZeroValue(int ordinal)
        {
            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                if (_scratch.Get(row, ordinal) != 0.0)
                {
                    return row;
                }
            }

            return -1;
        }

        private int LocateNextRowFromDiagonalThatCanSumToOne(int ordinal, double value)
        {
            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                if (_scratch.Get(row, ordinal) + value == 1.0)
                {
                    return row;
                }
            }

            return -1;
        }
    }
}
