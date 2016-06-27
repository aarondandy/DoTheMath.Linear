using System;
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
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix4D((Matrix4D)null));
            }

            [Fact]
            public void copy_constructor_contains_same_element()
            {
                var expected = CreateIncremenetalMatrix();

                var actual = new Matrix4D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new Matrix4D((IMatrix<double>)null));
            }

            [Fact]
            public void copy_constructor_imatrix_throws_for_bad_size()
            {
                var source = new MatrixD(3, 6);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix4D((IMatrix<double>)source));
            }

            [Fact]
            public void copy_constructor_imatrix_contains_same_elements()
            {
                var source = new MatrixD(4, 4);
                source.Set(0, 0, 16);
                source.Set(0, 1, 1);
                source.Set(0, 2, 2);
                source.Set(0, 3, 6);
                source.Set(1, 0, 3);
                source.Set(1, 1, 4);
                source.Set(1, 2, 5);
                source.Set(1, 3, 7);
                source.Set(2, 0, 8);
                source.Set(2, 1, 9);
                source.Set(2, 2, 10);
                source.Set(2, 3, 11);
                source.Set(3, 0, 12);
                source.Set(3, 1, 13);
                source.Set(3, 2, 14);
                source.Set(3, 3, 15);
                var expected = new Matrix4D(
                    16, 1, 2, 6,
                    3, 4, 5, 7,
                    8, 9, 10, 11,
                    12, 13, 14, 15);

                var actual = new Matrix4D((IMatrix<double>)source);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Factories : Matrix4DTests
        {
            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix4D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void x_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix4D
                {
                    E00 = 1.0,
                    E11 = Math.Cos(radians),
                    E12 = Math.Sin(radians),
                    E21 = -Math.Sin(radians),
                    E22 = Math.Cos(radians),
                    E33 = 1.0
                };

                var actual = Matrix4D.CreateRotationX(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void y_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix4D
                {
                    E00 = Math.Cos(radians),
                    E02 = -Math.Sin(radians),
                    E11 = 1.0,
                    E20 = Math.Sin(radians),
                    E22 = Math.Cos(radians),
                    E33 = 1.0
                };

                var actual = Matrix4D.CreateRotationY(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void z_rotation_factory_creates_matrix()
            {
                var radians = 1.0;
                var expected = new Matrix4D
                {
                    E00 = Math.Cos(radians),
                    E01 = Math.Sin(radians),
                    E10 = -Math.Sin(radians),
                    E11 = Math.Cos(radians),
                    E22 = 1.0,
                    E33 = 1.0
                };

                var actual = Matrix4D.CreateRotationZ(radians);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_scaling_matrix3()
            {
                var factors = new Vector3D(2, -3, 4);
                var expected = Matrix4D.CreateIdentity();
                expected.E00 = factors.X;
                expected.E11 = factors.Y;
                expected.E22 = factors.Z;

                var actual = Matrix4D.CreateScaled(factors);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_scaling_matrix4()
            {
                var factors = new Vector4D(2, -3, 4, -5);
                var expected = Matrix4D.CreateIdentity();
                expected.E00 = factors.X;
                expected.E11 = factors.Y;
                expected.E22 = factors.Z;
                expected.E33 = factors.W;

                var actual = Matrix4D.CreateScaled(factors);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_create_translation()
            {
                var delta = new Vector3D(2, -3, 4.3);
                var expected = Matrix4D.CreateIdentity();
                expected.E30 = delta.X;
                expected.E31 = delta.Y;
                expected.E32 = delta.Z;

                var actual = Matrix4D.CreateTranslation(delta);

                Assert.Equal(expected, actual);
            }
        }

        public class OperatorOverloads : Matrix4DTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix4D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5, 10.9, 8.3, 1.3, -9.8, 1.13, 33.33, -78);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_addition_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix4D)null;

                Assert.Throws<ArgumentNullException>(() => matrix + @null);
                Assert.Throws<ArgumentNullException>(() => @null + matrix);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix4D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5, 10.9, 8.3, 1.3, -9.8, 1.13, 33.33, -78);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtract_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix4D)null;

                Assert.Throws<ArgumentNullException>(() => matrix - @null);
                Assert.Throws<ArgumentNullException>(() => @null - matrix);
            }

            [Fact]
            public void op_multiply_mimics_multiply_matrix()
            {
                var left = CreateIncremenetalMatrix();
                var right = new Matrix4D(4.4, 3.3, 2.2, 1.1, 5.9, 6.8, 7.7, 8.6, 9.5, 10.9, 8.3, 1.3, -9.8, 1.13, 33.33, -78);
                var expected = left.GetProduct(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_null_operand_throws()
            {
                var matrix = CreateIncremenetalMatrix();
                var @null = (Matrix4D)null;

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
                var @null = (Matrix4D)null;
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
                var @null = (Matrix4D)null;
                var divisor = 1.0;

                Assert.Throws<ArgumentNullException>(() => @null * divisor);
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

        public class IndexerGet : Matrix4DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(1.0d, m[0, 0]);
                Assert.Equal(-5.0, m[0, 1]);
                Assert.Equal(9.0d, m[0, 2]);
                Assert.Equal(0.1d, m[0, 3]);
                Assert.Equal(-1.0, m[1, 0]);
                Assert.Equal(8.0d, m[1, 1]);
                Assert.Equal(-4.0, m[1, 2]);
                Assert.Equal(-0.9, m[1, 3]);
                Assert.Equal(21.0, m[2, 0]);
                Assert.Equal(-0.5d, m[2, 1]);
                Assert.Equal(1.4d, m[2, 2]);
                Assert.Equal(-9.9d, m[2, 3]);
                Assert.Equal(-101.9, m[3, 0]);
                Assert.Equal(5.0, m[3, 1]);
                Assert.Equal(-17.0, m[3, 2]);
                Assert.Equal(19.3, m[3, 3]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[4, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0]);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0]);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 4]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue]);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue]);
            }
        }

        public class IndexerSet : Matrix4DTests
        {
            [Fact]
            public void can_set_all_elements()
            {
                var m = new Matrix4D();

                m[0, 0] = 1.0;
                m[0, 1] = -5.0;
                m[0, 2] = 9.0;
                m[0, 3] = 0.1;
                m[1, 0] = -1.0;
                m[1, 1] = 8.0;
                m[1, 2] = -4.0;
                m[1, 3] = -0.9;
                m[2, 0] = 21.0;
                m[2, 1] = -0.5;
                m[2, 2] = 1.4;
                m[2, 3] = -9.9;
                m[3, 0] = -101.9;
                m[3, 1] = 5.0;
                m[3, 2] = -17.0;
                m[3, 3] = 19.3;

                Assert.Equal(1.0d, m[0, 0]);
                Assert.Equal(-5.0, m[0, 1]);
                Assert.Equal(9.0d, m[0, 2]);
                Assert.Equal(0.1d, m[0, 3]);
                Assert.Equal(-1.0, m[1, 0]);
                Assert.Equal(8.0d, m[1, 1]);
                Assert.Equal(-4.0, m[1, 2]);
                Assert.Equal(-0.9, m[1, 3]);
                Assert.Equal(21.0, m[2, 0]);
                Assert.Equal(-0.5d, m[2, 1]);
                Assert.Equal(1.4d, m[2, 2]);
                Assert.Equal(-9.9d, m[2, 3]);
                Assert.Equal(-101.9, m[3, 0]);
                Assert.Equal(5.0, m[3, 1]);
                Assert.Equal(-17.0, m[3, 2]);
                Assert.Equal(19.3, m[3, 3]);
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<IndexOutOfRangeException>(() => m[-1, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[4, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MinValue, 0] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[int.MaxValue, 0] = 0);
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<IndexOutOfRangeException>(() => m[0, -1] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, 4] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MinValue] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => m[0, int.MaxValue] = 0);
            }
        }

        public class IEquatable_Self_Equals : Matrix4DTests
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
                var b = new Matrix4D(4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void matrix_does_not_equal_null()
            {
                var m = CreateIncremenetalMatrix();

                Assert.False(m.Equals((Matrix4D)null));
            }
        }

        public class Object_Equals : Matrix4DTests
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
                var b = new Matrix4D(4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11);

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

        public class GetHashCodeTests : Matrix4DTests
        {
            [Fact]
            public void same_matrix_reference_has_same_hashcode_when_changed()
            {
                var actual = CreateIncremenetalMatrix();
                var expectedHashCode = actual.GetHashCode();

                actual.E00 = 4;
                actual.E11 = 9;
                actual.E20 = 15.0;
                actual.E13 = -9.0;

                Assert.Equal(expectedHashCode, actual.GetHashCode());
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
                var expected = CreateIncremenetalMatrix();
                var actual = CreateIncremenetalMatrix();

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
            [InlineData(3)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();
                Assert.NotSame(expected, actual);

                actual.SwapRows(row, row);

                Assert.Equal(expected, actual);
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
                var expected = CreateIncremenetalMatrix();
                var actual = CreateIncremenetalMatrix();

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
            [InlineData(3)]
            public void swapping_same_rows_does_nothing(int row)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = CreateIncremenetalMatrix();

                actual.SwapColumns(row, row);

                Assert.Equal(expected, actual);
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
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    10, 20, 30, 40,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    13, 14, 15, 16);

                actual.ScaleRow(0, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    50, 60, 70, 80,
                    9, 10, 11, 12,
                    13, 14, 15, 16);

                actual.ScaleRow(1, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_third_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    90, 100, 110, 120,
                    13, 14, 15, 16);

                actual.ScaleRow(2, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_fourth_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    130, 140, 150, 160);

                actual.ScaleRow(3, 10);

                Assert.Equal(expected, actual);
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
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    10, 2, 3, 4,
                    50, 6, 7, 8,
                    90, 10, 11, 12,
                    130, 14, 15, 16);

                actual.ScaleColumn(0, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 20, 3, 4,
                    5, 60, 7, 8,
                    9, 100, 11, 12,
                    13, 140, 15, 16);

                actual.ScaleColumn(1, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_third_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 30, 4,
                    5, 6, 70, 8,
                    9, 10, 110, 12,
                    13, 14, 150, 16);

                actual.ScaleColumn(2, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_fourth_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 40,
                    5, 6, 7, 80,
                    9, 10, 11, 120,
                    13, 14, 15, 160);

                actual.ScaleColumn(3, 10);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideRow : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(4, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideRow(20, 1));
            }

            [Fact]
            public void can_scale_first_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    0.1, 0.2, 0.3, 0.4,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    13, 14, 15, 16);

                actual.DivideRow(0, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_second_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    0.5, 0.6, 0.7, 0.8,
                    9, 10, 11, 12,
                    13, 14, 15, 16);

                actual.DivideRow(1, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_third_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    0.9, 1.0, 1.1, 1.2,
                    13, 14, 15, 16);

                actual.DivideRow(2, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_fourth_row()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    1.3, 1.4, 1.5, 1.6);

                actual.DivideRow(3, 10);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideColumn : Matrix4DTests
        {
            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-1, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(-100, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(4, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.DivideColumn(20, 1));
            }

            [Fact]
            public void can_scale_first_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    0.1, 2, 3, 4,
                    0.5, 6, 7, 8,
                    0.9, 10, 11, 12,
                    1.3, 14, 15, 16);

                actual.DivideColumn(0, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_second_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 0.2, 3, 4,
                    5, 0.6, 7, 8,
                    9, 1.0, 11, 12,
                    13, 1.4, 15, 16);

                actual.DivideColumn(1, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_third_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 0.3, 4,
                    5, 6, 0.7, 8,
                    9, 10, 1.1, 12,
                    13, 14, 1.5, 16);

                actual.DivideColumn(2, 10);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_scale_fourth_column()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 2, 3, 0.4,
                    5, 6, 7, 0.8,
                    9, 10, 11, 1.2,
                    13, 14, 15, 1.6);

                actual.DivideColumn(3, 10);

                Assert.Equal(expected, actual);
            }
        }

        public class AddRow : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(0, 3)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(1, 3)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            [InlineData(2, 3)]
            [InlineData(3, 0)]
            [InlineData(3, 1)]
            [InlineData(3, 2)]
            [InlineData(3, 3)]
            public void can_add_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) + value);
                }

                actual.AddRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractRow : Matrix4DTests
        {
            [Fact]
            public void invalid_rows_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractRow(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(0, 3)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(1, 3)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            [InlineData(2, 3)]
            [InlineData(3, 0)]
            [InlineData(3, 1)]
            [InlineData(3, 2)]
            [InlineData(3, 3)]
            public void can_subtract_row(int sourceRow, int targetRow)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(actual);

                for (int c = 0; c < actual.Columns; c++)
                {
                    var value = expected.Get(sourceRow, c);
                    expected.Set(targetRow, c, expected.Get(targetRow, c) - value);
                }

                actual.SubtractRow(sourceRow, targetRow);

                Assert.Equal(expected, actual);
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
                var actual = CreateIncremenetalMatrix();
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

        public class AddColumn : Matrix4DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.AddColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(0, 3)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(1, 3)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            [InlineData(2, 3)]
            [InlineData(3, 0)]
            [InlineData(3, 1)]
            [InlineData(3, 2)]
            [InlineData(3, 3)]
            public void can_add_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) + value);
                }

                actual.AddColumn(sourceColumn, targetColumn);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractColumn : Matrix4DTests
        {
            [Fact]
            public void invalid_columns_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(99, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.SubtractColumn(0, 99));
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(0, 1)]
            [InlineData(0, 2)]
            [InlineData(0, 3)]
            [InlineData(1, 0)]
            [InlineData(1, 1)]
            [InlineData(1, 2)]
            [InlineData(1, 3)]
            [InlineData(2, 0)]
            [InlineData(2, 1)]
            [InlineData(2, 2)]
            [InlineData(2, 3)]
            [InlineData(3, 0)]
            [InlineData(3, 1)]
            [InlineData(3, 2)]
            [InlineData(3, 3)]
            public void can_subtract_column(int sourceColumn, int targetColumn)
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(actual);

                for (int r = 0; r < actual.Rows; r++)
                {
                    var value = expected.Get(r, sourceColumn);
                    expected.Set(r, targetColumn, expected.Get(r, targetColumn) - value);
                }

                actual.SubtractColumn(sourceColumn, targetColumn);

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
                var actual = CreateIncremenetalMatrix();
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

        public class AddMatrix : Matrix4DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentNullException>(() => m.GetSum((Matrix4D)null));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix4D(
                    4.4, 3.3, 2.2, 1.1,
                    -0.1, 1.2, 2.3, 4.6,
                    7.7, 1.9, -9, 0,
                    -5, -8, 6.6, 4);
                var expected = new Matrix4D(
                    5.4, 5.3, 5.2, 5.1,
                    4.9, 7.2, 9.3, 12.6,
                    16.7, 11.9, 2, 12,
                    8, 6, 21.6, 20);

                var actual = a.GetSum(b);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractMatrix : Matrix4DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentNullException>(() => m.GetSum((Matrix4D)null));
            }

            [Fact]
            public void can_add_all_elements()
            {
                var a = CreateIncremenetalMatrix();
                var b = new Matrix4D(
                    4.4, 3.3, 2.2, 1.1,
                    -0.1, 1.2, 2.3, 4.6,
                    7.7, 1.9, -9, 0,
                    -5, -8, 6.6, 4);
                var expected = new Matrix4D(
                    1 - 4.4, 2 - 3.3, 3 - 2.2, 4 - 1.1,
                    5 + 0.1, 6 - 1.2, 7 - 2.3, 8 - 4.6,
                    9 - 7.7, 10 - 1.9, 11 + 9, 12 - 0,
                    13 + 5, 14 + 8, 15 - 6.6, 16 - 4);

                var actual = a.GetDifference(b);

                Assert.Equal(expected, actual);
            }
        }

        public class MultiplyScalar : Matrix4DTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, -7, 8,
                    -9, 10, 11, 12,
                    13, 14, 15, 16);
                var expected = new Matrix4D(
                    1.1, 2 * 1.1, 3 * 1.1, 4 * 1.1,
                    5 * 1.1, 6 * 1.1, -7 * 1.1, 8 * 1.1,
                    -9 * 1.1, 10 * 1.1, 11 * 1.1, 12 * 1.1,
                    13 * 1.1, 14 * 1.1, 15 * 1.1, 16 * 1.1);

                var actual = a.GetScaled(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideScalar : Matrix4DTests
        {
            [Fact]
            public void all_elements_get_scaled()
            {
                var a = new Matrix4D(
                    1, 2, 3, 4,
                    5, 6, -7, 8,
                    -9, 10, 11, 12,
                    13, 14, 15, 16);
                var expected = new Matrix4D(
                    1 / 1.1, 2 / 1.1, 3 / 1.1, 4 / 1.1,
                    5 / 1.1, 6 / 1.1, -7 / 1.1, 8 / 1.1,
                    -9 / 1.1, 10 / 1.1, 11 / 1.1, 12 / 1.1,
                    13 / 1.1, 14 / 1.1, 15 / 1.1, 16 / 1.1);

                var actual = a.GetQuotient(1.1);

                Assert.Equal(expected, actual);
            }
        }

        public class Multiply : Matrix4DTests
        {
            [Fact]
            public void null_matrix_throws()
            {
                var sut = CreateIncremenetalMatrix();

                Assert.Throws<ArgumentNullException>(() => sut.GetProduct((Matrix4D)null));
            }

            [Fact]
            public void can_multiply_same_size_matrix()
            {
                var a = new Matrix4D(
                    3, 1, 2, -2,
                    -10, -2, 4, 0,
                    -9, 5, -3, 500,
                    1, 0, 10, -2);
                var b = new Matrix4D(
                    2, 3, 90, 31,
                    -4, 5, 7, 0,
                    13, -2, -1, 0,
                    2, 0, 1, -10);
                var expected = new Matrix4D(
                    24, 10, 273, 113,
                    40, -48, -918, -310,
                    923, 4, -272, -5279,
                    128, -17, 78, 51);

                var actual = a.GetProduct(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void multiplying_with_identity_results_in_same_matrix()
            {
                var a = new Matrix4D(3, 1, -10, -2, 5, 6, 7, 1, 99.9, 0, 1.1, 1.2, 3.0, 3.1, 3.2, 3.3);
                var identity = Matrix4D.CreateIdentity();
                Assert.NotEqual(identity, a);

                Assert.Equal(a, a.GetProduct(identity));
                Assert.Equal(a, identity.GetProduct(a));
            }
        }

        public class Transposed : Matrix4DTests
        {
            [Fact]
            public void can_transpose()
            {
                var source = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 5, 9, 13,
                    2, 6, 10, 14,
                    3, 7, 11, 15,
                    4, 8, 12, 16);

                var actual = source.GetTranspose();

                Assert.Equal(expected, actual);
            }
        }

        public class Transpose : Matrix4DTests
        {
            [Fact]
            public void can_transpose()
            {
                var actual = CreateIncremenetalMatrix();
                var expected = new Matrix4D(
                    1, 5, 9, 13,
                    2, 6, 10, 14,
                    3, 7, 11, 15,
                    4, 8, 12, 16);

                actual.Transpose();

                Assert.Equal(expected, actual);
            }
        }

        public class IsSymmetric : Matrix4DTests
        {
            [Fact]
            public void symetric_matrix_is_symetric()
            {
                var matrix = new Matrix4D(
                    9, 4, 3, 2,
                    4, 15, 1, 6,
                    3, 1, 99, 14,
                    2, 6, 14, -2);

                Assert.True(matrix.IsSymetric);
            }

            [Fact]
            public void incremental_values_are_not_symetric()
            {
                var matrix = CreateIncremenetalMatrix();

                Assert.False(matrix.IsSymetric);
            }
        }

        public class Trace : Matrix4DTests
        {
            [Fact]
            public void can_get_trace()
            {
                var matrix = new Matrix4D(
                    9, 4, 3, 2,
                    4, 15, 1, 6,
                    3, 1, 99, 14,
                    2, 6, 14, -2);
                var expected = 9 + 15 + 99 - 2;

                var actual = matrix.Trace;

                Assert.Equal(expected, actual);
            }
        }

        public class GetInverse : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_returns_self_for_inverse()
            {
                var matrix = Matrix4D.CreateIdentity();
                var expected = Matrix4D.CreateIdentity();

                var actual = matrix.GetInverse();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void zero_matrix_has_no_inverse()
            {
                var matrix = new Matrix4D();

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void all_one_matrix_has_no_inverse()
            {
                var matrix = new Matrix4D(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

                Assert.Throws<NoInverseException>(() => matrix.GetInverse());
            }

            [Fact]
            public void matrix_is_not_mutated_by_inverse()
            {
                var actual = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);
                var expected = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);

                var result = actual.GetInverse();

                Assert.NotEqual(result, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void invert_can_round_trip()
            {
                var matrix = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);
                var expected = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);

                var inverse = matrix.GetInverse();
                var actual = inverse.GetInverse();

                Assert.NotEqual(expected, inverse);
                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }

            }

            [Fact]
            public void can_invert_example_0()
            {
                var matrix = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);

                var expected = new Matrix4D(
                    17.85, -7.7, -4.55, 2.5,
                    -14.6, 6.2, 3.8, -2,
                    4.35, -1.7, -1.05, 0.5,
                    -0.7, 0.4, 0.1, 0);

                var actual = matrix.GetInverse();

                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class TryGetInverse : Matrix4DTests
        {
            [Fact]
            public void failure_does_not_mutate_matrix()
            {
                var actual = new Matrix4D(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix4D(actual);

                Matrix4D inverse;
                var result = actual.TryGetInverse(out inverse);

                Assert.NotNull(inverse);
                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_inverse()
            {
                var matrix = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);
                var expected = new Matrix4D(
                    17.85, -7.7, -4.55, 2.5,
                    -14.6, 6.2, 3.8, -2,
                    4.35, -1.7, -1.05, 0.5,
                    -0.7, 0.4, 0.1, 0);

                Matrix4D actual;
                var result = matrix.TryGetInverse(out actual);

                Assert.True(result);

                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class Invert : Matrix4DTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix4D(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix4D(actual);

                Action act = () => actual.Invert();

                Assert.Throws<NoInverseException>(act);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);
                var expected = new Matrix4D(
                    17.85, -7.7, -4.55, 2.5,
                    -14.6, 6.2, 3.8, -2,
                    4.35, -1.7, -1.05, 0.5,
                    -0.7, 0.4, 0.1, 0);

                actual.Invert();

                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class TryInvert : Matrix4DTests
        {
            [Fact]
            public void failure_does_not_mutate()
            {
                var actual = new Matrix4D(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
                var expected = new Matrix4D(actual);

                var result = actual.TryInvert();

                Assert.False(result);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_invert()
            {
                var actual = new Matrix4D(
                    1, 2, 3, 1,
                    0, 1, 4, 4,
                    7, 10, 5, 1,
                    6, 7, 0, 7);
                var expected = new Matrix4D(
                    17.85, -7.7, -4.55, 2.5,
                    -14.6, 6.2, 3.8, -2,
                    4.35, -1.7, -1.05, 0.5,
                    -0.7, 0.4, 0.1, 0);

                var result = actual.TryInvert();

                Assert.True(result);

                for (int r = 0; r < expected.Rows; r++)
                {
                    for (int c = 0; c < expected.Columns; c++)
                    {
                        Assert.Equal(expected.Get(r, c), actual.Get(r, c), 10);
                    }
                }
            }
        }

        public class GetDeterminant : Matrix4DTests
        {
            [Fact]
            public void example_0()
            {
                var matrix = new Matrix4D(
                    2, 1, 0, 4,
                    0, -1, 0, 2,
                    7, -2, 3, 5,
                    0, 1, 0, -3);
                var expected = 6;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var matrix = new Matrix4D(
                    1, 2, 3, 4,
                    12, 13, 14, 5,
                    11, 16, 15, 6,
                    10, 9, 8, 7);
                var expected = 660;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_2()
            {
                var matrix = new Matrix4D(
                    1, 5, 9, 13,
                    2, 6, 10, 14,
                    3, 7, 11, 15,
                    4, 8, 12, 16);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_3()
            {
                var matrix = new Matrix4D(1, -1, 0, 2, -1, 1, 2, 3, 2, -2, 3, 4, 6, -6, 6, 1);
                var expected = 0;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_4()
            {
                var matrix = new Matrix4D(1, 0, 2, 1, 2, -1, 1, 0, 1, 0, 0, 3, -1, 0, 2, 1);
                var expected = 12;

                var actual = matrix.GetDeterminant();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_5()
            {
                var matrix = new Matrix4D
                {
                    [0, 3] = 0.5773508360503944,
                    [1, 1] = 0.32610490979911055,
                    [1, 2] = 0.6966490217934591,
                    [2, 0] = 0.11025381745316731,
                    [2, 2] = 0.015784395400334335,
                    [3, 0] = 0.8044707895277398,
                    [3, 1] = 0.329659461662946,
                    [3, 2] = 0.7691706343410399,
                    [3, 3] = 0.9036252377105994
                };
                var expected = -0.0010429623620855184;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_6()
            {
                var matrix = new Matrix4D
                {
                    [0, 0] = 0.8044707895277398,
                    [0, 1] = 0.329659461662946,
                    [0, 2] = 0.7691706343410399,
                    [0, 3] = 0.9036252377105994,

                    [1, 1] = 0.32610490979911055,
                    [1, 2] = 0.6966490217934591,

                    [2, 0] = 0.11025381745316731,
                    [2, 2] = 0.015784395400334335,

                    [3, 3] = 0.5773508360503944
                };
                var expected = 0.0010429623620855184;

                var actual = matrix.GetDeterminant();

                Assert.Equal(expected, actual, 10);
            }
        }

        public class GetProductVector3DTests : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var original = new Vector3D(3, 5, -2);
                var expected = new Vector3D(original);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector3D(2, 3, 1.3);
                var matrix = Matrix4D.CreateTranslation(delta);
                var original = new Vector3D(1.1, 2.2, -4.4);
                var expected = original + delta;

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector4D(3, 4, 5, 6);
                var matrix = Matrix4D.CreateScaled(scalars);
                var origin = new Vector3D(1.1, 2.2, 3.3);
                var expected = new Vector3D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z);

                var actual = matrix.GetProduct(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        public class GetProductVector4DTets : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var original = new Vector4D(3, 5, -9, 2);
                var expected = new Vector4D(original);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector4D(3, 4, 5, 6);
                var matrix = Matrix4D.CreateScaled(scalars);
                var origin = new Vector4D(1.1, 2.2, -4.4, -5.5);
                var expected = new Vector4D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z, origin.W * scalars.W);

                var actual = matrix.GetProduct(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector4D(2.2, 1, 1, 5);
                var expected = new Vector4D(2.2, -1, 1, 5);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
                Assert.Equal(expected.W, actual.W);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var original = new Vector4D(1, 4.5, 1, 6);
                var expected = new Vector4D(1, 4.5, -1, 6);
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
                Assert.Equal(expected.W, actual.W);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var original = new Vector4D(1, 1, 3.2, 7);
                var expected = new Vector4D(-1, 1, 3.2, 7);
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                var actual = matrix.GetProduct(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
                Assert.Equal(expected.W, actual.W);
            }
        }

        public class GetProductColumnVector3DTests : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var original = new Vector3D(3, 5, -9);
                var expected = new Vector3D(original);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector3D(2, 3, 4);
                var matrix = Matrix4D.CreateTranslation(delta);
                var original = new Vector3D(1.1, 2.2, 3.3);
                var expected = new Vector3D(original) + delta;

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }

            [Fact]
            public void can_apply_scale3_transform()
            {
                var scalars = new Vector3D(3, 4, 5);
                var matrix = Matrix4D.CreateScaled(scalars);
                var origin = new Vector3D(1.1, 2.2, -3.3);
                var expected = new Vector3D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z);

                var actual = matrix.GetProductColumnVector(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale4_transform()
            {
                var scalars = new Vector4D(3, 4, 5, 6);
                var matrix = Matrix4D.CreateScaled(scalars);
                var origin = new Vector3D(1.1, 2.2, -3.3);
                var expected = new Vector3D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z);

                var actual = matrix.GetProductColumnVector(origin);

                Assert.Equal(expected, actual);
            }
        }

        public class GetProductColumnVector4DTests : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var original = new Vector4D(3, 5, -9, -11);
                var expected = new Vector4D(original);

                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector4D(3, 4, 5, -11);
                var matrix = Matrix4D.CreateScaled(scalars);
                var origin = new Vector4D(1.1, 2.2, -4.4, 90);
                var expected = new Vector4D(origin.X * scalars.X, origin.Y * scalars.Y, origin.Z * scalars.Z, origin.W * scalars.W);

                var actual = matrix.GetProductColumnVector(origin);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var original = new Vector4D(2.2, 1, 1, 6);
                var expected = new Vector4D(2.2, -1, 1, 6);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
                Assert.Equal(expected.W, actual.W);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var original = new Vector4D(1, 4.5, 1, -9);
                var expected = new Vector4D(1, 4.5, -1, -9);
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
                Assert.Equal(expected.W, actual.W);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var original = new Vector4D(1, 1, 3.2, 2.2);
                var expected = new Vector4D(-1, 1, 3.2, 2.2);
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transpose();
                var actual = matrix.GetProductColumnVector(original);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
                Assert.Equal(expected.W, actual.W);
            }
        }

        public class TransformVector3DTests : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var actual = new Vector3D(3, 5, -2);
                var expected = new Vector3D(actual);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_translation()
            {
                var delta = new Vector3D(2, 3, -5);
                var matrix = Matrix4D.CreateTranslation(delta);
                var actual = new Vector3D(1.1, 2.2, 9.9);
                var expected = actual + delta;

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector4D(3, 4, 5, -2);
                var matrix = Matrix4D.CreateScaled(scalars);
                var actual = new Vector3D(1.1, 2.2, 3.3);
                var expected = new Vector3D(actual.X * scalars.X, actual.Y * scalars.Y, actual.Z * scalars.Z);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var actual = new Vector3D(2.2, 1, 1);
                var expected = new Vector3D(2.2, -1, 1);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

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
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        public class TransformVector4DTets : Matrix4DTests
        {
            [Fact]
            public void identity_matrix_leaves_values_unchanged()
            {
                var matrix = Matrix4D.CreateIdentity();
                var actual = new Vector4D(3, 5, -9, 2.2);
                var expected = new Vector4D(actual);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_scale_transform()
            {
                var scalars = new Vector4D(3, 4, 5, -9.9);
                var matrix = Matrix4D.CreateScaled(scalars);
                var actual = new Vector4D(1.1, 2.2, -4.4, 1.3);
                var expected = new Vector4D(actual.X * scalars.X, actual.Y * scalars.Y, actual.Z * scalars.Z, actual.W * scalars.W);

                matrix.Transform(ref actual);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_apply_x_rotation()
            {
                var actual = new Vector4D(2.2, 1, 1, 7.8);
                var expected = new Vector4D(2.2, -1, 1, 7.8);
                var matrix = Matrix4D.CreateRotationX(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_y_rotation()
            {
                var actual = new Vector4D(1, 4.5, 1, -3.4);
                var expected = new Vector4D(1, 4.5, -1, -3.4);
                var matrix = Matrix4D.CreateRotationY(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y);
                Assert.Equal(expected.Z, actual.Z, 10);
            }

            [Fact]
            public void can_apply_z_rotation()
            {
                var actual = new Vector4D(1, 1, 3.2, -9.1);
                var expected = new Vector4D(-1, 1, 3.2, -9.1);
                var matrix = Matrix4D.CreateRotationZ(Math.PI / 2.0);

                matrix.Transform(ref actual);

                Assert.Equal(expected.X, actual.X, 10);
                Assert.Equal(expected.Y, actual.Y, 10);
                Assert.Equal(expected.Z, actual.Z);
            }
        }

        protected Matrix4D CreateIncremenetalMatrix()
        {
            return new Matrix4D(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
        }
    }
}
