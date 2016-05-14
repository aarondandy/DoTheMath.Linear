﻿using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix2DTests
    {
        public class ConstructorsAndFactories : Matrix2DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix2D();

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix2D(1.0, -5.0, 9.0, -1.0);

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0d, m.E01);
                Assert.Equal(9.0d, m.E10);
                Assert.Equal(-1.0d, m.E11);
            }

            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix2D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix2D((Matrix2D)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = new Matrix2D(0, 1, 2, 3);

                var actual = new Matrix2D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Properties : Matrix2DTests
        {
            [Fact]
            public void rows_and_cols_are_two()
            {
                var m = new Matrix2D();

                Assert.Equal(2, m.Rows);
                Assert.Equal(2, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix2D();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix2DTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix2D();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix2D();
                m.E00 = 1.0;
                m.E01 = 0.0;
                m.E10 = 0.0;
                m.E11 = 1.0;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix2D();
                m.E00 = 1.0;
                m.E01 = 2.0;
                m.E10 = 3.0;
                m.E11 = 4.0;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix2DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix2D(1.0, -5.0, 9.0, -1.0);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0d, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(1, 0));
                Assert.Equal(-1.0d, m.Get(1, 1));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }

        public class Set : Matrix2DTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix2D();

                m.Set(0, 0, 1.0);
                m.Set(0, 1, -5.0);
                m.Set(1, 0, 9.0);
                m.Set(1, 1, -1.0);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0d, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(1, 0));
                Assert.Equal(-1.0d, m.Get(1, 1));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(2, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MinValue, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MaxValue, 0, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MaxValue, 0));
            }
        }

        public class IEquatable_Self_Equals : Matrix2DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var b = new Matrix2D(1, 2, 3, 4);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var b = new Matrix2D(4, 3, 2, 1);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                Assert.False(m.Equals((Matrix2D)null));
            }
        }

        public class Object_Equals : Matrix2DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var b = new Matrix2D(1, 2, 3, 4);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var b = new Matrix2D(4, 3, 2, 1);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                Assert.False(m.Equals((object)null));
            }

            [Fact]
            public void matrix_does_not_equal_unknown_type()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                Assert.False(m.Equals((object)"not-a-matrix"));
            }
        }

        public class GetHashCodeTests : Matrix2DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = new Matrix2D(1, 2, 3, 4);
                var expectedHashCode = m.GetHashCode();
                m.E00 = 4;
                m.E11 = 9;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class SwapRows : Matrix2DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, 99));
            }

            [Fact]
            public void can_swap_first_and_second_rows()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                m.SwapRows(0, 1);

                Assert.Equal(new Matrix2D(2, 3, 0, 1), m);
            }

            [Fact]
            public void can_swap_second_and_first_rows()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                m.SwapRows(1, 0);

                Assert.Equal(new Matrix2D(2, 3, 0, 1), m);
            }

            [Fact]
            public void swapping_same_rows_does_nothing()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                for (int r = 0; r < m.Rows; r++)
                {
                    m.SwapRows(r, r);

                    Assert.Equal(new Matrix2D(0, 1, 2, 3), m);
                }
            }
        }

        public class SwapColumns : Matrix2DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, 99));
            }

            [Fact]
            public void can_swap_first_and_second_columns()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                m.SwapColumns(0, 1);

                Assert.Equal(new Matrix2D(1, 0, 3, 2), m);
            }

            [Fact]
            public void can_swap_second_and_first_columns()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                m.SwapColumns(1, 0);

                Assert.Equal(new Matrix2D(1, 0, 3, 2), m);
            }

            [Fact]
            public void swapping_same_columns_does_nothing()
            {
                var m = new Matrix2D(0, 1, 2, 3);

                for (int r = 0; r < m.Columns; r++)
                {
                    m.SwapColumns(r, r);

                    Assert.Equal(new Matrix2D(0, 1, 2, 3), m);
                }
            }
        }

        public class ScaleRow : Matrix2DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(20, 0));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                m.ScaleRow(0, 10);

                Assert.Equal(
                    new Matrix2D(
                        10, 20,
                        3, 4),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                m.ScaleRow(1, 10);

                Assert.Equal(
                    new Matrix2D(
                        1, 2,
                        30, 40),
                    m);
            }
        }

        public class ScaleColumn : Matrix2DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(20, 0));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                m.ScaleColumn(0, 10);

                Assert.Equal(
                    new Matrix2D(
                        10, 2,
                        30, 4),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = new Matrix2D(1, 2, 3, 4);

                m.ScaleColumn(1, 10);

                Assert.Equal(
                    new Matrix2D(
                        1, 20,
                        3, 40),
                    m);
            }
        }

        public class AddScaledRow : Matrix2DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(2, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(1, 0, 10.234)]
            [InlineData(1, 1, -99.9)]
            public void can_add_scaled_row(int sourceRow, int targetRow, double scalar)
            {
                var actual = new Matrix2D(1, 2, 3, 4);
                var expected = new Matrix2D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c) * scalar;
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : Matrix2DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(2, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(1, 0, 10.234)]
            [InlineData(1, 1, -99.9)]
            public void can_add_scaled_column(int sourceColumn, int targetColumn, double scalar)
            {
                var actual = new Matrix2D(1, 2, 3, 4);
                var expected = new Matrix2D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn) * scalar;
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddMatrix : Matrix2DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentNullException>(() => m.Add((Matrix2D)null));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var b = new Matrix2D(4.4, 3.3, 2.2, 1.1);
                var expected = new Matrix2D(5.4, 5.3, 5.2, 5.1);

                var actual = a.Add(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : Matrix2DTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new Matrix2D(1, 2, 3, 4);
                var expected = new Matrix2D(1 * 1.1, 2 * 1.1, 3 * 1.1, 4 * 1.1);

                var actual = a.Multiply(1.1);

                Assert.Equal(expected, actual);
            }
        }
    }
}
