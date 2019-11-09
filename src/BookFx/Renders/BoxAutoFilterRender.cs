namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxAutoFilterRender
    {
        public static Act<ExcelRangeBase> AutoFilterRender(this BoxCore box) =>
            excelRange =>
            {
                if (box.IsAutoFilter)
                {
                    excelRange
                        .Offset(
                            RowOffset: excelRange.Rows - 1,
                            ColumnOffset: 0,
                            NumberOfRows: 1,
                            NumberOfColumns: excelRange.Columns)
                        .AutoFilter = true;
                }

                return Unit();
            };
    }
}