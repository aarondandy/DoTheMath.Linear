using System;
using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector3D : IVector<double>
    {
        public double X;

        public double Y;

        public double Z;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int Dimensions
        {
#if !PRE_NETSTANDARD
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
            get { return 3; }
        }

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public double Get(int dimension)
        {
            if (dimension == 0)
            {
                return X;
            }
            if (dimension == 1)
            {
                return Y;
            }
            if(dimension == 2)
            {
                return Z;
            }

            throw new ArgumentOutOfRangeException(nameof(dimension));
        }
    }
}
