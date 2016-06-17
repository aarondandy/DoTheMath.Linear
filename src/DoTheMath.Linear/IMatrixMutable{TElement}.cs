namespace DoTheMath.Linear
{
    public interface IMatrixMutable<TElement> : IMatrix<TElement>
    {
        new TElement this[int row, int column] { get; set; }

        void Set(int row, int column, TElement value);

        void SwapRows(int rowA, int rowB);

        void SwapColumns(int columnA, int columnB);

        void ScaleRow(int row, TElement value);

        void ScaleColumn(int column, TElement value);

        void DivideRow(int row, TElement denominator);

        void DivideColumn(int column, TElement denominator);

        void AddRow(int sourceRow, int targetRow);

        void SubtractRow(int sourceRow, int targetRow);

        void AddScaledRow(int sourceRow, int targetRow, TElement scalar);

        void AddColumn(int sourceColumn, int targetColumn);

        void SubtractColumn(int sourceColumn, int targetColumn);

        void AddScaledColumn(int sourceColumn, int targetColumn, TElement scalar);

        void Transpose();
    }
}
