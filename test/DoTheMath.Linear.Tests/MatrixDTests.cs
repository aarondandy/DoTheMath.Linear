using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class MatrixDTests
    {
        public class Constructors : MatrixDTests
        {
            [Fact]
            public void size_constructor_sets_elements_to_zero()
            {
                var m = new MatrixD(3, 5);

                Assert.Equal(3, m.Rows);
                Assert.Equal(5, m.Columns);
                for(var r = 0; r < m.Rows; r++)
                {
                    for(var c = 0; c < m.Columns; c++)
                    {
                        Assert.Equal(0.0, m.Get(r, c));
                    }
                }
            }

            [Fact]
            public void size_constructor_can_make_empty()
            {
                var m = new MatrixD(0, 0);

                Assert.Equal(0, m.Rows);
                Assert.Equal(0, m.Columns);
            }

            [Fact]
            public void size_constructor_neg_rows_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new MatrixD(-1, 2));
            }

            [Fact]
            public void size_constructor_neg_cols_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new MatrixD(3, -2));
            }
        }

        public class Get : MatrixDTests
        {
            [Fact]
            public void get_negative_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
            }

            [Fact]
            public void get_large_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 100));
            }

            [Fact]
            public void get_negative_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-2, 1));
            }

            [Fact]
            public void get_large_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(101, 1));
            }
        }
    }
}
