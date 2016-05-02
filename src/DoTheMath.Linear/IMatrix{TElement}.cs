using System;

namespace DoTheMath.Linear
{
    public interface IMatrix<TElement>
    {
        int Rows { get; }

        int Columns { get; }

        bool IsSquare { get; }

        [Obsolete("This should change to a method.")]
        bool IsIdentity { get; }

        TElement Get(int row, int column);
    }
}
