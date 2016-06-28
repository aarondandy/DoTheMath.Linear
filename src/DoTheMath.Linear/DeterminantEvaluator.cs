using System.Runtime.CompilerServices;
using System;
using static DoTheMath.Linear.Utilities.MathEx;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    internal struct DeterminantEvaluator<TMatrix, TElement> where TMatrix : IMatrixMutable<TElement>
    {
        private TMatrix _scratch;

        private bool _determinantNegationRequired;

        /// <summary>
        /// Sets up the evaluator.
        /// </summary>
        /// <param name="scratch">A scratch space used to calculate the determinant. Must be populated with the matrix to find the determinant of. The matrix will be destroyed.</param>
#if !PRE_NETSTANDARD && RELEASE
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

        public TElement Evaluate()
        {
            for (int ordinal = 0; ordinal < _scratch.Columns; ordinal++)
            {
                for(int row = 0; row < _scratch.Rows; row++)
                {
                    var value = _scratch[row, ordinal];
                    if (IsNotNumber(value))
                    {
                        return value;
                    }
                }

                AttemptToMakeElementsUnderOrdinalZeroBySwapping(ordinal);

                if (!ForceElementsUnderOrdinalToZero(ordinal))
                {
                    return GetZero<TElement>();
                }
            }

            return GetDeterminantProduct();
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        private TElement GetDeterminantProduct()
        {
            if (_scratch.Columns == 0)
            {
                return GetZero<TElement>();
            }

            TElement product;

            if (_scratch.Columns == 2)
            {
                product = Multiply(_scratch[0, 0], _scratch[1, 1]);
            }
            else if (_scratch.Columns == 3)
            {
                product = Multiply(_scratch[0, 0], _scratch[1, 1], _scratch[2, 2]);
            }
            else if (_scratch.Columns == 4)
            {
                product = Multiply(_scratch[0, 0], _scratch[1, 1], _scratch[2, 2], _scratch[3, 3]);
            }
            else
            {
                product = _scratch[0, 0];

                for (int ordinal = 1; ordinal < _scratch.Columns; ordinal++)
                {
                    product = Multiply(_scratch[ordinal, ordinal], product);
                }
            }

            if (_determinantNegationRequired)
            {
                product = Negate(product);
            }

            return product;
        }

        private bool ForceElementsUnderOrdinalToZero(int ordinal)
        {
            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                if (!ForceElementUnderOrdinalToZero(row, ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        /// <remarks>
        /// All values under the diagonal and left of the ordinal column are to be assumed as zero.
        /// </remarks>
        private void AttemptToMakeElementsUnderOrdinalZeroBySwapping(int ordinal)
        {
            var diagonalValue = _scratch[ordinal, ordinal];
            if (!IsZero(diagonalValue))
            {
                return;
            }

            for (var row = ordinal + 1; row < _scratch.Rows; row++)
            {
                var currentElementValue = _scratch[row, ordinal];
                if (IsZero(currentElementValue))
                {
                    continue;
                }

                if (row > ordinal || ElementLeftOfColumnAreAllZeros(row, ordinal))
                {
#if HAS_CODECONTRACTS
                    Assume(!IsZero(currentElementValue));
                    Assume(IsZero(diagonalValue));
#endif

                    SwapRows(ordinal, row);
                    return; // there is nothing more that could be swapped
                }
            }
        }

        private bool TryForcingElementUnderOrdinalToZeroUsingAdditionOrSubtraction(int row, int ordinal)
        {
            var currentElementValue = _scratch[row, ordinal];

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                var searchElementValue = _scratch[searchRow, ordinal];
                if (!IsZero(searchElementValue) && (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal)))
                {
                    if (IsZero(Add(currentElementValue, searchElementValue)))
                    {
                        _scratch.AddRow(searchRow, row);
                        return true;
                    }
                    else if (IsZero(Subtract(currentElementValue, searchElementValue)))
                    {
                        _scratch.SubtractRow(searchRow, row);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryForcingElementUnderOrdinalToZeroUsingScaledAdditionOrSubtraction(int row, int ordinal)
        {
            var currentElementValue = _scratch[row, ordinal];

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                var searchElementValue = _scratch[searchRow, ordinal];
                if (!IsZero(searchElementValue) && (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal)))
                {
                    var scalar = Negate(Divide(currentElementValue, searchElementValue));

                    if (IsZero(Add(Multiply(searchElementValue, scalar), currentElementValue)))
                    {
                        _scratch.AddScaledRow(searchRow, row, scalar);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ForceElementUnderOrdinalToZero(int row, int ordinal)
        {
            var currentElementValue = _scratch[row, ordinal];
            if (IsZero(currentElementValue))
            {
                return true;
            }

            if (TryForcingElementUnderOrdinalToZeroUsingAdditionOrSubtraction(row, ordinal))
            {
                return true;
            }

            if(TryForcingElementUnderOrdinalToZeroUsingScaledAdditionOrSubtraction(row, ordinal))
            {
                return true;
            }

            for (var searchRow = 0; searchRow < _scratch.Rows; searchRow++)
            {
                if (searchRow == row)
                {
                    continue;
                }

                // find a value where currentElementValue + (searchElementValue * factor) == 0
                var searchElementValue = _scratch[searchRow, ordinal];
                if (!IsZero(searchElementValue) && !IsNotNumber(searchElementValue))
                {
                    if (searchRow >= ordinal || ElementLeftOfColumnAreAllZeros(searchRow, ordinal))
                    {
                        _scratch.AddScaledRow(searchRow, row, Negate(Divide(currentElementValue, searchElementValue)));
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
                if (!IsZero(_scratch[row, searchColumn]))
                {
                    return false;
                }
            }

            return true;
        }

#if !PRE_NETSTANDARD && RELEASE
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
