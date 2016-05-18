namespace DoTheMath.Linear
{
    public interface IVector3<TComponenet> : IVector<TComponenet>
    {
        TComponenet X { get; }

        TComponenet Y { get; }

        TComponenet Z { get; }
    }
}
