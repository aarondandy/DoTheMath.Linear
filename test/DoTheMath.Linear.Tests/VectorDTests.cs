using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class VectorDTests
    {
        public class Constructors : VectorDTests
        {
            [Fact]
            public void size_constructor_sets_components_to_zero()
            {
                var v = new VectorD(15);

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
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(-40));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(int.MinValue));
            }

            [Fact]
            public void copy_constructor_throws_for_null()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorD((VectorD)null));
            }

            [Fact]
            public void copy_constructor_copies_all_componenets()
            {
                var expected = new VectorD(5);
                expected.Set(0, 0);
                expected.Set(1, 1);
                expected.Set(2, 2);
                expected.Set(3, 3);
                expected.Set(4, 4);

                var actual = new VectorD(expected);

                Assert.NotSame(expected, actual);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void null_componenets_array_constructor_throws()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorD((double[])null));
            }

            [Fact]
            public void array_constructor_assigns_componenets_and_length()
            {
                var components = new double[] { 4, 5, 6, 7 };
                var expected = new VectorD(4);
                expected.Set(0, 4);
                expected.Set(1, 5);
                expected.Set(2, 6);
                expected.Set(3, 7);

                var actual = new VectorD(components);

                Assert.Equal(components.Length, actual.Dimensions);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void array_constructor_copies_all_componenets()
            {
                var arrayValues = new[] { 1.0, 2.0, 3.0 };
                var expected = new VectorD(3);
                expected.Set(0, 1.0);
                expected.Set(1, 2.0);
                expected.Set(2, 3.0);

                var actual = new VectorD(arrayValues);
                arrayValues[0] = -99.0;
                arrayValues[1] = -99.0;
                arrayValues[2] = -99.0;

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_copies_componenets_from_ivector()
            {
                var expected = new VectorD(new[] { 1.0, 2.0, 3.3, 4.4 });

                var actual = new VectorD((IVector<double>)expected);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void copy_constructor_throws_for_null_ivector()
            {
                Assert.Throws<ArgumentNullException>(() => new VectorD((IVector<double>)null));
            }
        }

        public class Factories : VectorDTests
        {
            [Theory]
            [InlineData(1, 0)]
            [InlineData(3, 0)]
            [InlineData(3, 2)]
            [InlineData(3, 1)]
            public void can_create_axis_unit_vector(int size, int dimension)
            {
                var expected = new VectorD(size);
                expected.Set(dimension, 1.0);

                var actual = VectorD.CreateUnit(size, dimension);

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
                Assert.Throws<ArgumentOutOfRangeException>(() => VectorD.CreateUnit(size, dimension));
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-6)]
            public void create_axis_unit_vector_with_bad_size_throws(int size)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => VectorD.CreateUnit(size, 0));
            }
        }

        public class Get : VectorDTests
        {
            [Fact]
            public void can_get_componenets()
            {
                var v = new VectorD(new[] { 3.0, 4.0, 5.0 });

                Assert.Equal(3.0, v.Get(0));
                Assert.Equal(4.0, v.Get(1));
                Assert.Equal(5.0, v.Get(2));
            }

            [Fact]
            public void get_negative_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-4));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-5));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void get_large_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(5));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(6));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(101));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }

        public class Set : VectorDTests
        {
            [Fact]
            public void can_set_componenets()
            {
                var v = new VectorD(new[] { 2.0, 6.0, 7.0 });

                v.Set(0, 3.0);
                v.Set(1, 4.0);
                v.Set(2, 5.0);

                Assert.Equal(3.0, v.Get(0));
                Assert.Equal(4.0, v.Get(1));
                Assert.Equal(5.0, v.Get(2));
            }

            [Fact]
            public void set_negative_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-1, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-4, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(-5, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MinValue, 0.0));
            }

            [Fact]
            public void set_large_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(5, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(6, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(101, 0.0));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Set(int.MaxValue, 0.0));
            }
        }

        public class IndexerGet : VectorDTests
        {
            [Fact]
            public void can_get_componenets()
            {
                var v = new VectorD(new[] { 3.0, 4.0, 5.0 });

                Assert.Equal(3.0, v[0]);
                Assert.Equal(4.0, v[1]);
                Assert.Equal(5.0, v[2]);
            }

            [Fact]
            public void get_negative_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[-1]);
                Assert.Throws<IndexOutOfRangeException>(() => v[-4]);
                Assert.Throws<IndexOutOfRangeException>(() => v[-5]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue]);
            }

            [Fact]
            public void get_large_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[v.Dimensions]);
                Assert.Throws<IndexOutOfRangeException>(() => v[5]);
                Assert.Throws<IndexOutOfRangeException>(() => v[6]);
                Assert.Throws<IndexOutOfRangeException>(() => v[101]);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue]);
            }
        }

        public class IndexerSet : VectorDTests
        {
            [Fact]
            public void can_set_componenets()
            {
                var v = new VectorD(new[] { 2.0, 6.0, 7.0 });

                v[0] = 3.0;
                v[1] = 4.0;
                v[2] = 5.0;

                Assert.Equal(3.0, v[0]);
                Assert.Equal(4.0, v[1]);
                Assert.Equal(5.0, v[2]);
            }

            [Fact]
            public void set_negative_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[-1] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[-4] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[-5] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MinValue] = 0.0);
            }

            [Fact]
            public void set_large_dimension_throws()
            {
                var v = new VectorD(5);

                Assert.Throws<IndexOutOfRangeException>(() => v[v.Dimensions] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[5] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[6] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[101] = 0.0);
                Assert.Throws<IndexOutOfRangeException>(() => v[int.MaxValue] = 0.0);
            }
        }

        public class GetAndSet : VectorDTests
        {
            [Fact]
            public void get_all_element_for_dim_5()
            {
                var v = new VectorD(5);
                v.Set(0, -1.0);
                v.Set(1, 5.7);
                v.Set(2, -0.4);
                v.Set(3, 9.0);
                v.Set(4, -101.1);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.7, v.Get(1));
                Assert.Equal(-0.4, v.Get(2));
                Assert.Equal(9.0, v.Get(3));
                Assert.Equal(-101.1, v.Get(4));
            }
        }

        public class IEquatable_Self_Equals : VectorDTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var v = new VectorD(2);

                Assert.True(v.Equals(v));
            }

            [Fact]
            public void different_ref_same_components_are_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(2);
                b.Set(0, 9.0);
                b.Set(1, -10.3);

                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(2);
                b.Set(0, -9.0);
                b.Set(1, 20.4);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void different_sizes_are_not_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(3);
                b.Set(0, 9.0);
                b.Set(1, -10.3);

                Assert.False(a.Equals(b));
                Assert.False(b.Equals(a));
            }

            [Fact]
            public void instance_not_equal_to_null()
            {
                var v = new VectorD(4);

                Assert.False(v.Equals((VectorD)null));
            }
        }

        public class Object_Equals : VectorDTests
        {
            [Fact]
            public void same_ref_are_equal()
            {
                var v = new VectorD(2);

                Assert.True(v.Equals((object)v));
            }

            [Fact]
            public void different_ref_same_components_are_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(2);
                b.Set(0, 9.0);
                b.Set(1, -10.3);

                Assert.True(a.Equals((object)b));
                Assert.True(b.Equals((object)a));
            }

            [Fact]
            public void different_components_are_not_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(2);
                b.Set(0, -9.0);
                b.Set(1, 20.4);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void different_sizes_are_not_equal()
            {
                var a = new VectorD(2);
                a.Set(0, 9.0);
                a.Set(1, -10.3);
                var b = new VectorD(3);
                b.Set(0, 9.0);
                b.Set(1, -10.3);

                Assert.False(a.Equals((object)b));
                Assert.False(b.Equals((object)a));
            }

            [Fact]
            public void instance_not_equal_to_null()
            {
                var v = new VectorD(4);

                Assert.False(v.Equals((object)null));
            }
        }

        public class GetHashCodeTests : VectorDTests
        {
            [Fact]
            public void same_vector_reference_has_same_hashcode_when_changed()
            {
                var m = new VectorD(2);
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
                Assert.NotEqual(new VectorD(2).GetHashCode(), new VectorD(0).GetHashCode());
                Assert.NotEqual(new VectorD(2).GetHashCode(), new VectorD(4).GetHashCode());
                Assert.NotEqual(new VectorD(2).GetHashCode(), new VectorD(10).GetHashCode());
            }
        }

        public class AddTests : VectorDTests
        {
            [Fact]
            public void adding_vectors_of_different_size_throws()
            {
                var left = new VectorD(3);
                var right = new VectorD(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetSum(right));
            }

            [Fact]
            public void adding_null_vector_throws()
            {
                var left = new VectorD(3);
                VectorD right = null;

                Assert.Throws<ArgumentNullException>(() => left.GetSum(right));
            }

            [Fact]
            public void adding_vectors_does_not_mutate_operands()
            {
                var left = new VectorD(3);
                left.Set(0, 1);
                left.Set(1, 3);
                left.Set(2, -4);
                var expectedLeft = new VectorD(left);
                var right = new VectorD(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expectedRight = new VectorD(right);

                left.GetSum(right);

                Assert.Equal(expectedRight, right);
                Assert.Equal(expectedLeft, left);
            }

            [Fact]
            public void can_add_vectors_of_same_size()
            {
                var left = new VectorD(3);
                left.Set(0, 1);
                left.Set(1, 3);
                left.Set(2, -4);
                var right = new VectorD(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expected = new VectorD(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, -24);

                var actual = left.GetSum(right);

                Assert.Equal(expected, actual);
            }
        }

        public class AddToTests : VectorDTests
        {
            [Fact]
            public void adding_vectors_of_different_size_throws()
            {
                var left = new VectorD(3);
                var right = new VectorD(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.Add(right));
            }

            [Fact]
            public void adding_null_vector_throws()
            {
                var left = new VectorD(3);
                VectorD right = null;

                Assert.Throws<ArgumentNullException>(() => left.Add(right));
            }

            [Fact]
            public void can_add_vectors_of_same_size()
            {
                var actual = new VectorD(3);
                actual.Set(0, 1);
                actual.Set(1, 3);
                actual.Set(2, -4);
                var right = new VectorD(3);
                right.Set(0, -10);
                right.Set(1, 4);
                right.Set(2, -20);
                var expected = new VectorD(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, -24);

                actual.Add(right);

                Assert.Equal(expected, actual);
            }
        }

        public class SubtractTests : VectorDTests
        {
            [Fact]
            public void subtracting_vectors_of_different_size_throws()
            {
                var left = new VectorD(3);
                var right = new VectorD(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetDiffernce(right));
            }

            [Fact]
            public void subtracting_null_vector_throws()
            {
                var left = new VectorD(3);
                VectorD right = null;

                Assert.Throws<ArgumentNullException>(() => left.GetDiffernce(right));
            }

            [Fact]
            public void subtracting_vector_produces_diff_vector()
            {
                var left = new VectorD(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var right = new VectorD(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expected = new VectorD(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, 5);

                var actual = left.GetDiffernce(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_operands_unchanged()
            {
                var left = new VectorD(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var expectedLeft = new VectorD(left);
                var right = new VectorD(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expectedRight = new VectorD(right);

                var result = left.GetDiffernce(right);

                Assert.Equal(expectedLeft, left);
                Assert.Equal(expectedRight, right);
            }
        }

        public class SubtractFromTests : VectorDTests
        {
            [Fact]
            public void subtracting_vectors_of_different_size_throws()
            {
                var left = new VectorD(3);
                var right = new VectorD(4);
                Assert.NotEqual(left.Dimensions, right.Dimensions);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.Subtract(right));
            }

            [Fact]
            public void subtracting_null_vector_throws()
            {
                var left = new VectorD(3);
                VectorD right = null;

                Assert.Throws<ArgumentNullException>(() => left.Subtract(right));
            }

            [Fact]
            public void subtracting_vector_subtracts_from_components()
            {
                var actual = new VectorD(3);
                actual.Set(0, 1);
                actual.Set(1, 10);
                actual.Set(2, 6);
                var right = new VectorD(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expected = new VectorD(3);
                expected.Set(0, -9);
                expected.Set(1, 7);
                expected.Set(2, 5);

                actual.Subtract(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void subtracting_vectors_leaves_right_unchanged()
            {
                var left = new VectorD(3);
                left.Set(0, 1);
                left.Set(1, 10);
                left.Set(2, 6);
                var right = new VectorD(3);
                right.Set(0, 10);
                right.Set(1, 3);
                right.Set(2, 1);
                var expectedRight = new VectorD(right);

                left.Subtract(right);

                Assert.Equal(expectedRight, right);
            }
        }

        public class GetScaledTests : VectorDTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var source = new VectorD(3);
                source.Set(0, 1);
                source.Set(1, 2);
                source.Set(2, -4);
                var expected = new VectorD(3);
                expected.Set(0, 3);
                expected.Set(1, 6);
                expected.Set(2, -12);

                var actual = source.GetScaled(3);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new VectorD(3);
                actual.Set(0, 1);
                actual.Set(1, 2);
                actual.Set(2, -4);
                var expected = new VectorD(actual);

                var result = actual.GetScaled(2);

                Assert.Equal(expected, actual);
            }
        }

        public class ScaleTests : VectorDTests
        {
            [Fact]
            public void can_get_scaled_vector()
            {
                var actual = new VectorD(3);
                actual.Set(0, 1);
                actual.Set(1, 2);
                actual.Set(2, -4);
                var expected = new VectorD(3);
                expected.Set(0, 3);
                expected.Set(1, 6);
                expected.Set(2, -12);

                actual.Scale(3);

                Assert.Equal(expected, actual);
            }
        }

        public class GetQuotientTests : VectorDTests
        {
            [Fact]
            public void can_get_quotient_vector()
            {
                var source = new VectorD(2);
                source.Set(0, 4.5);
                source.Set(1, 888);
                var expected = new VectorD(2);
                expected.Set(0, 4.5 / 4.0);
                expected.Set(1, 888 / 4.0);

                var actual = source.GetQuotient(4);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void source_vector_is_unchanged()
            {
                var actual = new VectorD(2);
                actual.Set(0, 4.5);
                actual.Set(1, 888);
                var expected = new VectorD(actual);

                var result = actual.GetQuotient(2);

                Assert.Equal(expected, actual);
            }
        }

        public class DivideTests : VectorDTests
        {
            [Fact]
            public void can_divide_vector()
            {
                var actual = new VectorD(2);
                actual.Set(0, 4.5);
                actual.Set(1, 888);
                var expected = new VectorD(2);
                expected.Set(0, 4.5 / 4.0);
                expected.Set(1, 888 / 4.0);

                actual.Divide(4);

                Assert.Equal(expected, actual);
            }
        }

        public class GetNegative : VectorDTests
        {
            [Fact]
            public void can_get_negative_vector()
            {
                var source = new VectorD(2);
                source.Set(0, 1.5);
                source.Set(1, -6);
                var expected = new VectorD(2);
                expected.Set(0, -1.5);
                expected.Set(1, 6);

                var actual = source.GetNegative();

                Assert.Equal(expected, actual);
            }
        }

        public class Negate : VectorDTests
        {
            [Fact]
            public void can_negate()
            {
                var actual = new VectorD(2);
                actual.Set(0, -8.8);
                actual.Set(1, 6.1);
                var expected = new VectorD(2);
                expected.Set(0, 8.8);
                expected.Set(1, -6.1);

                actual.Negate();

                Assert.Equal(expected, actual);
            }
        }

        public class Dot : VectorDTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var left = new VectorD(5);

                Assert.Throws<ArgumentNullException>(() => left.GetDot((VectorD)null));
            }

            [Fact]
            public void different_sizes_throws()
            {
                var left = new VectorD(5);
                var right = new VectorD(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => left.GetDot(right));
            }

            [Fact]
            public void zero_size_returns_zero()
            {
                var left = new VectorD(0);
                var right = new VectorD(0);
                var expected = 0.0;

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_dot()
            {
                var left = new VectorD(3);
                left.Set(0, 1.2);
                left.Set(1, 3.0);
                left.Set(2, -9.0);
                var right = new VectorD(3);
                right.Set(0, -1.1);
                right.Set(1, 6.7);
                right.Set(2, 3.3);
                var expected = (1.2 * -1.1) + (3.0 * 6.7) + (-9.0 * 3.3);

                var actual = left.GetDot(right);

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitude : VectorDTests
        {
            [Fact]
            public void zero_size_vector_has_zero_magnitude()
            {
                var vector = new VectorD(0);
                var expected = 0.0;

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_magnitude()
            {
                var vector = new VectorD(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = Math.Sqrt(50.0);

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void negative_values_produce_same_magnitude()
            {
                var vector = new VectorD(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetMagnitudeSquared : VectorDTests
        {
            [Fact]
            public void zero_size_vector_has_zero_squared_magnitude()
            {
                var vector = new VectorD(0);
                var expected = 0.0;

                var actual = vector.GetMagnitude();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_get_magnitude_squared()
            {
                var vector = new VectorD(3);
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
                var vector = new VectorD(3);
                vector.Set(0, 3);
                vector.Set(1, -4);
                vector.Set(2, 5);
                var expected = vector.GetMagnitude();

                var actual = vector.GetNegative().GetMagnitude();

                Assert.Equal(expected, actual);
            }
        }

        public class GetAngleBetween : VectorDTests
        {
            [Fact]
            public void null_argument_throws()
            {
                var vector = CreateIncremenetal(5);

                Assert.Throws<ArgumentNullException>(() => vector.GetAngleBetween((VectorD)null));
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
                var left = new VectorD(4);
                left.Set(0, 1);
                left.Set(2, 1);
                var right = new VectorD(4);
                right.Set(1, 1);
                right.Set(3, 1);
                var expected = Math.PI / 2.0;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void opposite_vector_angle_is_pi()
            {
                var left = CreateIncremenetal(6);
                var right = left.GetNegative();
                var expected = Math.PI;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_1()
            {
                var left = new VectorD(5);
                left.Set(0, 1);
                var right = new VectorD(5);
                right.Set(0, 1);
                right.Set(2, 1);
                var expected = Math.Acos(1.0 / Math.Sqrt(2.0));

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }

            [Fact]
            public void example_2()
            {
                var left = new VectorD(new[] { 1.0, 3, 4, 9, -13 });
                var right = new VectorD(new[] { -2, 5.6, -9, 0.1, 5 });
                var expected = 2.017320409990204;

                var actual = left.GetAngleBetween(right);

                Assert.Equal(expected, actual, 10);
            }
        }

        public class DistanceTests : VectorDTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var a = new VectorD(5);

                Assert.Throws<ArgumentNullException>(() => a.GetDistance((VectorD)null));
            }

            [Fact]
            public void wrong_size_throws()
            {
                var a = new VectorD(5);
                var b = new VectorD(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDistance(b));
            }

            [Fact]
            public void zero_lengths_have_zero_distance()
            {
                var a = new VectorD(0);
                var b = new VectorD(0);
                var expected = 0.0;

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance()
            {
                var a = new VectorD(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorD(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(3, 2);
                b.Set(4, 3);
                var expected = Math.Sqrt(139.0);

                var actual = a.GetDistance(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance_in_both_directions()
            {
                var a = new VectorD(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorD(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(4, 3);
                var expected = a.GetDistance(b);

                var actual = b.GetDistance(a);

                Assert.Equal(expected, actual);
            }
        }

        public class DistanceSquaredTests : VectorDTests
        {
            [Fact]
            public void null_vector_throws()
            {
                var a = new VectorD(5);

                Assert.Throws<ArgumentNullException>(() => a.GetDistanceSquared((VectorD)null));
            }

            [Fact]
            public void wrong_size_throws()
            {
                var a = new VectorD(5);
                var b = new VectorD(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => a.GetDistanceSquared(b));
            }

            [Fact]
            public void zero_lengths_have_zero_distance()
            {
                var a = new VectorD(0);
                var b = new VectorD(0);
                var expected = 0.0;

                var actual = a.GetDistanceSquared(b);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_find_distance()
            {
                var a = new VectorD(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorD(5);
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
                var a = new VectorD(5);
                a.Set(0, 1);
                a.Set(1, 2);
                a.Set(2, 3);
                var b = new VectorD(5);
                b.Set(1, -9);
                b.Set(2, 5);
                b.Set(4, 3);
                var expected = a.GetDistanceSquared(b);

                var actual = b.GetDistanceSquared(a);

                Assert.Equal(expected, actual);
            }
        }

        public class ProtectedTests : VectorDTests
        {
            [Fact]
            public void projecting_different_dimensions_throws()
            {
                var u = new VectorD(5);
                var v = new VectorD(3);

                Assert.Throws<ArgumentOutOfRangeException>(() => u.GetProjected(v));
            }

            [Fact]
            public void projecting_zero_size_returns_zero_vector()
            {
                var u = new VectorD(0);
                var v = new VectorD(0);
                var expected = new VectorD(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_against_zero_results_in_same_vector()
            {
                var u = new VectorD(4);
                var v = new VectorD(new[] { -3.0, 8, 5, -10 });
                var expected = new VectorD(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projecting_zero_against_another_vector_results_in_zero()
            {
                var u = new VectorD(new[] { 3.0, -17, 5, -6 });
                var v = new VectorD(4);
                var expected = new VectorD(4);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_1()
            {
                var u = new VectorD(new[] { -1.0, 2, 1, 3 });
                var v = new VectorD(new[] { 2.0, -1, 3, 1 });
                var expected = new VectorD(new[] { -2.0 / 15.0, 4.0 / 15.0, 2.0 / 15.0, 2.0 / 5.0 });

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_2()
            {
                var u = new VectorD(new[] { 1.0, 2, 0 });
                var v = new VectorD(new[] { 1.0, 2, 3 });
                var expected = new VectorD(u);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_3()
            {
                var u = new VectorD(new[] { 1.0, 4 });
                var v = new VectorD(new[] { 6.0, 7 });
                var expected = new VectorD(new[] { 2.0, 8 });

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void projection_example_4()
            {
                var u = new VectorD(new[] { 1.0 });
                var v = new VectorD(new[] { 6.0 });
                var expected = new VectorD(v);

                var actual = u.GetProjected(v);

                Assert.Equal(expected, actual);
            }
        }

        public class NormalizeTests : VectorDTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var actual = new VectorD(4);
                var expected = new VectorD(4);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_axis_vector_normalizes_to_self()
            {
                var actual = VectorD.CreateUnit(4, 2);
                var expected = new VectorD(actual);

                actual.Normalize();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var actual = new VectorD(new[] { 3.0, 4, 5, -6 });
                var expected = actual.GetQuotient(actual.GetMagnitude());

                actual.Normalize();

                Assert.Equal(expected, actual);
            }
        }

        public class GetNormalTests : VectorDTests
        {
            [Fact]
            public void zero_vector_normalizes_to_zero()
            {
                var vector = new VectorD(4);
                var expected = new VectorD(4);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void unit_axis_vector_normalizes_to_self()
            {
                var vector = VectorD.CreateUnit(5, 1);
                var expected = new VectorD(vector);

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void can_normalize_sample()
            {
                var vector = new VectorD(new[] { 3.0, 4, 5, -6 });
                var expected = vector.GetQuotient(vector.GetMagnitude());

                var actual = vector.GetNormal();

                Assert.Equal(expected, actual);
            }
        }

        protected VectorD CreateIncremenetal(int dimension)
        {
            var vector = new VectorD(dimension);
            for (int i = 0; i < dimension; i++)
            {
                vector.Set(i, i + 1);
            }

            return vector;
        }
    }
}
