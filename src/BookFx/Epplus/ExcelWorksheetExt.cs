namespace BookFx.Epplus
{
    using System.Linq;
    using OfficeOpenXml;

    internal static class ExcelWorksheetExt
    {
        public static bool CanColAutoFit(this ExcelWorksheet excelSheet, int col) =>
            excelSheet
                .Cells[1, col, ExcelPackage.MaxRows, col]
                .Where(x => x.Value != null)
                .Where(x => !x.Style.WrapText)
                .Any(x => !x.Merge);
    }
}