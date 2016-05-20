﻿using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;

namespace DoTheMath.Linear
{
    public sealed class Matrix2D :
        IMatrixMutable<double>,
        IEquatable<Matrix2D>
    {
        /// <summary>
        /// The element at row 0 and column 0.
        /// </summary>
        public double E00;
        /// <summary>
        /// The element at row 0 and column 1.
        /// </summary>
        public double E01;
        /// <summary>
        /// The element at row 1 and column 0.
        /// </summary>
        public double E10;
        /// <summary>
        /// The element at row 1 and column 1.
        /// </summary>
        public double E11;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D()
        {
        }

        /// <summary>
        /// Constructs a new matrix with the given element values.
        /// </summary>
        /// <param name="e00">The value for the element at 0,0.</param>
        /// <param name="e01">The value for the element at 0,1.</param>
        /// <param name="e10">The value for the element at 1,0.</param>
        /// <param name="e11">The value for the element at 1,1.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D(
            double e00, double e01,
            double e10, double e11
        )
        {
            E00 = e00;
            E01 = e01;
            E10 = e10;
            E11 = e11;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D(Matrix2D source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            E00 = source.E00;
            E01 = source.E01;
            E10 = source.E10;
            E11 = source.E11;
        }

        public int Columns
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return 2;
            }
        }

        public int Rows
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return 2;
            }
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
                return true;
            }
        }

        public bool IsIdentity
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return E00 == 1.0 && E11 == 1.0
                    && E10 == 0.0 && E01 == 0.0;
            }
        }

        public bool IsSymetric
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return E01 == E10;
            }
        }

        public double Trace
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return E00 + E11;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Matrix2D CreateIdentity()
        {
            return new Matrix2D
            {
                E00 = 1.0,
                E11 = 1.0
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double Get(int row, int column)
        {
            if (row == 0)
            {
                if (column == 0)
                {
                    return E00;
                }
                if (column == 1)
                {
                    return E01;
                }
            }
            if (row == 1)
            {
                if (column == 0)
                {
                    return E10;
                }
                if (column == 1)
                {
                    return E11;
                }
            }

            throw new ArgumentOutOfRangeException((row & 0xfffffffe) == 0 ? nameof(column) : nameof(row));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int row, int column, double value)
        {
            if (row == 0)
            {
                if (column == 0)
                {
                    E00 = value;
                    return;
                }
                if (column == 1)
                {
                    E01 = value;
                    return;
                }
            }
            else if (row == 1)
            {
                if (column == 0)
                {
                    E10 = value;
                    return;
                }
                if (column == 1)
                {
                    E11 = value;
                    return;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            throw new ArgumentOutOfRangeException(nameof(column));
        }

        public void SwapRows(int rowA, int rowB)
        {
            if (unchecked((rowA & 0xfe) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(rowA));
            }
            if (unchecked((rowB & 0xfe) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(rowB));
            }

            if (rowA != rowB)
            {
                SwapPairs(
                    ref E00, ref E10,
                    ref E01, ref E11);
            }
        }

        public void SwapColumns(int columnA, int columnB)
        {
            if (unchecked((columnA & 0xfe) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(columnA));
            }
            if (unchecked((columnB & 0xfe) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(columnB));
            }

            if (columnA != columnB)
            {
                SwapPairs(
                    ref E00, ref E01,
                    ref E10, ref E11);
            }
        }

        public void ScaleRow(int row, double value)
        {
            if (row == 0)
            {
                E00 *= value;
                E01 *= value;
            }
            else if (row == 1)
            {
                E10 *= value;
                E11 *= value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
        }

        public void ScaleColumn(int column, double value)
        {
            if (column == 0)
            {
                E00 *= value;
                E10 *= value;
            }
            else if (column == 1)
            {
                E01 *= value;
                E11 *= value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void DivideRow(int row, double denominator)
        {
            if (row == 0)
            {
                E00 /= denominator;
                E01 /= denominator;
            }
            else if (row == 1)
            {
                E10 /= denominator;
                E11 /= denominator;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
        }

        public void DivideColumn(int column, double denominator)
        {
            if (column == 0)
            {
                E00 /= denominator;
                E10 /= denominator;
            }
            else if (column == 1)
            {
                E01 /= denominator;
                E11 /= denominator;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void AddScaledRow(int sourceRow, int targetRow, double scalar)
        {
            double e0, e1;
            if (sourceRow == 0)
            {
                e0 = E00;
                e1 = E01;
            }
            else if (sourceRow == 1)
            {
                e0 = E10;
                e1 = E11;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }

            e0 *= scalar;
            e1 *= scalar;

            if (targetRow == 0)
            {
                E00 += e0;
                E01 += e1;
            }
            else if (targetRow == 1)
            {
                E10 += e0;
                E11 += e1;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void AddScaledColumn(int sourceColumn, int targetColumn, double scalar)
        {
            double e0, e1;
            if (sourceColumn == 0)
            {
                e0 = E00;
                e1 = E10;
            }
            else if (sourceColumn == 1)
            {
                e0 = E01;
                e1 = E11;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }

            e0 *= scalar;
            e1 *= scalar;

            if (targetColumn == 0)
            {
                E00 += e0;
                E10 += e1;
            }
            else if (targetColumn == 1)
            {
                E01 += e0;
                E11 += e1;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix2D Add(Matrix2D other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            return new Matrix2D
            {
                E00 = other.E00 + E00,
                E01 = other.E01 + E01,
                E10 = other.E10 + E10,
                E11 = other.E11 + E11
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix2D Multiply(double scalar)
        {
            return new Matrix2D
            {
                E00 = E00 * scalar,
                E01 = E01 * scalar,
                E10 = E10 * scalar,
                E11 = E11 * scalar
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix2D Multiply(Matrix2D right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new Matrix2D
            {
                E00 = (E00 * right.E00) + (E01 * right.E10),
                E01 = (E00 * right.E01) + (E01 * right.E11),
                E10 = (E10 * right.E00) + (E11 * right.E10),
                E11 = (E10 * right.E01) + (E11 * right.E11)
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix2D Transposed()
        {
            return new Matrix2D
            {
                E00 = E00,
                E01 = E10,
                E10 = E01,
                E11 = E11
            };
        }

        public void Transpose()
        {
            Swap(ref E01, ref E10);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public double GetDeterminant()
        {
            var evaluator = new DeterminantEvaluator<Matrix2D>(new Matrix2D(this));
            return evaluator.Evaluate();
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix2D GetInverse()
        {
            var inverter = new GaussJordanInverter<Matrix2D>(
                new Matrix2D(this),
                Matrix2D.CreateIdentity());

            if (inverter.Invert())
            {
                return inverter.Inverse;
            }
            else
            {
                throw new NoInverseException();
            }
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(Matrix2D other)
        {
            return object.ReferenceEquals(this, other)
                || (
                    !object.ReferenceEquals(null, other)
                    && E00.Equals(other.E00)
                    && E01.Equals(other.E01)
                    && E10.Equals(other.E10)
                    && E11.Equals(other.E11)
                );
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix2D);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Rows;
        }
    }
}
