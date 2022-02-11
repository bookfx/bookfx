namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxGlobalNameRender
    {
        public static Tee<ExcelRangeBase> GlobalNameRender(this BoxCore box) =>
            excelRange => box.GlobalName.Match(
                none: () => Valid(Unit()),
                some: name =>
                {
                    if (excelRange.Worksheet.Workbook.Names.ContainsKey(name))
                    {
                        return Errors.Book.BoxGlobalNameIsNotUnique(name);
                    }

                    excelRange.Worksheet.Workbook.Names.Add(name, excelRange);

                    return Valid(Unit());
                });
    }
}