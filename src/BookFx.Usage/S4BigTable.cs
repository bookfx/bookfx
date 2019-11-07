namespace BookFx.Usage
{
    using System.Linq;

    public static class S4BigTable
    {
        private const int RowCount = 1000; // About 1.5 seconds per 1K rows.
        private const int ColCount = 10;

        public static byte[] Create() =>
            Enumerable
                .Range(1, RowCount)
                .Select(GetRow)
                .ToCol()
                .ToSheet()
                .ToBook()
                .ToBytes();

        private static Box GetRow(int rowNumber) =>
            Enumerable
                .Range(1, ColCount)
                .Select(col => (rowNumber - 1) * ColCount + col)
                .Select(Make.Value)
                .ToRow();
    }
}