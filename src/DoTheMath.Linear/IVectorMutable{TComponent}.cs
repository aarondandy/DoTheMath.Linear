namespace DoTheMath.Linear
{
    public interface IVectorMutable<TComponenet> : IVector<TComponenet>
    {
        void Set(int dimension, TComponenet value);

        void Scale(TComponenet scalar);

        void Divide(TComponenet divisor);
    }
}
