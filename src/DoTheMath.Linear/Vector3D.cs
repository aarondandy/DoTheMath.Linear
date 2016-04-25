﻿using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector3D
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
    }
}
