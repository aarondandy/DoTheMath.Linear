namespace DoTheMath.Linear
{
    public interface IVectorMutable<TComponenet> : IVector<TComponenet>
    {
        new TComponenet this[int index] { set;  get; }

        void Set(int dimension, TComponenet value);

        void Scale(TComponenet scalar);

        void Divide(TComponenet divisor);

        void Negate();
    }
}
