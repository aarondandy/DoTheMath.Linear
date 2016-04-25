using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix3DTests
    {
        public class Constructors : Matrix3DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix3D();

                Assert.Equal(m.E00, 0.0d);
                Assert.Equal(m.E01, 0.0d);
                Assert.Equal(m.E02, 0.0d);
                Assert.Equal(m.E10, 0.0d);
                Assert.Equal(m.E11, 0.0d);
                Assert.Equal(m.E12, 0.0d);
                Assert.Equal(m.E20, 0.0d);
                Assert.Equal(m.E21, 0.0d);
                Assert.Equal(m.E22, 0.0d);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(m.E00, 1.0d);
                Assert.Equal(m.E01, -5.0);
                Assert.Equal(m.E02, 9.0d);
                Assert.Equal(m.E10, -1.0);
                Assert.Equal(m.E11, 8.0d);
                Assert.Equal(m.E12, -4.0);
                Assert.Equal(m.E20, 21.0);
                Assert.Equal(m.E21, -0.5d);
                Assert.Equal(m.E22, 1.4d);
            }
        }
    }
}
