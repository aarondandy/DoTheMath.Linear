﻿using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;

namespace DoTheMath.Linear
{
    public sealed class Matrix3D :
        IMatrix<double>,
        IEquatable<Matrix3D>
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
        /// The element at row 0 and column 2.
        /// </summary>
        public double E02;
        /// <summary>
        /// The element at row 1 and column 0.
        /// </summary>
        public double E10;
        /// <summary>
        /// The element at row 1 and column 1.
        /// </summary>
        public double E11;
        /// <summary>
        /// The element at row 1 and column 2.
        /// </summary>
        public double E12;
        /// <summary>
        /// The element at row 2 and column 0.
        /// </summary>
        public double E20;
        /// <summary>
        /// The element at row 2 and column 1.
        /// </summary>
        public double E21;
        /// <summary>
        /// The element at row 2 and column 2.
        /// </summary>
        public double E22;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3D()
        {
        }

        /// <summary>
        /// Constructs a new matrix with the given element values.
        /// </summary>
        /// <param name="e00">The value for the element at 0,0.</param>
        /// <param name="e01">The value for the element at 0,1.</param>
        /// <param name="e02">The value for the element at 0,2.</param>
        /// <param name="e10">The value for the element at 1,0.</param>
        /// <param name="e11">The value for the element at 1,1.</param>
        /// <param name="e12">The value for the element at 1,2.</param>
        /// <param name="e20">The value for the element at 2,0.</param>
        /// <param name="e21">The value for the element at 2,1.</param>
        /// <param name="e22">The value for the element at 2,2.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3D(
            double e00, double e01, double e02,
            double e10, double e11, double e12,
            double e20, double e21, double e22
        )
        {
            E00 = e00;
            E01 = e01;
            E02 = e02;
            E10 = e10;
            E11 = e11;
            E12 = e12;
            E20 = e20;
            E21 = e21;
            E22 = e22;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3D(Matrix3D source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            E00 = source.E00;
            E01 = source.E01;
            E02 = source.E02;
            E10 = source.E10;
            E11 = source.E11;
            E12 = source.E12;
            E20 = source.E20;
            E21 = source.E21;
            E22 = source.E22;
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
                return 3;
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
                return 3;
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
                return E00 == 1.0 && E11 == 1.0 && E22 == 1.0
                    && E01 == 0.0 && E02 == 0.0
                    && E10 == 0.0 && E12 == 0.0
                    && E20 == 0.0 && E21 == 0.0;
            }
        }

        public bool IsSymetric
        {
#if HAS_CODECONTRACTS
            [System.Diagnostics.Contracts.Pure]
#endif
            get
            {
                return E01 == E10
                    && E02 == E20
                    && E12 == E21;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Matrix3D CreateIdentity()
        {
            return new Matrix3D
            {
                E00 = 1.0,
                E11 = 1.0,
                E22 = 1.0
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
                if (column == 2)
                {
                    return E02;
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
                if (column == 2)
                {
                    return E12;
                }
            }
            if (row == 2)
            {
                if (column == 0)
                {
                    return E20;
                }
                if (column == 1)
                {
                    return E21;
                }
                if (column == 2)
                {
                    return E22;
                }
            }

            throw new ArgumentOutOfRangeException(row >= 0 && row <= 2 ? nameof(column) : nameof(row));
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
                else if (column == 1)
                {
                    E01 = value;
                    return;
                }
                else if (column == 2)
                {
                    E02 = value;
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
                else if (column == 1)
                {
                    E11 = value;
                    return;
                }
                else if (column == 2)
                {
                    E12 = value;
                    return;
                }
            }
            else if (row == 2)
            {
                if (column == 0)
                {
                    E20 = value;
                    return;
                }
                else if (column == 1)
                {
                    E21 = value;
                    return;
                }
                else if (column == 2)
                {
                    E22 = value;
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
            if (rowA < 0 || rowA > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(rowA));
            }
            if (rowB < 0 || rowB > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(rowB));
            }

            if (rowA == rowB)
            {
                return;
            }

            if (rowA > rowB)
            {
                Swap(ref rowA, ref rowB);
            }

#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(rowA < rowB);
            System.Diagnostics.Contracts.Contract.Assume(rowA == 0 || rowA == 1);
            System.Diagnostics.Contracts.Contract.Assume(rowB == 1 || rowB == 2);
#endif

            if (rowA == 0)
            {
                if (rowB == 1)
                {
                    SwapPairs(
                        ref E00, ref E10,
                        ref E01, ref E11,
                        ref E02, ref E12);
                }
                else if (rowB == 2)
                {
                    SwapPairs(
                        ref E00, ref E20,
                        ref E01, ref E21,
                        ref E02, ref E22);
                }
            }
            else if (rowA == 1)
            {
#if HAS_CODECONTRACTS
                System.Diagnostics.Contracts.Contract.Assume(rowB == 2);
#endif
                SwapPairs(
                    ref E10, ref E20,
                    ref E11, ref E21,
                    ref E12, ref E22);

            }
        }

        public void SwapColumns(int columnA, int columnB)
        {
            if (columnA < 0 || columnA > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(columnA));
            }
            if (columnB < 0 || columnB > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(columnB));
            }

            if (columnA == columnB)
            {
                return;
            }
            if (columnA > columnB)
            {
                Swap(ref columnA, ref columnB);
            }


#if HAS_CODECONTRACTS
            System.Diagnostics.Contracts.Contract.Assume(columnA < columnB);
            System.Diagnostics.Contracts.Contract.Assume(columnA == 0 || columnA == 1);
            System.Diagnostics.Contracts.Contract.Assume(columnB == 1 || columnB == 2);
#endif

            if (columnA == 0)
            {
                if (columnB == 1)
                {
                    SwapPairs(
                        ref E00, ref E01,
                        ref E10, ref E11,
                        ref E20, ref E21);
                }
                else if (columnB == 2)
                {
                    SwapPairs(
                        ref E00, ref E02,
                        ref E10, ref E12,
                        ref E20, ref E22);
                }
            }
            else if (columnA == 1)
            {
#if HAS_CODECONTRACTS
                System.Diagnostics.Contracts.Contract.Assume(columnB == 2);
#endif
                SwapPairs(
                    ref E01, ref E02,
                    ref E11, ref E12,
                    ref E21, ref E22);
            }
        }

        public void ScaleRow(int row, double value)
        {
            switch (row)
            {
                case 0:
                    {
                        E00 *= value;
                        E01 *= value;
                        E02 *= value;
                        break;
                    }
                case 1:
                    {
                        E10 *= value;
                        E11 *= value;
                        E12 *= value;
                        break;
                    }
                case 2:
                    {
                        E20 *= value;
                        E21 *= value;
                        E22 *= value;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }

        public void ScaleColumn(int column, double value)
        {
            switch (column)
            {
                case 0:
                    {
                        E00 *= value;
                        E10 *= value;
                        E20 *= value;
                        break;
                    }
                case 1:
                    {
                        E01 *= value;
                        E11 *= value;
                        E21 *= value;
                        break;
                    }
                case 2:
                    {
                        E02 *= value;
                        E12 *= value;
                        E22 *= value;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void AddScaledRow(int sourceRow, int targetRow, double scalar)
        {
            double e0, e1, e2;
            switch (sourceRow)
            {
                case 0:
                    e0 = E00;
                    e1 = E01;
                    e2 = E02;
                    break;
                case 1:
                    e0 = E10;
                    e1 = E11;
                    e2 = E12;
                    break;
                case 2:
                    e0 = E20;
                    e1 = E21;
                    e2 = E22;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }

            e0 *= scalar;
            e1 *= scalar;
            e2 *= scalar;

            switch (targetRow)
            {
                case 0:
                    E00 += e0;
                    E01 += e1;
                    E02 += e2;
                    break;
                case 1:
                    E10 += e0;
                    E11 += e1;
                    E12 += e2;
                    break;
                case 2:
                    E20 += e0;
                    E21 += e1;
                    E22 += e2;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void AddScaledColumn(int sourceColumn, int targetColumn, double scalar)
        {
            double e0, e1, e2;
            switch (sourceColumn)
            {
                case 0:
                    e0 = E00;
                    e1 = E10;
                    e2 = E20;
                    break;
                case 1:
                    e0 = E01;
                    e1 = E11;
                    e2 = E21;
                    break;
                case 2:
                    e0 = E02;
                    e1 = E12;
                    e2 = E22;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }

            e0 *= scalar;
            e1 *= scalar;
            e2 *= scalar;

            switch (targetColumn)
            {
                case 0:
                    E00 += e0;
                    E10 += e1;
                    E20 += e2;
                    break;
                case 1:
                    E01 += e0;
                    E11 += e1;
                    E21 += e2;
                    break;
                case 2:
                    E02 += e0;
                    E12 += e1;
                    E22 += e2;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix3D Add(Matrix3D other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            return new Matrix3D
            {
                E00 = other.E00 + E00,
                E01 = other.E01 + E01,
                E02 = other.E02 + E02,
                E10 = other.E10 + E10,
                E11 = other.E11 + E11,
                E12 = other.E12 + E12,
                E20 = other.E20 + E20,
                E21 = other.E21 + E21,
                E22 = other.E22 + E22
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix3D Multiply(double scalar)
        {
            return new Matrix3D
            {
                E00 = E00 * scalar,
                E01 = E01 * scalar,
                E02 = E02 * scalar,
                E10 = E10 * scalar,
                E11 = E11 * scalar,
                E12 = E12 * scalar,
                E20 = E20 * scalar,
                E21 = E21 * scalar,
                E22 = E22 * scalar,
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix3D Multiply(Matrix3D right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new Matrix3D
            {
                E00 = (E00 * right.E00) + (E01 * right.E10) + (E02 * right.E20),
                E01 = (E00 * right.E01) + (E01 * right.E11) + (E02 * right.E21),
                E02 = (E00 * right.E02) + (E01 * right.E12) + (E02 * right.E22),
                E10 = (E10 * right.E00) + (E11 * right.E10) + (E12 * right.E20),
                E11 = (E10 * right.E01) + (E11 * right.E11) + (E12 * right.E21),
                E12 = (E10 * right.E02) + (E11 * right.E12) + (E12 * right.E22),
                E20 = (E20 * right.E00) + (E21 * right.E10) + (E22 * right.E20),
                E21 = (E20 * right.E01) + (E21 * right.E11) + (E22 * right.E21),
                E22 = (E20 * right.E02) + (E21 * right.E12) + (E22 * right.E22)
            };
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public Matrix3D Transposed()
        {
            return new Matrix3D
            {
                E00 = E00,
                E01 = E10,
                E02 = E20,

                E10 = E01,
                E11 = E11,
                E12 = E21,

                E20 = E02,
                E21 = E12,
                E22 = E22,
            };
        }

        public void Transpose()
        {
            Swap(ref E01, ref E10);
            Swap(ref E02, ref E20);
            Swap(ref E12, ref E21);
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public bool Equals(Matrix3D other)
        {
            return object.ReferenceEquals(this, other)
                || (
                    !object.ReferenceEquals(null, other)
                    && E00.Equals(other.E00)
                    && E01.Equals(other.E01)
                    && E02.Equals(other.E02)
                    && E10.Equals(other.E10)
                    && E11.Equals(other.E11)
                    && E12.Equals(other.E12)
                    && E20.Equals(other.E20)
                    && E21.Equals(other.E21)
                    && E22.Equals(other.E22)
                );
        }

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix3D);
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
