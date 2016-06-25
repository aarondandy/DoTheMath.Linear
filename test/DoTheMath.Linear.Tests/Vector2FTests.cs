using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector2FTests
    {
        public class Constructors : Vector2FTests
        {
            [Fact]
            public void default_constructor_sets_components_to_zero()
            {
                var v = new Vector2F();

                Assert.Equal(0.0f, v.X);
                Assert.Equal(0.0f, v.Y);
                Assert.Equal(2, v.Dimensions);
            }

            [Fact]
            public void component_constructor_assigns_correct_fields()
            {
                var v = new Vector2F(1.0f, -5.0f);

                Assert.Equal(1.0f, v.X);
                Assert.Equal(-5.0f, v.Y);
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new Vector2F(1, 2);

                var actual = new Vector2F(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector2F((IVector<float>)null));
            }

            [Fact]
            public void copy_constructor_throws_for_ivector_of_wrong_size()
            {
                var source = new VectorF(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => new Vector2F((IVector<float>)source));
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector()
            {
                var expected = new Vector2F(1, 2);

                var actual = new Vector2F((IVector<float>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector2()
            {
                Assert.Throws<ArgumentNullException>(() => new Vector2F((IVector2<float>)null));
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector2()
            {
                var expected = new Vector2F(1, 2);

                var actual = new Vector2F((IVector2<float>)expected);

                Assert.Equal(expected, actual);
            }
        }

        public class Factories : Vector2FTests
        {
            [Fact]
            public void create_x_unit()
            {
                var expected = new Vector2F(1, 0);

                var actual = Vector2F.CreateXUnit();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void create_y_unit()
            {
                var expected = new Vector2F(0, 1);

                var actual = Vector2F.CreateYUnit();

                Assert.Equal(expected, actual);
            }
        }

        public class OperatorOverloads : Vector2FTests
        {
            [Fact]
            public void op_addition_mimics_add()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(-3, -100);
                var expected = left.GetSum(right);

                var actual = left + right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_subtraction_mimics_subtract()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(-3, -100);
                var expected = left.GetDifference(right);

                var actual = left - right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_mimics_dot()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(-3, -100);
                var expected = left.GetDot(right);

                var actual = left * right;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale()
            {
                var vector = new Vector2F(1, 2);
                var scalar = 2.4f;
                var expected = vector.GetScaled(scalar);

                var actual = vector * scalar;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_scale_first()
            {
                var vector = new Vector2F(1, 2);
                var scalar = 2.4f;
                var expected = vector.GetScaled(scalar);

                var actual = scalar * vector;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void op_multiply_scalar_mimics_divide()
            {
                var vector = new Vector2F(1, 2);
                var denominator = 2.4f;
                var expected = vector.GetQuotient(denominator);

                var actual = vector / denominator;

                Assert.Equal(expected, actual);
            }
        }

        public class Get : Vector2FTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector2F(-1.0f, 5.0f);

                Assert.Equal(-1.0f, v.Get(0));
                Assert.Equal(5.0f, v.Get(1));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(2));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : Vector2FTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector2F(2, 3);

                v.Set(0, -1.0f);
                v.Set(1, 5.0f);

                Assert.Equal(-1.0f, v.Get(0));
                Assert.Equal(5.0f, v.Get(1));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0.0f));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(2, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0.0f));
            }
        }

        public class IndexerGet : Vector2FTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector2F(-1.0f, 5.0f);

                Assert.Equal(-1.0, v[0]);
                Assert.Equal(5.0, v[1]);
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue]);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<IndexOutOfRangeException>(() => v[2]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue]);
            }
        }

        public class IndexerSet : Vector2FTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector2F(2, 3);

                v[0] = -1.0f;
                v[1] = 5.0f;

                Assert.Equal(-1.0f, v.Get(0));
                Assert.Equal(5.0f, v.Get(1));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<IndexOutOfRangeException>(() => v[-1] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue] = 0.0f);
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2F();

                Assert.Throws<IndexOutOfRangeException>(() => v[2] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue] = 0.0f);
            }
        }

        public class IEquatable_Self_Equals : Vector2FTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector2F(1.0f, 2.0f);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector2F(5.0f, -1.1f);
                var b = new Vector2F(5.0f, -1.1f);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector2F(5.0f, -1.1f);
                var b = new Vector2F(-1.1f, 5.0f);
                var c = new Vector2F(5.0f, 5.0f);
                var d = new Vector2F(-1.1f, -1.1f);
                var e = new Vector2F();

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

        public class Object_Equals : Vector2FTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector2F(1.0f, 2.0f);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void self_not_equal_to_null()
            {
                var v = new Vector2F(1.0f, 2.0f);

                Assert.False(v.Equals((object)null));
            }

            [Fact]
            public void self_not_equal_to_unknown_type()
            {
                var v = new Vector2F(1.0f, 2.0f);

                Assert.False(v.Equals((object)"not-a-matrix"));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector2F(5.0f, -1.1f);
                var b = new Vector2F(5.0f, -1.1f);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector2F(5.0f, -1.1f);
                var b = new Vector2F(-1.1f, 5.0f);
                var c = new Vector2F(5.0f, 5.0f);
                var d = new Vector2F(-1.1f, -1.1f);
                var e = new Vector2F();

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

        public class GetHashCodeTests : Vector2FTests
        {
            [Fact]
            public void same_vector_has_same_hashcode_when_mutated()
            {
                var m = new Vector2F(1, 2);
                var expectedHashCode = m.GetHashCode();
                m.X = -1.1f;
                m.Y = 100.2f;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class AddTests : Vector2FTests
        {
            [Fact]
            public void adding_vector_produces_sum_vector()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(3, 4);
                var expected = new Vector2F(4, 6);

                var actual = left.GetSum(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_operands_unchanged()
            {
                var left = new Vector2F(1, 2);
                var expectedLeft = new Vector2F(left);
                var right = new Vector2F(3, 4);
                var expectedRight = new Vector2F(right);

                var result = left.GetSum(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class AddToTests : Vector2FTests
        {
            [Fact]
            public void adding_vector_adds_to_components()
            {
                var actual = new Vector2F(1, 2);
                var right = new Vector2F(3, 4);
                var expected = new Vector2F(4, 6);

                actual.Add(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_right_unchanged()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(3, 4);
                var expectedRight = new Vector2F(right);

                left.Add(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractTests : Vector2FTests
        {
            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new Vector2F(1, 15);
                var right = new Vector2F(3, 2);
                var expected = new Vector2F(-2, 13);

                var actual = left.GetDifference(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new Vector2F(1, 2);
                var expectedLeft = new Vector2F(left);
                var right = new Vector2F(3, 4);
                var expectedRight = new Vector2F(right);

                var result = left.GetDifference(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : Vector2FTests
        {
            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new Vector2F(1, 15);
                var right = new Vector2F(3, 4);
                var expected = new Vector2F(-2, 11);

                actual.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new Vector2F(1, 2);
                var right = new Vector2F(3, 4);
                var expectedRight = new Vector2F(right);

                left.Subtract(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : Vector2FTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new Vector2F(1, -2);
                var expected = new Vector2F(3, -6);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector2F(1, 2);
                var expected = new Vector2F(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : Vector2FTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new Vector2F(1, -2);
                var expected = new Vector2F(3, -6);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : Vector2FTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new Vector2F(3, -2);
                var expected = new Vector2F(3.0f / 4.0f, -2f / 4.0f);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector2F(1, 2);
                var expected = new Vector2F(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : Vector2FTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new Vector2F(3, -2);
                var expected = new Vector2F(3.0f / 4.0f, -2.0f / 4.0f);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }

        public class GetNegative : Vector2FTests
        {
            [Fact]
            public void can_get_negative_vector()
            {
                var source = new Vector2F(-2, 5);
                var expected = new Vector2F(2, -5);

                var actual = source.GetNegative();

                Assert.Equal(expected, actual);
            }
        }

        public class Negate : Vector2FTests
        {
            [Fact]
            public void can_negate()
            {
                var actual = new Vector2F(9, -3);
                var expected = new Vector2F(-9, 3);

                actual.Negate();

                Assert.Equal(expected, actual);
            }
        }

        public class Dot : Vector2FTests
        {
            [Fact]
            public void can_get_dot()
            {
                var left = new Vector2F(9.0f, 1.2f);
                var right = new Vector2F(-0.3f, 3.0f);
                var expected = (9.0f * -0.3f) + (1.2f * 3.0f);

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual, 6);
            }
        }

        public class GetMagnitude : Vector2FTests
        {
            [Fact]
            public void can_get_magnitude()
            {
                var vector = new Vector2F(3, 4);
                var expected = 5.0;

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude()
            {
                var vector = new Vector2F(3, 4);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitudeSquared : Vector2FTests
        {
            [Fact]
            public void can_get_magnitude_squared()
            {
                var vector = new Vector2F(3, 4);
                var expected = 25.0;

                var actual = vector.GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude_squared()
            {
                var vector = new Vector2F(3, 4);
                var expected = vector.GetMagnitudeSquared();

                var actual = vector.GetNegative().GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }
        }

        public class GetAngleBetween : Vector2FTests
        {
            [Fact]
            public void self_angle_is_zero()
            {
                var vector = new Vector2F(10, 57);
                var expected = 0.0;

                var actual = vector.GetAngleBetween(vector);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void right_angle_is_half_pi()
            {
                var left = new Vector2F(1, 1);
                var right = new Vector2F(-1, 1);
                var expected = (float)(Math.PI / 2.0);

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void opposite_vector_angle_is_pi()
            {
                var left = new Vector2F(14, 98);
                var right = left.GetNegative();
                var expected = (float)Math.PI;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_1()
            {
                var left = new Vector2F(1, 3);
                var right = new Vector2F(-2, 5.6f);
                var expected = 0.6647744948173457f;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }
        }

        public class DistanceTests : Vector2FTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector2F(1, 2);
                var b = new Vector2F(3, 5);
                var expected = (float)Math.Sqrt(13);

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector2F(1, 2);
                var b = new Vector2F(3, 5);
                var expected = a.GetDistance(b);

                var actual = b.GetDistance(a);

                Assert.Equal(expected, actual);
            }
        }

        public class DistanceSquareTests : Vector2FTests
        {
            [Fact]
            public void can_find_distance()
            {
                var a = new Vector2F(1, 2);
                var b = new Vector2F(3, 5);
                var expected = 13.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new Vector2F(1, 2);
                var b = new Vector2F(3, 5);
                var expected = a.GetDistanceSquared(b);

                var actual = b.GetDistanceSquared(a);

                Assert.Equal(expected, actual);
            }
        }

        public class ProtectedTests : Vector2FTests
        {
            [Fact]
            public void projecting_against_zero_results_in_same_vector()
            {
                var u = new Vector2F();
                var v = new Vector2F(3, 4);
                var expected = new Vector2F(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_zero_against_another_vector_results_in_zero()
            {
                var u = new Vector2F(3, 4);
                var v = new Vector2F();
                var expected = new Vector2F();

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_1()
            {
                var u = new Vector2F(1, 4);
                var v = new Vector2F(6, 7);
                var expected = new Vector2F(2, 8);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_2()
            {
                var u = new Vector2F(2, 5);
                var v = new Vector2F(1, 2);
                var expected = u.GetScaled(12.0f / 29.0f);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }
        }

        public class NormalizeTests : Vector2FTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var actual = new Vector2F();
                var expected = new Vector2F();

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var actual = Vector2F.CreateXUnit();
                var expected = new Vector2F(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var actual = Vector2F.CreateYUnit();
                var expected = new Vector2F(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var actual = new Vector2F(3, 4);
                var expected = actual.GetQuotient(5.0f);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }
        }

        public class GetNormalTests : Vector2FTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var vector = new Vector2F();
                var expected = new Vector2F();

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_x_normalizes_to_self()
            {
                var vector = Vector2F.CreateXUnit();
                var expected = new Vector2F(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_y_normalizes_to_self()
            {
                var vector = Vector2F.CreateYUnit();
                var expected = new Vector2F(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var vector = new Vector2F(3, 4);
                var expected = vector.GetQuotient(5.0f);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }
        }

        public class RotatePerpendicularClockwiseTests : Vector2FTests
        {
            [Fact]
            public void rotate_zero_vector_produces_zero_vector()
            {
                var actual = new Vector2F();
                var expected = new Vector2F();

                actual.RotatePerpendicularClockwise();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var actual = new Vector2F(3, 2);
                var expected = new Vector2F(2, -3);

                actual.RotatePerpendicularClockwise();

                Assert.Equal(expected, actual);
            }
        }

        public class GetPerpendicularClockwiseTests : Vector2FTests
        {
            [Fact]
            public void rotate_zero_vector_produces_zero_vector()
            {
                var vector = new Vector2F();
                var expected = new Vector2F();

                var actual = vector.GetPerpendicularClockwise();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var vector = new Vector2F(3, 2);
                var expected = new Vector2F(2, -3);

                var actual = vector.GetPerpendicularClockwise();

                Assert.Equal(expected, actual);
            }
        }

        public class RotatePerpendicularCounterclockwise : Vector2FTests
        {
            [Fact]
            public void rotate_zero_vector_produces_zero_vector()
            {
                var actual = new Vector2F();
                var expected = new Vector2F();

                actual.RotatePerpendicularCounterclockwise();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var actual = new Vector2F(3, 2);
                var expected = new Vector2F(-2, 3);

                actual.RotatePerpendicularCounterclockwise();

                Assert.Equal(expected, actual);
            }
        }

        public class GetPerpendicularCounterclockwiseTests : Vector2FTests
        {
            [Fact]
            public void rotate_zero_vector_produces_zero_vector()
            {
                var vector = new Vector2F();
                var expected = new Vector2F();

                var actual = vector.GetPerpendicularCounterclockwise();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var vector = new Vector2F(3, 2);
                var expected = new Vector2F(-2, 3);

                var actual = vector.GetPerpendicularCounterclockwise();

                Assert.Equal(expected, actual);
            }
        }
    }
}
