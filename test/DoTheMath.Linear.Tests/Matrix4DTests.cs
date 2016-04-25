using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix4DTests
    {
        public class Constructors : Matrix4DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix4D();

                Assert.Equal(m.E00, 0.0d);
                Assert.Equal(m.E01, 0.0d);
                Assert.Equal(m.E02, 0.0d);
                Assert.Equal(m.E03, 0.0d);
                Assert.Equal(m.E10, 0.0d);
                Assert.Equal(m.E11, 0.0d);
                Assert.Equal(m.E12, 0.0d);
                Assert.Equal(m.E13, 0.0d);
                Assert.Equal(m.E20, 0.0d);
                Assert.Equal(m.E21, 0.0d);
                Assert.Equal(m.E22, 0.0d);
                Assert.Equal(m.E23, 0.0d);
                Assert.Equal(m.E30, 0.0d);
                Assert.Equal(m.E31, 0.0d);
                Assert.Equal(m.E32, 0.0d);
                Assert.Equal(m.E33, 0.0d);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(m.E00, 1.0d);
                Assert.Equal(m.E01, -5.0);
                Assert.Equal(m.E02, 9.0d);
                Assert.Equal(m.E03, 0.1d);
                Assert.Equal(m.E10, -1.0);
                Assert.Equal(m.E11, 8.0d);
                Assert.Equal(m.E12, -4.0);
                Assert.Equal(m.E13, -0.9);
                Assert.Equal(m.E20, 21.0);
                Assert.Equal(m.E21, -0.5d);
                Assert.Equal(m.E22, 1.4d);
                Assert.Equal(m.E23, -9.9d);
                Assert.Equal(m.E30, -101.9);
                Assert.Equal(m.E31, 5.0);
                Assert.Equal(m.E32, -17.0);
                Assert.Equal(m.E33, 19.3);
            }
        }
    }
}
