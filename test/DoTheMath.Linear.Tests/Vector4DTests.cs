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
                var expected = new Vector4D(0, 1, 2, 3);

                var actual = new Vector4D(expected);

                Assert.NotSame(expected, actual);
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
    }
}
