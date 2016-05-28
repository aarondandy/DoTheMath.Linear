namespace DoTheMath.Linear
{
    public interface IVector<TComponenet>
    {
        int Dimensions { get; }

        TComponenet Get(int dimension);

        TComponenet GetMagnitude();

        TComponenet GetMagnitudeSquared();
    }
}
