namespace DoTheMath.Linear
{
    public interface IVector<out TComponenet>
    {
        int Dimensions { get; }

        TComponenet this[int index] { get; }

        TComponenet Get(int dimension);

        TComponenet GetMagnitude();

        TComponenet GetMagnitudeSquared();
    }
}
