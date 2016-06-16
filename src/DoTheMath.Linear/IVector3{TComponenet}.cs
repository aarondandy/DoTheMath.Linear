namespace DoTheMath.Linear
{
    public interface IVector3<out TComponenet> : IVector<TComponenet>
    {
        TComponenet X { get; }

        TComponenet Y { get; }

        TComponenet Z { get; }
    }
}
