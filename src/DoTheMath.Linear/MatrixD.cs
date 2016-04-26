﻿using System;

namespace DoTheMath.Linear
{
    public class MatrixD
    {
        private double[] elements;

        /// <summary>
        /// Constructs a new zero matrix.
        /// </summary>
        public MatrixD(int rows, int columns)
        {
            if(rows < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }
            if(columns < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Rows = rows;
            Columns = columns;
            elements = new double[checked(rows * columns)];
        }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        /// <summary>
        /// Retrieves the element value at the given row and column.
        /// </summary>
        /// <param name="r">The row.</param>
        /// <param name="c">The column.</param>
        /// <returns>The element value from the given location.</returns>
        public double Get(int row, int column)
        {
            if(row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if(column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return elements[(Columns * row) + column];
        }
    }
}
