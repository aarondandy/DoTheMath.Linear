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

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix2D(1.0, -5.0, 9.0, -1.0);

                Assert.Equal( 1.0d, m.E00);
                Assert.Equal(-5.0d, m.E01);
                Assert.Equal( 9.0d, m.E10);
                Assert.Equal(-1.0d, m.E11);
            }
        }
    }
}
