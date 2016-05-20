using System;

namespace DoTheMath.Linear
{
#if PRE_CORE
    [Serializable]
#endif
    public sealed class NoDeterminantException : Exception
    {
        public NoDeterminantException()
        {
        }

#if PRE_CORE
        protected NoDeterminantException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
