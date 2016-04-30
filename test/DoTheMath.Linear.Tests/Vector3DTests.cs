using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector3DTests
    {
        public class Constructors : Vector3DTests
        {
            [Fact]
            public void default_constructor_sets_components_to_zero()
            {
                var v = new Vector3D();

                Assert.Equal(v.X, 0.0d);
                Assert.Equal(v.Y, 0.0d);
                Assert.Equal(v.Z, 0.0d);
            }

            [Fact]
            public void component_constructor_assigns_correct_fields()
            {
                var v = new Vector3D(1.0, -5.0, 6.0);

                Assert.Equal(v.X, 1.0);
                Assert.Equal(v.Y, -5.0);
                Assert.Equal(v.Z, 6.0);
            }
        }

        public class Get : Vector3DTests
        {
            [Fact]
            public void can_get_all_componenets()
            {
                var v = new Vector3D(-1.0, 5.0, -0.5);

                Assert.Equal(-1.0, v.Get(0));
                Assert.Equal(5.0, v.Get(1));
                Assert.Equal(-0.5, v.Get(2));
            }

            [Fact]
            public void negative_dimension_throws()
            {
                var v = new Vector3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(-1));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MinValue));
            }

            [Fact]
            public void large_dimension_throws()
            {
                var v = new Vector3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(3));
                Assert.Throws<ArgumentOutOfRangeException>(() => v.Get(int.MaxValue));
            }
        }
    }
}
