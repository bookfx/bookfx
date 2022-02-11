namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;

    internal static class BoxLocalNameRender
    {
        public static Tee<ExcelRangeBase> LocalNameRender(this BoxCore box) =>
            excelRange => box.LocalName.Match(
                none: () => F.Valid(F.Unit()),
                some: name =>
                {
                    if (excelRange.Worksheet.Names.ContainsKey(name))
                    {
                        return Errors.Sheet.BoxLocalNameIsNotUnique(name);
                    }

                    excelRange.Worksheet.Names.Add(name, excelRange);

                    return F.Valid(F.Unit());
                });
    }
}