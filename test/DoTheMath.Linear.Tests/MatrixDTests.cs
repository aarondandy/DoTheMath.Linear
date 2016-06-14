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

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new MatrixD((MatrixD)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = new MatrixD(2, 3);
                expected.Set(0, 0, 0);
                expected.Set(0, 1, 1);
                expected.Set(0, 2, 2);
                expected.Set(1, 0, 3);
                expected.Set(1, 1, 4);
                expected.Set(1, 2, 5);

                var actual = new MatrixD(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class CreateIdentity : MatrixDTests
        {
            [Fact]
            public void can_constructs_order_4_identity()
            {
                var m = MatrixD.CreateIdentity(4);

                Assert.Equal(4, m.Rows);
                Assert.Equal(4, m.Columns);
                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void can_constructs_order_0_identity()
            {
                var m = MatrixD.CreateIdentity(0);

                Assert.Equal(0, m.Rows);
                Assert.Equal(0, m.Columns);
                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void can_constructs_order_1_identity()
            {
                var m = MatrixD.CreateIdentity(1);

                Assert.Equal(1, m.Rows);
                Assert.Equal(1, m.Columns);
                Assert.Equal(1.0, m.Get(0, 0));
                Assert.True(m.CheckIdentity());
            }
        }

        public class IsSquare : MatrixDTests
        {
            [Fact]
            public void zero_size_matrix_is_square()
            {
                var m = new MatrixD(0, 0);

                Assert.True(m.IsSquare);
            }

            [Fact]
            public void various_equal_sizes_are_square()
            {
                for (var order = 1; order < 10; order++)
                {
                    var m = new MatrixD(order, order);

                    Assert.True(m.IsSquare);
                }
            }

            [Fact]
            public void various_nonequal_sizes_are_not_square()
            {
                for (var rows = 1; rows < 10; rows++)
                {
                    var columns = 11 - rows;
                    var m = new MatrixD(rows, columns);

                    Assert.False(m.IsSquare);
                }
            }
        }

        public class CheckIdentity : MatrixDTests
        {
            [Fact]
            public void default_square_matrix_is_not_identity()
            {
                var m = new MatrixD(2, 2);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new MatrixD(4, 4);
                m.Set(0, 0, 1.0);
                m.Set(0, 1, 0.0);
                m.Set(0, 2, 0.0);
                m.Set(0, 3, 0.0);
                m.Set(1, 0, 0.0);
                m.Set(1, 1, 1.0);
                m.Set(1, 2, 0.0);
                m.Set(1, 3, 0.0);
                m.Set(2, 0, 0.0);
                m.Set(2, 1, 0.0);
                m.Set(2, 2, 1.0);
                m.Set(2, 3, 0.0);
                m.Set(3, 0, 0.0);
                m.Set(3, 1, 0.0);
                m.Set(3, 2, 0.0);
                m.Set(3, 3, 1.0);

                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void single_element_matrix_can_be_identity()
            {
                var m = new MatrixD(1, 1);
                m.Set(0, 0, 1.0);

                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void assorted_square_values_are_not_identity()
            {
                var m = new MatrixD(4, 4);
                m.Set(0, 0, 1.0);
                m.Set(0, 1, 2.0);
                m.Set(0, 2, 3.0);
                m.Set(0, 3, 4.0);
                m.Set(1, 0, 5.0);
                m.Set(1, 1, 6.0);
                m.Set(1, 2, 7.0);
                m.Set(1, 3, 8.0);
                m.Set(2, 0, 9.0);
                m.Set(2, 1, 0.0);
                m.Set(2, 2, 1.0);
                m.Set(2, 3, 2.0);
                m.Set(3, 0, 3.0);
                m.Set(3, 1, 4.0);
                m.Set(3, 2, 5.0);
                m.Set(3, 3, 6.0);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void assorted_square_with_one_diagonal_is_not_identity()
            {
                var m = new MatrixD(4, 4);
                m.Set(0, 0, 1.0);
                m.Set(0, 1, 2.0);
                m.Set(0, 2, 3.0);
                m.Set(0, 3, 4.0);
                m.Set(1, 0, 5.0);
                m.Set(1, 1, 1.0);
                m.Set(1, 2, 7.0);
                m.Set(1, 3, 8.0);
                m.Set(2, 0, 9.0);
                m.Set(2, 1, 0.0);
                m.Set(2, 2, 1.0);
                m.Set(2, 3, 2.0);
                m.Set(3, 0, 3.0);
                m.Set(3, 1, 4.0);
                m.Set(3, 2, 5.0);
                m.Set(3, 3, 1.0);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void non_square_matrix_is_not_identity()
            {
                var m = new MatrixD(2, 3);
                m.Set(0, 0, 1.0);
                m.Set(0, 1, 0.0);
                m.Set(0, 2, 0.0);
                m.Set(1, 0, 0.0);
                m.Set(1, 1, 1.0);
                m.Set(1, 2, 0.0);

                Assert.False(m.CheckIdentity());
            }

            [Theory]
            [InlineData(3, 0, 1)]
            [InlineData(3, 0, 2)]
            [InlineData(3, 1, 0)]
            [InlineData(3, 1, 2)]
            [InlineData(3, 2, 0)]
            [InlineData(3, 2, 1)]
            public void each_non_diagonal_element_is_checked(int size, int row, int col)
            {
                var m = MatrixD.CreateIdentity(size);
                m.Set(row, col, 9);

                Assert.False(m.CheckIdentity());
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

        public class IndexerGet : MatrixDTests
        {
            [Fact]
            public void get_negative_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1]);
            }

            [Fact]
            public void get_large_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, 100]);
            }

            [Fact]
            public void get_negative_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[-2, 1]);
            }

            [Fact]
            public void get_large_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[101, 1]);
            }
        }

        public class IndexerSet : MatrixDTests
        {
            [Fact]
            public void set_negative_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1] = 100.0);
            }

            [Fact]
            public void set_large_col_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, 100] = 100.0);
            }

            [Fact]
            public void set_negative_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[-2, 1] = 100.0);
            }

            [Fact]
            public void set_large_row_throws()
            {
                var m = new MatrixD(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[101, 1] = 100.0);
            }
        }

        public class IndexerGetAndSet : MatrixDTests
        {
            [Fact]
            public void get_all_elements_for_3_3()
            {
                var m = new MatrixD(3, 3);
                m[0, 0] = 1.0;
                m[0, 1] = 2.0;
                m[0, 2] = 3.0;
                m[1, 0] = 4.0;
                m[1, 1] = 5.0;
                m[1, 2] = 6.0;
                m[2, 0] = 7.0;
                m[2, 1] = 8.0;
                m[2, 2] = 9.0;

                Assert.Equal(1.0, m[0, 0]);
                Assert.Equal(2.0, m[0, 1]);
                Assert.Equal(3.0, m[0, 2]);
                Assert.Equal(4.0, m[1, 0]);
                Assert.Equal(5.0, m[1, 1]);
                Assert.Equal(6.0, m[1, 2]);
                Assert.Equal(7.0, m[2, 0]);
                Assert.Equal(8.0, m[2, 1]);
                Assert.Equal(9.0, m[2, 2]);
            }
        }

        public class IEquatable_Self_Equals : MatrixDTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new MatrixD(2, 71);

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new MatrixD(4, 5);
                a.Set(0, 0, 1);
                a.Set(1, 3, -9);
                a.Set(3, 4, 8);
                var b = new MatrixD(4, 5);
                b.Set(0, 0, 1);
                b.Set(1, 3, -9);
                b.Set(3, 4, 8);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new MatrixD(4, 5);
                a.Set(0, 0, -1);
                a.Set(1, 3, -9);
                a.Set(2, 4, 8);
                var b = new MatrixD(4, 5);
                b.Set(0, 0, 1);
                b.Set(1, 3, 9);
                b.Set(2, 3, 8);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void different_size_are_not_equal()
            {
                var a = new MatrixD(4, 5);
                a.Set(0, 0, -1);
                a.Set(1, 3, -9);
                a.Set(3, 4, 8);
                var b = new MatrixD(5, 5);
                b.Set(0, 0, -1);
                b.Set(1, 3, -9);
                b.Set(3, 4, 8);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new MatrixD(6, 2);

                Assert.False(m.Equals((MatrixD)null));
            }
        }

        public class Object_Equals : MatrixDTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new MatrixD(2, 71);

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new MatrixD(4, 5);
                a.Set(0, 0, 1);
                a.Set(1, 3, -9);
                a.Set(3, 4, 8);
                var b = new MatrixD(4, 5);
                b.Set(0, 0, 1);
                b.Set(1, 3, -9);
                b.Set(3, 4, 8);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new MatrixD(4, 5);
                a.Set(0, 0, -1);
                a.Set(1, 3, -9);
                a.Set(2, 4, 8);
                var b = new MatrixD(4, 5);
                b.Set(0, 0, 1);
                b.Set(1, 3, 9);
                b.Set(2, 3, 8);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new MatrixD(6, 2);

                Assert.False(m.Equals((object)null));
            }
        }

        public class GetHashCodeTests : MatrixDTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = new MatrixD(2, 3);
                m.Set(1, 1, 9);
                m.Set(1, 2, -8);
                var expectedHashCode = m.GetHashCode();
                m.Set(1, 1, 4);
                m.Set(0, 2, 19);

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class SwapRows : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(-m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(m.Rows * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, -m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, m.Rows * 2));
            }

            [Theory]
            [InlineData(0, 5)]
            [InlineData(1, 5)]
            [InlineData(1, 10)]
            [InlineData(5, 2)]
            [InlineData(5, 5)]
            public void swapping_same_rows_does_nothing(int rows, int columns)
            {
                var actual = new MatrixD(rows, columns);
                var expected = new MatrixD(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        double elementValue = (r * actual.Columns) + c;
                        actual.Set(r, c, elementValue);
                        expected.Set(r, c, elementValue);
                    }
                }

                for (int row = 0; row < actual.Rows; row++)
                {
                    actual.SwapRows(row, row);

                    Assert.Equal(expected, actual);
                }
            }

            [Theory]
            [InlineData(1, 5, 0, 0)]
            [InlineData(2, 10, 0, 1)]
            [InlineData(2, 10, 1, 0)]
            [InlineData(5, 2, 0, 3)]
            [InlineData(5, 5, 3, 4)]
            [InlineData(5, 7, 4, 0)]
            public void can_swap_rows(int rows, int columns, int rowA, int rowB)
            {
                var actual = new MatrixD(rows, columns);
                var expected = new MatrixD(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        double elementValue = (r * actual.Columns) + c;
                        actual.Set(r, c, elementValue);
                        expected.Set(r, c, elementValue);
                    }
                }

                for (var col = 0; col < expected.Columns; col++)
                {
                    var temp = expected.Get(rowA, col);
                    expected.Set(rowA, col, expected.Get(rowB, col));
                    expected.Set(rowB, col, temp);
                }

                actual.SwapRows(rowA, rowB);

                Assert.Equal(expected, actual);
            }
        }

        public class SwapColumns : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(-m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(m.Columns * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, -m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, m.Columns * 2));
            }

            [Theory]
            [InlineData(0, 5)]
            [InlineData(1, 5)]
            [InlineData(1, 10)]
            [InlineData(5, 2)]
            [InlineData(5, 5)]
            public void swapping_same_columns_does_nothing(int columns, int rows)
            {
                var actual = new MatrixD(rows, columns);
                var expected = new MatrixD(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        double elementValue = (r * actual.Columns) + c;
                        actual.Set(r, c, elementValue);
                        expected.Set(r, c, elementValue);
                    }
                }

                for (int column = 0; column < actual.Columns; column++)
                {
                    actual.SwapColumns(column, column);

                    Assert.Equal(expected, actual);
                }
            }

            [Theory]
            [InlineData(1, 5, 0, 0)]
            [InlineData(2, 10, 0, 1)]
            [InlineData(2, 10, 1, 0)]
            [InlineData(5, 2, 0, 3)]
            [InlineData(5, 5, 3, 4)]
            [InlineData(5, 7, 4, 0)]
            public void can_swap_columns(int columns, int rows, int columnA, int columnB)
            {
                var actual = new MatrixD(rows, columns);
                var expected = new MatrixD(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        double elementValue = (r * actual.Columns) + c;
                        actual.Set(r, c, elementValue);
                        expected.Set(r, c, elementValue);
                    }
                }

                for (var row = 0; row < expected.Rows; row++)
                {
                    var temp = expected.Get(row, columnA);
                    expected.Set(row, columnA, expected.Get(row, columnB));
                    expected.Set(row, columnB, temp);
                }

                actual.SwapColumns(columnA, columnB);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleRow : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_row_throws(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(m.Rows * 2, 0));
            }

            [Theory]
            [InlineData(1, 1, 0, 2)]
            [InlineData(2, 3, 1, -9.3)]
            [InlineData(4, 0, 1, -2)]
            [InlineData(5, 5, 0, 3.3)]
            [InlineData(5, 5, 4, 3.3)]
            public void can_scale_rows(int rows, int columns, int row, double value)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);

                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(row, c, expected.Get(row, c) * value);
                }

                actual.ScaleRow(row, value);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleColumn : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_column_throws(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(m.Columns * 2, 0));
            }

            [Theory]
            [InlineData(1, 1, 0, 2)]
            [InlineData(2, 3, 1, -9.3)]
            [InlineData(4, 0, 1, -2)]
            [InlineData(5, 5, 0, 3.3)]
            [InlineData(5, 5, 4, 3.3)]
            public void can_scale_rows(int columns, int rows, int column, double value)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);

                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, column, expected.Get(r, column) * value);
                }

                actual.ScaleColumn(column, value);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideRow : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_row_throws(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-m.Rows, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(m.Rows, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(m.Rows * 2, 1));
            }

            [Theory]
            [InlineData(1, 1, 0, 2)]
            [InlineData(2, 3, 1, -9.3)]
            [InlineData(4, 0, 1, -2)]
            [InlineData(5, 5, 0, 3.3)]
            [InlineData(5, 5, 4, 3.3)]
            public void can_divide_rows(int rows, int columns, int row, double denominator)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);

                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(row, c, expected.Get(row, c) / denominator);
                }

                actual.DivideRow(row, denominator);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideColumn : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_column_throws(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-m.Columns, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(m.Columns, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(m.Columns * 2, 1));
            }

            [Theory]
            [InlineData(1, 1, 0, 2)]
            [InlineData(2, 3, 1, -9.3)]
            [InlineData(4, 0, 1, -2)]
            [InlineData(5, 5, 0, 3.3)]
            [InlineData(5, 5, 4, 3.3)]
            public void can_scale_rows(int columns, int rows, int column, double denominator)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);

                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, column, expected.Get(r, column) / denominator);
                }

                actual.DivideColumn(column, denominator);

                Assert.Equal(expected, actual);
            }
        }

        public class AddRow : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(-m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(m.Rows * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, -m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, m.Rows * 2));
            }

            [Theory]
            [InlineData(1, 1, 0, 0)]
            [InlineData(2, 3, 1, 0)]
            [InlineData(4, 0, 1, 3)]
            [InlineData(5, 5, 0, 2)]
            [InlineData(5, 5, 4, 3)]
            public void can_add_row(int rows, int columns, int sourceRow, int targetRow)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + actual.Get(sourceRow, c));
                }

                actual.AddRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractRow : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(-m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(m.Rows * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, -m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, m.Rows * 2));
            }

            [Theory]
            [InlineData(1, 1, 0, 0)]
            [InlineData(2, 3, 1, 0)]
            [InlineData(4, 0, 1, 3)]
            [InlineData(5, 5, 0, 2)]
            [InlineData(5, 5, 4, 3)]
            public void can_subtract_row(int rows, int columns, int sourceRow, int targetRow)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) - actual.Get(sourceRow, c));
                }

                actual.SubtractRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledRow : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixD(rows, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(-m.Rows, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(m.Rows, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(m.Rows * 2, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, -m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, m.Rows * 2, 0));
            }

            [Theory]
            [InlineData(1, 1, 0, 0, 2)]
            [InlineData(2, 3, 1, 0, -9.3)]
            [InlineData(4, 0, 1, 3, -2)]
            [InlineData(5, 5, 0, 2, 3.3)]
            [InlineData(5, 5, 4, 3, 3.3)]
            public void can_add_scaled_row(int rows, int columns, int sourceRow, int targetRow, double scalar)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + (actual.Get(sourceRow, c) * scalar));
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddColumn : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(-m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(m.Columns * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, -m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, m.Columns * 2));
            }

            [Theory]
            [InlineData(1, 1, 0, 0)]
            [InlineData(2, 3, 1, 0)]
            [InlineData(4, 0, 1, 3)]
            [InlineData(5, 5, 0, 2)]
            [InlineData(5, 5, 4, 3)]
            public void can_add_column(int columns, int rows, int sourceColumn, int targetColumn)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + actual.Get(r, sourceColumn));
                }

                actual.AddColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractColumn : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(-m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(m.Columns * 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, -m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, m.Columns * 2));
            }

            [Theory]
            [InlineData(1, 1, 0, 0)]
            [InlineData(2, 3, 1, 0)]
            [InlineData(4, 0, 1, 3)]
            [InlineData(5, 5, 0, 2)]
            [InlineData(5, 5, 4, 3)]
            public void can_subtract_column(int columns, int rows, int sourceColumn, int targetColumn)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) - actual.Get(r, sourceColumn));
                }

                actual.SubtractColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : MatrixDTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixD(2, columns);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-m.Columns, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(m.Columns, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(m.Columns * 2, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, m.Columns * 2, 0));
            }

            [Theory]
            [InlineData(1, 1, 0, 0, 2)]
            [InlineData(2, 3, 1, 0, -9.3)]
            [InlineData(4, 0, 1, 3, -2)]
            [InlineData(5, 5, 0, 2, 3.3)]
            [InlineData(5, 5, 4, 3, 3.3)]
            public void can_add_scaled_column(int columns, int rows, int sourceColumn, int targetColumn, double scalar)
            {
                var actual = new MatrixD(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixD(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + (actual.Get(r, sourceColumn) * scalar));
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddMatrix : MatrixDTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new MatrixD(4, 2);

                Assert.Throws<ArgumentNullException>(() => m.GetSum((MatrixD)null));
            }

            [Fact]
            public void mixed_size_throws()
            {
                var a = new MatrixD(4, 2);
                var b = new MatrixD(2, 4);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetSum(b));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = new MatrixD(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var b = new MatrixD(2, 3);
                b.Set(0, 0, -1.1);
                b.Set(0, 1, 0.2);
                b.Set(0, 2, -3.2);
                b.Set(1, 0, 0.4);
                b.Set(1, 1, -5.3);
                b.Set(1, 2, 0.6);

                var expected = new MatrixD(2, 3);
                expected.Set(0, 0, 1 - 1.1);
                expected.Set(0, 1, 2 + 0.2);
                expected.Set(0, 2, 3 - 3.2);
                expected.Set(1, 0, 4 + 0.4);
                expected.Set(1, 1, 5 - 5.3);
                expected.Set(1, 2, 6 + 0.6);

                var actual = a.GetSum(b);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractMatrix : MatrixDTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new MatrixD(4, 2);

                Assert.Throws<ArgumentNullException>(() => m.GetDifference((MatrixD)null));
            }

            [Fact]
            public void mixed_size_throws()
            {
                var a = new MatrixD(4, 2);
                var b = new MatrixD(2, 4);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDifference(b));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = new MatrixD(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var b = new MatrixD(2, 3);
                b.Set(0, 0, -1.1);
                b.Set(0, 1, 0.2);
                b.Set(0, 2, -3.2);
                b.Set(1, 0, 0.4);
                b.Set(1, 1, -5.3);
                b.Set(1, 2, 0.6);

                var expected = new MatrixD(2, 3);
                expected.Set(0, 0, 1 + 1.1);
                expected.Set(0, 1, 2 - 0.2);
                expected.Set(0, 2, 3 + 3.2);
                expected.Set(1, 0, 4 - 0.4);
                expected.Set(1, 1, 5 + 5.3);
                expected.Set(1, 2, 6 - 0.6);

                var actual = a.GetDifference(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : MatrixDTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new MatrixD(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var expected = new MatrixD(2, 3);
                expected.Set(0, 0, 1 * 1.1);
                expected.Set(0, 1, 2 * 1.1);
                expected.Set(0, 2, 3 * 1.1);
                expected.Set(1, 0, 4 * 1.1);
                expected.Set(1, 1, 5 * 1.1);
                expected.Set(1, 2, 6 * 1.1);

                var actual = a.GetScaled(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideDenominator : MatrixDTests
        {
            [Fact]
            public void all_elements_get_divided()
            {
                var a = new MatrixD(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var expected = new MatrixD(2, 3);
                expected.Set(0, 0, 1 / 1.1);
                expected.Set(0, 1, 2 / 1.1);
                expected.Set(0, 2, 3 / 1.1);
                expected.Set(1, 0, 4 / 1.1);
                expected.Set(1, 1, 5 / 1.1);
                expected.Set(1, 2, 6 / 1.1);

                var actual = a.GetQuotient(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : MatrixDTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = new MatrixD(4, 7);

                Assert.Throws<ArgumentNullException>(() => sut.GetProduct((MatrixD)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new MatrixD(3, 3);
                a.Set(0, 0, 3);
                a.Set(0, 1, 1);
                a.Set(0, 2, 2);
                a.Set(1, 0, -10);
                a.Set(1, 1, -2);
                a.Set(1, 2, 4);
                a.Set(2, 0, -9);
                a.Set(2, 1, 5);
                a.Set(2, 2, -3);

                var b = new MatrixD(3, 3);
                b.Set(0, 0, 2);
                b.Set(0, 1, 3);
                b.Set(0, 2, 90);
                b.Set(1, 0, -4);
                b.Set(1, 1, 5);
                b.Set(1, 2, 7);
                b.Set(2, 0, 13);
                b.Set(2, 1, -2);
                b.Set(2, 2, -1);

                var expected = new MatrixD(3, 3);
                expected.Set(0, 0, 28);
                expected.Set(0, 1, 10);
                expected.Set(0, 2, 275);
                expected.Set(1, 0, 40);
                expected.Set(1, 1, -48);
                expected.Set(1, 2, -918);
                expected.Set(2, 0, -77);
                expected.Set(2, 1, 4);
                expected.Set(2, 2, -772);

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void mismatched_inner_row_and_column_throws()
            {
                var a = new MatrixD(2, 2);
                var b = new MatrixD(3, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetProduct(b));
            }

            [Fact]
            public void can_multiply_different_size_matricies()
            {
                var a = new MatrixD(1, 2);
                a.Set(0, 0, 2);
                a.Set(0, 1, 3);

                var b = new MatrixD(2, 3);
                b.Set(0, 0, 1);
                b.Set(0, 1, 2);
                b.Set(0, 2, 3);
                b.Set(1, 0, 4);
                b.Set(1, 1, 5);
                b.Set(1, 2, 6);

                var expected = new MatrixD(1, 3);
                expected.Set(0, 0, (2 * 1) + (3 * 4));
                expected.Set(0, 1, (2 * 2) + (3 * 5));
                expected.Set(0, 2, (2 * 3) + (3 * 6));

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new MatrixD(3, 3);
                a.Set(0, 0, 3);
                a.Set(0, 1, 1);
                a.Set(0, 2, 2);
                a.Set(1, 0, -10);
                a.Set(1, 1, -2);
                a.Set(1, 2, 4);
                a.Set(2, 0, -9);
                a.Set(2, 1, 5);
                a.Set(2, 2, -3);

                var identity = MatrixD.CreateIdentity(3);
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.GetProduct(identity));
                Assert.Equal(a, identity.GetProduct(a));
            }
        }

        public class Transposed : MatrixDTests
        {
            [Fact]
            public void can_transpose_diff_rows_and_columns()
            {
                var source = new MatrixD(2, 3);
                source.Set(0, 0, 1);
                source.Set(0, 1, 2);
                source.Set(0, 2, 3);
                source.Set(1, 0, 4);
                source.Set(1, 1, 5);
                source.Set(1, 2, 6);

                var expected = new MatrixD(3, 2);
                expected.Set(0, 0, 1);
                expected.Set(0, 1, 4);
                expected.Set(1, 0, 2);
                expected.Set(1, 1, 5);
                expected.Set(2, 0, 3);
                expected.Set(2, 1, 6);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : MatrixDTests
        {
            [Fact]
            public void can_transpose_diff_rows_and_columns()
            {
                var actual = new MatrixD(2, 3);
                actual.Set(0, 0, 1);
                actual.Set(0, 1, 2);
                actual.Set(0, 2, 3);
                actual.Set(1, 0, 4);
                actual.Set(1, 1, 5);
                actual.Set(1, 2, 6);

                var expected = new MatrixD(3, 2);
                expected.Set(0, 0, 1);
                expected.Set(0, 1, 4);
                expected.Set(1, 0, 2);
                expected.Set(1, 1, 5);
                expected.Set(2, 0, 3);
                expected.Set(2, 1, 6);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void transpose_has_same_hash_code()
            {
                var actual = new MatrixD(2, 3);
                var expectedHashCode = actual.GetHashCode();

                actual.Set(0, 0, 1);
                actual.Set(0, 1, 2);
                actual.Set(0, 2, 3);
                actual.Set(1, 0, 4);
                actual.Set(1, 1, 5);
                actual.Set(1, 2, 6);

                actual.Transpose();

                Assert.Equal(expectedHashCode, actual.GetHashCode());
            }
        }

        public class GetInverse : MatrixDTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = MatrixD.CreateIdentity(7);
                var expected = MatrixD.CreateIdentity(7);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new MatrixD(5, 5);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void rectangle_matrix_has_no_inverse()
            {
                var matrix = new MatrixD(3, 5);

                Assert.Throws<NotSquareMatrixException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new MatrixD(3, 3);
                matrix.Set(0, 0, 1);
                matrix.Set(0, 1, 1);
                matrix.Set(0, 2, 1);
                matrix.Set(1, 0, 1);
                matrix.Set(1, 1, 1);
                matrix.Set(1, 2, 1);
                matrix.Set(2, 0, 1);
                matrix.Set(2, 1, 1);
                matrix.Set(2, 2, 1);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void matrix_is_not_mutated_by_inverse()
            {
                var actual = new MatrixD(2, 2);
                actual.Set(0, 0, 2);
                actual.Set(0, 1, -1);
                actual.Set(1, 0, 0);
                actual.Set(1, 1, 1);
                var expected = new MatrixD(actual);

                var result = actual.GetInverse();

                Assert.NotEqual(result, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void invert_can_round_trip()
            {
                var matrix = new MatrixD(2, 2);
                matrix.Set(0, 0, 2);
                matrix.Set(0, 1, -1);
                matrix.Set(1, 0, 0);
                matrix.Set(1, 1, 1);
                var expected = new MatrixD(matrix);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new MatrixD(5, 5);
                matrix.Set(0, 0, 1);
                matrix.Set(0, 1, 0);
                matrix.Set(0, 2, 6);
                matrix.Set(0, 3, 7);
                matrix.Set(0, 4, 8);

                matrix.Set(1, 0, 9);
                matrix.Set(1, 1, 2);
                matrix.Set(1, 2, 13);
                matrix.Set(1, 3, 14);
                matrix.Set(1, 4, 15);

                matrix.Set(2, 0, 18);
                matrix.Set(2, 1, 12);
                matrix.Set(2, 2, 3);
                matrix.Set(2, 3, 16);
                matrix.Set(2, 4, 17);

                matrix.Set(3, 0, 10);
                matrix.Set(3, 1, 19);
                matrix.Set(3, 2, 20);
                matrix.Set(3, 3, 4);
                matrix.Set(3, 4, 21);

                matrix.Set(4, 0, 11);
                matrix.Set(4, 1, 22);
                matrix.Set(4, 2, 24);
                matrix.Set(4, 3, 23);
                matrix.Set(4, 4, 5);

                var expected = new MatrixD(5, 5);

                expected.Set(0, 0, -0.27854210671081375);
                expected.Set(0, 1, 0.15644380322696155);
                expected.Set(0, 2, 0.0024547327558916317);
                expected.Set(0, 3, -0.004930224351156512);
                expected.Set(0, 4, -0.01130318803875677);

                expected.Set(1, 0, 0.09203258445820205);
                expected.Set(1, 1, -0.12163987911435856);
                expected.Set(1, 2, 0.030844175502147603);
                expected.Set(1, 3, 0.021912684861789416);
                expected.Set(1, 4, 0.02076402908313395);

                expected.Set(2, 0, -0.10734914810912889);
                expected.Set(2, 1, 0.09910269916808939);
                expected.Set(2, 2, -0.0577251425872779);
                expected.Set(2, 3, 0.01445853162349692);
                expected.Set(2, 4, 0.009990191448396114);

                expected.Set(3, 0, 0.12960954353042098);
                expected.Set(3, 1, -0.05121378663718908);
                expected.Set(3, 2, 0.024960774443100832);
                expected.Set(3, 3, -0.03885708750023782);
                expected.Set(3, 4, 0.02459922465734939);

                expected.Set(4, 0, 0.12692127383158003);
                expected.Set(4, 1, -0.0490704364718968);
                expected.Set(4, 2, 0.02114633770825874);
                expected.Set(4, 3, 0.023772330888979974);
                expected.Set(4, 4, -0.02760406665663321);

                var actual = matrix.GetInverse();

                Assert.Equal(expected.Rows, actual.Rows);
                Assert.Equal(expected.Columns, actual.Columns);
                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class GetDeterminant : MatrixDTests
        {
            [Fact]
            public void zero_size_matrix_has_zero_determinant()
            {
                var matrix = MatrixD.CreateIdentity(0);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void identity_matrix_returns_one_for_determinant()
            {
                var matrix = MatrixD.CreateIdentity(7);
                var expected = 1.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_zero_determinant()
            {
                var matrix = new MatrixD(5, 5);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void rectangle_matrix_has_no_determinant()
            {
                var matrix = new MatrixD(3, 5);

                Assert.Throws<NoDeterminantException>(() => matrix.GetDeterminant());
            }

            [Fact]
            public void all_one_matrix_has_zero_determinant()
            {
                var matrix = new MatrixD(3, 3);
                matrix.Set(0, 0, 1);
                matrix.Set(0, 1, 1);
                matrix.Set(0, 2, 1);
                matrix.Set(1, 0, 1);
                matrix.Set(1, 1, 1);
                matrix.Set(1, 2, 1);
                matrix.Set(2, 0, 1);
                matrix.Set(2, 1, 1);
                matrix.Set(2, 2, 1);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void matrix_is_not_mutated_by_determinant_calculations()
            {
                var actual = new MatrixD(2, 2);
                actual.Set(0, 0, 2);
                actual.Set(0, 1, -1);
                actual.Set(1, 0, 0);
                actual.Set(1, 1, 1);
                var expected = new MatrixD(actual);

                actual.GetDeterminant();

                Assert.Equal(expected, actual);
            }


            [Fact]
            public void example_0()
            {
                var matrix = new MatrixD(5, 5);
                matrix.Set(0, 0, 1);
                matrix.Set(0, 1, 0);
                matrix.Set(0, 2, 6);
                matrix.Set(0, 3, 7);
                matrix.Set(0, 4, 8);

                matrix.Set(1, 0, 9);
                matrix.Set(1, 1, 2);
                matrix.Set(1, 2, 13);
                matrix.Set(1, 3, 14);
                matrix.Set(1, 4, 15);

                matrix.Set(2, 0, 18);
                matrix.Set(2, 1, 12);
                matrix.Set(2, 2, 3);
                matrix.Set(2, 3, 16);
                matrix.Set(2, 4, 17);

                matrix.Set(3, 0, 10);
                matrix.Set(3, 1, 19);
                matrix.Set(3, 2, 20);
                matrix.Set(3, 3, 4);
                matrix.Set(3, 4, 21);

                matrix.Set(4, 0, 11);
                matrix.Set(4, 1, 22);
                matrix.Set(4, 2, 24);
                matrix.Set(4, 3, 23);
                matrix.Set(4, 4, 5);

                var expected = 578067;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual, 8);
            }
        }

        public class GetTraceTests : MatrixDTests
        {
            [Fact]
            public void rectangle_matrix_throws()
            {
                var actual = new MatrixD(2, 3);

                Assert.Throws<NotSquareMatrixException>(() => actual.GetTrace());
            }

            [Fact]
            public void can_get_trance()
            {
                var sut = new MatrixD(3, 3);
                sut.Set(0, 0, 2);
                sut.Set(1, 1, 3);
                sut.Set(2, 2, 4);
                sut.Set(0, 1, 99);
                sut.Set(0, 2, 99);
                sut.Set(1, 0, 99);
                sut.Set(1, 2, 99);
                sut.Set(2, 0, 99);
                sut.Set(2, 1, 99);
                var expected = 9;

                var actual = sut.GetTrace();

                Assert.Equal(expected, actual);
            }
        }
    }
}
