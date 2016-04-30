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
    }
}
