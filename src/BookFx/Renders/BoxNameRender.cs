namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxNameRender
    {
        public static Tee<ExcelRangeBase> NameRender(this BoxCore box) =>
            excelRange => box.Name.Match(
                none: () => Valid(Unit()),
                some: name =>
                {
                    if (excelRange.Worksheet.Workbook.Names.ContainsKey(name))
                    {
                        return Errors.Book.BoxNameIsNotUnique(name);
                    }

                    excelRange.Worksheet.Workbook.Names.Add(name, excelRange);

                    return Valid(Unit());
                });
    }
}