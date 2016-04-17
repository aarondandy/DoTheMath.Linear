using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Vector3DTests
    {
        public class Constructors : Vector2DTests
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
    }
}
