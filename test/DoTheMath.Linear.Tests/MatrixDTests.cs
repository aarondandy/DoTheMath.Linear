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

                for (var r = 0; r < m.Rows; r++)
                {
                    for (var c = 0; c < m.Columns; c++)
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

        public class Set : MatrixDTests
        {
            [Fact]
            public void set_negative_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 100.0));
            }

            [Fact]
            public void set_large_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 100, 100.0));
            }

            [Fact]
            public void set_negative_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-2, 1, 100.0));
            }

            [Fact]
            public void set_large_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(101, 1, 100.0));
            }
        }

        public class GetAndSet : MatrixDTests
        {
            [Fact]
            public void get_all_elements_for_3_3()
            {
                var m = new MatrixD(3, 3);
                m.Set(0, 0, 1.0);
                m.Set(0, 1, 2.0);
                m.Set(0, 2, 3.0);
                m.Set(1, 0, 4.0);
                m.Set(1, 1, 5.0);
                m.Set(1, 2, 6.0);
                m.Set(2, 0, 7.0);
                m.Set(2, 1, 8.0);
                m.Set(2, 2, 9.0);

                Assert.Equal(1.0, m.Get(0, 0));
                Assert.Equal(2.0, m.Get(0, 1));
                Assert.Equal(3.0, m.Get(0, 2));
                Assert.Equal(4.0, m.Get(1, 0));
                Assert.Equal(5.0, m.Get(1, 1));
                Assert.Equal(6.0, m.Get(1, 2));
                Assert.Equal(7.0, m.Get(2, 0));
                Assert.Equal(8.0, m.Get(2, 1));
                Assert.Equal(9.0, m.Get(2, 2));
            }
        }
    }
}
