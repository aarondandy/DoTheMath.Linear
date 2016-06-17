using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public sealed class Matrix4D :
        IMatrixMutable<double>,
        IEquatable<Matrix4D>
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
        /// The element at row 0 and column 3.
        /// </summary>
        public double E03;
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
        /// The element at row 1 and column 3.
        /// </summary>
        public double E13;
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
        /// The element at row 2 and column 3.
        /// </summary>
        public double E23;
        /// <summary>
        /// The element at row 3 and column 0.
        /// </summary>
        public double E30;
        /// <summary>
        /// The element at row 3 and column 1.
        /// </summary>
        public double E31;
        /// <summary>
        /// The element at row 3 and column 2.
        /// </summary>
        public double E32;
        /// <summary>
        /// The element at row 3 and column 3.
        /// </summary>
        public double E33;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix4D()
        {
        }

        /// <summary>
        /// Constructs a new matrix with the given element values.
        /// </summary>
        /// <param name="e00">The value for the element at 0,0.</param>
        /// <param name="e01">The value for the element at 0,1.</param>
        /// <param name="e02">The value for the element at 0,2.</param>
        /// <param name="e03">The value for the element at 0,3.</param>
        /// <param name="e10">The value for the element at 1,0.</param>
        /// <param name="e11">The value for the element at 1,1.</param>
        /// <param name="e12">The value for the element at 1,2.</param>
        /// <param name="e13">The value for the element at 1,3.</param>
        /// <param name="e20">The value for the element at 2,0.</param>
        /// <param name="e21">The value for the element at 2,1.</param>
        /// <param name="e22">The value for the element at 2,2.</param>
        /// <param name="e23">The value for the element at 2,3.</param>
        /// <param name="e30">The value for the element at 3,0.</param>
        /// <param name="e31">The value for the element at 3,1.</param>
        /// <param name="e32">The value for the element at 3,2.</param>
        /// <param name="e33">The value for the element at 3,3.</param>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix4D(
            double e00, double e01, double e02, double e03,
            double e10, double e11, double e12, double e13,
            double e20, double e21, double e22, double e23,
            double e30, double e31, double e32, double e33
        )
        {
            E00 = e00;
            E01 = e01;
            E02 = e02;
            E03 = e03;
            E10 = e10;
            E11 = e11;
            E12 = e12;
            E13 = e13;
            E20 = e20;
            E21 = e21;
            E22 = e22;
            E23 = e23;
            E30 = e30;
            E31 = e31;
            E32 = e32;
            E33 = e33;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix4D(Matrix4D source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            E00 = source.E00;
            E01 = source.E01;
            E02 = source.E02;
            E03 = source.E03;
            E10 = source.E10;
            E11 = source.E11;
            E12 = source.E12;
            E13 = source.E13;
            E20 = source.E20;
            E21 = source.E21;
            E22 = source.E22;
            E23 = source.E23;
            E30 = source.E30;
            E31 = source.E31;
            E32 = source.E32;
            E33 = source.E33;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix4D(IMatrix<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Rows != 4 || source.Columns != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            E00 = source[0, 0];
            E01 = source[0, 1];
            E02 = source[0, 2];
            E03 = source[0, 3];
            E10 = source[1, 0];
            E11 = source[1, 1];
            E12 = source[1, 2];
            E13 = source[1, 3];
            E20 = source[2, 0];
            E21 = source[2, 1];
            E22 = source[2, 2];
            E23 = source[2, 3];
            E30 = source[3, 0];
            E31 = source[3, 1];
            E32 = source[3, 2];
            E33 = source[3, 3];
        }

        public int Columns
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return 4;
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
            get
            {
                return 4;
            }
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
                return E00 == 1.0 && E11 == 1.0 && E22 == 1.0 && E33 == 1.0
                    && E01 == 0.0 && E02 == 0.0 && E03 == 0.0
                    && E10 == 0.0 && E12 == 0.0 && E13 == 0.0
                    && E20 == 0.0 && E21 == 0.0 && E23 == 0.0
                    && E30 == 0.0 && E31 == 0.0 && E32 == 0.0;
            }
        }

        public bool IsSymetric
        {
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E01 == E10
                    && E02 == E20
                    && E03 == E30
                    && E12 == E21
                    && E13 == E31
                    && E23 == E32;
            }
        }

        public double Trace
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E00 + E11 + E22 + E33;
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
                if (unchecked((row & 0xfffffffc) == 0) && unchecked((column & 0xfffffffc) == 0))
                {
                    switch (unchecked((row << 2) | column))
                    {
                        case 0: return E00;
                        case 1: return E01;
                        case 2: return E02;
                        case 3: return E03;
                        case 4: return E10;
                        case 5: return E11;
                        case 6: return E12;
                        case 7: return E13;
                        case 8: return E20;
                        case 9: return E21;
                        case 10: return E22;
                        case 11: return E23;
                        case 12: return E30;
                        case 13: return E31;
                        case 14: return E32;
                        default: return E33;
                    }
                }

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            set
            {
                if (unchecked((row & 0xfffffffc) == 0) && unchecked((column & 0xfffffffc) == 0))
                {
                    switch (unchecked((row << 2) | column))
                    {
                        case 0: E00 = value; return;
                        case 1: E01 = value; return;
                        case 2: E02 = value; return;
                        case 3: E03 = value; return;
                        case 4: E10 = value; return;
                        case 5: E11 = value; return;
                        case 6: E12 = value; return;
                        case 7: E13 = value; return;
                        case 8: E20 = value; return;
                        case 9: E21 = value; return;
                        case 10: E22 = value; return;
                        case 11: E23 = value; return;
                        case 12: E30 = value; return;
                        case 13: E31 = value; return;
                        case 14: E32 = value; return;
                        default: E33 = value; return;
                    }
                }

                throw new IndexOutOfRangeException();
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator +(Matrix4D left, Matrix4D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetSum(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator -(Matrix4D left, Matrix4D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetDifference(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator *(Matrix4D left, Matrix4D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return left.GetProduct(right);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator *(Matrix4D matrix, double scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return matrix.GetScaled(scalar);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator *(double scalar, Matrix4D matrix)
        {
            return matrix * scalar;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D operator /(Matrix4D matrix, double divisor)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return matrix.GetQuotient(divisor);
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateIdentity()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<Matrix4D>() != null);
#endif

            return new Matrix4D
            {
                E00 = 1.0,
                E11 = 1.0,
                E22 = 1.0,
                E33 = 1.0
            };
        }

        /// <summary>
        /// Creates a rotation matrix for the given rotation <paramref name="radians"/> around the X axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateRotationX(double radians)
        {
            var result = new Matrix4D
            {
                E00 = 1.0,
                E33 = 1.0,
                E11 = Math.Cos(radians),
                E21 = Math.Sin(radians)
            };
            result.E12 = -result.E21;
            result.E22 = result.E11;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix for the given rotation <paramref name="radians"/> around the Y axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateRotationY(double radians)
        {
            var result = new Matrix4D
            {
                E11 = 1.0,
                E33 = 1.0,
                E00 = Math.Cos(radians),
                E02 = Math.Sin(radians)
            };
            result.E20 = -result.E02;
            result.E22 = result.E00;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix for the given rotation <paramref name="radians"/> around the Z axis.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateRotationZ(double radians)
        {
            var result = new Matrix4D
            {
                E22 = 1.0,
                E33 = 1.0,
                E00 = Math.Cos(radians),
                E10 = Math.Sin(radians)
            };
            result.E01 = -result.E10;
            result.E11 = result.E00;

            return result;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateScaled(Vector3D factors)
        {
            return new Matrix4D
            {
                E00 = factors.X,
                E11 = factors.Y,
                E22 = factors.Z,
                E33 = 1.0
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateScaled(Vector4D factors)
        {
            return new Matrix4D
            {
                E00 = factors.X,
                E11 = factors.Y,
                E22 = factors.Z,
                E33 = factors.W
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix4D CreateTranslation(Vector3D delta)
        {
            return new Matrix4D
            {
                E00 = 1.0,
                E11 = 1.0,
                E22 = 1.0,
                E33 = 1.0,
                E03 = delta.X,
                E13 = delta.Y,
                E23 = delta.Z
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double Get(int row, int column)
        {
            if (unchecked((row & 0xfffffffc) == 0))
            {
                if (unchecked((column & 0xfffffffc) == 0))
                {
                    switch (unchecked((row << 2) | column))
                    {
                        case 0: return E00;
                        case 1: return E01;
                        case 2: return E02;
                        case 3: return E03;
                        case 4: return E10;
                        case 5: return E11;
                        case 6: return E12;
                        case 7: return E13;
                        case 8: return E20;
                        case 9: return E21;
                        case 10: return E22;
                        case 11: return E23;
                        case 12: return E30;
                        case 13: return E31;
                        case 14: return E32;
                        default: return E33;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(column));
            }

            throw new ArgumentOutOfRangeException(nameof(row));
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Set(int row, int column, double value)
        {
            if (unchecked((row & 0xfffffffc) == 0))
            {
                if (unchecked((column & 0xfffffffc) == 0))
                {
                    switch (unchecked((row << 2) | column))
                    {
                        case 0: E00 = value; return;
                        case 1: E01 = value; return;
                        case 2: E02 = value; return;
                        case 3: E03 = value; return;
                        case 4: E10 = value; return;
                        case 5: E11 = value; return;
                        case 6: E12 = value; return;
                        case 7: E13 = value; return;
                        case 8: E20 = value; return;
                        case 9: E21 = value; return;
                        case 10: E22 = value; return;
                        case 11: E23 = value; return;
                        case 12: E30 = value; return;
                        case 13: E31 = value; return;
                        case 14: E32 = value; return;
                        default: E33 = value; return;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(column));
            }

            throw new ArgumentOutOfRangeException(nameof(row));
        }

        public void SwapRows(int rowA, int rowB)
        {
            if (unchecked((rowA & 0xfffffffc) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(rowA));
            }
            if (unchecked((rowB & 0xfffffffc) != 0))
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
            Assume(rowA == 0 || rowA == 1 || rowA == 2);
            Assume(rowB == 1 || rowB == 2 || rowB == 3);
#endif

            if (rowA == 0)
            {
                if (rowB == 1)
                {
                    SwapPairs(
                        ref E00, ref E10,
                        ref E01, ref E11,
                        ref E02, ref E12,
                        ref E03, ref E13);
                }
                else if (rowB == 2)
                {
                    SwapPairs(
                        ref E00, ref E20,
                        ref E01, ref E21,
                        ref E02, ref E22,
                        ref E03, ref E23);
                }
                else if (rowB == 3)
                {
                    SwapPairs(
                        ref E00, ref E30,
                        ref E01, ref E31,
                        ref E02, ref E32,
                        ref E03, ref E33);
                }
            }
            else if (rowA == 1)
            {
#if HAS_CODECONTRACTS
                Assume(rowB == 2 || rowB == 3);
#endif
                if (rowB == 2)
                {
                    SwapPairs(
                        ref E10, ref E20,
                        ref E11, ref E21,
                        ref E12, ref E22,
                        ref E13, ref E23);
                }
                else if (rowB == 3)
                {
                    SwapPairs(
                        ref E10, ref E30,
                        ref E11, ref E31,
                        ref E12, ref E32,
                        ref E13, ref E33);
                }
            }
            else if (rowA == 2)
            {
#if HAS_CODECONTRACTS
                Assume(rowB == 3);
#endif

                SwapPairs(
                    ref E20, ref E30,
                    ref E21, ref E31,
                    ref E22, ref E32,
                    ref E23, ref E33);
            }

        }

        public void SwapColumns(int columnA, int columnB)
        {
            if (unchecked((columnA & 0xfffffffc) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(columnA));
            }
            if (unchecked((columnB & 0xfffffffc) != 0))
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
            Assume(columnA == 0 || columnA == 1 || columnA == 2);
            Assume(columnB == 1 || columnB == 2 || columnB == 3);
#endif

            if (columnA == 0)
            {
                if (columnB == 1)
                {
                    SwapPairs(
                        ref E00, ref E01,
                        ref E10, ref E11,
                        ref E20, ref E21,
                        ref E30, ref E31);
                }
                else if (columnB == 2)
                {
                    SwapPairs(
                        ref E00, ref E02,
                        ref E10, ref E12,
                        ref E20, ref E22,
                        ref E30, ref E32);
                }
                else if (columnB == 3)
                {
                    SwapPairs(
                        ref E00, ref E03,
                        ref E10, ref E13,
                        ref E20, ref E23,
                        ref E30, ref E33);
                }
            }
            else if (columnA == 1)
            {
#if HAS_CODECONTRACTS
                Assume(columnB == 2 || columnB == 3);
#endif

                if (columnB == 2)
                {
                    SwapPairs(
                        ref E01, ref E02,
                        ref E11, ref E12,
                        ref E21, ref E22,
                        ref E31, ref E32);
                }
                else if (columnB == 3)
                {
                    SwapPairs(
                        ref E01, ref E03,
                        ref E11, ref E13,
                        ref E21, ref E23,
                        ref E31, ref E33);
                }
            }
            else if (columnA == 2)
            {
#if HAS_CODECONTRACTS
                Assume(columnB == 3);
#endif

                SwapPairs(
                    ref E02, ref E03,
                    ref E12, ref E13,
                    ref E22, ref E23,
                    ref E32, ref E33);
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
                        E03 *= value;
                        break;
                    }
                case 1:
                    {
                        E10 *= value;
                        E11 *= value;
                        E12 *= value;
                        E13 *= value;
                        break;
                    }
                case 2:
                    {
                        E20 *= value;
                        E21 *= value;
                        E22 *= value;
                        E23 *= value;
                        break;
                    }
                case 3:
                    {
                        E30 *= value;
                        E31 *= value;
                        E32 *= value;
                        E33 *= value;
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
                        E30 *= value;
                        break;
                    }
                case 1:
                    {
                        E01 *= value;
                        E11 *= value;
                        E21 *= value;
                        E31 *= value;
                        break;
                    }
                case 2:
                    {
                        E02 *= value;
                        E12 *= value;
                        E22 *= value;
                        E32 *= value;
                        break;
                    }
                case 3:
                    {
                        E03 *= value;
                        E13 *= value;
                        E23 *= value;
                        E33 *= value;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void DivideRow(int row, double denominator)
        {
            switch (row)
            {
                case 0:
                    {
                        E00 /= denominator;
                        E01 /= denominator;
                        E02 /= denominator;
                        E03 /= denominator;
                        break;
                    }
                case 1:
                    {
                        E10 /= denominator;
                        E11 /= denominator;
                        E12 /= denominator;
                        E13 /= denominator;
                        break;
                    }
                case 2:
                    {
                        E20 /= denominator;
                        E21 /= denominator;
                        E22 /= denominator;
                        E23 /= denominator;
                        break;
                    }
                case 3:
                    {
                        E30 /= denominator;
                        E31 /= denominator;
                        E32 /= denominator;
                        E33 /= denominator;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }

        public void DivideColumn(int column, double denominator)
        {
            switch (column)
            {
                case 0:
                    {
                        E00 /= denominator;
                        E10 /= denominator;
                        E20 /= denominator;
                        E30 /= denominator;
                        break;
                    }
                case 1:
                    {
                        E01 /= denominator;
                        E11 /= denominator;
                        E21 /= denominator;
                        E31 /= denominator;
                        break;
                    }
                case 2:
                    {
                        E02 /= denominator;
                        E12 /= denominator;
                        E22 /= denominator;
                        E32 /= denominator;
                        break;
                    }
                case 3:
                    {
                        E03 /= denominator;
                        E13 /= denominator;
                        E23 /= denominator;
                        E33 /= denominator;
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public void AddRow(int sourceRow, int targetRow)
        {
            double e0, e1, e2, e3;
            switch (sourceRow)
            {
                case 0:
                    e0 = E00;
                    e1 = E01;
                    e2 = E02;
                    e3 = E03;
                    break;
                case 1:
                    e0 = E10;
                    e1 = E11;
                    e2 = E12;
                    e3 = E13;
                    break;
                case 2:
                    e0 = E20;
                    e1 = E21;
                    e2 = E22;
                    e3 = E23;
                    break;
                case 3:
                    e0 = E30;
                    e1 = E31;
                    e2 = E32;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }

            switch (targetRow)
            {
                case 0:
                    E00 += e0;
                    E01 += e1;
                    E02 += e2;
                    E03 += e3;
                    break;
                case 1:
                    E10 += e0;
                    E11 += e1;
                    E12 += e2;
                    E13 += e3;
                    break;
                case 2:
                    E20 += e0;
                    E21 += e1;
                    E22 += e2;
                    E23 += e3;
                    break;
                case 3:
                    E30 += e0;
                    E31 += e1;
                    E32 += e2;
                    E33 += e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void SubtractRow(int sourceRow, int targetRow)
        {
            double e0, e1, e2, e3;
            switch (sourceRow)
            {
                case 0:
                    e0 = E00;
                    e1 = E01;
                    e2 = E02;
                    e3 = E03;
                    break;
                case 1:
                    e0 = E10;
                    e1 = E11;
                    e2 = E12;
                    e3 = E13;
                    break;
                case 2:
                    e0 = E20;
                    e1 = E21;
                    e2 = E22;
                    e3 = E23;
                    break;
                case 3:
                    e0 = E30;
                    e1 = E31;
                    e2 = E32;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }

            switch (targetRow)
            {
                case 0:
                    E00 -= e0;
                    E01 -= e1;
                    E02 -= e2;
                    E03 -= e3;
                    break;
                case 1:
                    E10 -= e0;
                    E11 -= e1;
                    E12 -= e2;
                    E13 -= e3;
                    break;
                case 2:
                    E20 -= e0;
                    E21 -= e1;
                    E22 -= e2;
                    E23 -= e3;
                    break;
                case 3:
                    E30 -= e0;
                    E31 -= e1;
                    E32 -= e2;
                    E33 -= e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void AddScaledRow(int sourceRow, int targetRow, double scalar)
        {
            double e0, e1, e2, e3;
            switch (sourceRow)
            {
                case 0:
                    e0 = E00;
                    e1 = E01;
                    e2 = E02;
                    e3 = E03;
                    break;
                case 1:
                    e0 = E10;
                    e1 = E11;
                    e2 = E12;
                    e3 = E13;
                    break;
                case 2:
                    e0 = E20;
                    e1 = E21;
                    e2 = E22;
                    e3 = E23;
                    break;
                case 3:
                    e0 = E30;
                    e1 = E31;
                    e2 = E32;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceRow));
            }

            e0 *= scalar;
            e1 *= scalar;
            e2 *= scalar;
            e3 *= scalar;

            switch (targetRow)
            {
                case 0:
                    E00 += e0;
                    E01 += e1;
                    E02 += e2;
                    E03 += e3;
                    break;
                case 1:
                    E10 += e0;
                    E11 += e1;
                    E12 += e2;
                    E13 += e3;
                    break;
                case 2:
                    E20 += e0;
                    E21 += e1;
                    E22 += e2;
                    E23 += e3;
                    break;
                case 3:
                    E30 += e0;
                    E31 += e1;
                    E32 += e2;
                    E33 += e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetRow));
            }
        }

        public void AddColumn(int sourceColumn, int targetColumn)
        {
            double e0, e1, e2, e3;
            switch (sourceColumn)
            {
                case 0:
                    e0 = E00;
                    e1 = E10;
                    e2 = E20;
                    e3 = E30;
                    break;
                case 1:
                    e0 = E01;
                    e1 = E11;
                    e2 = E21;
                    e3 = E31;
                    break;
                case 2:
                    e0 = E02;
                    e1 = E12;
                    e2 = E22;
                    e3 = E32;
                    break;
                case 3:
                    e0 = E03;
                    e1 = E13;
                    e2 = E23;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }

            switch (targetColumn)
            {
                case 0:
                    E00 += e0;
                    E10 += e1;
                    E20 += e2;
                    E30 += e3;
                    break;
                case 1:
                    E01 += e0;
                    E11 += e1;
                    E21 += e2;
                    E31 += e3;
                    break;
                case 2:
                    E02 += e0;
                    E12 += e1;
                    E22 += e2;
                    E32 += e3;
                    break;
                case 3:
                    E03 += e0;
                    E13 += e1;
                    E23 += e2;
                    E33 += e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

        public void SubtractColumn(int sourceColumn, int targetColumn)
        {
            double e0, e1, e2, e3;
            switch (sourceColumn)
            {
                case 0:
                    e0 = E00;
                    e1 = E10;
                    e2 = E20;
                    e3 = E30;
                    break;
                case 1:
                    e0 = E01;
                    e1 = E11;
                    e2 = E21;
                    e3 = E31;
                    break;
                case 2:
                    e0 = E02;
                    e1 = E12;
                    e2 = E22;
                    e3 = E32;
                    break;
                case 3:
                    e0 = E03;
                    e1 = E13;
                    e2 = E23;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }

            switch (targetColumn)
            {
                case 0:
                    E00 -= e0;
                    E10 -= e1;
                    E20 -= e2;
                    E30 -= e3;
                    break;
                case 1:
                    E01 -= e0;
                    E11 -= e1;
                    E21 -= e2;
                    E31 -= e3;
                    break;
                case 2:
                    E02 -= e0;
                    E12 -= e1;
                    E22 -= e2;
                    E32 -= e3;
                    break;
                case 3:
                    E03 -= e0;
                    E13 -= e1;
                    E23 -= e2;
                    E33 -= e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

        public void AddScaledColumn(int sourceColumn, int targetColumn, double scalar)
        {
            double e0, e1, e2, e3;
            switch (sourceColumn)
            {
                case 0:
                    e0 = E00;
                    e1 = E10;
                    e2 = E20;
                    e3 = E30;
                    break;
                case 1:
                    e0 = E01;
                    e1 = E11;
                    e2 = E21;
                    e3 = E31;
                    break;
                case 2:
                    e0 = E02;
                    e1 = E12;
                    e2 = E22;
                    e3 = E32;
                    break;
                case 3:
                    e0 = E03;
                    e1 = E13;
                    e2 = E23;
                    e3 = E33;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(sourceColumn));
            }

            e0 *= scalar;
            e1 *= scalar;
            e2 *= scalar;
            e3 *= scalar;

            switch (targetColumn)
            {
                case 0:
                    E00 += e0;
                    E10 += e1;
                    E20 += e2;
                    E30 += e3;
                    break;
                case 1:
                    E01 += e0;
                    E11 += e1;
                    E21 += e2;
                    E31 += e3;
                    break;
                case 2:
                    E02 += e0;
                    E12 += e1;
                    E22 += e2;
                    E32 += e3;
                    break;
                case 3:
                    E03 += e0;
                    E13 += e1;
                    E23 += e2;
                    E33 += e3;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(targetColumn));
            }
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetSum(Matrix4D other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Matrix4D
            {
                E00 = other.E00 + E00,
                E01 = other.E01 + E01,
                E02 = other.E02 + E02,
                E03 = other.E03 + E03,
                E10 = other.E10 + E10,
                E11 = other.E11 + E11,
                E12 = other.E12 + E12,
                E13 = other.E13 + E13,
                E20 = other.E20 + E20,
                E21 = other.E21 + E21,
                E22 = other.E22 + E22,
                E23 = other.E23 + E23,
                E30 = other.E30 + E30,
                E31 = other.E31 + E31,
                E32 = other.E32 + E32,
                E33 = other.E33 + E33
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetDifference(Matrix4D other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Matrix4D
            {
                E00 = E00 - other.E00,
                E01 = E01 - other.E01,
                E02 = E02 - other.E02,
                E03 = E03 - other.E03,
                E10 = E10 - other.E10,
                E11 = E11 - other.E11,
                E12 = E12 - other.E12,
                E13 = E13 - other.E13,
                E20 = E20 - other.E20,
                E21 = E21 - other.E21,
                E22 = E22 - other.E22,
                E23 = E23 - other.E23,
                E30 = E30 - other.E30,
                E31 = E31 - other.E31,
                E32 = E32 - other.E32,
                E33 = E33 - other.E33
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetScaled(double scalar)
        {
            return new Matrix4D
            {
                E00 = E00 * scalar,
                E01 = E01 * scalar,
                E02 = E02 * scalar,
                E03 = E03 * scalar,
                E10 = E10 * scalar,
                E11 = E11 * scalar,
                E12 = E12 * scalar,
                E13 = E13 * scalar,
                E20 = E20 * scalar,
                E21 = E21 * scalar,
                E22 = E22 * scalar,
                E23 = E23 * scalar,
                E30 = E30 * scalar,
                E31 = E31 * scalar,
                E32 = E32 * scalar,
                E33 = E33 * scalar,
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetQuotient(double divisor)
        {
            return new Matrix4D
            {
                E00 = E00 / divisor,
                E01 = E01 / divisor,
                E02 = E02 / divisor,
                E03 = E03 / divisor,
                E10 = E10 / divisor,
                E11 = E11 / divisor,
                E12 = E12 / divisor,
                E13 = E13 / divisor,
                E20 = E20 / divisor,
                E21 = E21 / divisor,
                E22 = E22 / divisor,
                E23 = E23 / divisor,
                E30 = E30 / divisor,
                E31 = E31 / divisor,
                E32 = E32 / divisor,
                E33 = E33 / divisor,
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetProduct(Matrix4D right)
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return new Matrix4D
            {
                E00 = (E00 * right.E00) + (E01 * right.E10) + (E02 * right.E20) + (E03 * right.E30),
                E01 = (E00 * right.E01) + (E01 * right.E11) + (E02 * right.E21) + (E03 * right.E31),
                E02 = (E00 * right.E02) + (E01 * right.E12) + (E02 * right.E22) + (E03 * right.E32),
                E03 = (E00 * right.E03) + (E01 * right.E13) + (E02 * right.E23) + (E03 * right.E33),

                E10 = (E10 * right.E00) + (E11 * right.E10) + (E12 * right.E20) + (E13 * right.E30),
                E11 = (E10 * right.E01) + (E11 * right.E11) + (E12 * right.E21) + (E13 * right.E31),
                E12 = (E10 * right.E02) + (E11 * right.E12) + (E12 * right.E22) + (E13 * right.E32),
                E13 = (E10 * right.E03) + (E11 * right.E13) + (E12 * right.E23) + (E13 * right.E33),

                E20 = (E20 * right.E00) + (E21 * right.E10) + (E22 * right.E20) + (E23 * right.E30),
                E21 = (E20 * right.E01) + (E21 * right.E11) + (E22 * right.E21) + (E23 * right.E31),
                E22 = (E20 * right.E02) + (E21 * right.E12) + (E22 * right.E22) + (E23 * right.E32),
                E23 = (E20 * right.E03) + (E21 * right.E13) + (E22 * right.E23) + (E23 * right.E33),

                E30 = (E30 * right.E00) + (E31 * right.E10) + (E32 * right.E20) + (E33 * right.E30),
                E31 = (E30 * right.E01) + (E31 * right.E11) + (E32 * right.E21) + (E33 * right.E31),
                E32 = (E30 * right.E02) + (E31 * right.E12) + (E32 * right.E22) + (E33 * right.E32),
                E33 = (E30 * right.E03) + (E31 * right.E13) + (E32 * right.E23) + (E33 * right.E33)
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetTranspose()
        {
            return new Matrix4D
            {
                E00 = E00,
                E01 = E10,
                E02 = E20,
                E03 = E30,

                E10 = E01,
                E11 = E11,
                E12 = E21,
                E13 = E31,

                E20 = E02,
                E21 = E12,
                E22 = E22,
                E23 = E32,

                E30 = E03,
                E31 = E13,
                E32 = E23,
                E33 = E33
            };
        }

        public void Transpose()
        {
            Swap(ref E01, ref E10);
            Swap(ref E02, ref E20);
            Swap(ref E03, ref E30);
            Swap(ref E12, ref E21);
            Swap(ref E13, ref E31);
            Swap(ref E23, ref E32);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public double GetDeterminant()
        {
            var evaluator = new DeterminantEvaluator<Matrix4D>(new Matrix4D(this));
            return evaluator.Evaluate();
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix4D GetInverse()
        {
            var inverter = new GaussJordanInverter<Matrix4D>(
                new Matrix4D(this),
                Matrix4D.CreateIdentity());

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
        [Pure]
#endif
        public bool Equals(Matrix4D other)
        {
            return object.ReferenceEquals(this, other)
                || (
                    !object.ReferenceEquals(null, other)
                    && E00.Equals(other.E00)
                    && E01.Equals(other.E01)
                    && E02.Equals(other.E02)
                    && E03.Equals(other.E03)
                    && E10.Equals(other.E10)
                    && E11.Equals(other.E11)
                    && E12.Equals(other.E12)
                    && E13.Equals(other.E13)
                    && E20.Equals(other.E20)
                    && E21.Equals(other.E21)
                    && E22.Equals(other.E22)
                    && E23.Equals(other.E23)
                    && E30.Equals(other.E30)
                    && E31.Equals(other.E31)
                    && E32.Equals(other.E32)
                    && E33.Equals(other.E33)
                );
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix4D);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Rows;
        }
    }
}
