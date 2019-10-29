namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;

    internal static class BookRender
    {
        public static Act<ExcelPackage> Render(this BookCore book) =>
            HarvestErrors(book.Sheets.Map(SheetRender));

        private static Act<ExcelPackage> SheetRender(SheetCore sheet) =>
            package => sheet.Render()(
                sheet.ProtoSheet.Match(
                    none: () => package.Workbook.Worksheets.Add(sheet.Name.ValueUnsafe()),
                    some: protoSheet => package.Workbook.Worksheets.Add(sheet.Name.ValueUnsafe(), protoSheet)));
    }
}