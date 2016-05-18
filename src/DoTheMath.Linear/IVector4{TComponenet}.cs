namespace DoTheMath.Linear
{
    public interface IVector4<TComponenet> : IVector<TComponenet>
    {
        TComponenet X { get; }

        TComponenet Y { get; }

        TComponenet Z { get; }

        TComponenet W { get; }
    }
}
