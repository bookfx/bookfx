namespace BookFx.Epplus
{
    using System.Drawing;
    using OfficeOpenXml.Style;

    internal static class ColorExt
    {
        public static string ToHex(this ExcelColor excelColor) => excelColor.Rgb ?? string.Empty;

        public static string ToHex(this Color color) => color.ToArgb().ToString("X");
    }
}