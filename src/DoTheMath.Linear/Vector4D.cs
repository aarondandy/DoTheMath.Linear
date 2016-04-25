using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector4D
    {
        public double X;

        public double Y;

        public double Z;

        public double W;

#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}