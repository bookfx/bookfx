namespace BookFx.Renders
{
    using System;
    using System.Collections.Generic;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.TeeComposition;

    internal static class BoxColSizesRender
    {
        private const double MaxDigitWidth = 7; // for Calibri 11

        public static Tee<ExcelWorksheet> ColSizesRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.ColSizes.Count > box.Placement.Dimension.Width)
                {
                    return Errors.Box.ColSizeCountIsInvalid(
                        sizeCount: box.ColSizes.Count,
                        boxWidth: box.Placement.Dimension.Width);
                }

                return HarvestErrors(box.ColSizeRenders())(excelSheet);
            };

        private static IEnumerable<Tee<ExcelWorksheet>> ColSizeRenders(this BoxCore box) =>
            box.ColSizes.Map((size, i) => ColSizeRender(size, box.Placement.Position.Col + i));

        private static Tee<ExcelWorksheet> ColSizeRender(TrackSize size, int col) =>
            excelSheet => size.ForEach(
                fit: () =>
                {
                    if (excelSheet.CanColAutoFit(col))
                    {
                        excelSheet.Column(col).AutoFit(0);
                    }
                },
                some: value => excelSheet.Column(col).Width = GetWidth(value));

        private static double GetWidth(double numberOfChars)
        {
            var charsWidth = numberOfChars * MaxDigitWidth;
            var scale = numberOfChars > 1 ? 1 : numberOfChars;
            var paddings = 2 * Math.Max(2, Math.Ceiling(MaxDigitWidth / 4)) + 1;
            return Math.Truncate(Math.Round(charsWidth + scale * paddings) / MaxDigitWidth * 256) / 256;
        }
    }
}