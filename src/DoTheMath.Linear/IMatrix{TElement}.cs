namespace DoTheMath.Linear
{
    public interface IMatrix<TElement>
    {
        int Rows { get; }

        int Columns { get; }

        TElement Get(int row, int column);
    }
}
