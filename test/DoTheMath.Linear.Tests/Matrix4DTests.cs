using System;
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

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0, m.E01);
                Assert.Equal(9.0d, m.E02);
                Assert.Equal(0.1d, m.E03);
                Assert.Equal(-1.0, m.E10);
                Assert.Equal(8.0d, m.E11);
                Assert.Equal(-4.0, m.E12);
                Assert.Equal(-0.9, m.E13);
                Assert.Equal(21.0, m.E20);
                Assert.Equal(-0.5d, m.E21);
                Assert.Equal(1.4d, m.E22);
                Assert.Equal(-9.9d, m.E23);
                Assert.Equal(-101.9, m.E30);
                Assert.Equal(5.0, m.E31);
                Assert.Equal(-17.0, m.E32);
                Assert.Equal(19.3, m.E33);
            }
        }

        public class Properties : Matrix4DTests
        {
            [Fact]
            public void rows_and_cols_are_two()
            {
                var m = new Matrix4D();

                Assert.Equal(4, m.Rows);
                Assert.Equal(4, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix4D();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix4DTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix4D();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix4D();
                m.E00 = 1.0;
                m.E01 = 0.0;
                m.E02 = 0.0;
                m.E03 = 0.0;
                m.E10 = 0.0;
                m.E11 = 1.0;
                m.E12 = 0.0;
                m.E13 = 0.0;
                m.E20 = 0.0;
                m.E21 = 0.0;
                m.E22 = 1.0;
                m.E23 = 0.0;
                m.E30 = 0.0;
                m.E31 = 0.0;
                m.E32 = 0.0;
                m.E33 = 1.0;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix4D();
                m.E00 = 1.0;
                m.E01 = 2.0;
                m.E02 = 3.0;
                m.E03 = 4.0;
                m.E10 = 5.0;
                m.E11 = 6.0;
                m.E12 = 7.0;
                m.E13 = 8.0;
                m.E20 = 9.0;
                m.E21 = 0.0;
                m.E22 = 1.0;
                m.E23 = 2.0;
                m.E30 = 3.0;
                m.E31 = 4.0;
                m.E32 = 5.0;
                m.E33 = 6.0;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix4DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix4D(
                    1.0, -5.0, 9.0, 0.1,
                    -1.0, 8.0, -4.0, -0.9,
                    21.0, -0.5, 1.4, -9.9,
                    -101.9, 5.0, -17.0, 19.3);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(0.1d, m.Get(0, 3));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(-0.9, m.Get(1, 3));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
                Assert.Equal(-9.9d, m.Get(2, 3));
                Assert.Equal(-101.9, m.Get(3, 0));
                Assert.Equal(5.0, m.Get(3, 1));
                Assert.Equal(-17.0, m.Get(3, 2));
                Assert.Equal(19.3, m.Get(3, 3));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(4, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix4D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 4));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }
    }
}
