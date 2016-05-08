using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public sealed class Matrix4D :
        IMatrix<double>,
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
                return 4;
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
                return 4;
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
                return E00 == 1.0 && E11 == 1.0 && E22 == 1.0 && E33 == 1.0
                    && E01 == 0.0 && E02 == 0.0 && E03 == 0.0
                    && E10 == 0.0 && E12 == 0.0 && E13 == 0.0
                    && E20 == 0.0 && E21 == 0.0 && E23 == 0.0
                    && E30 == 0.0 && E31 == 0.0 && E32 == 0.0;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Matrix4D CreateIdentity()
        {
            return new Matrix4D
            {
                E00 = 1.0,
                E11 = 1.0,
                E22 = 1.0,
                E33 = 1.0
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
                        case 0: E00  = value; return;
                        case 1: E01  = value; return;
                        case 2: E02  = value; return;
                        case 3: E03  = value; return;
                        case 4: E10  = value; return;
                        case 5: E11  = value; return;
                        case 6: E12  = value; return;
                        case 7: E13  = value; return;
                        case 8: E20  = value; return;
                        case 9: E21  = value; return;
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

#if HAS_CODECONTRACTS
        [System.Diagnostics.Contracts.Pure]
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
        [System.Diagnostics.Contracts.Pure]
#endif
        public sealed override bool Equals(object obj)
        {
            return this.Equals(obj as Matrix4D);
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
