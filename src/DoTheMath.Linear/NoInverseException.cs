using System;

namespace DoTheMath.Linear
{
#if PRE_CORE
    [Serializable]
#endif
    public sealed class NoInverseException : Exception
    {
        public NoInverseException()
        {
        }

#if PRE_CORE
        protected NoInverseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
