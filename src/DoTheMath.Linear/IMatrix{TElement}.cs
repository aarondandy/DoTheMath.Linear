namespace DoTheMath.Linear
{
    public interface IMatrix<TElement>
    {
        int Rows { get; }

        int Columns { get; }

        bool IsSquare { get; }

        TElement Get(int row, int column);
    }
}
