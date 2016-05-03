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

        public class CreateIdentity : MatrixDTests
        {
            [Fact]
            public void can_constructs_order_4_identity()
            {
                var m = MatrixD.CreateIdentity(4);

                Assert.Equal(4, m.Rows);
                Assert.Equal(4, m.Columns);
                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void can_constructs_order_0_identity()
            {
                var m = MatrixD.CreateIdentity(0);

                Assert.Equal(0, m.Rows);
                Assert.Equal(0, m.Columns);
                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void can_constructs_order_1_identity()
            {
                var m = MatrixD.CreateIdentity(1);

                Assert.Equal(1, m.Rows);
                Assert.Equal(1, m.Columns);
                Assert.Equal(1.0, m.Get(0, 0));
                Assert.True(m.IsIdentity);
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

        public class IsIdentity : MatrixDTests
        {
            [Fact]
            public void default_square_matrix_is_not_identity()
            {
                var m = new MatrixD(2, 2);

                Assert.False(m.IsIdentity);
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

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void single_element_matrix_can_be_identity()
            {
                var m = new MatrixD(1, 1);
                m.Set(0, 0, 1.0);

                Assert.True(m.IsIdentity);
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

                Assert.False(m.IsIdentity);
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

                Assert.False(m.IsIdentity);
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

                Assert.False(m.IsIdentity);
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

            [Fact]
            public void some_different_size_matrices_have_different_hashcodes()
            {
                Assert.NotEqual(new MatrixD(2, 3).GetHashCode(), new MatrixD(0, 0).GetHashCode());
                Assert.NotEqual(new MatrixD(2, 3).GetHashCode(), new MatrixD(3, 2).GetHashCode());
                Assert.NotEqual(new MatrixD(2, 3).GetHashCode(), new MatrixD(4, 8).GetHashCode());
                Assert.NotEqual(new MatrixD(2, 3).GetHashCode(), new MatrixD(10, 2).GetHashCode());
            }
        }
    }
}
