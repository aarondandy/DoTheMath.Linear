using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector4DTests
    {
        public class Constructors : Vector4DTests
        {
            [Fact]
            public void default_constructor_sets_components_to_zero()
            {
                var v = new Vector4D();

                Assert.Equal(0.0d, v.X);
                Assert.Equal(0.0d, v.Y);
                Assert.Equal(0.0d, v.Z);
                Assert.Equal(0.0d, v.W);
                Assert.Equal(4, v.Dimensions);
            }

            [Fact]
            public void component_constructor_assigns_correct_fields()
            {
                var v = new Vector4D(1.0, -5.0, 6.0, -2.0);

                Assert.Equal(1.0, v.X);
                Assert.Equal(-5.0, v.Y);
                Assert.Equal(6.0, v.Z);
                Assert.Equal(-2.0, v.W);
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new Vector4D(1, 2, 3, 4);

                var actual = new Vector4D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector()
            {
                var expected = new Vector4D(1, 2, 3, 4);

                var actual = new Vector4D((IVector<double>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_ivector_of_wrong_size()
            {
                var source = new VectorD(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Vector4D((IVector<double>)source));
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector4D((IVector<double>)null));
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector4()
            {
                var expected = new Vector4D(1, 2, 3, 4);

                var actual = new Vector4D((IVector4<double>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector4()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector4D((IVector4<double>)null));
            }
        }

        public class Factories : Vector4DTests
        {
            [Fact]
            public void create_x_unit()
            {
                var expected = new Vector4D(1, 0, 0, 0);

                var actual = Vector4D.CreateXUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_y_unit()
            {
                var expected = new Vector4D(0, 1, 0, 0);

                var actual = Vector4D.CreateYUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_z_unit()
            {
                var expected = new Vector4D(0, 0, 1, 0);

                var actual = Vector4D.CreateZUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_w_unit()
            {
                var expected = new Vector4D(0, 0, 0, 1);

                var actual = Vector4D.CreateWUnit();

                Assert.Equal(expected, actual);
            }
        }

        public class OperatorOverloads : Vector4DTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = new Vector4D(1, 2, 4, 5);
                var right = new Vector4D(-3, -100, 40, 52);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = new Vector4D(1, 2, 4, 5);
                var right = new Vector4D(-3, -100, 40, 52);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_mimics_dot()
            {
                var left = new Vector4D(1, 2, 4, 5);
                var right = new Vector4D(-3, -100, 40, 52);
                var expected = left.GetDot(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale()
            {
                var vector = new Vector4D(1, 2, 3, 4);
                var scalar = 2.4;
                var expected = vector.GetScaled(scalar);

                var actual = vector * scalar;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale_first()
            {
                var vector = new Vector4D(1, 2, 3, 4);
                var scalar = 2.4;
                var expected = vector.GetScaled(scalar);

                var actual = scalar * vector;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_divide()
            {
                var vector = new Vector4D(1, 2, 3, 4);
                var divisor = 2.4;
                var expected = vector.GetQuotient(divisor);

                var actual = vector / divisor;

                Assert.Equal(expected, actual);
            }
        }

        public class Get : Vector4DTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector4D(-1.0, 5.0, -0.5, 0.1);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
                Assert.Equal(-0.5, v.Get(2));
                Assert.Equal(0.1, v.Get(3));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(4));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : Vector4DTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector4D(1, 2, 3, 4);

                v.Set(0, -1.0);
                v.Set(1, 5.0);
                v.Set(2, -0.5);
                v.Set(3, 0.1);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
                Assert.Equal(-0.5, v.Get(2));
                Assert.Equal(0.1, v.Get(3));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0));
            }
        }

        public class IndexerGet : Vector4DTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector4D(-1.0, 5.0, -0.5, 0.1);

                Assert.Equal(-1.0, v[0]);
                Assert.Equal(5.0, v[1]);
                Assert.Equal(-0.5, v[2]);
                Assert.Equal(0.1, v[3]);
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue]);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<IndexOutOfRangeException>(() => v[4]);
                Assert.Throws<IndexOutOfRangeException>(() => v[99]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue]);
            }
        }

        public class IndexerSet : Vector4DTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector4D(1, 2, 3, 4);

                v[0] = -1.0;
                v[1] = 5.0;
                v[2] = -0.5;
                v[3] = 0.1;

                Assert.Equal(-1.0, v[0]);
                Assert.Equal(5.0, v[1]);
                Assert.Equal(-0.5, v[2]);
                Assert.Equal(0.1, v[3]);
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue] = 0);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector4D();

                Assert.Throws<IndexOutOfRangeException>(() => v[4] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue] = 0);
            }
        }

        public class IEquatable_Self_Equals : Vector4DTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector4D(1.0, 2.0, 3.0, 4.0);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector4D(5.0, -1.1, 99.9, -8.5);
                var b = new Vector4D(5.0, -1.1, 99.9, -8.5);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector4D(5.0, -1.1, 0.0, 78.1);
                var b = new Vector4D(-1.1, 5.0, 5.0, 78.1);
                var c = new Vector4D(5.0, -1.1, 0.0, 78.2);
                var d = new Vector4D(-1.1, -1.1, -1.1, -1.1);
                var e = new Vector4D();

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
                Assert.False(a.Equals(c));
                Assert.False(c.Equals(a));
                Assert.False(a.Equals(d));
                Assert.False(d.Equals(a));
                Assert.False(a.Equals(e));
                Assert.False(e.Equals(a));
            }
        }

        public class Object_Equals : Vector4DTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector4D(-1.0, 5.0, -0.5, 0.1);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void self_not_equal_to_null()
            {
                var v = new Vector4D(-1.0, 5.0, -0.5, 0.1);

                Assert.False(v.Equals((object)null));
            }

            [Fact]
            public void self_not_equal_to_unknown_type()
            {
                var v = new Vector4D(-1.0, 5.0, -0.5, 0.1);

                Assert.False(v.Equals((object)"not-a-matrix"));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector4D(5.0, -1.1, 99.9, -8.5);
                var b = new Vector4D(5.0, -1.1, 99.9, -8.5);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector4D(5.0, -1.1, 0.0, 78.1);
                var b = new Vector4D(-1.1, 5.0, 5.0, 78.1);
                var c = new Vector4D(5.0, -1.1, 0.0, 78.2);
                var d = new Vector4D(-1.1, -1.1, -1.1, -1.1);
                var e = new Vector4D();

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
                Assert.False(a.Equals((object)c));
                Assert.False(c.Equals((object)a));
                Assert.False(a.Equals((object)d));
                Assert.False(d.Equals((object)a));
                Assert.False(a.Equals((object)e));
                Assert.False(e.Equals((object)a));
            }
        }

        public class GetHashCodeTests : Vector4DTests
        {
            [Fact]
            public void same_vector_has_same_hashcode_when_mutated()
            {
                var m = new Vector4D(1.0, 2.0, 3.0, 4.0);
                var expectedHashCode = m.GetHashCode();
                m.X = -1.1;
                m.Y = 100.2;
                m.Z = 0.1;
                m.W = -2.2222;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class AddTests : Vector4DTests
        {
            [Fact]
            public void adding_vector_produces_sum_vector()
            {
                var left = new Vector4D(1, 2, 10, -4);
                var right = new Vector4D(3, 4, 11, 1);
                var expected = new Vector4D(4, 6, 21, -3);

                var actual = left.GetSum(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_operands_unchanged()
            {
                var left = new Vector4D(1, 2, 10, -4);
                var expectedLeft = new Vector4D(left);
                var right = new Vector4D(3, 4, 11, 1);
                var expectedRight = new Vector4D(right);

                var result = left.GetSum(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class AddToTests : Vector4DTests
        {
            [Fact]
            public void adding_vector_adds_to_components()
            {
                var actual = new Vector4D(1, 2, 10, -4);
                var right = new Vector4D(3, 4, 11, 1);
                var expected = new Vector4D(4, 6, 21, -3);

                actual.Add(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_right_unchanged()
            {
                var left = new Vector4D(1, 2, 10, -4);
                var right = new Vector4D(3, 4, 11, 1);
                var expectedRight = new Vector4D(right);

                left.Add(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractTests : Vector4DTests
        {
            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new Vector4D(1, 15, 5, 3);
                var right = new Vector4D(3, 2, 10, 1);
                var expected = new Vector4D(-2, 13, -5, 2);

                var actual = left.GetDifference(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new Vector4D(1, 2, 3, 4);
                var expectedLeft = new Vector4D(left);
                var right = new Vector4D(3, 4, 5, 6);
                var expectedRight = new Vector4D(right);

                var result = left.GetDifference(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : Vector4DTests
        {
            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new Vector4D(1, 15, 5, 3);
                var right = new Vector4D(3, 4, 10, 1);
                var expected = new Vector4D(-2, 11, -5, 2);

                actual.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new Vector4D(1, 2, 3, 4);
                var right = new Vector4D(3, 4, 5, 6);
                var expectedRight = new Vector4D(right);

                left.Subtract(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : Vector4DTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new Vector4D(1, -2, 4, -9);
                var expected = new Vector4D(3, -6, 12, -27);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector4D(1, 2, 4, -9);
                var expected = new Vector4D(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : Vector4DTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new Vector4D(1, -2, 4, -9);
                var expected = new Vector4D(3, -6, 12, -27);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : Vector4DTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new Vector4D(3, -2, 8, 1.5);
                var expected = new Vector4D(3.0 / 4.0, -2 / 4.0, 8 / 4.0, 1.5 / 4.0);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector4D(1, 2, 3, 4);
                var expected = new Vector4D(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : Vector4DTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new Vector4D(3, -2, 8, 1.5);
                var expected = new Vector4D(3.0 / 4.0, -2 / 4.0, 8 / 4.0, 1.5 / 4.0);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }

        public class GetNegative : Vector4DTests
        {
            [Fact]
            public void can_get_negative_vector()
            {
                var source = new Vector4D(-2, 5, -12, 3);
                var expected = new Vector4D(2, -5, 12, -3);

                var actual = source.GetNegative();

                Assert.Equal(expected, actual);
            }
        }

        public class Negate : Vector4DTests
        {
            [Fact]
            public void can_negate()
            {
                var actual = new Vector4D(9, -3, 17, -6);
                var expected = new Vector4D(-9, 3, -17, 6);

                actual.Negate();

                Assert.Equal(expected, actual);
            }
        }

        public class Dot : VectorDTests
        {
            [Fact]
            public void can_get_dot()
            {
                var left = new Vector4D(9, 1.2, 3.5, 0.1);
                var right = new Vector4D(-0.3, 3, -0.9, 2.1);
                var expected = (9 * -0.3) + (1.2 * 3) + (3.5 * -0.9) + (0.1 * 2.1);

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitude : Vector4DTests
        {
            [Fact]
            public void can_get_magnitude()
            {
                var vector = new Vector4D(3, 4, 5, 6);
                var expected = Math.Sqrt(86.0);

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude()
            {
                var vector = new Vector4D(0.1, 2, 3, -4);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitudeSquared : Vector4DTests
        {
            [Fact]
            public void can_get_magnitude_squared()
            {
                var vector = new Vector4D(3, 4, 5, 6);
                var expected = 86.0;

                var actual = vector.GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude_squared()
            {
                var vector = new Vector4D(0.1, 2, 3, -4);
                var expected = vector.GetMagnitudeSquared();

                var actual = vector.GetNegative().GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }
        }

        public class GetAngleBetween : Vector4DTests
        {
            [Fact]
            public void self_angle_is_zero()
            {
                var vector = new Vector4D(10, 57, -9, 10);
                var expected = 0.0;

                var actual = vector.GetAngleBetween(vector);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void right_angle_is_half_pi()
            {
                var left = new Vector4D(1, 0, 1, 0);
                var right = new Vector4D(0, 1, 0, 1);
                var expected = Math.PI / 2.0;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void opposite_vector_angle_is_pi()
            {
                var left = new Vector4D(14, 98, -19, 88);
                var right = left.GetNegative();
                var expected = Math.PI;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_1()
            {
                var left = new Vector4D(1, 0, 0, 0);
                var right = new Vector4D(1, 0, 1, 0);
                var expected = Math.Acos(1.0 / Math.Sqrt(2.0));

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_2()
            {
                var left = new Vector4D(1, 3, 4, 9);
                var right = new Vector4D(-2, 5.6, -9, 0.1);
                var expected = 1.7537363654297111;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }
        }

        public class DistanceTests : Vector4DTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector4D(1, -2, 3, 0);
                var b = new Vector4D(4, 0, -3, 5);
                var expected = Math.Sqrt(74);

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector4D(1, -2, -5, 0);
                var b = new Vector4D(3, 5, 1, 9);
                var expected = a.GetDistance(b);

                var actual = b.GetDistance(a);

                Assert.Equal(expected, actual);
            }
        }

        public class DistanceSquareTests : Vector4DTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector4D(1, -2, 3, 0);
                var b = new Vector4D(4, 0, -3, 5);
                var expected = 74.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector4D(16, 2, 5, 3);
                var b = new Vector4D(3, 5, 1, -1);
                var expected = a.GetDistanceSquared(b);

                var actual = b.GetDistanceSquared(a);

                Assert.Equal(expected, actual);
            }
        }

        public class ProtectedTests : Vector4DTests
        {
            [Fact]
            public void projecting_against_zero_results_in_same_vector()
            {
                var u = new Vector4D();
                var v = new Vector4D(-3, 8, 5, -10);
                var expected = new Vector4D(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_zero_against_another_vector_results_in_zero()
            {
                var u = new Vector4D(3, -17, 5, -6);
                var v = new Vector4D();
                var expected = new Vector4D();

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_1()
            {
                var u = new Vector4D(-1, 2, 1, 3);
                var v = new Vector4D(2, -1, 3, 1);
                var expected = new Vector4D(-2.0 / 15.0, 4.0 / 15.0, 2.0 / 15.0, 2.0 / 5.0);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }
        }

        public class NormalizeTests : Vector4DTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var actual = new Vector4D();
                var expected = new Vector4D();

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var actual = Vector4D.CreateXUnit();
                var expected = new Vector4D(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var actual = Vector4D.CreateYUnit();
                var expected = new Vector4D(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_z_normalizes_to_self()
            {
                var actual = Vector4D.CreateZUnit();
                var expected = new Vector4D(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_w_normalizes_to_self()
            {
                var actual = Vector4D.CreateWUnit();
                var expected = new Vector4D(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var actual = new Vector4D(3, 4, 5, -6);
                var expected = actual.GetQuotient(actual.GetMagnitude());

                actual.Normalize();

                Assert.Equal(expected, actual);
            }
        }

        public class GetNormalTests : Vector4DTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var vector = new Vector4D();
                var expected = new Vector4D();

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var vector = Vector4D.CreateXUnit();
                var expected = new Vector4D(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var vector = Vector4D.CreateYUnit();
                var expected = new Vector4D(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_z_normalizes_to_self()
            {
                var vector = Vector4D.CreateZUnit();
                var expected = new Vector4D(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_w_normalizes_to_self()
            {
                var vector = Vector4D.CreateWUnit();
                var expected = new Vector4D(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var vector = new Vector4D(3, 4, 5, -6);
                var expected = vector.GetQuotient(vector.GetMagnitude());

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }
        }
    }
}
