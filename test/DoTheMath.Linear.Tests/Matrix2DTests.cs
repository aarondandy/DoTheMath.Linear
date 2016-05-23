using System;
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
                var expected = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

                Assert.True(m.Equals(m));
            }

            [Fact]
            public void different_ref_same_element_are_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = CreateIncremenetalMatrix();

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_elements_are_not_equal()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix2D(4, 3, 2, 1);
                Assert.NotSame(a, b);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((Matrix2D)null));
            }
        }

        public class Object_Equals : Matrix2DTests
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
                var b = new Matrix2D(4, 3, 2, 1);

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

        public class GetHashCodeTests : Matrix2DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var sut = CreateIncremenetalMatrix();
                var expectedHashCode = sut.GetHashCode();

                sut.E00 = 4;
                sut.E01 = -1.1;
                sut.E10 = -999.999;
                sut.E11 = 9;

                Assert.Equal(expectedHashCode, sut.GetHashCode());
            }
        }

        public class SwapRows : Matrix2DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var sut = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapRows(0, 99));
            }

            [Fact]
            public void can_swap_first_and_second_rows()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2D(3, 4, 1, 2);

                actual.SwapRows(0, 1);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_swap_second_and_first_rows()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2D(3, 4, 1, 2);

                actual.SwapRows(1, 0);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void swapping_same_rows_does_nothing()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();

                for (int r = 0; r < actual.Rows; r++)
                {
                    actual.SwapRows(r, r);

                    Assert.Equal(expected, actual);
                }
            }
        }

        public class SwapColumns : Matrix2DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var sut = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SwapColumns(0, 99));
            }

            [Fact]
            public void can_swap_first_and_second_columns()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2D(2, 1, 4, 3);

                actual.SwapColumns(0, 1);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_swap_second_and_first_columns()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2D(2, 1, 4, 3);

                actual.SwapColumns(1, 0);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void swapping_same_columns_does_nothing()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();

                for (int r = 0; r < actual.Columns; r++)
                {
                    actual.SwapColumns(r, r);

                    Assert.Equal(expected, actual);
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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

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
                var m = CreateIncremenetalMatrix();

                m.ScaleColumn(1, 10);

                Assert.Equal(
                    new Matrix2D(
                        1, 20,
                        3, 40),
                    m);
            }
        }

        public class DivideRow : Matrix2DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(2, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(20, 1));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(0, 10);

                Assert.Equal(
                    new Matrix2D(
                        0.1, 0.2,
                        3, 4),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(1, 10);

                Assert.Equal(
                    new Matrix2D(
                        1, 2,
                        0.3, 0.4),
                    m);
            }
        }

        public class DivideColumn : Matrix2DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(2, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(20, 1));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(0, 10);

                Assert.Equal(
                    new Matrix2D(
                        0.1, 2,
                        0.3, 4),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(1, 10);

                Assert.Equal(
                    new Matrix2D(
                        1, 0.2,
                        3, 0.4),
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
                var actual = CreateIncremenetalMatrix();
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
                var actual = CreateIncremenetalMatrix();
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
                var a = CreateIncremenetalMatrix();
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
                var a = CreateIncremenetalMatrix();
                var expected = new Matrix2D(1 * 1.1, 2 * 1.1, 3 * 1.1, 4 * 1.1);

                var actual = a.Multiply(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : Matrix2DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = CreateIncremenetalMatrix();

                Assert.Throws<ArgumentNullException>(() => sut.Multiply((Matrix2D)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new Matrix2D(3, 1, -10, -2);
                var b = new Matrix2D(2, 3, -4, 5);
                var expected = new Matrix2D(2, 14, -12, -40);

                var actual = a.Multiply(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new Matrix2D(3, 1, -10, -2);
                var identity = Matrix2D.CreateIdentity();
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.Multiply(identity));
                Assert.Equal(a, identity.Multiply(a));
            }
        }

        public class Transposed : Matrix2DTests
        {
            [Fact]
            public void can_transpose()
            {
                var source = CreateIncremenetalMatrix();
                var expected = new Matrix2D(
                    1, 3,
                    2, 4);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : Matrix2DTests
        {
            [Fact]
            public void can_transpose()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2D(
                    1, 3,
                    2, 4);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }
        }

        public class IsSymmetric : Matrix2DTests
        {
            [Fact]
            public void symetric_matrix_is_symetric()
            {
                var matrix = new Matrix2D(
                    9, 4,
                    4, 15);

                Assert.True(matrix.IsSymetric);
            }

            [Fact]
            public void incremental_values_are_not_symetric()
            {
                var matrix = CreateIncremenetalMatrix();

                Assert.False(matrix.IsSymetric);
            }
        }

        public class Trace : Matrix2DTests
        {
            [Fact]
            public void can_get_trace()
            {
                var matrix = new Matrix2D(
                    9, 4,
                    4, 15);
                var expected = 9 + 15;

                var actual = matrix.Trace;

                Assert.Equal(expected, actual);
            }
        }

        public class GetInverse : Matrix2DTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = Matrix2D.CreateIdentity();
                var expected = Matrix2D.CreateIdentity();

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new Matrix2D();

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new Matrix2D(1, 1, 1, 1);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void matrix_is_not_mutated_by_inverse()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();

                var result = actual.GetInverse();

                Assert.NotEqual(result, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void invert_can_round_trip()
            {
                var matrix = new Matrix2D(
                    2, -1,
                    0, 1);
                var expected = new Matrix2D(
                    2, -1,
                    0, 1);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void swapped_identity_has_inverse()
            {
                var matrix = new Matrix2D(
                    0, 1,
                    1, 0);
                var expected = new Matrix2D(
                    0, 1,
                    1, 0);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new Matrix2D(
                    1, 3,
                    2, 4);

                var expected = new Matrix2D(
                    -2, 1.5,
                    1, -0.5);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_1()
            {
                var matrix = new Matrix2D(
                    2, -1,
                    -1, 1);

                var expected = new Matrix2D(
                    1, 1,
                    1, 2);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_2()
            {
                var matrix = new Matrix2D(
                    2, -1,
                    0, 1);

                var expected = new Matrix2D(
                    0.5, 0.5,
                    0, 1);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_3()
            {
                var matrix = new Matrix2D(
                    0, -1,
                    2, 1);

                var expected = new Matrix2D(
                    0.5, 0.5,
                    -1, 0);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_4()
            {
                var matrix = new Matrix2D(
                    1, -1,
                    1, 1);

                var expected = new Matrix2D(
                    0.5, 0.5,
                    -0.5, 0.5);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }
        }

        public class GetDeterminant : Matrix2DTests
        {
            [Fact]
            public void identity_has_determinant_of_one()
            {
                var matrix = new Matrix2D(
                    1, 0,
                    0, 1);
                var expected = 1.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_determinant_of_zero()
            {
                var matrix = new Matrix2D();
                var expected = 0.0;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_0()
            {
                var matrix = new Matrix2D(
                    2, 4,
                    -3, 1);
                var expected = 14;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var matrix = new Matrix2D(
                    0, 1,
                    2, 3);
                var expected = -2;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_2()
            {
                var matrix = new Matrix2D(
                    3, 6,
                    1, 2);
                var expected = 0.0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_3()
            {
                var matrix = new Matrix2D(
                    5, 4,
                    -14, 3);
                var expected = 71.0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_4()
            {
                var matrix = new Matrix2D(
                    3, -8,
                    5, -2);
                var expected = 34.0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_5()
            {
                var matrix = new Matrix2D(
                    1, 3,
                    2, 4);
                var expected = -2.0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_6()
            {
                var matrix = new Matrix2D(
                    -2, 1.5,
                    1, -0.5);
                var expected = -0.5;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_7()
            {
                var matrix = new Matrix2D(
                    0, 2,
                    1, 3);
                var expected = -2;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_8()
            {
                var matrix = new Matrix2D(
                    3, 1,
                    -10, -2);
                var expected = 4;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_9()
            {
                var matrix = new Matrix2D(
                    2, 3,
                    -4, 5);
                var expected = 22;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_10()
            {
                var matrix = new Matrix2D(
                    2, 14,
                    -12, -40);
                var expected = 88;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_11()
            {
                var matrix = new Matrix2D(
                    1, 1,
                    1, 1);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_12()
            {
                var matrix = new Matrix2D(
                    1, 1,
                    1, 1.1);
                var expected = 0.1;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_13()
            {
                var matrix = new Matrix2D(
                    0, 1,
                    1, 2);
                var expected = -1;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }
        }

        protected Matrix2D CreateIncremenetalMatrix()
        {
            return new Matrix2D(1, 2, 3, 4);
        }
    }
}
