namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxMergeRender
    {
        public static Act<ExcelRangeBase> MergeRender(this BoxCore box) =>
            excelRange =>
            {
                if (box.Merge.GetOrElse(box.Content.IsSome) && CanBeMerged(excelRange))
                {
                    excelRange.Merge = true;
                }

                return Unit();
            };

        private static bool CanBeMerged(ExcelRangeBase excelRange) =>
            excelRange.Columns > 1 || excelRange.Rows > 1;
    }
}