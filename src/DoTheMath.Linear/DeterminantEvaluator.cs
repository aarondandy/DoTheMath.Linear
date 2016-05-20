using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    internal struct DeterminantEvaluator<TMatrix> where TMatrix : IMatrixMutable<double>
    {
        private TMatrix _scratch;

        private bool _determinantNegationRequired;

        /// <summary>
        /// Sets up the evaluator.
        /// </summary>
        /// <param name="scratch">A scratch space used to calculate the inverse. Must be populated with the matrix to be inverted. The data will be destroyed.</param>
        /// <param name="inverse">An identity matrix that will be converted into the inverse if successful.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public DeterminantEvaluator(TMatrix scratch)
        {
#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Requires(scratch != null);
            System.Diagnostics.Contracts.Contract.Requires(scratch.Rows == scratch.Columns);
#endif

            _scratch = scratch;
            _determinantNegationRequired = false;
        }

        public double Evaluate()
        {
            for (int ordinal = 0; ordinal < _scratch.Columns; ordinal++)
            {
                ForceElementsUnderOrdinalToZero(ordinal);

                if (_scratch.Get(ordinal, ordinal).Equals(0.0))
                {
                    return 0.0;
                }
            }

            return GetDeterminantProduct();
        }

        private double GetDeterminantProduct()
        {
            if (_scratch.Columns == 0)
            {
                return 0.0;
            }

            var product = _scratch.Get(0, 0);
            for (int column = 1; column < _scratch.Columns; column++)
            {
                product *= _scratch.Get(column, column);
            }

            if (_determinantNegationRequired)
            {
                product = -product;
            }

            return product;
        }

        private bool ForceElementsUnderOrdinalToZero(int ordinal)
        {
            AttemptToMakeElementsUnderOrdinalZeroBySwapping(ordinal);

            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                var currentElementValue = _scratch.Get(row, ordinal);
                if (currentElementValue.Equals(0.0))
                {
                    continue;
                }

                if (!ForceElementUnderOrdinalToZero(row, ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        private void AttemptToMakeElementsUnderOrdinalZeroBySwapping(int ordinal)
        {
            var diagonalValue = _scratch.Get(ordinal, ordinal);
            if (!diagonalValue.Equals(0.0))
            {
                return;
            }

            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                var currentElementValue = _scratch.Get(row, ordinal);
                if (currentElementValue.Equals(0.0))
                {
                    continue;
                }

#if HAS_CODECONTRACTS
                System.Diagnostics.Contracts.Contract.Assume(!currentElementValue.Equals(0.0));

                // it is really important that this value be 0.0 or it will do no good
                System.Diagnostics.Contracts.Contract.Assume(diagonalValue.Equals(0.0));
#endif

                var isRowSuitableToSwap = !CanFindNonZerosLeftOfColumn(row, ordinal);

                if (isRowSuitableToSwap)
                {
                    SwapRows(ordinal, row);
                    return; // there is nothing more that could be swapped
                }
            }
        }

        private bool ForceElementUnderOrdinalToZero(int row, int ordinal)
        {
#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Requires(!_scratch.Get(row, ordinal).Equals(0.0));
#endif

            var column = ordinal;
            var currentElementValue = _scratch.Get(row, ordinal);

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                // find a value where currentElementValue - searchElementValue == 0
                var searchElementValue = _scratch.Get(searchRow, ordinal);

                if ((currentElementValue - searchElementValue).Equals(0.0))
                {
                    if (searchRow >= ordinal || !CanFindNonZerosLeftOfColumn(searchRow, ordinal))
                    {
                        // TODO: optimize to avoid the multiplication
                        _scratch.AddScaledRow(searchRow, row, -1.0);
                        return true;
                    }
                }
            }

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                // find a value where currentElementValue + (searchElementValue * factor) == 0
                var searchElementValue = _scratch.Get(searchRow, ordinal);

                if (!searchElementValue.Equals(0.0))
                {
                    if (searchRow >= ordinal || !CanFindNonZerosLeftOfColumn(searchRow, ordinal))
                    {
                        var scalar = -currentElementValue / searchElementValue;
                        _scratch.AddScaledRow(searchRow, row, scalar);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanFindNonZerosLeftOfColumn(int row, int column)
        {
            for (var searchColumn = 0; searchColumn < column; searchColumn++)
            {
                if (!_scratch.Get(row, searchColumn).Equals(0.0))
                {
                    return true;
                }
            }

            return false;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void SwapRows(int rowA, int rowB)
        {
#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Requires(rowA != rowB);
#endif
            _scratch.SwapRows(rowA, rowB);
            _determinantNegationRequired = !_determinantNegationRequired;
        }
    }
}
