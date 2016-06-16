namespace DoTheMath.Linear
{
    public interface IMatrixMutable<TElement> : IMatrix<TElement>
    {
        new TElement this[int row, int column] { get; set; }

        void Set(int row, int column, TElement value);

        void SwapRows(int rowA, int rowB);

        void SwapColumns(int columnA, int columnB);

        void ScaleRow(int row, double value);

        void ScaleColumn(int column, double value);

        void DivideRow(int row, double denominator);

        void DivideColumn(int column, double denominator);

        void AddRow(int sourceRow, int targetRow);

        void SubtractRow(int sourceRow, int targetRow);

        void AddScaledRow(int sourceRow, int targetRow, double scalar);

        void AddColumn(int sourceColumn, int targetColumn);

        void SubtractColumn(int sourceColumn, int targetColumn);

        void AddScaledColumn(int sourceColumn, int targetColumn, double scalar);

        void Transpose();
    }
}
