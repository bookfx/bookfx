namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxPrintAreaRender
    {
        public static Act<ExcelRangeBase> PrintAreaRender(this BoxCore box) =>
            excelRange =>
            {
                if (box.IsPrintArea)
                {
                    excelRange.Worksheet.PrinterSettings.PrintArea = excelRange;
                }

                return Unit();
            };
    }
}