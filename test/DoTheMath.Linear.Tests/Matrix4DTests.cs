﻿using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix4DTests
    {
        public class ConstructorsAndFactories : Matrix4DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix4D();

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E02);
                Assert.Equal(0.0d, m.E03);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
                Assert.Equal(0.0d, m.E12);
                Assert.Equal(0.0d, m.E13);
                Assert.Equal(0.0d, m.E20);
                Assert.Equal(0.0d, m.E21);
                Assert.Equal(0.0d, m.E22);
                Assert.Equal(0.0d, m.E23);
                Assert.Equal(0.0d, m.E30);
                Assert.Equal(0.0d, m.E31);
                Assert.Equal(0.0d, m.E32);
                Assert.Equal(0.0d, m.E33);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0, m.E01);
                Assert.Equal(9.0d, m.E02);
                Assert.Equal(0.1d, m.E03);
                Assert.Equal(-1.0, m.E10);
                Assert.Equal(8.0d, m.E11);
                Assert.Equal(-4.0, m.E12);
                Assert.Equal(-0.9, m.E13);
                Assert.Equal(21.0, m.E20);
                Assert.Equal(-0.5d, m.E21);
                Assert.Equal(1.4d, m.E22);
                Assert.Equal(-9.9d, m.E23);
                Assert.Equal(-101.9, m.E30);
                Assert.Equal(5.0, m.E31);
                Assert.Equal(-17.0, m.E32);
                Assert.Equal(19.3, m.E33);
            }

            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix4D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix4D((Matrix4D)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = new Matrix4D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);

                var actual = new Matrix4D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Properties : Matrix4DTests
        {
            [Fact]
            public void rows_and_cols_are_correct_size()
            {
                var m = new Matrix4D();

                Assert.Equal(4, m.Rows);
                Assert.Equal(4, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix4D();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix4DTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix4D();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix4D();
                m.E00 = 1.0;
                m.E01 = 0.0;
                m.E02 = 0.0;
                m.E03 = 0.0;
                m.E10 = 0.0;
                m.E11 = 1.0;
                m.E12 = 0.0;
                m.E13 = 0.0;
                m.E20 = 0.0;
                m.E21 = 0.0;
                m.E22 = 1.0;
                m.E23 = 0.0;
                m.E30 = 0.0;
                m.E31 = 0.0;
                m.E32 = 0.0;
                m.E33 = 1.0;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix4D();
                m.E00 = 1.0;
                m.E01 = 2.0;
                m.E02 = 3.0;
                m.E03 = 4.0;
                m.E10 = 5.0;
                m.E11 = 6.0;
                m.E12 = 7.0;
                m.E13 = 8.0;
                m.E20 = 9.0;
                m.E21 = 0.0;
                m.E22 = 1.0;
                m.E23 = 2.0;
                m.E30 = 3.0;
                m.E31 = 4.0;
                m.E32 = 5.0;
                m.E33 = 6.0;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix4DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(0.1d, m.Get(0, 3));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(-0.9, m.Get(1, 3));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
                Assert.Equal(-9.9d, m.Get(2, 3));
                Assert.Equal(-101.9, m.Get(3, 0));
                Assert.Equal(5.0, m.Get(3, 1));
                Assert.Equal(-17.0, m.Get(3, 2));
                Assert.Equal(19.3, m.Get(3, 3));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }

        public class Set : Matrix4DTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix4D();

                m.Set(0, 0, 1.0);
                m.Set(0, 1, -5.0);
                m.Set(0, 2, 9.0);
                m.Set(0, 3, 0.1);
                m.Set(1, 0, -1.0);
                m.Set(1, 1, 8.0);
                m.Set(1, 2, -4.0);
                m.Set(1, 3, -0.9);
                m.Set(2, 0, 21.0);
                m.Set(2, 1, -0.5);
                m.Set(2, 2, 1.4);
                m.Set(2, 3, -9.9);
                m.Set(3, 0, -101.9);
                m.Set(3, 1, 5.0);
                m.Set(3, 2, -17.0);
                m.Set(3, 3, 19.3);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(0.1d, m.Get(0, 3));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(-0.9, m.Get(1, 3));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
                Assert.Equal(-9.9d, m.Get(2, 3));
                Assert.Equal(-101.9, m.Get(3, 0));
                Assert.Equal(5.0, m.Get(3, 1));
                Assert.Equal(-17.0, m.Get(3, 2));
                Assert.Equal(19.3, m.Get(3, 3));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(4, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MinValue, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MaxValue, 0, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MaxValue, 0));
            }
        }

        public class IEquatable_Self_Equals : Matrix4DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var b = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var b = new Matrix4D(4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.False(m.Equals((Matrix4D)null));
            }
        }

        public class Object_Equals : Matrix4DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var b = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var b = new Matrix4D(4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.False(m.Equals((object)null));
            }

            [Fact]
            public void matrix_does_not_equal_unknown_type()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                Assert.False(m.Equals((object)"not-a-matrix"));
            }
        }

        public class GetHashCodeTests : Matrix4DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var expectedHashCode = m.GetHashCode();
                m.E00 = 4;
                m.E11 = 9;
                m.E20 = 15.0;
                m.E13 = -9.0;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class SwapRows : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix4D();

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
            [InlineData(0, 3)]
            [InlineData(3, 0)]
            [InlineData(1, 2)]
            [InlineData(2, 1)]
            [InlineData(1, 3)]
            [InlineData(3, 1)]
            [InlineData(2, 3)]
            [InlineData(3, 2)]
            public void can_swap_rows(int rowA, int rowB)
            {
                var expected = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                for (var col = 0; col < expected.Columns; col++)
                {
                    var temp = expected.Get(rowA, col);
                    expected.Set(rowA, col, expected.Get(rowB, col));
                    expected.Set(rowB, col, temp);
                }

                var actual = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                actual.SwapRows(rowA, rowB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var m = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                m.SwapRows(row, row);

                Assert.Equal(
                    new Matrix4D(
                        0, 1, 2, 3,
                        4, 5, 6, 7,
                        8, 9, 10, 11,
                        12, 13, 14, 15),
                    m);
            }
        }

        public class SwapColumns : Matrix4DTests
        {
            [Fact]
            public void invalid_columnss_throws()
            {
                var m = new Matrix4D();

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
            [InlineData(0, 3)]
            [InlineData(3, 0)]
            [InlineData(1, 2)]
            [InlineData(2, 1)]
            [InlineData(1, 3)]
            [InlineData(3, 1)]
            [InlineData(2, 3)]
            [InlineData(3, 2)]
            public void can_swap_columns(int columnA, int columnB)
            {
                var expected = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                for (var row = 0; row < expected.Rows; row++)
                {
                    var temp = expected.Get(row, columnA);
                    expected.Set(row, columnA, expected.Get(row, columnB));
                    expected.Set(row, columnB, temp);
                }

                var actual = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                actual.SwapColumns(columnA, columnB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var m = new Matrix4D(
                    0, 1, 2, 3,
                    4, 5, 6, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                m.SwapColumns(row, row);

                Assert.Equal(
                    new Matrix4D(
                        0, 1, 2, 3,
                        4, 5, 6, 7,
                        8, 9, 10, 11,
                        12, 13, 14, 15),
                    m);
            }
        }

        public class ScaleRow : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleRow(20, 0));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleRow(0, 10);

                Assert.Equal(
                    new Matrix4D(
                        10, 20, 30, 40,
                        5, 6, 7, 8,
                        9, 10, 11, 12,
                        13, 14, 15, 16),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleRow(1, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 2, 3, 4,
                        50, 60, 70, 80,
                        9, 10, 11, 12,
                        13, 14, 15, 16),
                    m);
            }

            [Fact]
            public void can_scale_third_row()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleRow(2, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                        90, 100, 110, 120,
                        13, 14, 15, 16),
                    m);
            }

            [Fact]
            public void can_scale_fourth_row()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleRow(3, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                        9, 10, 11, 12,
                        130, 140, 150, 160),
                    m);
            }
        }

        public class ScaleColumn : Matrix4DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(-100, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.ScaleColumn(20, 0));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleColumn(0, 10);

                Assert.Equal(
                    new Matrix4D(
                        10, 2, 3, 4,
                        50, 6, 7, 8,
                        90, 10, 11, 12,
                        130, 14, 15, 16),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleColumn(1, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 20, 3, 4,
                        5, 60, 7, 8,
                        9, 100, 11, 12,
                        13, 140, 15, 16),
                    m);
            }

            [Fact]
            public void can_scale_third_column()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleColumn(2, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 2, 30, 4,
                        5, 6, 70, 8,
                        9, 10, 110, 12,
                        13, 14, 150, 16),
                    m);
            }

            [Fact]
            public void can_scale_fourth_column()
            {
                var m = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

                m.ScaleColumn(3, 10);

                Assert.Equal(
                    new Matrix4D(
                        1, 2, 3, 40,
                        5, 6, 7, 80,
                        9, 10, 11, 120,
                        13, 14, 15, 160),
                    m);
            }
        }

        public class AddScaledRow : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(4, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(0, 2, 4)]
            [InlineData(0, 3, -3)]
            [InlineData(1, 0, 20)]
            [InlineData(1, 1, -4)]
            [InlineData(1, 2, -40)]
            [InlineData(1, 3, -4.444)]
            [InlineData(2, 0, 2.1234)]
            [InlineData(2, 1, -4)]
            [InlineData(2, 2, -4.09)]
            [InlineData(2, 3, -4.0956)]
            [InlineData(3, 0, 11.1234)]
            [InlineData(3, 1, -14)]
            [InlineData(3, 2, -41.09)]
            [InlineData(3, 3, -123.0956)]
            public void can_add_scaled_row(int sourceRow, int targetRow, double scalar)
            {
                var actual = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var expected = new Matrix4D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c) * scalar;
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : Matrix4DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(4, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(0, 2, 4)]
            [InlineData(0, 3, -3)]
            [InlineData(1, 0, 20)]
            [InlineData(1, 1, -4)]
            [InlineData(1, 2, -40)]
            [InlineData(1, 3, -4.444)]
            [InlineData(2, 0, 2.1234)]
            [InlineData(2, 1, -4)]
            [InlineData(2, 2, -4.09)]
            [InlineData(2, 3, -4.0956)]
            [InlineData(3, 0, 11.1234)]
            [InlineData(3, 1, -14)]
            [InlineData(3, 2, -41.09)]
            [InlineData(3, 3, -123.0956)]
            public void can_add_scaled_column(int sourceColumn, int targetColumn, double scalar)
            {
                var actual = new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
                var expected = new Matrix4D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn) * scalar;
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }
    }
}
