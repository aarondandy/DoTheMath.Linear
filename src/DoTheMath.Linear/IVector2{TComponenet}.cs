namespace DoTheMath.Linear
{
    public interface IVector2<out TComponenet> : IVector<TComponenet>
    {
        TComponenet X { get; }

        TComponenet Y { get; }
    }
}
