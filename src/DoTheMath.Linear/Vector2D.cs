using System.Runtime.CompilerServices;

namespace DoTheMath.Linear
{
    public struct Vector2D
    {
        public double X;

        public double Y;

#if !PRE_NETSTANDARD
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
