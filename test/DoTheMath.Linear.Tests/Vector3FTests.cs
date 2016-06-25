using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector3FTests
    {
        public class Constructors : Vector3FTests
        {
            [Fact]
            public void default_constructor_sets_components_to_zero()
            {
                var v = new Vector3F();

                Assert.Equal(0.0f, v.X);
                Assert.Equal(0.0f, v.Y);
                Assert.Equal(0.0f, v.Z);
                Assert.Equal(3, v.Dimensions);
            }

            [Fact]
            public void component_constructor_assigns_correct_fields()
            {
                var v = new Vector3F(1.0f, -5.0f, 6.0f);

                Assert.Equal(1.0f, v.X);
                Assert.Equal(-5.0f, v.Y);
                Assert.Equal(6.0f, v.Z);
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new Vector3F(1, 2, 3);

                var actual = new Vector3F(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector()
            {
                var expected = new Vector3F(1, 2, 3);

                var actual = new Vector3F((IVector<float>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector3F((IVector<float>)null));
            }

            [Fact]
            public void copy_constructor_throws_for_ivector_of_wrong_size()
            {
                var source = new VectorF(4);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Vector3F((IVector<float>)source));
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector3()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector3F((IVector3<float>)null));
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector3()
            {
                var expected = new Vector3F(1, 2, 3);

                var actual = new Vector3F((IVector3<float>)expected);

                Assert.Equal(expected, actual);
            }
        }

        public class Factories : Vector3FTests
        {
            [Fact]
            public void create_x_unit()
            {
                var expected = new Vector3F(1, 0, 0);

                var actual = Vector3F.CreateXUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_y_unit()
            {
                var expected = new Vector3F(0, 1, 0);

                var actual = Vector3F.CreateYUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_z_unit()
            {
                var expected = new Vector3F(0, 0, 1);

                var actual = Vector3F.CreateZUnit();

                Assert.Equal(expected, actual);
            }
        }

        public class OperatorOverloads : Vector3FTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = new Vector3F(1, 2, 3);
                var right = new Vector3F(-3, -100, 40);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = new Vector3F(1, 2, 3);
                var right = new Vector3F(-3, -100, 40);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_mimics_dot()
            {
                var left = new Vector3F(1, 2, 3);
                var right = new Vector3F(-3, -100, 40);
                var expected = left.GetDot(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale()
            {
                var vector = new Vector3F(1, 2, 3);
                var scalar = 2.4f;
                var expected = vector.GetScaled(scalar);

                var actual = vector * scalar;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale_first()
            {
                var vector = new Vector3F(1, 2, 3);
                var scalar = 2.4f;
                var expected = vector.GetScaled(scalar);

                var actual = scalar * vector;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_divide()
            {
                var vector = new Vector3F(1, 2, 3);
                var divisor = 2.4f;
                var expected = vector.GetQuotient(divisor);

                var actual = vector / divisor;

                Assert.Equal(expected, actual);
            }
        }

        public class Get : Vector3FTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector3F(-1.0f, 5.0f, -0.5f);

                Assert.Equal(-1.0f, v.Get(0));
                Assert.Equal(5.0f, v.Get(1));
                Assert.Equal(-0.5f, v.Get(2));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(3));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : Vector3FTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector3F(1, 2, 3);

                v.Set(0, -1.0f);
                v.Set(1, 5.0f);
                v.Set(2, -0.5f);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
                Assert.Equal(-0.5, v.Get(2));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0));
            }
        }

        public class IndexerGet : Vector3FTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector3F(-1.0f, 5.0f, -0.5f);

                Assert.Equal(-1.0f, v[0]);
                Assert.Equal(5.0f, v[1]);
                Assert.Equal(-0.5f, v[2]);
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue]);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<IndexOutOfRangeException>(() => v[3]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue]);
            }
        }

        public class IndexerSet : Vector3FTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector3F(1, 2, 3);

                v[0] = -1.0f;
                v[1] = 5.0f;
                v[2] = -0.5f;

                Assert.Equal(-1.0, v[0]);
                Assert.Equal(5.0, v[1]);
                Assert.Equal(-0.5, v[2]);
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue] = 0);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector3F();

                Assert.Throws<IndexOutOfRangeException>(() => v[3] = 0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue] = 0);
            }
        }

        public class IEquatable_Self_Equals : Vector3FTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector3F(1.0f, 2.0f, 3.0f);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector3F(5.0f, -1.1f, 99.9f);
                var b = new Vector3F(5.0f, -1.1f, 99.9f);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector3F(5.0f, -1.1f, 0.0f);
                var b = new Vector3F(-1.1f, 5.0f, 5.0f);
                var c = new Vector3F(5.0f, -1.1f, 11.2f);
                var d = new Vector3F(-1.1f, -1.1f, -1.1f);
                var e = new Vector3F();

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

        public class Object_Equals : Vector3FTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector3F(1.0f, 2.0f, 3.0f);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void self_not_equal_to_null()
            {
                var v = new Vector3F(1.0f, 2.0f, 3.0f);

                Assert.False(v.Equals((object)null));
            }

            [Fact]
            public void self_not_equal_to_unknown_type()
            {
                var v = new Vector3F(1.0f, 2.0f, 3.0f);

                Assert.False(v.Equals((object)"not-a-matrix"));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector3F(5.0f, -1.1f, 99.9f);
                var b = new Vector3F(5.0f, -1.1f, 99.9f);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector3F(5.0f, -1.1f, 0.0f);
                var b = new Vector3F(-1.1f, 5.0f, 5.0f);
                var c = new Vector3F(5.0f, -1.1f, 11.2f);
                var d = new Vector3F(-1.1f, -1.1f, -1.1f);
                var e = new Vector3F();

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

        public class GetHashCodeTests : Vector3FTests
        {
            [Fact]
            public void same_vector_has_same_hashcode_when_mutated()
            {
                var m = new Vector3F(1.0f, 2.0f, 3.0f);
                var expectedHashCode = m.GetHashCode();
                m.X = -1.1f;
                m.Y = 100.2f;
                m.Z = 9999.7f;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class AddTests : Vector3FTests
        {
            [Fact]
            public void adding_vector_produces_sum_vector()
            {
                var left = new Vector3F(1, 2, 10);
                var right = new Vector3F(3, 4, 11);
                var expected = new Vector3F(4, 6, 21);

                var actual = left.GetSum(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_operands_unchanged()
            {
                var left = new Vector3F(1, 2, 10);
                var expectedLeft = new Vector3F(left);
                var right = new Vector3F(3, 4, 11);
                var expectedRight = new Vector3F(right);

                var result = left.GetSum(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class AddToTests : Vector3FTests
        {
            [Fact]
            public void adding_vector_adds_to_components()
            {
                var actual = new Vector3F(1, 2, 10);
                var right = new Vector3F(3, 4, 11);
                var expected = new Vector3F(4, 6, 21);

                actual.Add(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_right_unchanged()
            {
                var left = new Vector3F(1, 2, 10);
                var right = new Vector3F(3, 4, 11);
                var expectedRight = new Vector3F(right);

                left.Add(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractTests : Vector3FTests
        {
            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new Vector3F(1, 15, 5);
                var right = new Vector3F(3, 2, 10);
                var expected = new Vector3F(-2, 13, -5);

                var actual = left.GetDifference(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new Vector3F(1, 2, 3);
                var expectedLeft = new Vector3F(left);
                var right = new Vector3F(3, 4, 5);
                var expectedRight = new Vector3F(right);

                var result = left.GetDifference(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : Vector3FTests
        {
            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new Vector3F(1, 15, 5);
                var right = new Vector3F(3, 4, 10);
                var expected = new Vector3F(-2, 11, -5);

                actual.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new Vector3F(1, 2, 3);
                var right = new Vector3F(3, 4, 5);
                var expectedRight = new Vector3F(right);

                left.Subtract(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : Vector3FTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new Vector3F(1, -2, 4);
                var expected = new Vector3F(3, -6, 12);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector3F(1, 2, 4);
                var expected = new Vector3F(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : Vector3FTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new Vector3F(1, -2, 4);
                var expected = new Vector3F(3, -6, 12);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : Vector3FTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new Vector3F(3, -2, 8);
                var expected = new Vector3F(3.0f / 4.0f, -2.0f / 4.0f, 8.0f / 4.0f);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector3F(1, 2, 3);
                var expected = new Vector3F(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : Vector3FTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new Vector3F(3, -2, 8);
                var expected = new Vector3F(3.0f / 4.0f, -2.0f / 4.0f, 8.0f / 4.0f);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }

        public class GetNegative : Vector3FTests
        {
            [Fact]
            public void can_get_negative_vector()
            {
                var source = new Vector3F(-2, 5, -12);
                var expected = new Vector3F(2, -5, 12);

                var actual = source.GetNegative();

                Assert.Equal(expected, actual);
            }
        }

        public class Negate : Vector3FTests
        {
            [Fact]
            public void can_negate()
            {
                var actual = new Vector3F(9, -3, 17);
                var expected = new Vector3F(-9, 3, -17);

                actual.Negate();

                Assert.Equal(expected, actual);
            }
        }

        public class Dot : Vector3FTests
        {
            [Fact]
            public void can_get_dot()
            {
                var left = new Vector3F(9.0f, 1.2f, 3.5f);
                var right = new Vector3F(-0.3f, 3.0f, -0.9f);
                var expected = (9 * -0.3) + (1.2 * 3) + (3.5 * -0.9);

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitude : Vector3FTests
        {
            [Fact]
            public void can_get_magnitude()
            {
                var vector = new Vector3F(3, 4, 5);
                var expected = (float)Math.Sqrt(50.0);

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude()
            {
                var vector = new Vector3F(0.1f, 2.0f, 3.0f);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitudeSquared : Vector3FTests
        {
            [Fact]
            public void can_get_magnitude_squared()
            {
                var vector = new Vector3F(3, 4, 5);
                var expected = 50.0;

                var actual = vector.GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude_squared()
            {
                var vector = new Vector3F(0.1f, 2.0f, 3.0f);
                var expected = vector.GetMagnitudeSquared();

                var actual = vector.GetNegative().GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }
        }

        public class GetAngleBetween : Vector3FTests
        {
            [Fact]
            public void self_angle_is_zero()
            {
                var vector = new Vector3F(10.0f, 57.0f, -9.0f);
                var expected = 0.0f;

                var actual = vector.GetAngleBetween(vector);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void right_angle_is_half_pi()
            {
                var left = new Vector3F(1, 0, 1);
                var right = new Vector3F(0, 1, 0);
                var expected = (float)(Math.PI / 2.0);

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void opposite_vector_angle_is_pi()
            {
                var left = new Vector3F(14, 98, -19);
                var right = left.GetNegative();
                var expected = (float)Math.PI;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_1()
            {
                var left = new Vector3F(1, 0, 0);
                var right = new Vector3F(1, 0, 1);
                var expected = (float)Math.Acos(1.0 / Math.Sqrt(2.0));

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }

            [Fact]
            public void example_2()
            {
                var left = new Vector3F(1f, 3f, 4f);
                var right = new Vector3F(-2f, 5.6f, -9f);
                var expected = 1.9664718552074198;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }
        }

        public class DistanceTests : Vector3FTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector3F(1, 2, 5);
                var b = new Vector3F(3, 5, 1);
                var expected = (float)Math.Sqrt(29);

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector3F(1, -2, -5);
                var b = new Vector3F(3, 5, 1);
                var expected = a.GetDistance(b);

                var actual = b.GetDistance(a);

                Assert.Equal(expected, actual);
            }
        }

        public class DistanceSquareTests : Vector3FTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector3F(1, 2, 5);
                var b = new Vector3F(3, 5, 1);
                var expected = (float)29.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector3F(16, 2, 5);
                var b = new Vector3F(3, 5, 1);
                var expected = a.GetDistanceSquared(b);

                var actual = b.GetDistanceSquared(a);

                Assert.Equal(expected, actual);
            }
        }

        public class ProtectedTests : Vector3FTests
        {
            [Fact]
            public void projecting_against_zero_results_in_same_vector()
            {
                var u = new Vector3F();
                var v = new Vector3F(3, 4, 5);
                var expected = new Vector3F(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_zero_against_another_vector_results_in_zero()
            {
                var u = new Vector3F(3, 4, 5);
                var v = new Vector3F();
                var expected = new Vector3F();

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_1()
            {
                var u = new Vector3F(1, 2, 0);
                var v = new Vector3F(1, 2, 3);
                var expected = new Vector3F(u);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }
        }

        public class NormalizeTests : Vector3FTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var actual = new Vector3F();
                var expected = new Vector3F();

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var actual = Vector3F.CreateXUnit();
                var expected = new Vector3F(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var actual = Vector3F.CreateYUnit();
                var expected = new Vector3F(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_z_normalizes_to_self()
            {
                var actual = Vector3F.CreateZUnit();
                var expected = new Vector3F(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var actual = new Vector3F(3, 4, 5);
                var expected = actual.GetQuotient(actual.GetMagnitude());

                actual.Normalize();

                Assert.Equal(expected, actual);
            }
        }

        public class GetNormalTests : Vector3FTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var vector = new Vector3F();
                var expected = new Vector3F();

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var vector = Vector3F.CreateXUnit();
                var expected = new Vector3F(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var vector = Vector3F.CreateYUnit();
                var expected = new Vector3F(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_z_normalizes_to_self()
            {
                var vector = Vector3F.CreateZUnit();
                var expected = new Vector3F(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var vector = new Vector3F(3, 4, 5);
                var expected = vector.GetQuotient(vector.GetMagnitude());

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }
        }

        public class GetCrossProductTest : Vector2DTests
        {
            [Fact]
            public void self_cross_product_is_zero_vector()
            {
                var v = new Vector3F(3, 4, -5);
                var expected = new Vector3F();

                var actual = v.GetCrossProduct(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void swapped_operands_produces_negated_result()
            {
                var a = new Vector3F(9.2f, -3.5f, 1.1f);
                var b = new Vector3F(-4.8f, 9.4f, 2.9f);
                var expected = a.GetCrossProduct(b).GetNegative();

                var actual = b.GetCrossProduct(a);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var a = new Vector3F(3.0f, -3, 1);
                var b = new Vector3F(4.0f, 9, 2);
                var expected = new Vector3F(-15.0f, -2, 39);

                var actual = a.GetCrossProduct(b);

                Assert.Equal(expected, actual);
            }
        }
    }
}
