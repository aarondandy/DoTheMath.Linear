﻿using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix3DTests
    {
        public class ConstructorsAndFactories : Matrix3DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix3D();

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E02);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
                Assert.Equal(0.0d, m.E12);
                Assert.Equal(0.0d, m.E20);
                Assert.Equal(0.0d, m.E21);
                Assert.Equal(0.0d, m.E22);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0, m.E01);
                Assert.Equal(9.0d, m.E02);
                Assert.Equal(-1.0, m.E10);
                Assert.Equal(8.0d, m.E11);
                Assert.Equal(-4.0, m.E12);
                Assert.Equal(21.0, m.E20);
                Assert.Equal(-0.5d, m.E21);
                Assert.Equal(1.4d, m.E22);
            }

            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix3D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix3D((Matrix3D)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = new Matrix3D(0, 1, 2, 3, 4, 5, 6, 7, 8);

                var actual = new Matrix3D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Properties : Matrix3DTests
        {
            [Fact]
            public void rows_and_cols_are_correct()
            {
                var m = new Matrix3D();

                Assert.Equal(3, m.Rows);
                Assert.Equal(3, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix3D();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix3DTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix3D();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix3D();
                m.E00 = 1.0;
                m.E01 = 0.0;
                m.E02 = 0.0;
                m.E10 = 0.0;
                m.E11 = 1.0;
                m.E12 = 0.0;
                m.E20 = 0.0;
                m.E21 = 0.0;
                m.E22 = 1.0;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix3D();
                m.E00 = 1.0;
                m.E01 = 2.0;
                m.E02 = 3.0;
                m.E10 = 4.0;
                m.E11 = 5.0;
                m.E12 = 6.0;
                m.E20 = 7.0;
                m.E21 = 8.0;
                m.E22 = 9.0;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix3DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }

        public class Set : Matrix3DTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix3D();

                m.Set(0, 0, 1.0);
                m.Set(0, 1, -5.0);
                m.Set(0, 2, 9.0);
                m.Set(1, 0, -1.0);
                m.Set(1, 1, 8.0);
                m.Set(1, 2, -4.0);
                m.Set(2, 0, 21.0);
                m.Set(2, 1, -0.5);
                m.Set(2, 2, 1.4);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(3, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MinValue, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MaxValue, 0, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MaxValue, 0));
            }
        }

        public class IEquatable_Self_Equals : Matrix3DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
                var b = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
                var b = new Matrix3D(4, 3, 2, 1, 0, -1, -2, -3, -4);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.False(m.Equals((Matrix3D)null));
            }
        }

        public class Object_Equals : Matrix3DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
                var b = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
                var b = new Matrix3D(4, 3, 2, 1, -1, -2, -3, -4, -5);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.False(m.Equals((object)null));
            }

            [Fact]
            public void matrix_does_not_equal_unknown_type()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                Assert.False(m.Equals((object)"not-a-matrix"));
            }
        }

        public class GetHashCodeTests : Matrix3DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
                var expectedHashCode = m.GetHashCode();
                m.E00 = 4;
                m.E11 = 9;
                m.E20 = 15.0;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class SwapRows : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(m.Rows, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, m.Rows));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapRows(0, 99));
            }

            [Theory]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(0, 2)]
            [InlineData(2, 0)]
            [InlineData(1, 2)]
            [InlineData(2, 1)]
            public void can_swap_rows(int rowA, int rowB)
            {
                var expected = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                for (var col = 0; col < expected.Columns; col++)
                {
                    var temp = expected.Get(rowA, col);
                    expected.Set(rowA, col, expected.Get(rowB, col));
                    expected.Set(rowB, col, temp);
                }

                var actual = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                actual.SwapRows(rowA, rowB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var m = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                m.SwapRows(row, row);

                Assert.Equal(
                    new Matrix3D(
                        0, 1, 2,
                        3, 4, 5,
                        6, 7, 8),
                    m);
            }
        }

        public class SwapColumns : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(m.Columns, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, m.Columns));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SwapColumns(0, 99));
            }

            [Theory]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(0, 2)]
            [InlineData(2, 0)]
            [InlineData(1, 2)]
            [InlineData(2, 1)]
            public void can_swap_columns(int columnA, int columnB)
            {
                var expected = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                for (var row = 0; row < expected.Rows; row++)
                {
                    var temp = expected.Get(row, columnA);
                    expected.Set(row, columnA, expected.Get(row, columnB));
                    expected.Set(row, columnB, temp);
                }

                var actual = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                actual.SwapColumns(columnA, columnB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            public void swapping_same_columns_does_nothing(int column)
            {
                var m = new Matrix3D(
                    0, 1, 2,
                    3, 4, 5,
                    6, 7, 8);

                m.SwapColumns(column, column);

                Assert.Equal(
                    new Matrix3D(
                        0, 1, 2,
                        3, 4, 5,
                        6, 7, 8),
                    m);
            }
        }

        public class ScaleRow : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(20, 0));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleRow(0, 10);

                Assert.Equal(
                    new Matrix3D(
                        10, 20, 30,
                        4, 5, 6,
                        7, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleRow(1, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 3,
                        40, 50, 60,
                        7, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_third_row()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleRow(2, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 3,
                        4, 5, 6,
                        70, 80, 90),
                    m);
            }
        }

        public class ScaleColumn : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(20, 0));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleColumn(0, 10);

                Assert.Equal(
                    new Matrix3D(
                        10, 2, 3,
                        40, 5, 6,
                        70, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleColumn(1, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 20, 3,
                        4, 50, 6,
                        7, 80, 9),
                    m);
            }

            [Fact]
            public void can_scale_third_column()
            {
                var m = new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);

                m.ScaleColumn(2, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 30,
                        4, 5, 60,
                        7, 8, 90),
                    m);
            }
        }
    }
}
