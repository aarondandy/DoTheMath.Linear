using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix2FTests
    {
        public class ConstructorsAndFactories : Matrix2FTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix2F();

                Assert.Equal(0.0f, m.E00);
                Assert.Equal(0.0f, m.E01);
                Assert.Equal(0.0f, m.E10);
                Assert.Equal(0.0f, m.E11);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix2F(1.0f, -5.0f, 9.0f, -1.0f);

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0d, m.E01);
                Assert.Equal(9.0d, m.E10);
                Assert.Equal(-1.0d, m.E11);
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix2F((Matrix2F)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = CreateIncremenetalMatrix();

                var actual = new Matrix2F(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix2F((IMatrix<float>)null));
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_bad_size()
            {
                var source = new MatrixF(3, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix2F((IMatrix<float>)source));
            }

            [Fact]
            public void copy_constructor_imatrix_contains_same_elements()
            {
                var source = new MatrixF(2, 2);
                source.Set(0, 0, 16.0f);
                source.Set(0, 1, 1.0f);
                source.Set(1, 0, 3.0f);
                source.Set(1, 1, 4.0f);

                var expected = new Matrix2F(
                    16.0f, 1.0f,
                    3.0f, 4.0f);

                var actual = new Matrix2F((IMatrix<float>)source);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Factories : Matrix2FTests
        {
            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix2F.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void rotation_factory_creates_ccw_rotation_matrix()
            {
                var rotation = (float)(Math.PI / 2.0);
                var expected = new Matrix2F
                {
                    E00 = 0.0f,
                    E01 = 1.0f,
                    E10 = -1.0f,
                    E11 = 0.0f
                };

                var actual = Matrix2F.CreateRotation(rotation);

                Assert.Equal(expected.E00, actual.E00, 7);
                Assert.Equal(expected.E01, actual.E01, 7);
                Assert.Equal(expected.E10, actual.E10, 7);
                Assert.Equal(expected.E11, actual.E11, 7);
            }

            [Fact]
            public void rotation_factory_creates_cw_rotation_matrix()
            {
                var rotation = (float)(-Math.PI / 2.0);
                var expected = new Matrix2F
                {
                    E00 = 0.0f,
                    E01 = -1.0f,
                    E10 = 1.0f,
                    E11 = 0.0f
                };

                var actual = Matrix2F.CreateRotation(rotation);

                Assert.Equal(expected.E00, actual.E00, 7);
                Assert.Equal(expected.E01, actual.E01, 7);
                Assert.Equal(expected.E10, actual.E10, 7);
                Assert.Equal(expected.E11, actual.E11, 7);
            }

            [Fact]
            public void rotation_factory_creates_rotation_matrix()
            {
                var rotation = (float)(Math.Sqrt(2) / 2.0);
                var expected = new Matrix2F
                {
                    E00 = (float)Math.Cos(rotation),
                    E01 = (float)Math.Sin(rotation),
                    E10 = (float)-Math.Sin(rotation),
                    E11 = (float)Math.Cos(rotation)
                };

                var actual = Matrix2F.CreateRotation(rotation);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_scaling_matrix()
            {
                var factors = new Vector2F(2, -3);
                var expected = Matrix2F.CreateIdentity();
                expected.E00 = factors.X;
                expected.E11 = factors.Y;

                var actual = Matrix2F.CreateScaled(factors);

                Assert.Equal(expected, actual);
            }
        }

        public class OperatorOverloads : Matrix2FTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix2F(4.4f, 3.3f, 2.2f, 1.1f);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_addition_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix2F)null;

                Assert.Throws<ArgumentNullException>(() => matrix + @null);
                Assert.Throws<ArgumentNullException>(() => @null + matrix);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix2F(4.4f, 3.3f, 2.2f, 1.1f);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtract_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix2F)null;

                Assert.Throws<ArgumentNullException>(() => matrix - @null);
                Assert.Throws<ArgumentNullException>(() => @null - matrix);
            }

            [Fact]
            public void op_multiply_mimics_multiply_matrix()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix2F(4.4f, 3.3f, 2.2f, 1.1f);
                var expected = left.GetProduct(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix2F)null;

                Assert.Throws<ArgumentNullException>(() => matrix * @null);
                Assert.Throws<ArgumentNullException>(() => @null * matrix);
            }

            [Fact]
            public void op_multiply_mimics_multiply_scalar()
            {
                var matrix = CreateIncremenetalMatrix();
                var scalar = -9.5f;
                var expected = matrix.GetScaled(scalar);

                var actual = matrix * scalar;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_mimics_multiply_prefix_scalar()
            {
                var matrix = CreateIncremenetalMatrix();
                var scalar = -13.5f;
                var expected = matrix.GetScaled(scalar);

                var actual = scalar * matrix;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalars_null_matrix_throws()
            {
                var @null = (Matrix2F)null;
                var scalar = 1.0f;

                Assert.Throws<ArgumentNullException>(() => scalar * @null);
                Assert.Throws<ArgumentNullException>(() => @null * scalar);
            }

            [Fact]
            public void op_division_mimics_divide_denominator()
            {
                var matrix = CreateIncremenetalMatrix();
                var divisor = -1.3f;
                var expected = matrix.GetQuotient(divisor);

                var actual = matrix / divisor;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_division_null_matrix_throws()
            {
                var @null = (Matrix2F)null;
                var divisor = 1.0f;

                Assert.Throws<ArgumentNullException>(() => @null * divisor);
            }
        }

        public class Properties : Matrix2FTests
        {
            [Fact]
            public void rows_and_cols_are_two()
            {
                var m = new Matrix2F();

                Assert.Equal(2, m.Rows);
                Assert.Equal(2, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix2F();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix2FTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix2F();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix2F();
                m.E00 = 1.0f;
                m.E01 = 0.0f;
                m.E10 = 0.0f;
                m.E11 = 1.0f;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix2F();
                m.E00 = 1.0f;
                m.E01 = 2.0f;
                m.E10 = 3.0f;
                m.E11 = 4.0f;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix2FTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix2F(1.0f, -5.0f, 9.0f, -1.0f);

                Assert.Equal(1.0f, m.Get(0, 0));
                Assert.Equal(-5.0f, m.Get(0, 1));
                Assert.Equal(9.0f, m.Get(1, 0));
                Assert.Equal(-1.0f, m.Get(1, 1));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }

        public class Set : Matrix2FTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix2F();

                m.Set(0, 0, 1.0f);
                m.Set(0, 1, -5.0f);
                m.Set(1, 0, 9.0f);
                m.Set(1, 1, -1.0f);

                Assert.Equal(1.0f, m.Get(0, 0));
                Assert.Equal(-5.0f, m.Get(0, 1));
                Assert.Equal(9.0f, m.Get(1, 0));
                Assert.Equal(-1.0f, m.Get(1, 1));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(-1, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(2, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MinValue, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(int.MaxValue, 0, 0.0f));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, -1, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, 2, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MinValue, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Set(0, int.MaxValue, 0.0f));
            }
        }

        public class IndexerGet : Matrix2FTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix2F(1.0f, -5.0f, 9.0f, -1.0f);

                Assert.Equal(1.0f, m[0, 0]);
                Assert.Equal(-5.0f, m[0, 1]);
                Assert.Equal(9.0f, m[1, 0]);
                Assert.Equal(-1.0f, m[1, 1]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[2, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0]);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 2]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue]);
            }
        }

        public class IndexerSet : Matrix2FTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix2F();

                m[0, 0] = 1.0f;
                m[0, 1] = -5.0f;
                m[1, 0] = 9.0f;
                m[1, 1] = -1.0f;

                Assert.Equal(1.0f, m[0, 0]);
                Assert.Equal(-5.0f, m[0, 1]);
                Assert.Equal(9.0f, m[1, 0]);
                Assert.Equal(-1.0f, m[1, 1]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[2, 0] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0] = 0.0f);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 2] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue] = 0.0f);
            }
        }

        public class IEquatable_Self_Equals : Matrix2FTests
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
                var b = new Matrix2F(4.0f, 3.0f, 2.0f, 1.0f);
                Assert.NotSame(a, b);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((Matrix2F)null));
            }
        }

        public class Object_Equals : Matrix2FTests
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
                var b = new Matrix2F(4.0f, 3.0f, 2.0f, 1.0f);

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

        public class GetHashCodeTests : Matrix2FTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var sut = CreateIncremenetalMatrix();
                var expectedHashCode = sut.GetHashCode();

                sut.E00 = 4.0f;
                sut.E01 = -1.1f;
                sut.E10 = -999.999f;
                sut.E11 = 9.0f;

                Assert.Equal(expectedHashCode, sut.GetHashCode());
            }
        }

        public class SwapRows : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var sut = new Matrix2F();

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
                var expected = new Matrix2F(3.0f, 4.0f, 1.0f, 2.0f);

                actual.SwapRows(0, 1);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_swap_second_and_first_rows()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(3.0f, 4.0f, 1.0f, 2.0f);

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

        public class SwapColumns : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var sut = new Matrix2F();

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
                var expected = new Matrix2F(2.0f, 1.0f, 4.0f, 3.0f);

                actual.SwapColumns(0, 1);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_swap_second_and_first_columns()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(2.0f, 1.0f, 4.0f, 3.0f);

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

        public class ScaleRow : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

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
                    new Matrix2F(
                        10.0f, 20.0f,
                        3.0f, 4.0f),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = CreateIncremenetalMatrix();

                m.ScaleRow(1, 10);

                Assert.Equal(
                    new Matrix2F(
                        1.0f, 2.0f,
                        30.0f, 40.0f),
                    m);
            }
        }

        public class ScaleColumn : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

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
                    new Matrix2F(
                        10.0f, 2.0f,
                        30.0f, 4.0f),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = CreateIncremenetalMatrix();

                m.ScaleColumn(1, 10);

                Assert.Equal(
                    new Matrix2F(
                        1.0f, 20.0f,
                        3.0f, 40.0f),
                    m);
            }
        }

        public class DivideRow : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix2F();

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
                    new Matrix2F(
                        0.1f, 0.2f,
                        3.0f, 4.0f),
                    m);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideRow(1, 10);

                Assert.Equal(
                    new Matrix2F(
                        1.0f, 2.0f,
                        0.3f, 0.4f),
                    m);
            }
        }

        public class DivideColumn : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix2F();

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
                    new Matrix2F(
                        0.1f, 2.0f,
                        0.3f, 4.0f),
                    m);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var m = CreateIncremenetalMatrix();

                m.DivideColumn(1, 10);

                Assert.Equal(
                    new Matrix2F(
                        1.0f, 0.2f,
                        3.0f, 0.4f),
                    m);
            }
        }

        public class AddRow : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            public void can_add_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractRow : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            public void can_subtract_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) - value);
                }

                actual.SubtractRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledRow : Matrix2FTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix2F();

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
            public void can_add_scaled_row(int sourceRow, int targetRow, float scalar)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c) * scalar;
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddScaledRow(sourceRow, targetRow, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddColumn : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            public void can_add_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractColumn : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(2, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            public void can_subtract_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) - value);
                }

                actual.SubtractColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class AddScaledColumn : Matrix2FTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(-1, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(2, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(99, 0, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, -1, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 2, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddScaledColumn(0, 99, 0.0f));
            }

            [Theory]
            [InlineData(0, 0, 2.0f)]
            [InlineData(0, 1, -4.0f)]
            [InlineData(1, 0, 10.234f)]
            [InlineData(1, 1, -99.9f)]
            public void can_add_scaled_column(int sourceColumn, int targetColumn, float scalar)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn) * scalar;
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddScaledColumn(sourceColumn, targetColumn, scalar);

                Assert.Equal(expected, actual);
            }
        }

        public class AddMatrix : Matrix2FTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentNullException>(() => m.GetSum((Matrix2F)null));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix2F(4.4f, 3.3f, 2.2f, 1.1f);
                var expected = new Matrix2F(5.4f, 5.3f, 5.2f, 5.1f);

                var actual = a.GetSum(b);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractMatrix : Matrix2FTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix2F();

                Assert.Throws<ArgumentNullException>(() => m.GetDifference((Matrix2F)null));
            }

            [Fact]
            public void can_subtract_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix2F(4.4f, 3.3f, 2.2f, 1.1f);
                var expected = new Matrix2F(1.0f - 4.4f, 2.0f - 3.3f, 3.0f - 2.2f, 4.0f - 1.1f);

                var actual = a.GetDifference(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : Matrix2FTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = CreateIncremenetalMatrix();
                var expected = new Matrix2F(1.0f * 1.1f, 2.0f * 1.1f, 3.0f * 1.1f, 4.0f * 1.1f);

                var actual = a.GetScaled(1.1f);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideDenominator : Matrix2FTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = CreateIncremenetalMatrix();
                var expected = new Matrix2F(1.0f / 1.1f, 2.0f / 1.1f, 3.0f / 1.1f, 4.0f / 1.1f);

                var actual = a.GetQuotient(1.1f);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : Matrix2FTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = CreateIncremenetalMatrix();

                Assert.Throws<ArgumentNullException>(() => sut.GetProduct((Matrix2F)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new Matrix2F(3.0f, 1.0f, -10.0f, -2.0f);
                var b = new Matrix2F(2.0f, 3.0f, -4.0f, 5.0f);
                var expected = new Matrix2F(2.0f, 14.0f, -12.0f, -40.0f);

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new Matrix2F(3.0f, 1.0f, -10.0f, -2.0f);
                var identity = Matrix2F.CreateIdentity();
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.GetProduct(identity));
                Assert.Equal(a, identity.GetProduct(a));
            }
        }

        public class Transposed : Matrix2FTests
        {
            [Fact]
            public void can_transpose()
            {
                var source = CreateIncremenetalMatrix();
                var expected = new Matrix2F(
                    1.0f, 3.0f,
                    2.0f, 4.0f);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : Matrix2FTests
        {
            [Fact]
            public void can_transpose()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix2F(
                    1.0f, 3.0f,
                    2.0f, 4.0f);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }
        }

        public class IsSymmetric : Matrix2FTests
        {
            [Fact]
            public void symetric_matrix_is_symetric()
            {
                var matrix = new Matrix2F(
                    9.0f, 4.0f,
                    4.0f, 15.0f);

                Assert.True(matrix.IsSymetric);
            }

            [Fact]
            public void incremental_values_are_not_symetric()
            {
                var matrix = CreateIncremenetalMatrix();

                Assert.False(matrix.IsSymetric);
            }
        }

        public class Trace : Matrix2FTests
        {
            [Fact]
            public void can_get_trace()
            {
                var matrix = new Matrix2F(
                    9.0f, 4.0f,
                    4.0f, 15.0f);
                var expected = 24.0f;

                var actual = matrix.Trace;

                Assert.Equal(expected, actual);
            }
        }

        public class GetInverse : Matrix2FTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = Matrix2F.CreateIdentity();
                var expected = Matrix2F.CreateIdentity();

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new Matrix2F();

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new Matrix2F(1.0f, 1.0f, 1.0f, 1.0f);

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
                var matrix = new Matrix2F(
                    2.0f, -1.0f,
                    0.0f, 1.0f);
                var expected = new Matrix2F(
                    2.0f, -1.0f,
                    0.0f, 1.0f);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void swapped_identity_has_inverse()
            {
                var matrix = new Matrix2F(
                    0.0f, 1.0f,
                    1.0f, 0.0f);
                var expected = new Matrix2F(
                    0.0f, 1.0f,
                    1.0f, 0.0f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new Matrix2F(
                    1.0f, 3.0f,
                    2.0f, 4.0f);

                var expected = new Matrix2F(
                    -2.0f, 1.5f,
                    1.0f, -0.5f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_1()
            {
                var matrix = new Matrix2F(
                    2.0f, -1.0f,
                    -1.0f, 1.0f);

                var expected = new Matrix2F(
                    1.0f, 1.0f,
                    1.0f, 2.0f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_2()
            {
                var matrix = new Matrix2F(
                    2.0f, -1.0f,
                    0.0f, 1.0f);

                var expected = new Matrix2F(
                    0.5f, 0.5f,
                    0.0f, 1.0f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_3()
            {
                var matrix = new Matrix2F(
                    0.0f, -1.0f,
                    2.0f, 1.0f);

                var expected = new Matrix2F(
                    0.5f, 0.5f,
                    -1.0f, 0.0f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert_example_4()
            {
                var matrix = new Matrix2F(
                    1.0f, -1.0f,
                    1.0f, 1.0f);

                var expected = new Matrix2F(
                    0.5f, 0.5f,
                    -0.5f, 0.5f);

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }
        }

        public class TryGetInverse : Matrix2FTests
        {
            [Fact]
            public void failure_does_not_mutate_matrix()
            {
                var actual = new Matrix2F(1, 1, 1, 1);
                var expected = new Matrix2F(actual);

                Matrix2F inverse;
                var result = actual.TryGetInverse(out inverse);

                Assert.NotNull(inverse);
                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_inverse()
            {
                var matrix = new Matrix2F(
                    1, 3,
                    2, 4);
                var expected = new Matrix2F(
                    -2, 1.5f,
                    1, -0.5f);

                Matrix2F actual;
                var result = matrix.TryGetInverse(out actual);

                Assert.True(result);
                Assert.Equal(expected, actual);
            }
        }

        public class Invert : Matrix2FTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix2F(1, 1, 1, 1);
                var expected = new Matrix2F(actual);

                Action act = () => actual.Invert();

                Assert.Throws<NoInverseException>(act);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix2F(
                    1, 3,
                    2, 4);
                var expected = new Matrix2F(
                    -2, 1.5f,
                    1, -0.5f);

                actual.Invert();

                Assert.Equal(expected, actual);
            }
        }

        public class TryInvert : Matrix2FTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix2F(1, 1, 1, 1);
                var expected = new Matrix2F(actual);

                var result = actual.TryInvert();

                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix2F(
                    1, 3,
                    2, 4);
                var expected = new Matrix2F(
                    -2, 1.5f,
                    1, -0.5f);

                var result = actual.TryInvert();

                Assert.True(result);
                Assert.Equal(expected, actual);
            }
        }

        public class GetDeterminant : Matrix2FTests
        {
            [Fact]
            public void identity_has_determinant_of_one()
            {
                var matrix = new Matrix2F(
                    1.0f, 0.0f,
                    0.0f, 1.0f);
                var expected = 1.0f;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_determinant_of_zero()
            {
                var matrix = new Matrix2F();
                var expected = 0.0f;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_0()
            {
                var matrix = new Matrix2F(
                    2.0f, 4.0f,
                    -3.0f, 1.0f);
                var expected = 14.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var matrix = new Matrix2F(
                    0.0f, 1.0f,
                    2.0f, 3.0f);
                var expected = -2.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_2()
            {
                var matrix = new Matrix2F(
                    3f, 6f,
                    1f, 2f);
                var expected = 0.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_3()
            {
                var matrix = new Matrix2F(
                    5.0f, 4.0f,
                    -14.0f, 3.0f);
                var expected = 71.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_4()
            {
                var matrix = new Matrix2F(
                    3.0f, -8.0f,
                    5.0f, -2.0f);
                var expected = 34.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_5()
            {
                var matrix = new Matrix2F(
                    1.0f, 3.0f,
                    2.0f, 4.0f);
                var expected = -2.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_6()
            {
                var matrix = new Matrix2F(
                    -2.0f, 1.5f,
                    1.0f, -0.5f);
                var expected = -0.5f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_7()
            {
                var matrix = new Matrix2F(
                    0.0f, 2.0f,
                    1.0f, 3.0f);
                var expected = -2.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_8()
            {
                var matrix = new Matrix2F(
                    3.0f, 1.0f,
                    -10.0f, -2.0f);
                var expected = 4.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual, 6);
            }

            [Fact]
            public void example_9()
            {
                var matrix = new Matrix2F(
                    2.0f, 3.0f,
                    -4.0f, 5.0f);
                var expected = 22.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_10()
            {
                var matrix = new Matrix2F(
                    2.0f, 14.0f,
                    -12.0f, -40.0f);
                var expected = 88.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_11()
            {
                var matrix = new Matrix2F(
                    1.0f, 1.0f,
                    1.0f, 1.0f);
                var expected = 0.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_12()
            {
                var matrix = new Matrix2F(
                    1.0f, 1.0f,
                    1.0f, 1.1f);
                var expected = 0.1f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual, 7);
            }

            [Fact]
            public void example_13()
            {
                var matrix = new Matrix2F(
                    0.0f, 1.0f,
                    1.0f, 2.0f);
                var expected = -1.0f;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }
        }

        public class GetProductVector2FTests : Matrix2FTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix2F.CreateIdentity();
                var original = new Vector2F(3.0f, 5.0f);
                var expected = new Vector2F(original);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void rotation_matrix_rotates_vector()
            {
                var matrix = Matrix2F.CreateRotation((float)(Math.PI / 2.0));
                var original = new Vector2F(1.0f, 2.0f);
                var expected = new Vector2F(-2.0f, 1.0f);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 6);
                Assert.Equal(expected.Y, actual.Y, 6);
            }

            [Fact]
            public void scale_matrix_scales()
            {
                var scalars = new Vector2F(1.5f, -6.3f);
                var matrix = Matrix2F.CreateScaled(scalars);
                var original = new Vector2F(2.0f, 0.9f);
                var expected = new Vector2F(scalars.X * original.X, scalars.Y * original.Y);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }
        }

        public class GetProductColumnVector2F : Matrix2FTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix2F.CreateIdentity();
                var original = new Vector2F(3.0f, 5.0f);
                var expected = new Vector2F(original);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void rotation_matrix_rotates_vector()
            {
                var matrix = Matrix2F.CreateRotation((float)(Math.PI / 2.0));
                var original = new Vector2F(1.0f, 2.0f);
                var expected = new Vector2F(-2.0f, 1.0f);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 6);
                Assert.Equal(expected.Y, actual.Y, 6);
            }

            [Fact]
            public void scale_matrix_scales()
            {
                var scalars = new Vector2F(1.5f, -6.3f);
                var matrix = Matrix2F.CreateScaled(scalars);
                var original = new Vector2F(2.0f, 0.9f);
                var expected = new Vector2F(scalars.X * original.X, scalars.Y * original.Y);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }
        }

        public class TransformVector2FTests : Matrix2FTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix2F.CreateIdentity();
                var actual = new Vector2F(3.0f, 5.0f);
                var expected = new Vector2F(actual);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void rotation_matrix_rotates_vector()
            {
                var matrix = Matrix2F.CreateRotation((float)(Math.PI / 2.0));
                var original = new Vector2F(1.0f, 2.0f);
                var expected = new Vector2F(-2.0f, 1.0f);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 6);
                Assert.Equal(expected.Y, actual.Y, 6);
            }

            [Fact]
            public void scale_matrix_scales()
            {
                var scalars = new Vector2F(1.5f, -6.3f);
                var matrix = Matrix2F.CreateScaled(scalars);
                var actual = new Vector2F(2.0f, 0.9f);
                var expected = new Vector2F(scalars.X * actual.X, scalars.Y * actual.Y);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }
        }

        protected Matrix2F CreateIncremenetalMatrix()
        {
            return new Matrix2F(1.0f, 2.0f, 3.0f, 4.0f);
        }
    }
}
