using DoTheMath.Linear;
using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class VectorFTests
    {
        public class Constructors : VectorFTests
        {
            [Fact]
            public void size_constructor_sets_components_to_zero()
            {
                var v = new VectorF(15);

                Assert.Equal(15, v.Dimensions);

                for (var d = 0; d < v.Dimensions; d++)
                {
                    Assert.Equal(0.0, v.Get(d));
                }
            }

            [Fact]
            public void size_constructor_can_make_empty()
            {
                var v = new VectorD(0);

                Assert.Equal(0, v.Dimensions);
            }

            [Fact]
            public void negative_size_constructor_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorF(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorF(-40));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorF(int.MinValue));
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorF((VectorF)null));
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new VectorF(5);
                expected.Set(0, 0);
                expected.Set(1, 1);
                expected.Set(2, 2);
                expected.Set(3, 3);
                expected.Set(4, 4);

                var actual = new VectorF(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void null_componenets_array_constructor_throws()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorF((float[])null));
            }

            [Fact]
            public void array_constructor_assigns_componenets_and_length()
            {
                var components = new float[] { 4, 5, 6, 7 };
                var expected = new VectorF(4);
                expected.Set(0, 4);
                expected.Set(1, 5);
                expected.Set(2, 6);
                expected.Set(3, 7);

                var actual = new VectorF(components);

                Assert.Equal(components.Length, actual.Dimensions);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void array_constructor_copies_all_componenets()
            {
                var arrayValues = new[] { 1.0f, 2.0f, 3.0f };
                var expected = new VectorF(3);
                expected.Set(0, 1.0f);
                expected.Set(1, 2.0f);
                expected.Set(2, 3.0f);

                var actual = new VectorF(arrayValues);
                arrayValues[0] = -99.0f;
                arrayValues[1] = -99.0f;
                arrayValues[2] = -99.0f;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector()
            {
                var expected = new VectorF(new[] { 1.0f, 2.0f, 3.3f, 4.4f });

                var actual = new VectorF((IVector<float>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorF((IVector<float>)null));
            }
        }

        public class Factories : VectorFTests
        {
            [Theory]
            [InlineData(1, 0)]
            [InlineData(3, 0)]
            [InlineData(3, 2)]
            [InlineData(3, 1)]
            public void can_create_axis_unit_vector(int size, int dimension)
            {
                var expected = new VectorF(size);
                expected.Set(dimension, 1.0f);

                var actual = VectorF.CreateUnit(size, dimension);

                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(1, -1)]
            [InlineData(1, 1)]
            [InlineData(3, -3)]
            [InlineData(3, 3)]
            [InlineData(5, -10)]
            [InlineData(5, 10)]
            public void create_axis_unit_vector_with_bad_dimension_throws(int size, int dimension)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => VectorF.CreateUnit(size, dimension));
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-6)]
            public void create_axis_unit_vector_with_bad_size_throws(int size)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => VectorF.CreateUnit(size, 0));
            }
        }

        public class Get : VectorFTests
        {
            [Fact]
            public void can_get_componenets()
            {
                var v = new VectorF(new[] { 3.0f, 4.0f, 5.0f });

                Assert.Equal(3.0, v.Get(0));
                Assert.Equal(4.0, v.Get(1));
                Assert.Equal(5.0, v.Get(2));
            }

            [Fact]
            public void get_negative_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-4));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-5));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void get_large_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(5));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(6));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(101));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : VectorFTests
        {
            [Fact]
            public void can_set_componenets()
            {
                var v = new VectorF(new[] { 2.0f, 6.0f, 7.0f });

                v.Set(0, 3.0f);
                v.Set(1, 4.0f);
                v.Set(2, 5.0f);

                Assert.Equal(3.0, v.Get(0));
                Assert.Equal(4.0, v.Get(1));
                Assert.Equal(5.0, v.Get(2));
            }

            [Fact]
            public void set_negative_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-4, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-5, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0.0f));
            }

            [Fact]
            public void set_large_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(5, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(6, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(101, 0.0f));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0.0f));
            }
        }

        public class IndexerGet : VectorFTests
        {
            [Fact]
            public void can_get_componenets()
            {
                var v = new VectorF(new[] { 3.0f, 4.0f, 5.0f });

                Assert.Equal(3.0, v[0]);
                Assert.Equal(4.0, v[1]);
                Assert.Equal(5.0, v[2]);
            }

            [Fact]
            public void get_negative_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[-1]);
                Assert.Throws<IndexOutOfRangeException>(() => v[-4]);
                Assert.Throws<IndexOutOfRangeException>(() => v[-5]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue]);
            }

            [Fact]
            public void get_large_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[v.Dimensions]);
                Assert.Throws<IndexOutOfRangeException>(() => v[5]);
                Assert.Throws<IndexOutOfRangeException>(() => v[6]);
                Assert.Throws<IndexOutOfRangeException>(() => v[101]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue]);
            }
        }

        public class IndexerSet : VectorFTests
        {
            [Fact]
            public void can_set_componenets()
            {
                var v = new VectorF(new[] { 2.0f, 6.0f, 7.0f });

                v[0] = 3.0f;
                v[1] = 4.0f;
                v[2] = 5.0f;

                Assert.Equal(3.0f, v[0]);
                Assert.Equal(4.0f, v[1]);
                Assert.Equal(5.0f, v[2]);
            }

            [Fact]
            public void set_negative_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[-1] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[-4] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[-5] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue] = 0.0f);
            }

            [Fact]
            public void set_large_dimension_throws()
            {
                var v = new VectorF(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[v.Dimensions] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[5] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[6] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[101] = 0.0f);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue] = 0.0f);
            }
        }

        public class GetAndSet : VectorFTests
        {
            [Fact]
            public void get_all_element_for_dim_5()
            {
                var v = new VectorF(5);
                v.Set(0, -1.0f);
                v.Set(1, 5.7f);
                v.Set(2, -0.4f);
                v.Set(3, 9.0f);
                v.Set(4, -101.1f);

                Assert.Equal(-1.0f, v.Get(0));
                Assert.Equal(5.7f, v.Get(1));
                Assert.Equal(-0.4f, v.Get(2));
                Assert.Equal(9.0f, v.Get(3));
                Assert.Equal(-101.1f, v.Get(4));
            }
        }

        public class IEquatable_Self_Equals : VectorFTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var v = new VectorF(2);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void different_ref_same_components_are_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(2);
                b.Set(0, 9.0f);
                b.Set(1, -10.3f);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(2);
                b.Set(0, -9.0f);
                b.Set(1, 20.4f);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void different_sizes_are_not_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(3);
                b.Set(0, 9.0f);
                b.Set(1, -10.3f);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void instance_not_equal_to_null()
            {
                var v = new VectorF(4);

                Assert.False(v.Equals((VectorF)null));
            }
        }

        public class Object_Equals : VectorFTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var v = new VectorF(2);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void different_ref_same_components_are_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(2);
                b.Set(0, 9.0f);
                b.Set(1, -10.3f);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(2);
                b.Set(0, -9.0f);
                b.Set(1, 20.4f);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void different_sizes_are_not_equal()
            {
                var a = new VectorF(2);
                a.Set(0, 9.0f);
                a.Set(1, -10.3f);
                var b = new VectorF(3);
                b.Set(0, 9.0f);
                b.Set(1, -10.3f);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void instance_not_equal_to_null()
            {
                var v = new VectorF(4);

                Assert.False(v.Equals((object)null));
            }
        }

        public class GetHashCodeTests : VectorFTests
        {
            [Fact]
            public void same_vector_reference_has_same_hashcode_when_changed()
            {
                var m = new VectorF(2);
                m.Set(0, 9);
                m.Set(1, -8);
                var expectedHashCode = m.GetHashCode();
                m.Set(0, 4);
                m.Set(1, 19);

                Assert.Equal(expectedHashCode, m.GetHashCode());
            }

            [Fact]
            public void some_different_size_vectors_have_different_hashcodes()
            {
                Assert.NotEqual(new VectorF(2).GetHashCode(), new VectorF(0).GetHashCode());
                Assert.NotEqual(new VectorF(2).GetHashCode(), new VectorF(4).GetHashCode());
                Assert.NotEqual(new VectorF(2).GetHashCode(), new VectorF(10).GetHashCode());
            }
        }

        public class AddTests : VectorFTests
        {
            [Fact]
            public void adding_vectors_of_different_size_throws()
            {
                var left = new VectorF(3);
                var right = new VectorF(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetSum(right));
            }

            [Fact]
            public void adding_null_vector_throws()
            {
                var left = new VectorF(3);
                VectorF right = null;

                Assert.Throws<ArgumentNullException>(() => left.GetSum(right));
            }

            [Fact]
            public void adding_vectors_does_not_mutate_operands()
            {
                var left = new VectorF(3);
                left.Set(0, 1);
                left.Set(1, 3);
                left.Set(2, -4);
                var expectedLeft = new VectorF(left);
                var right = new VectorF(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expectedRight = new VectorF(right);

                left.GetSum(right);

                Assert.Equal(expectedRight, right);
                Assert.Equal(expectedLeft, left);
            }

            [Fact]
            public void can_add_vectors_of_same_size()
            {
                var left = new VectorF(3);
                left.Set(0, 1);
                left.Set(1, 3);
                left.Set(2, -4);
                var right = new VectorF(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expected = new VectorF(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, -24);

                var actual = left.GetSum(right);

                Assert.Equal(expected, actual);
            }
        }

        public class AddToTests : VectorFTests
        {
            [Fact]
            public void adding_vectors_of_different_size_throws()
            {
                var left = new VectorF(3);
                var right = new VectorF(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.Add(right));
            }

            [Fact]
            public void adding_null_vector_throws()
            {
                var left = new VectorF(3);
                VectorF right = null;

                Assert.Throws<ArgumentNullException>(() => left.Add(right));
            }

            [Fact]
            public void can_add_vectors_of_same_size()
            {
                var actual = new VectorF(3);
                actual.Set(0, 1);
                actual.Set(1, 3);
                actual.Set(2, -4);
                var right = new VectorF(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expected = new VectorF(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, -24);

                actual.Add(right);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractTests : VectorFTests
        {
            [Fact]
            public void subtracting_vectors_of_different_size_throws()
            {
                var left = new VectorF(3);
                var right = new VectorF(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetDiffernce(right));
            }

            [Fact]
            public void subtracting_null_vector_throws()
            {
                var left = new VectorF(3);
                VectorF right = null;

                Assert.Throws<ArgumentNullException>(() => left.GetDiffernce(right));
            }

            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new VectorF(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var right = new VectorF(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expected = new VectorF(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, 5);

                var actual = left.GetDiffernce(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new VectorF(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var expectedLeft = new VectorF(left);
                var right = new VectorF(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expectedRight = new VectorF(right);

                var result = left.GetDiffernce(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : VectorFTests
        {
            [Fact]
            public void subtracting_vectors_of_different_size_throws()
            {
                var left = new VectorF(3);
                var right = new VectorF(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.Subtract(right));
            }

            [Fact]
            public void subtracting_null_vector_throws()
            {
                var left = new VectorF(3);
                VectorF right = null;

                Assert.Throws<ArgumentNullException>(() => left.Subtract(right));
            }

            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new VectorF(3);
                actual.Set(0, 1);
                actual.Set(1, 10);
                actual.Set(2, 6);
                var right = new VectorF(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expected = new VectorF(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, 5);

                actual.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new VectorF(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var right = new VectorF(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expectedRight = new VectorF(right);

                left.Subtract(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : VectorFTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new VectorF(3);
                source.Set(0, 1);
                source.Set(1, 2);
                source.Set(2, -4);
                var expected = new VectorF(3);
                expected.Set(0, 3);
                expected.Set(1, 6);
                expected.Set(2, -12);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new VectorF(3);
                actual.Set(0, 1);
                actual.Set(1, 2);
                actual.Set(2, -4);
                var expected = new VectorF(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : VectorFTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new VectorF(3);
                actual.Set(0, 1);
                actual.Set(1, 2);
                actual.Set(2, -4);
                var expected = new VectorF(3);
                expected.Set(0, 3);
                expected.Set(1, 6);
                expected.Set(2, -12);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : VectorFTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new VectorF(2);
                source.Set(0, 4.5f);
                source.Set(1, 888f);
                var expected = new VectorF(2);
                expected.Set(0, 4.5f / 4.0f);
                expected.Set(1, 888f / 4.0f);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new VectorF(2);
                actual.Set(0, 4.5f);
                actual.Set(1, 888f);
                var expected = new VectorF(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : VectorFTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new VectorF(2);
                actual.Set(0, 4.5f);
                actual.Set(1, 888f);
                var expected = new VectorF(2);
                expected.Set(0, 4.5f / 4.0f);
                expected.Set(1, 888f / 4.0f);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }

        public class GetNegative : VectorFTests
        {
            [Fact]
            public void can_get_negative_vector()
            {
                var source = new VectorF(2);
                source.Set(0, 1.5f);
                source.Set(1, -6f);
                var expected = new VectorF(2);
                expected.Set(0, -1.5f);
                expected.Set(1, 6f);

                var actual = source.GetNegative();

                Assert.Equal(expected, actual);
            }
        }

        public class Negate : VectorFTests
        {
            [Fact]
            public void can_negate()
            {
                var actual = new VectorF(2);
                actual.Set(0, -8.8f);
                actual.Set(1, 6.1f);
                var expected = new VectorF(2);
                expected.Set(0, 8.8f);
                expected.Set(1, -6.1f);

                actual.Negate();

                Assert.Equal(expected, actual);
            }
        }

        public class Dot : VectorFTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var left = new VectorF(5);

                Assert.Throws<ArgumentNullException>(() => left.GetDot((VectorF)null));
            }

            [Fact]
            public void different_sizes_throws()
            {
                var left = new VectorF(5);
                var right = new VectorF(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetDot(right));
            }

            [Fact]
            public void zero_size_returns_zero()
            {
                var left = new VectorF(0);
                var right = new VectorF(0);
                var expected = 0.0;

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_dot()
            {
                var left = new VectorF(3);
                left.Set(0, 1.2f);
                left.Set(1, 3.0f);
                left.Set(2, -9.0f);
                var right = new VectorF(3);
                right.Set(0, -1.1f);
                right.Set(1, 6.7f);
                right.Set(2, 3.3f);
                var expected = (left[0] * right[0]) + (left[1] * right[1]) + (left[2] * right[2]);

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual, 5);
            }
        }

        public class GetMagnitude : VectorFTests
        {
            [Fact]
            public void zero_size_vector_has_zero_magnitude()
            {
                var vector = new VectorF(0);
                var expected = 0.0;

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_magnitude()
            {
                var vector = new VectorF(3);
                vector.Set(0, 3f);
                vector.Set(1, -4f);
                vector.Set(2, 5f);
                var expected = (float)Math.Sqrt(50.0f);

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude()
            {
                var vector = new VectorF(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitudeSquared : VectorFTests
        {
            [Fact]
            public void zero_size_vector_has_zero_squared_magnitude()
            {
                var vector = new VectorF(0);
                var expected = 0.0;

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_magnitude_squared()
            {
                var vector = new VectorF(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = 50.0;

                var actual = vector.GetMagnitudeSquared();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude_squared()
            {
                var vector = new VectorF(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetAngleBetween : VectorFTests
        {
            [Fact]
            public void null_argument_throws()
            {
                var vector = CreateIncremenetal(5);

                Assert.Throws<ArgumentNullException>(() => vector.GetAngleBetween((VectorF)null));
            }

            [Fact]
            public void wrong_vector_size_throws()
            {
                var left = CreateIncremenetal(3);
                var right = CreateIncremenetal(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetAngleBetween(right));
            }

            [Fact]
            public void self_angle_is_zero()
            {
                var vector = CreateIncremenetal(5);
                var expected = 0.0;

                var actual = vector.GetAngleBetween(vector);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void right_angle_is_half_pi()
            {
                var left = new VectorF(4);
                left.Set(0, 1);
                left.Set(2, 1);
                var right = new VectorF(4);
                right.Set(1, 1);
                right.Set(3, 1);
                var expected = (float)(Math.PI / 2.0);

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }

            [Fact]
            public void opposite_vector_angle_is_pi()
            {
                var left = CreateIncremenetal(6);
                var right = left.GetNegative();
                var expected = (float)Math.PI;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }

            [Fact]
            public void example_1()
            {
                var left = new VectorF(5);
                left.Set(0, 1);
                var right = new VectorF(5);
                right.Set(0, 1);
                right.Set(2, 1);
                var expected = (float)(Math.Acos(1.0 / Math.Sqrt(2.0)));

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 5);
            }

            [Fact]
            public void example_2()
            {
                var left = new VectorF(new[] { 1.0f, 3f, 4, 9, -13 });
                var right = new VectorF(new[] { -2, 5.6f, -9, 0.1f, 5 });
                var expected = 2.017320409990204f;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }
        }

        public class DistanceTests : VectorFTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var a = new VectorF(5);

                Assert.Throws<ArgumentNullException>(() => a.GetDistance((VectorF)null));
            }

            [Fact]
            public void wrong_size_throws()
            {
                var a = new VectorF(5);
                var b = new VectorF(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDistance(b));
            }

            [Fact]
            public void zero_lengths_have_zero_distance()
            {
                var a = new VectorF(0);
                var b = new VectorF(0);
                var expected = 0.0;

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance()
            {
                var a = new VectorF(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorF(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(3, 2);
                b.Set(4, 3);
                var expected = (float)Math.Sqrt(139.0);

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new VectorF(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorF(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(4, 3);
                var expected = a.GetDistance(b);

                var actual = b.GetDistance(a);

                Assert.Equal(expected, actual);
            }
        }

        public class DistanceSquaredTests : VectorFTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var a = new VectorF(5);

                Assert.Throws<ArgumentNullException>(() => a.GetDistanceSquared((VectorF)null));
            }

            [Fact]
            public void wrong_size_throws()
            {
                var a = new VectorF(5);
                var b = new VectorF(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDistanceSquared(b));
            }

            [Fact]
            public void zero_lengths_have_zero_distance()
            {
                var a = new VectorF(0);
                var b = new VectorF(0);
                var expected = 0.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance()
            {
                var a = new VectorF(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorF(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(3, 2);
                b.Set(4, 3);
                var expected = 139.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new VectorF(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorF(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(4, 3);
                var expected = a.GetDistanceSquared(b);

                var actual = b.GetDistanceSquared(a);

                Assert.Equal(expected, actual);
            }
        }

        public class ProtectedTests : VectorFTests
        {
            [Fact]
            public void projecting_different_dimensions_throws()
            {
                var u = new VectorF(5);
                var v = new VectorF(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => u.GetProjected(v));
            }

            [Fact]
            public void projecting_zero_size_returns_zero_vector()
            {
                var u = new VectorF(0);
                var v = new VectorF(0);
                var expected = new VectorF(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_against_zero_results_in_same_vector()
            {
                var u = new VectorF(4);
                var v = new VectorF(new[] { -3.0f, 8, 5, -10 });
                var expected = new VectorF(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_zero_against_another_vector_results_in_zero()
            {
                var u = new VectorF(new[] { 3.0f, -17, 5, -6 });
                var v = new VectorF(4);
                var expected = new VectorF(4);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_1()
            {
                var u = new VectorF(new[] { -1.0f, 2, 1, 3 });
                var v = new VectorF(new[] { 2.0f, -1, 3, 1 });
                var expected = u.GetScaled(2.0f / 15.0f);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_2()
            {
                var u = new VectorF(new[] { 1.0f, 2, 0 });
                var v = new VectorF(new[] { 1.0f, 2, 3 });
                var expected = new VectorF(u);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_3()
            {
                var u = new VectorF(new[] { 1.0f, 4 });
                var v = new VectorF(new[] { 6.0f, 7 });
                var expected = new VectorF(new[] { 2.0f, 8 });

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_4()
            {
                var u = new VectorF(new[] { 1.0f });
                var v = new VectorF(new[] { 6.0f });
                var expected = new VectorF(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }
        }

        public class NormalizeTests : VectorFTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var actual = new VectorF(4);
                var expected = new VectorF(4);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_axis_vector_normalizes_to_self()
            {
                var actual = VectorF.CreateUnit(4, 2);
                var expected = new VectorF(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var actual = new VectorF(new[] { 3.0f, 4, 5, -6 });
                var expected = actual.GetQuotient(actual.GetMagnitude());

                actual.Normalize();

                Assert.Equal(expected, actual);
            }
        }

        public class GetNormalTests : VectorFTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var vector = new VectorF(4);
                var expected = new VectorF(4);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_axis_vector_normalizes_to_self()
            {
                var vector = VectorF.CreateUnit(5, 1);
                var expected = new VectorF(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var vector = new VectorF(new[] { 3.0f, 4, 5, -6 });
                var expected = vector.GetQuotient(vector.GetMagnitude());

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }
        }

        public class PerpendicularDot : Vector2DTests
        {
            [Fact]
            public void zero_vector_perp_dot_is_zero()
            {
                var left = new Vector2D();
                var right = new Vector2D();
                var expected = 0.0;

                var actual = left.GetPerpendicularDot(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void self_vector_perp_dot_is_zero()
            {
                var left = new Vector2D(3, 2);
                var right = new Vector2D(left);
                var expected = 0.0;

                var actual = left.GetPerpendicularDot(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_1()
            {
                var left = new Vector2D(5, 2);
                var right = new Vector2D(3, -3);
                var expected = -21.0;

                var actual = left.GetPerpendicularDot(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void example_2()
            {
                var left = new Vector2D(3, -3);
                var right = new Vector2D(5, 2);
                var expected = 21.0;

                var actual = left.GetPerpendicularDot(right);

                Assert.Equal(expected, actual);
            }
        }

        protected VectorF CreateIncremenetal(int dimension)
        {
            var vector = new VectorF(dimension);
            for (int i = 0; i < dimension; i++)
            {
                vector.Set(i, i + 1);
            }

            return vector;
        }
    }
}
