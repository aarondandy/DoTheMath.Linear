﻿using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public sealed class Matrix3F :
        IMatrixMutable<float>,
        IEquatable<Matrix3F>
    {
        /// <summary>
        /// The element at row 0 and column 0.
        /// </summary>
        public float E00;
        /// <summary>
        /// The element at row 0 and column 1.
        /// </summary>
        public float E01;
        /// <summary>
        /// The element at row 0 and column 2.
        /// </summary>
        public float E02;
        /// <summary>
        /// The element at row 1 and column 0.
        /// </summary>
        public float E10;
        /// <summary>
        /// The element at row 1 and column 1.
        /// </summary>
        public float E11;
        /// <summary>
        /// The element at row 1 and column 2.
        /// </summary>
        public float E12;
        /// <summary>
        /// The element at row 2 and column 0.
        /// </summary>
        public float E20;
        /// <summary>
        /// The element at row 2 and column 1.
        /// </summary>
        public float E21;
        /// <summary>
        /// The element at row 2 and column 2.
        /// </summary>
        public float E22;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3F()
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
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3F(
            float e00, float e01, float e02,
            float e10, float e11, float e12,
            float e20, float e21, float e22
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3F(Matrix3F source)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix3F(IMatrix<float> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Rows != 3 || source.Columns != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            E00 = source[0, 0];
            E01 = source[0, 1];
            E02 = source[0, 2];
            E10 = source[1, 0];
            E11 = source[1, 1];
            E12 = source[1, 2];
            E20 = source[2, 0];
            E21 = source[2, 1];
            E22 = source[2, 2];
        }

        public int Columns
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return 3;
            }
        }

        public int Rows
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return 3;
            }
        }

        public bool IsSquare
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return true;
            }
        }

        public bool IsIdentity
        {
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E00 == 1.0f && E11 == 1.0f && E22 == 1.0f
                    && E01 == 0.0f && E02 == 0.0f
                    && E10 == 0.0f && E12 == 0.0f
                    && E20 == 0.0f && E21 == 0.0f;
            }
        }

        public bool IsSymetric
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E01 == E10
                    && E02 == E20
                    && E12 == E21;
            }
        }

        public float Trace
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E00 + E11 + E22;
            }
        }

        public float this[int row, int column]
        {
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
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
                else if (row == 1)
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
                else if (row == 2)
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

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD && !DEBUG
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            set
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
                    if (column == 2)
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
                    if (column == 1)
                    {
                        E11 = value;
                        return;
                    }
                    if (column == 2)
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
                    if (column == 1)
                    {
                        E21 = value;
                        return;
                    }
                    if (column == 2)
                    {
                        E22 = value;
                        return;
                    }
                }

                throw new IndexOutOfRangeException();
            }
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator +(Matrix3F left, Matrix3F right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator -(Matrix3F left, Matrix3F right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator *(Matrix3F left, Matrix3F right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetProduct(right);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator *(Matrix3F matrix, float scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return matrix.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator *(float scalar, Matrix3F matrix)
        {
            return matrix * scalar;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F operator /(Matrix3F matrix, float divisor)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return matrix.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateIdentity()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<Matrix3F>() != null);
#endif

            return new Matrix3F
            {
                E00 = 1.0f,
                E11 = 1.0f,
                E22 = 1.0f
            };
        }

        /// <summary>
        /// Creates a right-handed rotation matrix for the given rotation <paramref name="radians"/> around the X axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateRotationX(float radians)
        {
            var result = new Matrix3F
            {
                E00 = 1.0f,
                E11 = (float)Math.Cos(radians),
                E12 = (float)Math.Sin(radians)
            };
            result.E21 = -result.E12;
            result.E22 = result.E11;

            return result;
        }

        /// <summary>
        /// Creates a right-handed rotation matrix for the given rotation <paramref name="radians"/> around the Y axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateRotationY(float radians)
        {
            var result = new Matrix3F
            {
                E11 = 1.0f,
                E00 = (float)Math.Cos(radians),
                E20 = (float)Math.Sin(radians)
            };
            result.E02 = -result.E20;
            result.E22 = result.E00;

            return result;
        }

        /// <summary>
        /// Creates a right-handed rotation matrix for the given rotation <paramref name="radians"/> around the Z axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateRotationZ(float radians)
        {
            var result = new Matrix3F
            {
                E22 = 1.0f,
                E00 = (float)Math.Cos(radians),
                E01 = (float)Math.Sin(radians)
            };
            result.E10 = -result.E01;
            result.E11 = result.E00;

            return result;
        }

        /// <summary>
        /// Creates a z-axis rotation matrix for the given rotation <paramref name="radians"/> around the given 2D <paramref name="origin"/>.
        /// </summary>
        /// <param name="origin">The origin to rotate around.</param>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates around the <paramref name="origin"/>.</returns>
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateRotation(Vector2F origin, float radians)
        {
            var result = new Matrix3F
            {
                E22 = 1.0f,
                E00 = (float)Math.Cos(radians),
                E01 = (float)Math.Sin(radians)
            };
            result.E10 = -result.E01;
            result.E11 = result.E00;
            result.E20 = origin.X - (result.E00 * origin.X) + (result.E01 * origin.Y);
            result.E21 = origin.Y - (result.E01 * origin.X) - (result.E00 * origin.Y);

            return result;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateScaled(Vector2F factors)
        {
            return new Matrix3F
            {
                E00 = factors.X,
                E11 = factors.Y,
                E22 = 1.0f
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateScaled(Vector3F factors)
        {
            return new Matrix3F
            {
                E00 = factors.X,
                E11 = factors.Y,
                E22 = factors.Z
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix3F CreateTranslation(Vector2F delta)
        {
            return new Matrix3F
            {
                E00 = 1.0f,
                E11 = 1.0f,
                E22 = 1.0f,
                E20 = delta.X,
                E21 = delta.Y
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public float Get(int row, int column)
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

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int row, int column, float value)
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
                if (column == 2)
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
                if (column == 1)
                {
                    E11 = value;
                    return;
                }
                if (column == 2)
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
                if (column == 1)
                {
                    E21 = value;
                    return;
                }
                if (column == 2)
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
            Assume(rowA < rowB);
            Assume(rowA == 0 || rowA == 1);
            Assume(rowB == 1 || rowB == 2);
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
                Assume(rowB == 2);
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
            Assume(columnA < columnB);
            Assume(columnA == 0 || columnA == 1);
            Assume(columnB == 1 || columnB == 2);
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
                Assume(columnB == 2);
#endif
                SwapPairs(
                    ref E01, ref E02,
                    ref E11, ref E12,
                    ref E21, ref E22);
            }
        }

        public void ScaleRow(int row, float value)
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

        public void ScaleColumn(int column, float value)
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

        public void DivideRow(int row, float denominator)
        {
            switch (row)
            {
                case 0:
                    {
                        E00 /= denominator;
                        E01 /= denominator;
                        E02 /= denominator;
                        break;
                    }
                case 1:
                    {
                        E10 /= denominator;
                        E11 /= denominator;
                        E12 /= denominator;
                        break;
                    }
                case 2:
                    {
                        E20 /= denominator;
                        E21 /= denominator;
                        E22 /= denominator;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }

        public void DivideColumn(int column, float denominator)
        {
            switch (column)
            {
                case 0:
                    {
                        E00 /= denominator;
                        E10 /= denominator;
                        E20 /= denominator;
                        break;
                    }
                case 1:
                    {
                        E01 /= denominator;
                        E11 /= denominator;
                        E21 /= denominator;
                        break;
                    }
                case 2:
                    {
                        E02 /= denominator;
                        E12 /= denominator;
                        E22 /= denominator;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void AddRow(int sourceRow, int targetRow)
        {
            float e0, e1, e2;
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

        public void SubtractRow(int sourceRow, int targetRow)
        {
            float e0, e1, e2;
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

            switch (targetRow)
            {
                case 0:
                    E00 -= e0;
                    E01 -= e1;
                    E02 -= e2;
                    break;
                case 1:
                    E10 -= e0;
                    E11 -= e1;
                    E12 -= e2;
                    break;
                case 2:
                    E20 -= e0;
                    E21 -= e1;
                    E22 -= e2;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void AddScaledRow(int sourceRow, int targetRow, float scalar)
        {
            float e0, e1, e2;
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

        public void AddColumn(int sourceColumn, int targetColumn)
        {
            float e0, e1, e2;
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

        public void SubtractColumn(int sourceColumn, int targetColumn)
        {
            float e0, e1, e2;
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

            switch (targetColumn)
            {
                case 0:
                    E00 -= e0;
                    E10 -= e1;
                    E20 -= e2;
                    break;
                case 1:
                    E01 -= e0;
                    E11 -= e1;
                    E21 -= e2;
                    break;
                case 2:
                    E02 -= e0;
                    E12 -= e1;
                    E22 -= e2;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

        public void AddScaledColumn(int sourceColumn, int targetColumn, float scalar)
        {
            float e0, e1, e2;
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
        [Pure]
#endif
        public Matrix3F GetSum(Matrix3F other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Matrix3F
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
        [Pure]
#endif
        public Matrix3F GetDifference(Matrix3F other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Matrix3F
            {
                E00 = E00 - other.E00,
                E01 = E01 - other.E01,
                E02 = E02 - other.E02,
                E10 = E10 - other.E10,
                E11 = E11 - other.E11,
                E12 = E12 - other.E12,
                E20 = E20 - other.E20,
                E21 = E21 - other.E21,
                E22 = E22 - other.E22
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix3F GetScaled(float scalar)
        {
            return new Matrix3F
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
        [Pure]
#endif
        public Matrix3F GetQuotient(float divisor)
        {
            return new Matrix3F
            {
                E00 = E00 / divisor,
                E01 = E01 / divisor,
                E02 = E02 / divisor,
                E10 = E10 / divisor,
                E11 = E11 / divisor,
                E12 = E12 / divisor,
                E20 = E20 / divisor,
                E21 = E21 / divisor,
                E22 = E22 / divisor,
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix3F GetProduct(Matrix3F right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new Matrix3F
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
        [Pure]
#endif
        public Matrix3F GetTranspose()
        {
            return new Matrix3F
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
        [Pure]
#endif
        public float GetDeterminant()
        {
            var evaluator = new DeterminantEvaluator<Matrix3F, float>(new Matrix3F(this));
            return evaluator.Evaluate();
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix3F GetInverse()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<Matrix3F>() != null);
#endif

            var inverter = new GaussJordanInverter<Matrix3F, float>(new Matrix3F(this), CreateIdentity());
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            return inverter.Inverse;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool TryGetInverse(out Matrix3F inverse)
        {
            var inverter = new GaussJordanInverter<Matrix3F, float>(new Matrix3F(this), CreateIdentity());
            var successful = inverter.Invert();
            inverse = inverter.Inverse;
            return successful;
        }

        public void Invert()
        {
            var inverter = new GaussJordanInverter<Matrix3F, float>(new Matrix3F(this), CreateIdentity());
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            CopyFrom(inverter.Inverse);
        }

        public bool TryInvert()
        {
            var inverter = new GaussJordanInverter<Matrix3F, float>(new Matrix3F(this), CreateIdentity());
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
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F GetProduct(Vector2F rowVector)
        {
            return new Vector2F
            {
                X = (rowVector.X * E00) + (rowVector.Y * E10) + E20,
                Y = (rowVector.X * E01) + (rowVector.Y * E11) + E21
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3F GetProduct(Vector3F rowVector)
        {
            return new Vector3F
            {
                X = (rowVector.X * E00) + (rowVector.Y * E10) + (rowVector.Z * E20),
                Y = (rowVector.X * E01) + (rowVector.Y * E11) + (rowVector.Z * E21),
                Z = (rowVector.X * E02) + (rowVector.Y * E12) + (rowVector.Z * E22)
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2F GetProductColumnVector(Vector2F columnVector)
        {
            return new Vector2F
            {
                X = (E00 * columnVector.X) + (E01 * columnVector.Y) + E02,
                Y = (E10 * columnVector.X) + (E11 * columnVector.Y) + E12
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3F GetProductColumnVector(Vector3F columnVector)
        {
            return new Vector3F
            {
                X = (E00 * columnVector.X) + (E01 * columnVector.Y) + (E02 * columnVector.Z),
                Y = (E10 * columnVector.X) + (E11 * columnVector.Y) + (E12 * columnVector.Z),
                Z = (E20 * columnVector.X) + (E21 * columnVector.Y) + (E22 * columnVector.Z)
            };
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Transform(ref Vector2F rowVector)
        {
            var x = rowVector.X;
            rowVector.X = (x * E00) + (rowVector.Y * E10) + E20;
            rowVector.Y = (x * E01) + (rowVector.Y * E11) + E21;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Transform(ref Vector3F rowVector)
        {
            var x = rowVector.X;
            var y = rowVector.Y;
            rowVector.X = (x * E00) + (y * E10) + (rowVector.Z * E20);
            rowVector.Y = (x * E01) + (y * E11) + (rowVector.Z * E21);
            rowVector.Z = (x * E02) + (y * E12) + (rowVector.Z * E22);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool Equals(Matrix3F other)
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
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix3F);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Rows;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CopyFrom(Matrix3F source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
#endif
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
    }
}
