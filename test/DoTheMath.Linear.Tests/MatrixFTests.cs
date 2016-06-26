using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class MatrixFTests
    {
        public class Constructors : MatrixFTests
        {
            [Fact]
            public void size_constructor_sets_elements_to_zero()
            {
                var m = new MatrixF(3, 5);

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
                var m = new MatrixF(0, 0);

                Assert.Equal(0, m.Rows);
                Assert.Equal(0, m.Columns);
            }

            [Fact]
            public void size_constructor_neg_rows_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new MatrixF(-1, 2));
            }

            [Fact]
            public void size_constructor_neg_cols_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new MatrixF(3, -2));
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new MatrixF((MatrixF)null));
            }

            [Fact]
            public void copy_constructor_contains_same_elements()
            {
                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 0);
                expected.Set(0, 1, 1);
                expected.Set(0, 2, 2);
                expected.Set(1, 0, 3);
                expected.Set(1, 1, 4);
                expected.Set(1, 2, 5);

                var actual = new MatrixF(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new MatrixF((IMatrix<float>)null));
            }

            [Fact]
            public void copy_constructor_imatrix_contains_same_elements()
            {
                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 0);
                expected.Set(0, 1, 1);
                expected.Set(0, 2, 2);
                expected.Set(1, 0, 3);
                expected.Set(1, 1, 4);
                expected.Set(1, 2, 5);

                var actual = new MatrixF((IMatrix<float>)expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class CreateIdentity : MatrixFTests
        {
            [Fact]
            public void can_constructs_order_4_identity()
            {
                var m = MatrixF.CreateIdentity(4);

                Assert.Equal(4, m.Rows);
                Assert.Equal(4, m.Columns);
                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void can_constructs_order_0_identity()
            {
                var m = MatrixF.CreateIdentity(0);

                Assert.Equal(0, m.Rows);
                Assert.Equal(0, m.Columns);
                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void can_constructs_order_1_identity()
            {
                var m = MatrixF.CreateIdentity(1);

                Assert.Equal(1, m.Rows);
                Assert.Equal(1, m.Columns);
                Assert.Equal(1.0, m.Get(0, 0));
                Assert.True(m.CheckIdentity());
            }
        }

        public class IsSquare : MatrixFTests
        {
            [Fact]
            public void zero_size_matrix_is_square()
            {
                var m = new MatrixF(0, 0);

                Assert.True(m.IsSquare);
            }

            [Fact]
            public void various_equal_sizes_are_square()
            {
                for (var order = 1; order < 10; order++)
                {
                    var m = new MatrixF(order, order);

                    Assert.True(m.IsSquare);
                }
            }

            [Fact]
            public void various_nonequal_sizes_are_not_square()
            {
                for (var rows = 1; rows < 10; rows++)
                {
                    var columns = 11 - rows;
                    var m = new MatrixF(rows, columns);

                    Assert.False(m.IsSquare);
                }
            }
        }

        public class CheckIdentity : MatrixFTests
        {
            [Fact]
            public void default_square_matrix_is_not_identity()
            {
                var m = new MatrixF(2, 2);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new MatrixF(4, 4);
                m.Set(0, 0, 1.0f);
                m.Set(0, 1, 0.0f);
                m.Set(0, 2, 0.0f);
                m.Set(0, 3, 0.0f);
                m.Set(1, 0, 0.0f);
                m.Set(1, 1, 1.0f);
                m.Set(1, 2, 0.0f);
                m.Set(1, 3, 0.0f);
                m.Set(2, 0, 0.0f);
                m.Set(2, 1, 0.0f);
                m.Set(2, 2, 1.0f);
                m.Set(2, 3, 0.0f);
                m.Set(3, 0, 0.0f);
                m.Set(3, 1, 0.0f);
                m.Set(3, 2, 0.0f);
                m.Set(3, 3, 1.0f);

                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void single_element_matrix_can_be_identity()
            {
                var m = new MatrixF(1, 1);
                m.Set(0, 0, 1.0f);

                Assert.True(m.CheckIdentity());
            }

            [Fact]
            public void assorted_square_values_are_not_identity()
            {
                var m = new MatrixF(4, 4);
                m.Set(0, 0, 1.0f);
                m.Set(0, 1, 2.0f);
                m.Set(0, 2, 3.0f);
                m.Set(0, 3, 4.0f);
                m.Set(1, 0, 5.0f);
                m.Set(1, 1, 6.0f);
                m.Set(1, 2, 7.0f);
                m.Set(1, 3, 8.0f);
                m.Set(2, 0, 9.0f);
                m.Set(2, 1, 0.0f);
                m.Set(2, 2, 1.0f);
                m.Set(2, 3, 2.0f);
                m.Set(3, 0, 3.0f);
                m.Set(3, 1, 4.0f);
                m.Set(3, 2, 5.0f);
                m.Set(3, 3, 6.0f);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void assorted_square_with_one_diagonal_is_not_identity()
            {
                var m = new MatrixF(4, 4);
                m.Set(0, 0, 1.0f);
                m.Set(0, 1, 2.0f);
                m.Set(0, 2, 3.0f);
                m.Set(0, 3, 4.0f);
                m.Set(1, 0, 5.0f);
                m.Set(1, 1, 1.0f);
                m.Set(1, 2, 7.0f);
                m.Set(1, 3, 8.0f);
                m.Set(2, 0, 9.0f);
                m.Set(2, 1, 0.0f);
                m.Set(2, 2, 1.0f);
                m.Set(2, 3, 2.0f);
                m.Set(3, 0, 3.0f);
                m.Set(3, 1, 4.0f);
                m.Set(3, 2, 5.0f);
                m.Set(3, 3, 1.0f);

                Assert.False(m.CheckIdentity());
            }

            [Fact]
            public void non_square_matrix_is_not_identity()
            {
                var m = new MatrixF(2, 3);
                m.Set(0, 0, 1.0f);
                m.Set(0, 1, 0.0f);
                m.Set(0, 2, 0.0f);
                m.Set(1, 0, 0.0f);
                m.Set(1, 1, 1.0f);
                m.Set(1, 2, 0.0f);

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
                var m = MatrixF.CreateIdentity(size);
                m.Set(row, col, 9.0f);

                Assert.False(m.CheckIdentity());
            }
        }

        public class Get : MatrixFTests
        {
            [Fact]
            public void get_negative_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
            }

            [Fact]
            public void get_large_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 100));
            }

            [Fact]
            public void get_negative_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-2, 1));
            }

            [Fact]
            public void get_large_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(101, 1));
            }
        }

        public class Set : MatrixFTests
        {
            [Fact]
            public void set_negative_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 100.0f));
            }

            [Fact]
            public void set_large_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 100, 100.0f));
            }

            [Fact]
            public void set_negative_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-2, 1, 100.0f));
            }

            [Fact]
            public void set_large_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(101, 1, 100.0f));
            }
        }

        public class GetAndSet : MatrixFTests
        {
            [Fact]
            public void get_all_elements_for_3_3()
            {
                var m = new MatrixF(3, 3);
                m.Set(0, 0, 1.0f);
                m.Set(0, 1, 2.0f);
                m.Set(0, 2, 3.0f);
                m.Set(1, 0, 4.0f);
                m.Set(1, 1, 5.0f);
                m.Set(1, 2, 6.0f);
                m.Set(2, 0, 7.0f);
                m.Set(2, 1, 8.0f);
                m.Set(2, 2, 9.0f);

                Assert.Equal(1.0f, m.Get(0, 0));
                Assert.Equal(2.0f, m.Get(0, 1));
                Assert.Equal(3.0f, m.Get(0, 2));
                Assert.Equal(4.0f, m.Get(1, 0));
                Assert.Equal(5.0f, m.Get(1, 1));
                Assert.Equal(6.0f, m.Get(1, 2));
                Assert.Equal(7.0f, m.Get(2, 0));
                Assert.Equal(8.0f, m.Get(2, 1));
                Assert.Equal(9.0f, m.Get(2, 2));
            }
        }

        public class IndexerGet : MatrixFTests
        {
            [Fact]
            public void get_negative_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1]);
            }

            [Fact]
            public void get_large_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, 100]);
            }

            [Fact]
            public void get_negative_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[-2, 1]);
            }

            [Fact]
            public void get_large_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[101, 1]);
            }
        }

        public class IndexerSet : MatrixFTests
        {
            [Fact]
            public void set_negative_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1] = 100.0f);
            }

            [Fact]
            public void set_large_col_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[0, 100] = 100.0f);
            }

            [Fact]
            public void set_negative_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[-2, 1] = 100.0f);
            }

            [Fact]
            public void set_large_row_throws()
            {
                var m = new MatrixF(2, 6);

                Assert.Throws<IndexOutOfRangeException>(() => m[101, 1] = 100.0f);
            }
        }

        public class IndexerGetAndSet : MatrixFTests
        {
            [Fact]
            public void get_all_elements_for_3_3()
            {
                var m = new MatrixF(3, 3);
                m[0, 0] = 1.0f;
                m[0, 1] = 2.0f;
                m[0, 2] = 3.0f;
                m[1, 0] = 4.0f;
                m[1, 1] = 5.0f;
                m[1, 2] = 6.0f;
                m[2, 0] = 7.0f;
                m[2, 1] = 8.0f;
                m[2, 2] = 9.0f;

                Assert.Equal(1.0f, m[0, 0]);
                Assert.Equal(2.0f, m[0, 1]);
                Assert.Equal(3.0f, m[0, 2]);
                Assert.Equal(4.0f, m[1, 0]);
                Assert.Equal(5.0f, m[1, 1]);
                Assert.Equal(6.0f, m[1, 2]);
                Assert.Equal(7.0f, m[2, 0]);
                Assert.Equal(8.0f, m[2, 1]);
                Assert.Equal(9.0f, m[2, 2]);
            }
        }

        public class IEquatable_Self_Equals : MatrixFTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new MatrixF(2, 71);

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new MatrixF(4, 5);
                a.Set(0, 0, 1.0f);
                a.Set(1, 3, -9.0f);
                a.Set(3, 4, 8.0f);
                var b = new MatrixF(4, 5);
                b.Set(0, 0, 1.0f);
                b.Set(1, 3, -9.0f);
                b.Set(3, 4, 8.0f);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new MatrixF(4, 5);
                a.Set(0, 0, -1.0f);
                a.Set(1, 3, -9.0f);
                a.Set(2, 4, 8.0f);
                var b = new MatrixF(4, 5);
                b.Set(0, 0, 1.0f);
                b.Set(1, 3, 9.0f);
                b.Set(2, 3, 8.0f);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void different_size_are_not_equal()
            {
                var a = new MatrixF(4, 5);
                a.Set(0, 0, -1.0f);
                a.Set(1, 3, -9.0f);
                a.Set(3, 4, 8.0f);
                var b = new MatrixF(5, 5);
                b.Set(0, 0, -1.0f);
                b.Set(1, 3, -9.0f);
                b.Set(3, 4, 8.0f);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new MatrixF(6, 2);

                Assert.False(m.Equals((MatrixF)null));
            }
        }

        public class Object_Equals : MatrixFTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new MatrixF(2, 71);

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new MatrixF(4, 5);
                a.Set(0, 0, 1.0f);
                a.Set(1, 3, -9.0f);
                a.Set(3, 4, 8.0f);
                var b = new MatrixF(4, 5);
                b.Set(0, 0, 1.0f);
                b.Set(1, 3, -9.0f);
                b.Set(3, 4, 8.0f);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new MatrixF(4, 5);
                a.Set(0, 0, -1.0f);
                a.Set(1, 3, -9.0f);
                a.Set(2, 4, 8.0f);
                var b = new MatrixF(4, 5);
                b.Set(0, 0, 1.0f);
                b.Set(1, 3, 9.0f);
                b.Set(2, 3, 8.0f);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new MatrixF(6, 2);

                Assert.False(m.Equals((object)null));
            }
        }

        public class GetHashCodeTests : MatrixFTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = new MatrixF(2, 3);
                m.Set(1, 1, 9.0f);
                m.Set(1, 2, -8.0f);
                var expectedHashCode = m.GetHashCode();
                m.Set(1, 1, 4.0f);
                m.Set(0, 2, 19.0f);

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class SwapRows : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixF(rows, 2);

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
                var actual = new MatrixF(rows, columns);
                var expected = new MatrixF(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        var elementValue = (r * actual.Columns) + c;
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
                var actual = new MatrixF(rows, columns);
                var expected = new MatrixF(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        var elementValue = (r * actual.Columns) + c;
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

        public class SwapColumns : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixF(2, columns);

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
                var actual = new MatrixF(rows, columns);
                var expected = new MatrixF(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        var elementValue = (r * actual.Columns) + c;
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
                var actual = new MatrixF(rows, columns);
                var expected = new MatrixF(actual.Rows, actual.Columns);
                for (int r = 0; r < actual.Rows; r++)
                {
                    for (int c = 0; c < actual.Columns; c++)
                    {
                        var elementValue = (r * actual.Columns) + c;
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

        public class ScaleRow : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_row_throws(int rows)
            {
                var m = new MatrixF(rows, 2);

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
            public void can_scale_rows(int rows, int columns, int row, float value)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);

                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(row, c, expected.Get(row, c) * value);
                }

                actual.ScaleRow(row, value);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleColumn : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_column_throws(int columns)
            {
                var m = new MatrixF(2, columns);

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
            public void can_scale_rows(int columns, int rows, int column, float value)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);

                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, column, expected.Get(r, column) * value);
                }

                actual.ScaleColumn(column, value);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideRow : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_row_throws(int rows)
            {
                var m = new MatrixF(rows, 2);

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
            public void can_divide_rows(int rows, int columns, int row, float denominator)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);

                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(row, c, expected.Get(row, c) / denominator);
                }

                actual.DivideRow(row, denominator);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideColumn : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_column_throws(int columns)
            {
                var m = new MatrixF(2, columns);

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
            public void can_scale_rows(int columns, int rows, int column, float denominator)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);

                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, column, expected.Get(r, column) / denominator);
                }

                actual.DivideColumn(column, denominator);

                Assert.Equal(expected, actual);
            }
        }

        public class AddRow : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixF(rows, 2);

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
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + actual.Get(sourceRow, c));
                }

                actual.AddRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractRow : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixF(rows, 2);

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
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) - actual.Get(sourceRow, c));
                }

                actual.SubtractRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledRow : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_rows_throw(int rows)
            {
                var m = new MatrixF(rows, 2);

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
            public void can_add_scaled_row(int rows, int columns, int sourceRow, int targetRow, float scalar)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var c = 0; c < expected.Columns; c++)
                {
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + (actual.Get(sourceRow, c) * scalar));
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddColumn : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixF(2, columns);

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
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + actual.Get(r, sourceColumn));
                }

                actual.AddColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractColumn : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixF(2, columns);

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
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) - actual.Get(r, sourceColumn));
                }

                actual.SubtractColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : MatrixFTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(10)]
            public void invaid_columns_throw(int columns)
            {
                var m = new MatrixF(2, columns);

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
            public void can_add_scaled_column(int columns, int rows, int sourceColumn, int targetColumn, float scalar)
            {
                var actual = new MatrixF(rows, columns);
                for (var r = 0; r < actual.Rows; r++)
                {
                    for (var c = 0; c < actual.Columns; c++)
                    {
                        actual.Set(r, c, (r * actual.Columns) + c);
                    }
                }

                var expected = new MatrixF(actual);
                for (var r = 0; r < expected.Rows; r++)
                {
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + (actual.Get(r, sourceColumn) * scalar));
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddMatrix : MatrixFTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new MatrixF(4, 2);

                Assert.Throws<ArgumentNullException>(() => m.GetSum((MatrixF)null));
            }

            [Fact]
            public void mixed_size_throws()
            {
                var a = new MatrixF(4, 2);
                var b = new MatrixF(2, 4);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetSum(b));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = new MatrixF(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var b = new MatrixF(2, 3);
                b.Set(0, 0, -1.1f);
                b.Set(0, 1, 0.2f);
                b.Set(0, 2, -3.2f);
                b.Set(1, 0, 0.4f);
                b.Set(1, 1, -5.3f);
                b.Set(1, 2, 0.6f);

                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 1.0f - 1.1f);
                expected.Set(0, 1, 2.0f + 0.2f);
                expected.Set(0, 2, 3.0f - 3.2f);
                expected.Set(1, 0, 4.0f + 0.4f);
                expected.Set(1, 1, 5.0f - 5.3f);
                expected.Set(1, 2, 6.0f + 0.6f);

                var actual = a.GetSum(b);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractMatrix : MatrixFTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new MatrixF(4, 2);

                Assert.Throws<ArgumentNullException>(() => m.GetDifference((MatrixF)null));
            }

            [Fact]
            public void mixed_size_throws()
            {
                var a = new MatrixF(4, 2);
                var b = new MatrixF(2, 4);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDifference(b));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = new MatrixF(2, 3);
                a.Set(0, 0, 1.0f);
                a.Set(0, 1, 2.0f);
                a.Set(0, 2, 3.0f);
                a.Set(1, 0, 4.0f);
                a.Set(1, 1, 5.0f);
                a.Set(1, 2, 6.0f);

                var b = new MatrixF(2, 3);
                b.Set(0, 0, -1.1f);
                b.Set(0, 1, 0.2f);
                b.Set(0, 2, -3.2f);
                b.Set(1, 0, 0.4f);
                b.Set(1, 1, -5.3f);
                b.Set(1, 2, 0.6f);

                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 1.0f + 1.1f);
                expected.Set(0, 1, 2.0f - 0.2f);
                expected.Set(0, 2, 3.0f + 3.2f);
                expected.Set(1, 0, 4.0f - 0.4f);
                expected.Set(1, 1, 5.0f + 5.3f);
                expected.Set(1, 2, 6.0f - 0.6f);

                var actual = a.GetDifference(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : MatrixFTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new MatrixF(2, 3);
                a.Set(0, 0, 1.0f);
                a.Set(0, 1, 2.0f);
                a.Set(0, 2, 3.0f);
                a.Set(1, 0, 4.0f);
                a.Set(1, 1, 5.0f);
                a.Set(1, 2, 6.0f);

                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 1.0f * 1.1f);
                expected.Set(0, 1, 2.0f * 1.1f);
                expected.Set(0, 2, 3.0f * 1.1f);
                expected.Set(1, 0, 4.0f * 1.1f);
                expected.Set(1, 1, 5.0f * 1.1f);
                expected.Set(1, 2, 6.0f * 1.1f);

                var actual = a.GetScaled(1.1f);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideDenominator : MatrixFTests
        {
            [Fact]
            public void all_elements_get_divided()
            {
                var a = new MatrixF(2, 3);
                a.Set(0, 0, 1);
                a.Set(0, 1, 2);
                a.Set(0, 2, 3);
                a.Set(1, 0, 4);
                a.Set(1, 1, 5);
                a.Set(1, 2, 6);

                var expected = new MatrixF(2, 3);
                expected.Set(0, 0, 1.0f / 1.1f);
                expected.Set(0, 1, 2.0f / 1.1f);
                expected.Set(0, 2, 3.0f / 1.1f);
                expected.Set(1, 0, 4.0f / 1.1f);
                expected.Set(1, 1, 5.0f / 1.1f);
                expected.Set(1, 2, 6.0f / 1.1f);

                var actual = a.GetQuotient(1.1f);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : MatrixFTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = new MatrixF(4, 7);

                Assert.Throws<ArgumentNullException>(() => sut.GetProduct((MatrixF)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new MatrixF(3, 3);
                a.Set(0, 0, 3.0f);
                a.Set(0, 1, 1.0f);
                a.Set(0, 2, 2.0f);
                a.Set(1, 0, -10.0f);
                a.Set(1, 1, -2.0f);
                a.Set(1, 2, 4.0f);
                a.Set(2, 0, -9.0f);
                a.Set(2, 1, 5.0f);
                a.Set(2, 2, -3.0f);

                var b = new MatrixF(3, 3);
                b.Set(0, 0, 2.0f);
                b.Set(0, 1, 3.0f);
                b.Set(0, 2, 90.0f);
                b.Set(1, 0, -4.0f);
                b.Set(1, 1, 5.0f);
                b.Set(1, 2, 7.0f);
                b.Set(2, 0, 13.0f);
                b.Set(2, 1, -2.0f);
                b.Set(2, 2, -1.0f);

                var expected = new MatrixF(3, 3);
                expected.Set(0, 0, 28.0f);
                expected.Set(0, 1, 10.0f);
                expected.Set(0, 2, 275.0f);
                expected.Set(1, 0, 40.0f);
                expected.Set(1, 1, -48.0f);
                expected.Set(1, 2, -918.0f);
                expected.Set(2, 0, -77.0f);
                expected.Set(2, 1, 4.0f);
                expected.Set(2, 2, -772.0f);

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void mismatched_inner_row_and_column_throws()
            {
                var a = new MatrixF(2, 2);
                var b = new MatrixF(3, 2);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetProduct(b));
            }

            [Fact]
            public void can_multiply_different_size_matricies()
            {
                var a = new MatrixF(1, 2);
                a.Set(0, 0, 2.0f);
                a.Set(0, 1, 3.0f);

                var b = new MatrixF(2, 3);
                b.Set(0, 0, 1.0f);
                b.Set(0, 1, 2.0f);
                b.Set(0, 2, 3.0f);
                b.Set(1, 0, 4.0f);
                b.Set(1, 1, 5.0f);
                b.Set(1, 2, 6.0f);

                var expected = new MatrixF(1, 3);
                expected.Set(0, 0, (2.0f * 1.0f) + (3.0f * 4.0f));
                expected.Set(0, 1, (2.0f * 2.0f) + (3.0f * 5.0f));
                expected.Set(0, 2, (2.0f * 3.0f) + (3.0f * 6.0f));

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new MatrixF(3, 3);
                a.Set(0, 0, 3.0f);
                a.Set(0, 1, 1.0f);
                a.Set(0, 2, 2.0f);
                a.Set(1, 0, -10.0f);
                a.Set(1, 1, -2.0f);
                a.Set(1, 2, 4.0f);
                a.Set(2, 0, -9.0f);
                a.Set(2, 1, 5.0f);
                a.Set(2, 2, -3.0f);

                var identity = MatrixF.CreateIdentity(3);
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.GetProduct(identity));
                Assert.Equal(a, identity.GetProduct(a));
            }
        }

        public class Transposed : MatrixFTests
        {
            [Fact]
            public void can_transpose_diff_rows_and_columns()
            {
                var source = new MatrixF(2, 3);
                source.Set(0, 0, 1.0f);
                source.Set(0, 1, 2.0f);
                source.Set(0, 2, 3.0f);
                source.Set(1, 0, 4.0f);
                source.Set(1, 1, 5.0f);
                source.Set(1, 2, 6.0f);

                var expected = new MatrixF(3, 2);
                expected.Set(0, 0, 1.0f);
                expected.Set(0, 1, 4.0f);
                expected.Set(1, 0, 2.0f);
                expected.Set(1, 1, 5.0f);
                expected.Set(2, 0, 3.0f);
                expected.Set(2, 1, 6.0f);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : MatrixFTests
        {
            [Fact]
            public void can_transpose_diff_rows_and_columns()
            {
                var actual = new MatrixF(2, 3);
                actual.Set(0, 0, 1.0f);
                actual.Set(0, 1, 2.0f);
                actual.Set(0, 2, 3.0f);
                actual.Set(1, 0, 4.0f);
                actual.Set(1, 1, 5.0f);
                actual.Set(1, 2, 6.0f);

                var expected = new MatrixF(3, 2);
                expected.Set(0, 0, 1.0f);
                expected.Set(0, 1, 4.0f);
                expected.Set(1, 0, 2.0f);
                expected.Set(1, 1, 5.0f);
                expected.Set(2, 0, 3.0f);
                expected.Set(2, 1, 6.0f);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void transpose_has_same_hash_code()
            {
                var actual = new MatrixF(2, 3);
                var expectedHashCode = actual.GetHashCode();

                actual.Set(0, 0, 1.0f);
                actual.Set(0, 1, 2.0f);
                actual.Set(0, 2, 3.0f);
                actual.Set(1, 0, 4.0f);
                actual.Set(1, 1, 5.0f);
                actual.Set(1, 2, 6.0f);

                actual.Transpose();

                Assert.Equal(expectedHashCode, actual.GetHashCode());
            }
        }

        public class GetInverse : MatrixFTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = MatrixF.CreateIdentity(7);
                var expected = MatrixF.CreateIdentity(7);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new MatrixF(5, 5);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void rectangle_matrix_has_no_inverse()
            {
                var matrix = new MatrixF(3, 5);

                Assert.Throws<NotSquareMatrixException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new MatrixF(3, 3);
                matrix.Set(0, 0, 1.0f);
                matrix.Set(0, 1, 1.0f);
                matrix.Set(0, 2, 1.0f);
                matrix.Set(1, 0, 1.0f);
                matrix.Set(1, 1, 1.0f);
                matrix.Set(1, 2, 1.0f);
                matrix.Set(2, 0, 1.0f);
                matrix.Set(2, 1, 1.0f);
                matrix.Set(2, 2, 1.0f);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void matrix_is_not_mutated_by_inverse()
            {
                var actual = new MatrixF(2, 2);
                actual.Set(0, 0, 2.0f);
                actual.Set(0, 1, -1.0f);
                actual.Set(1, 0, 0.0f);
                actual.Set(1, 1, 1.0f);
                var expected = new MatrixF(actual);

                var result = actual.GetInverse();

                Assert.NotEqual(result, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void invert_can_round_trip()
            {
                var matrix = new MatrixF(2, 2);
                matrix.Set(0, 0, 2.0f);
                matrix.Set(0, 1, -1.0f);
                matrix.Set(1, 0, 0.0f);
                matrix.Set(1, 1, 1.0f);
                var expected = new MatrixF(matrix);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new MatrixF(5, 5);
                matrix.Set(0, 0, 1.0f);
                matrix.Set(0, 1, 0.0f);
                matrix.Set(0, 2, 6.0f);
                matrix.Set(0, 3, 7.0f);
                matrix.Set(0, 4, 8.0f);

                matrix.Set(1, 0, 9.0f);
                matrix.Set(1, 1, 2.0f);
                matrix.Set(1, 2, 13.0f);
                matrix.Set(1, 3, 14.0f);
                matrix.Set(1, 4, 15.0f);

                matrix.Set(2, 0, 18.0f);
                matrix.Set(2, 1, 12.0f);
                matrix.Set(2, 2, 3.0f);
                matrix.Set(2, 3, 16.0f);
                matrix.Set(2, 4, 17.0f);

                matrix.Set(3, 0, 10.0f);
                matrix.Set(3, 1, 19.0f);
                matrix.Set(3, 2, 20.0f);
                matrix.Set(3, 3, 4.0f);
                matrix.Set(3, 4, 21.0f);

                matrix.Set(4, 0, 11.0f);
                matrix.Set(4, 1, 22.0f);
                matrix.Set(4, 2, 24.0f);
                matrix.Set(4, 3, 23.0f);
                matrix.Set(4, 4, 5.0f);

                var expected = new MatrixF(5, 5);

                expected.Set(0, 0, -0.27854210671081375f);
                expected.Set(0, 1, 0.15644380322696155f);
                expected.Set(0, 2, 0.0024547327558916317f);
                expected.Set(0, 3, -0.004930224351156512f);
                expected.Set(0, 4, -0.01130318803875677f);

                expected.Set(1, 0, 0.09203258445820205f);
                expected.Set(1, 1, -0.12163987911435856f);
                expected.Set(1, 2, 0.030844175502147603f);
                expected.Set(1, 3, 0.021912684861789416f);
                expected.Set(1, 4, 0.02076402908313395f);

                expected.Set(2, 0, -0.10734914810912889f);
                expected.Set(2, 1, 0.09910269916808939f);
                expected.Set(2, 2, -0.0577251425872779f);
                expected.Set(2, 3, 0.01445853162349692f);
                expected.Set(2, 4, 0.009990191448396114f);

                expected.Set(3, 0, 0.12960954353042098f);
                expected.Set(3, 1, -0.05121378663718908f);
                expected.Set(3, 2, 0.024960774443100832f);
                expected.Set(3, 3, -0.03885708750023782f);
                expected.Set(3, 4, 0.02459922465734939f);

                expected.Set(4, 0, 0.12692127383158003f);
                expected.Set(4, 1, -0.0490704364718968f);
                expected.Set(4, 2, 0.02114633770825874f);
                expected.Set(4, 3, 0.023772330888979974f);
                expected.Set(4, 4, -0.02760406665663321f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected.Rows, actual.Rows);
                Assert.Equal(expected.Columns, actual.Columns);
                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 7);
                    }
                }
            }
        }

        public class GetDeterminant : MatrixFTests
        {
            [Fact]
            public void zero_size_matrix_has_zero_determinant()
            {
                var matrix = MatrixF.CreateIdentity(0);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void identity_matrix_returns_one_for_determinant()
            {
                var matrix = MatrixF.CreateIdentity(7);
                var expected = 1.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_zero_determinant()
            {
                var matrix = new MatrixF(5, 5);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void rectangle_matrix_has_no_determinant()
            {
                var matrix = new MatrixF(3, 5);

                Assert.Throws<NoDeterminantException>(() => matrix.GetDeterminant());
            }

            [Fact]
            public void all_one_matrix_has_zero_determinant()
            {
                var matrix = new MatrixF(3, 3);
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
                var actual = new MatrixF(2, 2);
                actual.Set(0, 0, 2);
                actual.Set(0, 1, -1);
                actual.Set(1, 0, 0);
                actual.Set(1, 1, 1);
                var expected = new MatrixF(actual);

                actual.GetDeterminant();

                Assert.Equal(expected, actual);
            }


            [Fact]
            public void example_0()
            {
                var matrix = new MatrixF(5, 5);
                matrix.Set(0, 0, 1.0f);
                matrix.Set(0, 1, 0.0f);
                matrix.Set(0, 2, 6.0f);
                matrix.Set(0, 3, 7.0f);
                matrix.Set(0, 4, 8.0f);

                matrix.Set(1, 0, 9.0f);
                matrix.Set(1, 1, 2.0f);
                matrix.Set(1, 2, 13.0f);
                matrix.Set(1, 3, 14.0f);
                matrix.Set(1, 4, 15.0f);

                matrix.Set(2, 0, 18.0f);
                matrix.Set(2, 1, 12.0f);
                matrix.Set(2, 2, 3.0f);
                matrix.Set(2, 3, 16.0f);
                matrix.Set(2, 4, 17.0f);

                matrix.Set(3, 0, 10.0f);
                matrix.Set(3, 1, 19.0f);
                matrix.Set(3, 2, 20.0f);
                matrix.Set(3, 3, 4.0f);
                matrix.Set(3, 4, 21.0f);

                matrix.Set(4, 0, 11.0f);
                matrix.Set(4, 1, 22.0f);
                matrix.Set(4, 2, 24.0f);
                matrix.Set(4, 3, 23.0f);
                matrix.Set(4, 4, 5.0f);

                var expected = 578067.0f;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual, 0); // TODO: This needs to be way more accurate
            }
        }

        public class GetTraceTests : MatrixFTests
        {
            [Fact]
            public void rectangle_matrix_throws()
            {
                var actual = new MatrixF(2, 3);

                Assert.Throws<NotSquareMatrixException>(() => actual.GetTrace());
            }

            [Fact]
            public void can_get_trance()
            {
                var sut = new MatrixF(3, 3);
                sut.Set(0, 0, 2.0f);
                sut.Set(1, 1, 3.0f);
                sut.Set(2, 2, 4.0f);
                sut.Set(0, 1, 99.0f);
                sut.Set(0, 2, 99.0f);
                sut.Set(1, 0, 99.0f);
                sut.Set(1, 2, 99.0f);
                sut.Set(2, 0, 99.0f);
                sut.Set(2, 1, 99.0f);
                var expected = 9.0f;

                var actual = sut.GetTrace();

                Assert.Equal(expected, actual);
            }
        }
    }
}
