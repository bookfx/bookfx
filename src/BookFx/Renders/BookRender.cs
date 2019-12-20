namespace BookFx.Renders
{
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.TeeComposition;

    internal static class BookRender
    {
        public static Tee<ExcelPackage> Render(this BookCore book) => HarvestErrors(book.Sheets.Map(SheetRender));

        private static Tee<ExcelPackage> SheetRender(SheetCore sheet) =>
            package =>
            {
                return sheet.Render()(sheet.ProtoSheet.Match(
                        none: Create,
                        some: Copy));

                ExcelWorksheet Create() => package.Workbook.Worksheets.Add(sheet.Name.ValueUnsafe());

                ExcelWorksheet Copy(ExcelWorksheet excelSrcSheet)
                {
                    var excelTrgSheet = package.Workbook.Worksheets.Add(sheet.Name.ValueUnsafe(), excelSrcSheet);
                    CopyNames(excelSrcSheet, excelTrgSheet);
                    return excelTrgSheet;
                }
            };

        private static void CopyNames(ExcelWorksheet excelSrcSheet, ExcelWorksheet excelTrgSheet)
        {
            var existingNames = excelTrgSheet.Workbook.Names.Map(x => x.Name).ToImmutableHashSet();

            var names = excelSrcSheet.Workbook.Names.Where(name => name.Worksheet == excelSrcSheet);

            foreach (var name in names)
            {
                var range = excelTrgSheet.Cells[name.Address];

                if (existingNames.Contains(name.Name))
                {
                    excelTrgSheet.Names.Add(name.Name, range);
                }
                else
                {
                    excelTrgSheet.Workbook.Names.Add(name.Name, range);
                }
            }
        }
    }
}