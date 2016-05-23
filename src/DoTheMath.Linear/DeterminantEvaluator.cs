using System.Runtime.CompilerServices;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    internal struct DeterminantEvaluator<TMatrix> where TMatrix : IMatrixMutable<double>
    {
        private TMatrix _scratch;

        private bool _determinantNegationRequired;

        /// <summary>
        /// Sets up the evaluator.
        /// </summary>
        /// <param name="scratch">A scratch space used to calculate the determinant. Must be populated with the matrix to find the determinant of. The matrix will be destroyed.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public DeterminantEvaluator(TMatrix scratch)
        {
#if HAS_CODECONTRACTS
            Requires(scratch != null);
            Requires(scratch.Rows == scratch.Columns);
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

#if HAS_CODECONTRACTS
        [Pure]
#endif
        private double GetDeterminantProduct()
        {
            if (_scratch.Columns == 0)
            {
                return 0.0;
            }

            var product = _scratch.Get(0, 0);

#if HAS_CODECONTRACTS
            Assume(!product.Equals(0.0));
#endif

            for (int ordinal = 1; ordinal < _scratch.Columns; ordinal++)
            {
                var elementValue = _scratch.Get(ordinal, ordinal);

#if HAS_CODECONTRACTS
                Assume(!elementValue.Equals(0.0));
#endif

                product *= elementValue;
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

                if (ElementLeftOfColumnAreAllZeros(row, ordinal))
                {
#if HAS_CODECONTRACTS
                    Assume(!currentElementValue.Equals(0.0));
                    Assume(diagonalValue.Equals(0.0));
#endif

                    SwapRows(ordinal, row);
                    return; // there is nothing more that could be swapped
                }
            }
        }

        private bool ForceElementUnderOrdinalToZero(int row, int ordinal)
        {
            var currentElementValue = _scratch.Get(row, ordinal);
            if (currentElementValue.Equals(0.0))
            {
                return true;
            }

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                var searchElementValue = _scratch.Get(searchRow, ordinal);
                if (!searchElementValue.Equals(0.0))
                {
                    if ((currentElementValue + searchElementValue).Equals(0.0))
                    {
                        // find a value where currentElementValue + searchElementValue == 0
                        if (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal))
                        {
                            // TODO: optimize to avoid the multiplication
                            _scratch.AddScaledRow(searchRow, row, 1.0);
                            return true;
                        }
                    }
                    else if ((currentElementValue - searchElementValue).Equals(0.0))
                    {
                        // find a value where currentElementValue - searchElementValue == 0
                        if (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal))
                        {
                            // TODO: optimize to avoid the multiplication
                            _scratch.AddScaledRow(searchRow, row, -1.0);
                            return true;
                        }
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
                    if (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal))
                    {
                        _scratch.AddScaledRow(searchRow, row, -currentElementValue / searchElementValue);
                        return true;
                    }
                }
            }

            return false;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        private bool ElementLeftOfColumnAreAllZeros(int row, int column)
        {
            for (var searchColumn = 0; searchColumn < column; searchColumn++)
            {
                if (!_scratch.Get(row, searchColumn).Equals(0.0))
                {
                    return false;
                }
            }

            return true;
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
            _determinantNegationRequired = !_determinantNegationRequired;
        }
    }
}
