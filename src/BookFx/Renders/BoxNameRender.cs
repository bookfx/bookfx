namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxNameRender
    {
        public static Act<ExcelRangeBase> NameRender(this BoxCore box) =>
            excelRange =>
            {
                box.Name.ForEach(name =>
                {
                    excelRange.Worksheet.Workbook.Names.Remove(name);
                    excelRange.Worksheet.Workbook.Names.Add(name, excelRange);
                });

                return Unit();
            };
    }
}