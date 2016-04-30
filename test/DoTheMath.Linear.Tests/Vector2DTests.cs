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
    }
}
