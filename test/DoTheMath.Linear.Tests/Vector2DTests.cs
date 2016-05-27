using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector2DTests
    {
        public class Constructors : Vector2DTests
        {
            [Fact]
            public void default_constructor_sets_components_to_zero()
            {
                var v = new Vector2D();

                Assert.Equal(0.0d, v.X);
                Assert.Equal(0.0d, v.Y);
            }

            [Fact]
            public void component_constructor_assigns_correct_fields()
            {
                var v = new Vector2D(1.0, -5.0);

                Assert.Equal(1.0, v.X);
                Assert.Equal(-5.0, v.Y);
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new Vector2D(0, 1);

                var actual = new Vector2D(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }
        }

        public class Get : Vector2DTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector2D(-1.0, 5.0);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(2));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : Vector2DTests
        {
            [Fact]
            public void can_set_all_componenets()
            {
                var v = new Vector2D(2, 3);

                v.Set(0, -1.0);
                v.Set(1, 5.0);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0.0));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector2D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(2, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0.0));
            }
        }

        public class IEquatable_Self_Equals : Vector2DTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector2D(1.0, 2.0);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector2D(5.0, -1.1);
                var b = new Vector2D(5.0, -1.1);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector2D(5.0, -1.1);
                var b = new Vector2D(-1.1, 5.0);
                var c = new Vector2D(5.0, 5.0);
                var d = new Vector2D(-1.1, -1.1);
                var e = new Vector2D();

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

        public class Object_Equals : Vector2DTests
        {
            [Fact]
            public void self_equal_to_self()
            {
                var v = new Vector2D(1.0, 2.0);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void self_not_equal_to_null()
            {
                var v = new Vector2D(1.0, 2.0);

                Assert.False(v.Equals((object)null));
            }

            [Fact]
            public void self_not_equal_to_unknown_type()
            {
                var v = new Vector2D(1.0, 2.0);

                Assert.False(v.Equals((object)"not-a-matrix"));
            }

            [Fact]
            public void same_components_are_equal()
            {
                var a = new Vector2D(5.0, -1.1);
                var b = new Vector2D(5.0, -1.1);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new Vector2D(5.0, -1.1);
                var b = new Vector2D(-1.1, 5.0);
                var c = new Vector2D(5.0, 5.0);
                var d = new Vector2D(-1.1, -1.1);
                var e = new Vector2D();

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

        public class GetHashCodeTests : Vector2DTests
        {
            [Fact]
            public void same_vector_has_same_hashcode_when_mutated()
            {
                var m = new Vector2D(1, 2);
                var expectedHashCode = m.GetHashCode();
                m.X = -1.1;
                m.Y = 100.2;

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }
        }

        public class AddTests : Vector2DTests
        {
            [Fact]
            public void adding_vector_produces_sum_vector()
            {
                var left = new Vector2D(1, 2);
                var right = new Vector2D(3, 4);
                var expected = new Vector2D(4, 6);

                var actual = left.Add(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_operands_unchanged()
            {
                var left = new Vector2D(1, 2);
                var expectedLeft = new Vector2D(left);
                var right = new Vector2D(3, 4);
                var expectedRight = new Vector2D(right);

                var result = left.Add(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class AddToTests : Vector2DTests
        {
            [Fact]
            public void adding_vector_adds_to_components()
            {
                var actual = new Vector2D(1, 2);
                var right = new Vector2D(3, 4);
                var expected = new Vector2D(4, 6);

                actual.AddTo(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void adding_vectors_leaves_right_unchanged()
            {
                var left = new Vector2D(1, 2);
                var right = new Vector2D(3, 4);
                var expectedRight = new Vector2D(right);

                left.AddTo(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractTests : Vector2DTests
        {
            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new Vector2D(1, 15);
                var right = new Vector2D(3, 2);
                var expected = new Vector2D(-2, 13);

                var actual = left.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new Vector2D(1, 2);
                var expectedLeft = new Vector2D(left);
                var right = new Vector2D(3, 4);
                var expectedRight = new Vector2D(right);

                var result = left.Subtract(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : Vector2DTests
        {
            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new Vector2D(1, 15);
                var right = new Vector2D(3, 4);
                var expected = new Vector2D(-2, 11);

                actual.SubtractFrom(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new Vector2D(1, 2);
                var right = new Vector2D(3, 4);
                var expectedRight = new Vector2D(right);

                left.SubtractFrom(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : Vector2DTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new Vector2D(1, -2);
                var expected = new Vector2D(3, -6);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector2D(1, 2);
                var expected = new Vector2D(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : Vector2DTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new Vector2D(1, -2);
                var expected = new Vector2D(3, -6);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : Vector2DTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new Vector2D(3, -2);
                var expected = new Vector2D(3.0 / 4.0, -2 / 4.0);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new Vector2D(1, 2);
                var expected = new Vector2D(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : Vector2DTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new Vector2D(3, -2);
                var expected = new Vector2D(3.0 / 4.0, -2 / 4.0);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }
    }
}
