using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix2DTests
    {
        public class Constructors : Matrix2DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix2D();

                Assert.Equal(m.E00, 0.0d);
                Assert.Equal(m.E01, 0.0d);
                Assert.Equal(m.E10, 0.0d);
                Assert.Equal(m.E11, 0.0d);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix2D(1.0, -5.0, 9.0, -1.0);

                Assert.Equal(m.E00, 1.0d);
                Assert.Equal(m.E01, -5.0d);
                Assert.Equal(m.E10, 9.0d);
                Assert.Equal(m.E11, -1.0d);
            }
        }
    }
}
