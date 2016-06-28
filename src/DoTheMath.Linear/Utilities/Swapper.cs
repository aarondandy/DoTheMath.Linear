using System.Runtime.CompilerServices;

namespace DoTheMath.Linear.Utilities
{
    internal static class Swapper
    {
#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void SwapPairs<T>(ref T a1, ref T b1, ref T a2, ref T b2)
        {
            Swap(ref a1, ref b1);
            Swap(ref a2, ref b2);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void SwapPairs<T>(ref T a1, ref T b1, ref T a2, ref T b2, ref T a3, ref T b3)
        {
            SwapPairs(ref a1, ref b1, ref a2, ref b2);
            Swap(ref a3, ref b3);
        }

#if !PRE_NETSTANDARD && !DEBUG
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void SwapPairs<T>(ref T a1, ref T b1, ref T a2, ref T b2, ref T a3, ref T b3, ref T a4, ref T b4)
        {
            SwapPairs(ref a1, ref b1, ref a2, ref b2);
            SwapPairs(ref a3, ref b3, ref a4, ref b4);
        }
    }
}
