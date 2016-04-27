using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public class Matrix2D : IMatrix<double>
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

        public int Columns
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            get
            {
                return 2;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Get(int row, int column)
        {
            if ((row & 1) != row)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if ((column & 1) != column)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            switch (unchecked((row * 2) + column))
            {
                case 0: return E00;
                case 1: return E01;
                case 2: return E10;
                case 3: return E11;
                default: return default(double); // unreachable
            }
        }
    }
}
