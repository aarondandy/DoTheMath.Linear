namespace DoTheMath.Linear
{
    public interface IVector2<TComponenet> : IVector<TComponenet>
    {
        TComponenet X { get; }

        TComponenet Y { get; }
    }
}
