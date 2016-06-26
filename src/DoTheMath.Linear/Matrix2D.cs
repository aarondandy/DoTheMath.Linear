using System;
using System.Runtime.CompilerServices;

using static DoTheMath.Linear.Utilities.Swapper;
using System.Diagnostics;

#if HAS_CODECONTRACTS
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
#endif

namespace DoTheMath.Linear
{
    public sealed class Matrix2D :
        IMatrixMutable<double>,
        IEquatable<Matrix2D>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const uint IndexMask = 0xfe;

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

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D(IMatrix<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (source.Rows != 2 || source.Columns != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            E00 = source[0, 0];
            E01 = source[0, 1];
            E10 = source[1, 0];
            E11 = source[1, 1];
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
                return 2;
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
                return 2;
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
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E00 == 1.0 && E11 == 1.0
                    && E10 == 0.0 && E01 == 0.0;
            }
        }

        public bool IsSymetric
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
            [Pure]
#endif
            get
            {
                return E01 == E10;
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
                return E00 + E11;
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
                }

                throw new IndexOutOfRangeException();
            }
#if !PRE_NETSTANDARD
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

                throw new IndexOutOfRangeException();
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator +(Matrix2D left, Matrix2D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return new Matrix2D
            {
                E00 = left.E00 + right.E00,
                E01 = left.E01 + right.E01,
                E10 = left.E10 + right.E10,
                E11 = left.E11 + right.E11
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator -(Matrix2D left, Matrix2D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return new Matrix2D
            {
                E00 = left.E00 - right.E00,
                E01 = left.E01 - right.E01,
                E10 = left.E10 - right.E10,
                E11 = left.E11 - right.E11
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator *(Matrix2D left, Matrix2D right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return new Matrix2D
            {
                E00 = (left.E00 * right.E00) + (left.E01 * right.E10),
                E01 = (left.E00 * right.E01) + (left.E01 * right.E11),
                E10 = (left.E10 * right.E00) + (left.E11 * right.E10),
                E11 = (left.E10 * right.E01) + (left.E11 * right.E11)
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator *(Matrix2D matrix, double scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return new Matrix2D
            {
                E00 = matrix.E00 * scalar,
                E01 = matrix.E01 * scalar,
                E10 = matrix.E10 * scalar,
                E11 = matrix.E11 * scalar
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator *(double scalar, Matrix2D matrix)
        {
            return matrix * scalar;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D operator /(Matrix2D matrix, double divisor)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            return new Matrix2D
            {
                E00 = matrix.E00 / divisor,
                E01 = matrix.E01 / divisor,
                E10 = matrix.E10 / divisor,
                E11 = matrix.E11 / divisor
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D CreateIdentity()
        {
            return new Matrix2D
            {
                E00 = 1.0,
                E11 = 1.0
            };
        }

        /// <summary>
        /// Creates a counterclockwise rotation matrix based on the given <paramref name="radians"/>.
        /// </summary>
        /// <param name="radians">The number of radians to create a rotation matrix for.</param>
        /// <returns>A matrix that rotates by the given <paramref name="radians"/>.</returns>
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D CreateRotation(double radians)
        {
            var result = new Matrix2D
            {
                E00 = Math.Cos(radians),
                E01 = Math.Sin(radians)
            };
            result.E10 = -result.E01;
            result.E11 = result.E00;

            return result;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#if HAS_CODECONTRACTS
        [Pure]
#endif
        public static Matrix2D CreateScaled(Vector2D factors)
        {
            return new Matrix2D
            {
                E00 = factors.X,
                E11 = factors.Y
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

            throw new ArgumentOutOfRangeException((row & IndexMask) == 0 ? nameof(column) : nameof(row));
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
            if (unchecked((rowA & IndexMask) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(rowA));
            }
            if (unchecked((rowB & IndexMask) != 0))
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
            if (unchecked((columnA & IndexMask) != 0))
            {
                throw new ArgumentOutOfRangeException(nameof(columnA));
            }
            if (unchecked((columnB & IndexMask) != 0))
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

        public void AddRow(int sourceRow, int targetRow)
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

        public void SubtractRow(int sourceRow, int targetRow)
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

            if (targetRow == 0)
            {
                E00 -= e0;
                E01 -= e1;
            }
            else if (targetRow == 1)
            {
                E10 -= e0;
                E11 -= e1;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(targetRow));
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

        public void AddColumn(int sourceColumn, int targetColumn)
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

        public void SubtractColumn(int sourceColumn, int targetColumn)
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

            if (targetColumn == 0)
            {
                E00 -= e0;
                E10 -= e1;
            }
            else if (targetColumn == 1)
            {
                E01 -= e0;
                E11 -= e1;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(targetColumn));
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
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D GetSum(Matrix2D other)
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
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D GetDifference(Matrix2D other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Matrix2D
            {
                E00 = E00 - other.E00,
                E01 = E01 - other.E01,
                E10 = E10 - other.E10,
                E11 = E11 - other.E11
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D GetScaled(double scalar)
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
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D GetQuotient(double divisor)
        {
            return new Matrix2D
            {
                E00 = E00 / divisor,
                E01 = E01 / divisor,
                E10 = E10 / divisor,
                E11 = E11 / divisor
            };
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Matrix2D GetProduct(Matrix2D right)
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
        [Pure]
#endif
        public Matrix2D GetTranspose()
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
        [Pure]
#endif
        public double GetDeterminant()
        {
            var evaluator = new DeterminantEvaluator<Matrix2D, double>(new Matrix2D(this));
            return evaluator.Evaluate();
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public Matrix2D GetInverse()
        {
#if HAS_CODECONTRACTS
            Ensures(Result<Matrix2D>() != null);
#endif

            var inverter = new GaussJordanInverter<Matrix2D, double>(new Matrix2D(this), CreateIdentity());
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            return inverter.Inverse;
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public bool TryGetInverse(out Matrix2D inverse)
        {
            var inverter = new GaussJordanInverter<Matrix2D, double>(new Matrix2D(this), CreateIdentity());
            var successful = inverter.Invert();
            inverse = inverter.Inverse;
            return successful;
        }

        public void Invert()
        {
            var inverter = new GaussJordanInverter<Matrix2D, double>(new Matrix2D(this), CreateIdentity());
            if (!inverter.Invert())
            {
                throw new NoInverseException();
            }

            CopyFrom(inverter.Inverse);
        }

        public bool TryInvert()
        {
            var inverter = new GaussJordanInverter<Matrix2D, double>(new Matrix2D(this), CreateIdentity());
            var successful = inverter.Invert();
            if (successful)
            {
                CopyFrom(inverter.Inverse);
            }

            return successful;
        }

        /// <summary>
        /// Multiplies or transforms a row vector by this matrix.
        /// </summary>
        /// <param name="rowVector">A row vector to multiply or transform.</param>
        /// <returns>The product or multiplication or the transformed vector.</returns>
        /// <remarks>
        /// This method can be used to transform a vector by this matrix.
        /// </remarks>
#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2D GetProduct(Vector2D rowVector)
        {
            return new Vector2D
            {
                X = (rowVector.X * E00) + (rowVector.Y * E10),
                Y = (rowVector.X * E01) + (rowVector.Y * E11)
            };
        }

        /// <summary>
        /// Multiplies this matrix by a column vector.
        /// </summary>
        /// <param name="columnVector">The column vector to multiply by.</param>
        /// <returns>The product resulting from multiplying this matrix by a column vector.</returns>
#if HAS_CODECONTRACTS
        [Pure]
#endif
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2D GetProductColumnVector(Vector2D columnVector)
        {
            return new Vector2D
            {
                X = (E00 * columnVector.X) + (E01 * columnVector.Y),
                Y = (E10 * columnVector.X) + (E11 * columnVector.Y)
            };
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Transform(ref Vector2D rowVector)
        {
            var x = rowVector.X;
            rowVector.X = (x * E00) + (rowVector.Y * E10);
            rowVector.Y = (x * E01) + (rowVector.Y * E11);
        }

#if HAS_CODECONTRACTS
        [Pure]
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
        [Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix2D);
        }

#if HAS_CODECONTRACTS
        [Pure]
#endif
        public sealed override int GetHashCode()
        {
            return Rows;
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void CopyFrom(Matrix2D source)
        {
#if HAS_CODECONTRACTS
            Requires(source != null);
#endif
            E00 = source.E00;
            E01 = source.E01;
            E10 = source.E10;
            E11 = source.E11;
        }
    }
}
