using System;

namespace DoTheMath.Linear
{
#if PRE_CORE
    [Serializable]
#endif
    public sealed class NotSquareMatrixException : Exception
    {
        public NotSquareMatrixException()
        {
        }

#if PRE_CORE
        private NotSquareMatrixException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
