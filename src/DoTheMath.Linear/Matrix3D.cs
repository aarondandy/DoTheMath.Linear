﻿using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public class Matrix3D : IMatrix<double>
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

        public int Columns
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            get
            {
                return 3;
            }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Get(int row, int column)
        {
            if (row < 0 || row > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if (column < 0 || column > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            switch (unchecked((row * 3) + column))
            {
                case 0: return E00;
                case 1: return E01;
                case 2: return E02;
                case 3: return E10;
                case 4: return E11;
                case 5: return E12;
                case 6: return E20;
                case 7: return E21;
                case 8: return E22;
                default: return default(double); // unreachable
            }
        }
    }
}