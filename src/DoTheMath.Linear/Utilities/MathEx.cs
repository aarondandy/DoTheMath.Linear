using System.Runtime.CompilerServices;

namespace DoTheMath.Linear.Utilities
{
    internal static class MathEx
    {
#if !PRE_NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Negate(ref double value)
        {
            value = -value;
        }
    }
}
