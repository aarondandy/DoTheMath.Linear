namespace DoTheMath.Linear
{
    public interface IMatrix<out TElement>
    {
        int Rows { get; }

        int Columns { get; }

        bool IsSquare { get; }

        TElement this[int row, int column] { get; }

        TElement Get(int row, int column);
    }
}
