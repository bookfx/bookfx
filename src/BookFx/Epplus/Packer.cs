namespace BookFx.Epplus
{
    using System;
    using System.IO;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class Packer
    {
        public static Result<byte[]> Pack(Act<ExcelPackage> render) =>
            Using(
                new ExcelPackage(),
                package => render(package).Map(_ => package.GetAsByteArray()));

        public static void Unpack(this byte[] bytes, Action<ExcelWorkbook> action) =>
            Using(
                new MemoryStream(bytes),
                stream => Using(
                    new ExcelPackage(stream),
                    package => action.ToFunc()(package.Workbook)));

        public static void OnPackage(Action<ExcelPackage> action) =>
            Using(
                new ExcelPackage(),
                package => action.ToFunc()(package));

        public static void OnSheet(Action<ExcelWorksheet> action) =>
            Using(
                new ExcelPackage(),
                package => action.ToFunc()(package.Workbook.Worksheets.Add("Sheet")));
    }
}