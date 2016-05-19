namespace DoTheMath.Linear
{
    public interface IMatrixMutable<TElement> : IMatrix<TElement>
    {
        void Set(int row, int column, TElement value);

        void SwapRows(int rowA, int rowB);

        void SwapColumns(int columnA, int columnB);

        void ScaleRow(int row, double value);

        void ScaleColumn(int column, double value);

        void DivideRow(int row, double denominator);

        void DivideColumn(int column, double denominator);

        void AddScaledRow(int sourceRow, int targetRow, double scalar);

        void AddScaledColumn(int sourceColumn, int targetColumn, double scalar);

        void Transpose();
    }
}
