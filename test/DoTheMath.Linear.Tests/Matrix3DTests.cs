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
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix3D((Matrix3D)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = CreateIncremenetalMatrix();

                var actual = new Matrix3D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix3D((IMatrix<double>)null));
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_bad_size()
            {
                var source = new MatrixD(3, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix3D((IMatrix<double>)source));
            }

            [Fact]
            public void copy_constructor_imatrix_contains_same_elements()
            {
                var source = new MatrixD(3, 3);
                source.Set(0, 0, 16);
                source.Set(0, 1, 1);
                source.Set(0, 2, 2);
                source.Set(1, 0, 3);
                source.Set(1, 1, 4);
                source.Set(1, 2, 5);
                source.Set(2, 0, 8);
                source.Set(2, 1, 9);
                source.Set(2, 2, 10);

                var expected = new Matrix3D(
                    16, 1, 2,
                    3, 4, 5,
                    8, 9, 10);

                var actual = new Matrix3D((IMatrix<double>)source);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Factories : Matrix3DTests
        {
            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix3D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void x_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix3D
                {
                    E00 = 1.0,
                    E11 = Math.Cos(radians),
                    E12 = Math.Sin(radians),
                    E21 = -Math.Sin(radians),
                    E22 = Math.Cos(radians)
                };

                var actual = Matrix3D.CreateRotationX(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void y_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix3D
                {
                    E00 = Math.Cos(radians),
                    E02 = -Math.Sin(radians),
                    E11 = 1.0,
                    E20 = Math.Sin(radians),
                    E22 = Math.Cos(radians)
                };

                var actual = Matrix3D.CreateRotationY(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void z_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix3D
                {
                    E00 = Math.Cos(radians),
                    E01 = Math.Sin(radians),
                    E10 = -Math.Sin(radians),
                    E11 = Math.Cos(radians),
                    E22 = 1.0
                };

                var actual = Matrix3D.CreateRotationZ(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_scaling_matrix2()
            {
                var factors = new Vector2D(2, -3);
                var expected = Matrix3D.CreateIdentity();
                expected.E00 = factors.X;
                expected.E11 = factors.Y;

                var actual = Matrix3D.CreateScaled(factors);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_scaling_matrix3()
            {
                var factors = new Vector3D(2, -3, 4);
                var expected = Matrix3D.CreateIdentity();
                expected.E00 = factors.X;
                expected.E11 = factors.Y;
                expected.E22 = factors.Z;

                var actual = Matrix3D.CreateScaled(factors);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_translation()
            {
                var delta = new Vector2D(2, -3);
                var expected = Matrix3D.CreateIdentity();
                expected.E20 = delta.X;
                expected.E21 = delta.Y;

                var actual = Matrix3D.CreateTranslation(delta);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_rotation_around_non_zero_origin()
            {
                var origin = new Vector2D(5, 4);
                var theta = Math.PI / 2.3;
                var expected = Matrix3D.CreateTranslation(origin.GetNegative())
                    * Matrix3D.CreateRotationZ(theta)
                    * Matrix3D.CreateTranslation(origin);

                var actual = Matrix3D.CreateRotation(origin, theta);

                for (var r = 0; r < expected.Rows; r++)
                {
                    for (var c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class OperatorOverloads : Matrix3DTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix3D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_addition_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix3D)null;

                Assert.Throws<ArgumentNullException>(() => matrix + @null);
                Assert.Throws<ArgumentNullException>(() => @null + matrix);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix3D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtract_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix3D)null;

                Assert.Throws<ArgumentNullException>(() => matrix - @null);
                Assert.Throws<ArgumentNullException>(() => @null - matrix);
            }

            [Fact]
            public void op_multiply_mimics_multiply_matrix()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix3D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5);
                var expected = left.GetProduct(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix3D)null;

                Assert.Throws<ArgumentNullException>(() => matrix * @null);
                Assert.Throws<ArgumentNullException>(() => @null * matrix);
            }

            [Fact]
            public void op_multiply_mimics_multiply_scalar()
            {
                var matrix = CreateIncremenetalMatrix();
                var scalar = -9.5;
                var expected = matrix.GetScaled(scalar);

                var actual = matrix * scalar;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_mimics_multiply_prefix_scalar()
            {
                var matrix = CreateIncremenetalMatrix();
                var scalar = -13.5;
                var expected = matrix.GetScaled(scalar);

                var actual = scalar * matrix;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalars_null_matrix_throws()
            {
                var @null = (Matrix3D)null;
                var scalar = 1.0;

                Assert.Throws<ArgumentNullException>(() => scalar * @null);
                Assert.Throws<ArgumentNullException>(() => @null * scalar);
            }

            [Fact]
            public void op_multiply_mimics_divide_denominator()
            {
                var matrix = CreateIncremenetalMatrix();
                var divisor = -1.3;
                var expected = matrix.GetQuotient(divisor);

                var actual = matrix / divisor;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_division_null_matrix_throws()
            {
                var @null = (Matrix3D)null;
                var divisor = 1.0;

                Assert.Throws<ArgumentNullException>(() => @null * divisor);
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

        public class IndexerGet : Matrix3DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(1.0d, m[0, 0]);
                Assert.Equal(-5.0, m[0, 1]);
                Assert.Equal(9.0d, m[0, 2]);
                Assert.Equal(-1.0, m[1, 0]);
                Assert.Equal(8.0d, m[1, 1]);
                Assert.Equal(-4.0, m[1, 2]);
                Assert.Equal(21.0, m[2, 0]);
                Assert.Equal(-0.5d, m[2, 1]);
                Assert.Equal(1.4d, m[2, 2]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[3, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0]);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 3]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue]);
            }
        }

        public class IndexerSet : Matrix3DTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix3D();

                m[0, 0] = 1.0;
                m[0, 1] = -5.0;
                m[0, 2] = 9.0;
                m[1, 0] = -1.0;
                m[1, 1] = 8.0;
                m[1, 2] = -4.0;
                m[2, 0] = 21.0;
                m[2, 1] = -0.5;
                m[2, 2] = 1.4;

                Assert.Equal(1.0d, m[0, 0]);
                Assert.Equal(-5.0, m[0, 1]);
                Assert.Equal(9.0d, m[0, 2]);
                Assert.Equal(-1.0, m[1, 0]);
                Assert.Equal(8.0d, m[1, 1]);
                Assert.Equal(-4.0, m[1, 2]);
                Assert.Equal(21.0, m[2, 0]);
                Assert.Equal(-0.5d, m[2, 1]);
                Assert.Equal(1.4d, m[2, 2]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[3, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0] = 0);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 3] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue] = 0);
            }
        }

        public class IEquatable_Self_Equals : Matrix3DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = CreateIncremenetalMatrix();

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = CreateIncremenetalMatrix();
                Assert.NotSame(a, b);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix3D(4, 3, 2, 1, 0, -1, -2, -3, -4);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((Matrix3D)null));
            }
        }

        public class Object_Equals : Matrix3DTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var m = CreateIncremenetalMatrix();

                Assert.True(m.Equals((object)m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = CreateIncremenetalMatrix();
                Assert.NotSame(a, b);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix3D(4, 3, 2, 1, -1, -2, -3, -4, -5);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((object)null));
            }

            [Fact]
            public void matrix_does_not_equal_unknown_type()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((object)"not-a-matrix"));
            }
        }

        public class GetHashCodeTests : Matrix3DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var m = CreateIncremenetalMatrix();

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
                var expected = CreateIncremenetalMatrix();
                var actual = new Matrix3D(expected);

                for (var col = 0; col < expected.Columns; col++)
                {
                    var temp = expected.Get(rowA, col);
                    expected.Set(rowA, col, expected.Get(rowB, col));
                    expected.Set(rowB, col, temp);
                }

                actual.SwapRows(rowA, rowB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();

                actual.SwapRows(row, row);

                Assert.Equal(expected, actual);
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
                var expected = CreateIncremenetalMatrix();
                var actual = new Matrix3D(expected);

                for (var row = 0; row < expected.Rows; row++)
                {
                    var temp = expected.Get(row, columnA);
                    expected.Set(row, columnA, expected.Get(row, columnB));
                    expected.Set(row, columnB, temp);
                }

                actual.SwapColumns(columnA, columnB);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            public void swapping_same_columns_does_nothing(int column)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();
                Assert.NotSame(expected, actual);

                actual.SwapColumns(column, column);

                Assert.Equal(expected, actual);
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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

                m.ScaleColumn(2, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 30,
                        4, 5, 60,
                        7, 8, 90),
                    m);
            }
        }

        public class DivideRow : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(3, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(20, 1));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(0, 10);

                Assert.Equal(
                    new Matrix3D(
                        0.1, 0.2, 0.3,
                        4, 5, 6,
                        7, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(1, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 3,
                        0.4, 0.5, 0.6,
                        7, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_third_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(2, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 3,
                        4, 5, 6,
                        0.7, 0.8, 0.9),
                    m);
            }
        }

        public class DivideColumn : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(3, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(20, 1));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(0, 10);

                Assert.Equal(
                    new Matrix3D(
                        0.1, 2, 3,
                        0.4, 5, 6,
                        0.7, 8, 9),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(1, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 0.2, 3,
                        4, 0.5, 6,
                        7, 0.8, 9),
                    m);
            }

            [Fact]
            public void can_scale_third_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(2, 10);

                Assert.Equal(
                    new Matrix3D(
                        1, 2, 0.3,
                        4, 5, 0.6,
                        7, 8, 0.9),
                    m);
            }
        }

        public class AddRow : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            public void can_add_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractRow : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            public void can_subtract_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) - value);
                }

                actual.SubtractRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledRow : Matrix3DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(3, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledRow(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(0, 2, 4)]
            [InlineData(1, 0, 20)]
            [InlineData(1, 1, -4)]
            [InlineData(1, 2, -40)]
            [InlineData(2, 0, 2.1234)]
            [InlineData(2, 1, -4)]
            [InlineData(2, 2, -4.09)]
            public void can_add_scaled_row(int sourceRow, int targetRow, double scalar)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c) * scalar;
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddColumn : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            public void can_add_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractColumn : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            public void can_subtract_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) - value);
                }

                actual.SubtractColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : Matrix3DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-1, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(3, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(99, 0, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 99, 0));
            }

            [Theory]
            [InlineData(0, 0, 2)]
            [InlineData(0, 1, -4)]
            [InlineData(0, 2, 4)]
            [InlineData(1, 0, 20)]
            [InlineData(1, 1, -4)]
            [InlineData(1, 2, -40)]
            [InlineData(2, 0, 2.1234)]
            [InlineData(2, 1, -4)]
            [InlineData(2, 2, -4.09)]
            public void can_add_scaled_column(int sourceColumn, int targetColumn, double scalar)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn) * scalar;
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddMatrix : Matrix3DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentNullException>(() => m.GetSum((Matrix3D)null));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix3D(4.4, 3.3, 2.2, 1.1, -0.1, 1.2, 2.3, 4.6, 7.7);
                var expected = new Matrix3D(5.4, 5.3, 5.2, 5.1, 4.9, 7.2, 9.3, 12.6, 16.7);

                var actual = a.GetSum(b);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractMatrix : Matrix3DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentNullException>(() => m.GetDifference((Matrix3D)null));
            }

            [Fact]
            public void can_subtract_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix3D(4.4, 3.3, 2.2, 1.1, -0.1, 1.2, 2.3, 4.6, 7.7);
                var expected = new Matrix3D(1 - 4.4, 2 - 3.3, 3 - 2.2, 4 - 1.1, 5 + 0.1, 6 - 1.2, 7 - 2.3, 8 - 4.6, 9 - 7.7);

                var actual = a.GetDifference(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : Matrix3DTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new Matrix3D(1, 2, 3, 4, 5, 6, -7, 8, -9);
                var expected = new Matrix3D(1.1, 2 * 1.1, 3 * 1.1, 4 * 1.1, 5 * 1.1, 6 * 1.1, -7 * 1.1, 8 * 1.1, -9 * 1.1);

                var actual = a.GetScaled(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideDenominator : Matrix3DTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = CreateIncremenetalMatrix();
                var expected = new Matrix3D(1 / 1.1, 2 / 1.1, 3 / 1.1, 4 / 1.1, 5 / 1.1, 6 / 1.1, 7 / 1.1, 8 / 1.1, 9 / 1.1);

                var actual = a.GetQuotient(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : Matrix3DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = CreateIncremenetalMatrix();

                Assert.Throws<ArgumentNullException>(() => sut.GetProduct((Matrix3D)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new Matrix3D(
                    3, 1, 2,
                    -10, -2, 4,
                    -9, 5, -3);
                var b = new Matrix3D(
                    2, 3, 90,
                    -4, 5, 7,
                    13, -2, -1);
                var expected = new Matrix3D(
                    28, 10, 275,
                    40, -48, -918,
                    -77, 4, -772);

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new Matrix3D(3, 1, -10, -2, 5, 6, 7, 1, 99.9);
                var identity = Matrix3D.CreateIdentity();
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.GetProduct(identity));
                Assert.Equal(a, identity.GetProduct(a));
            }
        }

        public class Transposed : Matrix3DTests
        {
            [Fact]
            public void can_transpose()
            {
                var source = CreateIncremenetalMatrix();
                var expected = new Matrix3D(
                    1, 4, 7,
                    2, 5, 8,
                    3, 6, 9);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : Matrix3DTests
        {
            [Fact]
            public void can_transpose()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix3D(
                    1, 4, 7,
                    2, 5, 8,
                    3, 6, 9);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }
        }

        public class IsSymmetric : Matrix3DTests
        {
            [Fact]
            public void symetric_matrix_is_symetric()
            {
                var matrix = new Matrix3D(
                    9, 4, 3,
                    4, 15, 1,
                    3, 1, -2);

                Assert.True(matrix.IsSymetric);
            }

            [Fact]
            public void incremental_values_are_not_symetric()
            {
                var matrix = CreateIncremenetalMatrix();

                Assert.False(matrix.IsSymetric);
            }
        }

        public class Trace : Matrix3DTests
        {
            [Fact]
            public void can_get_trace()
            {
                var matrix = new Matrix3D(
                    9, 4, 7,
                    4, 15, 0,
                    1, 2, 3);
                var expected = 9 + 15 + 3;

                var actual = matrix.Trace;

                Assert.Equal(expected, actual);
            }
        }

        public class GetInverse : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = Matrix3D.CreateIdentity();
                var expected = Matrix3D.CreateIdentity();

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new Matrix3D();

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new Matrix3D(1, 1, 1, 1, 1, 1, 1, 1, 1);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void matrix_is_not_mutated_by_inverse()
            {
                var actual = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);
                var expected = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);

                var result = actual.GetInverse();

                Assert.NotEqual(result, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void invert_can_round_trip()
            {
                var matrix = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);
                var expected = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);

                var expected = new Matrix3D(
                    2, -1, 0,
                    4, -2, -1,
                    -2.5, 1.5, 0.5);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }
        }

        public class TryGetInverse : Matrix3DTests
        {
            [Fact]
            public void failure_does_not_mutate_matrix()
            {
                var actual = new Matrix3D(1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix3D(actual);

                Matrix3D inverse;
                var result = actual.TryGetInverse(out inverse);

                Assert.NotNull(inverse);
                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_inverse()
            {
                var matrix = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);
                var expected = new Matrix3D(
                    2, -1, 0,
                    4, -2, -1,
                    -2.5, 1.5, 0.5);

                Matrix3D actual;
                var result = matrix.TryGetInverse(out actual);

                Assert.True(result);
                Assert.Equal(expected, actual);
            }
        }

        public class Invert : Matrix3DTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix3D(1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix3D(actual);

                Action act = () => actual.Invert();

                Assert.Throws<NoInverseException>(act);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);
                var expected = new Matrix3D(
                    2, -1, 0,
                    4, -2, -1,
                    -2.5, 1.5, 0.5);

                actual.Invert();

                Assert.Equal(expected, actual);
            }
        }

        public class TryInvert : Matrix3DTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix3D(1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix3D(actual);

                var result = actual.TryInvert();

                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix3D(
                    1, 1, 2,
                    1, 2, 4,
                    2, -1, 0);
                var expected = new Matrix3D(
                    2, -1, 0,
                    4, -2, -1,
                    -2.5, 1.5, 0.5);

                var result = actual.TryInvert();

                Assert.True(result);
                Assert.Equal(expected, actual);
            }
        }

        public class GetDeterminant : Matrix3DTests
        {
            [Fact]
            public void example_0()
            {
                var matrix = new Matrix3D(
                    1, 2, -1,
                    3, 0, 1,
                    4, 2, 1);
                var expected = -6;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var matrix = new Matrix3D(
                    1, 5, 7,
                    4, 2, 9,
                    6, 3, 8);
                var expected = 99;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_2()
            {
                var matrix = new Matrix3D(
                    2, 3, 90,
                    -4, 5, 7,
                    13, -2, -1);
                var expected = -4851;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_3()
            {
                var matrix = new Matrix3D(1, 0, 1, 1, 1, 0, 0, 0, 0);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_4()
            {
                var matrix = new Matrix3D(2, 0, -7, 3, 0, 1, -4, 0, 9);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_5()
            {
                var matrix = new Matrix3D(2, -1, 3, 1, 2, 4, 2, 4, 8);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_6()
            {
                var matrix = Matrix3D.CreateIdentity();
                matrix.Set(1, 1, 0);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_7()
            {
                var matrix = new Matrix3D(
                    0, 1, 0,
                    1, 0, 1,
                    0, 1, 0);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }
        }

        public class GetProductVector2DTests : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var original = new Vector2D(3, 5);
                var expected = new Vector2D(original);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_rotation_and_translation_transforms()
            {
                var matrix = Matrix3D.CreateRotation(new Vector2D(1, 1), Math.PI / 2.0);
                var original = new Vector2D(1, 0);
                var expected = new Vector2D(2, 1);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector2D(2, 3);
                var matrix = Matrix3D.CreateTranslation(delta);
                var original = new Vector2D(1.1, 2.2);
                var expected = original + delta;

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_origin_rotation()
            {
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);
                var original = new Vector2D(1, 2);
                var expected = new Vector2D(-2, 1);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var origin = new Vector2D(1.1, 2.2);
                var expected = new Vector2D(origin.X * scalars.X, origin.Y * scalars.Y);

                var actual = matrix.GetProduct(origin);

                Assert.Equal(expected, actual);
            }
        }

        public class GetProductVector3DTets : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var original = new Vector3D(3, 5, -9);
                var expected = new Vector3D(original);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var origin = new Vector3D(1.1, 2.2, -4.4);
                var expected = new Vector3D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z);

                var actual = matrix.GetProduct(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix3D.CreateRotationX(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var original = new Vector3D(1, 4.5, 1);
                var expected = new Vector3D(1, 4.5, -1);
                var matrix = Matrix3D.CreateRotationY(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var original = new Vector3D(1, 1, 3.2);
                var expected = new Vector3D(-1, 1, 3.2);
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        public class GetProductColumnVector2DTests : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var original = new Vector2D(3, 5);
                var expected = new Vector2D(original);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_rotation_and_translation_transforms()
            {
                var matrix = Matrix3D.CreateRotation(new Vector2D(1, 1), Math.PI / 2.0);
                var original = new Vector2D(1, 0);
                var expected = new Vector2D(2, 1);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector2D(2, 3);
                var matrix = Matrix3D.CreateTranslation(delta);
                var original = new Vector2D(1.1, 2.2);
                var expected = new Vector2D(original) + delta;

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_origin_rotation()
            {
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);
                var original = new Vector2D(1, 2);
                var expected = new Vector2D(-2, 1);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var origin = new Vector2D(1.1, 2.2);
                var expected = new Vector2D(origin.X * scalars.X, origin.Y * scalars.Y);

                var actual = matrix.GetProductColumnVector(origin);

                Assert.Equal(expected, actual);
            }
        }

        public class GetProductColumnVector3DTests : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var original = new Vector3D(3, 5, -9);
                var expected = new Vector3D(original);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var origin = new Vector3D(1.1, 2.2, -4.4);
                var expected = new Vector3D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z);

                var actual = matrix.GetProductColumnVector(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix3D.CreateRotationX(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var original = new Vector3D(1, 4.5, 1);
                var expected = new Vector3D(1, 4.5, -1);
                var matrix = Matrix3D.CreateRotationY(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var original = new Vector3D(1, 1, 3.2);
                var expected = new Vector3D(-1, 1, 3.2);
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        public class TransformVector2DTests : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var actual = new Vector2D(3, 5);
                var expected = new Vector2D(actual);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_rotation_and_translation_transforms()
            {
                var matrix = Matrix3D.CreateRotation(new Vector2D(1, 1), Math.PI / 2.0);
                var actual = new Vector2D(1, 0);
                var expected = new Vector2D(2, 1);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector2D(2, 3);
                var matrix = Matrix3D.CreateTranslation(delta);
                var actual = new Vector2D(1.1, 2.2);
                var expected = actual + delta;

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_origin_rotation()
            {
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);
                var actual = new Vector2D(1, 2);
                var expected = new Vector2D(-2, 1);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var actual = new Vector2D(1.1, 2.2);
                var expected = new Vector2D(actual.X * scalars.X, actual.Y * scalars.Y);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }
        }

        public class TransformVector3DTets : Matrix3DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix3D.CreateIdentity();
                var actual = new Vector3D(3, 5, -9);
                var expected = new Vector3D(actual);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix3D.CreateScaled(scalars);
                var actual = new Vector3D(1.1, 2.2, -4.4);
                var expected = new Vector3D(actual.X * scalars.X, actual.Y * scalars.Y, actual.Z * scalars.Z);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var actual = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix3D.CreateRotationX(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var actual = new Vector3D(1, 4.5, 1);
                var expected = new Vector3D(1, 4.5, -1);
                var matrix = Matrix3D.CreateRotationY(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var actual = new Vector3D(1, 1, 3.2);
                var expected = new Vector3D(-1, 1, 3.2);
                var matrix = Matrix3D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        protected Matrix3D CreateIncremenetalMatrix()
        {
            return new Matrix3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
    }
}
