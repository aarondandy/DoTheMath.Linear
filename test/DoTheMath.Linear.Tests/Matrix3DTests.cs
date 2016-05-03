﻿using System;
using Xunit;

namespace DoTheMath.Linear.Tests
{
    public class Matrix3DTests
    {
        public class ConstructorsAndFactories : Matrix3DTests
        {
            [Fact]
            public void default_constructor_sets_elements_to_zero()
            {
                var m = new Matrix3D();

                Assert.Equal(0.0d, m.E00);
                Assert.Equal(0.0d, m.E01);
                Assert.Equal(0.0d, m.E02);
                Assert.Equal(0.0d, m.E10);
                Assert.Equal(0.0d, m.E11);
                Assert.Equal(0.0d, m.E12);
                Assert.Equal(0.0d, m.E20);
                Assert.Equal(0.0d, m.E21);
                Assert.Equal(0.0d, m.E22);
            }

            [Fact]
            public void element_constructor_assigns_correct_fields()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(1.0d, m.E00);
                Assert.Equal(-5.0, m.E01);
                Assert.Equal(9.0d, m.E02);
                Assert.Equal(-1.0, m.E10);
                Assert.Equal(8.0d, m.E11);
                Assert.Equal(-4.0, m.E12);
                Assert.Equal(21.0, m.E20);
                Assert.Equal(-0.5d, m.E21);
                Assert.Equal(1.4d, m.E22);
            }

            [Fact]
            public void identity_factory_constructs_identity_matrix()
            {
                var m = Matrix3D.CreateIdentity();

                Assert.True(m.IsIdentity);
            }
        }

        public class Properties : Matrix3DTests
        {
            [Fact]
            public void rows_and_cols_are_two()
            {
                var m = new Matrix3D();

                Assert.Equal(3, m.Rows);
                Assert.Equal(3, m.Columns);
            }

            [Fact]
            public void is_square()
            {
                var m = new Matrix3D();

                Assert.True(m.IsSquare);
            }
        }

        public class IsIdentity : Matrix3DTests
        {
            [Fact]
            public void default_matrix_is_not_identity()
            {
                var m = new Matrix3D();

                Assert.False(m.IsIdentity);
            }

            [Fact]
            public void explicit_identity_matrix_detected()
            {
                var m = new Matrix3D();
                m.E00 = 1.0;
                m.E01 = 0.0;
                m.E02 = 0.0;
                m.E10 = 0.0;
                m.E11 = 1.0;
                m.E12 = 0.0;
                m.E20 = 0.0;
                m.E21 = 0.0;
                m.E22 = 1.0;

                Assert.True(m.IsIdentity);
            }

            [Fact]
            public void assorted_values_are_not_identity()
            {
                var m = new Matrix3D();
                m.E00 = 1.0;
                m.E01 = 2.0;
                m.E02 = 3.0;
                m.E10 = 4.0;
                m.E11 = 5.0;
                m.E12 = 6.0;
                m.E20 = 7.0;
                m.E21 = 8.0;
                m.E22 = 9.0;

                Assert.False(m.IsIdentity);
            }
        }

        public class Get : Matrix3DTests
        {
            [Fact]
            public void can_get_all_elements()
            {
                var m = new Matrix3D(
                    1.0, -5.0, 9.0,
                    -1.0, 8.0, -4.0,
                    21.0, -0.5, 1.4);

                Assert.Equal(1.0d, m.Get(0, 0));
                Assert.Equal(-5.0, m.Get(0, 1));
                Assert.Equal(9.0d, m.Get(0, 2));
                Assert.Equal(-1.0, m.Get(1, 0));
                Assert.Equal(8.0d, m.Get(1, 1));
                Assert.Equal(-4.0, m.Get(1, 2));
                Assert.Equal(21.0, m.Get(2, 0));
                Assert.Equal(-0.5d, m.Get(2, 1));
                Assert.Equal(1.4d, m.Get(2, 2));
            }

            [Fact]
            public void invalid_rows_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(-1, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(3, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MinValue, 0));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(int.MaxValue, 0));
            }

            [Fact]
            public void invalid_columns_throw()
            {
                var m = new Matrix3D();

                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, 3));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MinValue));
                Assert.Throws<ArgumentOutOfRangeException>(() => m.Get(0, int.MaxValue));
            }
        }
    }
}
