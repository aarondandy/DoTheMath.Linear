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
            public void negative_size_throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(-40));
                Assert.Throws<ArgumentOutOfRangeException>(() => new VectorD(int.MinValue));
            }
        }

        public class Get : VectorDTests
        {
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
    }
}
