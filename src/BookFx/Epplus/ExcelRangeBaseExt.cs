namespace BookFx.Epplus
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static System.Linq.Enumerable;

    internal static class ExcelRangeBaseExt
    {
        public static IEnumerable<ExcelRangeBase> GetCells(this ExcelRangeBase range)
        {
            for (var rowOffset = 0; rowOffset < range.Rows; rowOffset++)
            {
                for (var columnOffset = 0; columnOffset < range.Columns; columnOffset++)
                {
                    yield return range.Offset(
                        RowOffset: rowOffset,
                        ColumnOffset: columnOffset,
                        NumberOfRows: 1,
                        NumberOfColumns: 1);
                }
            }
        }

        public static void CopyColSizes(this ExcelRangeBase source, ExcelRangeBase target) =>
            Enumerable
                .Zip(
                    Range(source.Start.Column, source.Columns),
                    Range(target.Start.Column, source.Columns),
                    (src, trg) => (
                        src: source.Worksheet.Column(src),
                        trg: target.Worksheet.Column(trg)))
                .ForEach(x => x.trg.Width = x.src.Width);

        public static void CopyRowSizes(this ExcelRangeBase source, ExcelRangeBase target) =>
            Enumerable
                .Zip(
                    Range(source.Start.Row, source.Rows),
                    Range(target.Start.Row, source.Rows),
                    (src, trg) => (
                        src: source.Worksheet.Row(src),
                        trg: target.Worksheet.Row(trg)))
                .ForEach(x => x.trg.Height = x.src.Height);

        public static Position GetPosition(this ExcelRangeBase range) =>
            Position.At(range.Start.Row, range.Start.Column);

        public static Dimension GetDimension(this ExcelRangeBase range) =>
            Dimension.Of(range.Rows, range.Columns);
    }
}