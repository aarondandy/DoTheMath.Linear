﻿using Xunit;

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

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E02);
                Assert.Equal(0.0d, m.E03);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
                Assert.Equal(0.0d, m.E12);
                Assert.Equal(0.0d, m.E13);
                Assert.Equal(0.0d, m.E20);
                Assert.Equal(0.0d, m.E21);
                Assert.Equal(0.0d, m.E22);
                Assert.Equal(0.0d, m.E23);
                Assert.Equal(0.0d, m.E30);
                Assert.Equal(0.0d, m.E31);
                Assert.Equal(0.0d, m.E32);
                Assert.Equal(0.0d, m.E33);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(  1.0d, m.E00);
                Assert.Equal(  -5.0, m.E01);
                Assert.Equal(  9.0d, m.E02);
                Assert.Equal(  0.1d, m.E03);
                Assert.Equal(  -1.0, m.E10);
                Assert.Equal(  8.0d, m.E11);
                Assert.Equal(  -4.0, m.E12);
                Assert.Equal(  -0.9, m.E13);
                Assert.Equal(  21.0, m.E20);
                Assert.Equal( -0.5d, m.E21);
                Assert.Equal(  1.4d, m.E22);
                Assert.Equal( -9.9d, m.E23);
                Assert.Equal(-101.9, m.E30);
                Assert.Equal(   5.0, m.E31);
                Assert.Equal( -17.0, m.E32);
                Assert.Equal(  19.3, m.E33);
            }
        }
    }
}
