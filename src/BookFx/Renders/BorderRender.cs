namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using static BookFx.Functional.F;

    internal static class BorderRender
    {
        public static Act<ExcelRangeBase> Render(this BoxBorderCore border) =>
            excelRange =>
            {
                var borderPart = border.Part.GetOrElse(BorderPart.All);

                switch (borderPart)
                {
                    case BorderPart.All:
                        SetBorder(excelRange.Style.Border.Top, border);
                        SetBorder(excelRange.Style.Border.Right, border);
                        SetBorder(excelRange.Style.Border.Bottom, border);
                        SetBorder(excelRange.Style.Border.Left, border);

                        break;

                    case BorderPart.Outside:
                        border.Color.Match(
                            none: () => excelRange.Style.Border.BorderAround(GetExcelBorderStyle(border.Style)),
                            some: color =>
                                excelRange.Style.Border.BorderAround(GetExcelBorderStyle(border.Style), color));

                        break;

                    default:
                        foreach (var cell in excelRange.GetCells())
                        {
                            if (IsApplicableForTop(excelRange, cell, borderPart))
                            {
                                SetBorder(cell.Style.Border.Top, border);
                            }

                            if (IsApplicableForRight(excelRange, cell, borderPart))
                            {
                                SetBorder(cell.Style.Border.Right, border);
                            }

                            if (IsApplicableForBottom(excelRange, cell, borderPart))
                            {
                                SetBorder(cell.Style.Border.Bottom, border);
                            }

                            if (IsApplicableForLeft(excelRange, cell, borderPart))
                            {
                                SetBorder(cell.Style.Border.Left, border);
                            }
                        }

                        break;
                }

                return Unit();
            };

        private static void SetBorder(ExcelBorderItem excelBorderItem, BoxBorder border)
        {
            excelBorderItem.Style = GetExcelBorderStyle(border.Get.Style);
            border.Get.Color.ForEach(color => excelBorderItem.Color.SetColor(color));
        }

        private static ExcelBorderStyle GetExcelBorderStyle(Option<BorderStyle> style) =>
            (ExcelBorderStyle)style.GetOrElse(BorderStyle.Thin);

        private static bool IsApplicableForTop(
            ExcelRangeBase excelRange,
            ExcelRangeBase cell,
            BorderPart part) =>
            excelRange.Start.Row == cell.Start.Row
                ? (part & BorderPart.OutsideTop) == BorderPart.OutsideTop
                : (part & BorderPart.InsideTop) == BorderPart.InsideTop;

        private static bool IsApplicableForRight(
            ExcelRangeBase excelRange,
            ExcelRangeBase cell,
            BorderPart part) =>
            excelRange.End.Column == cell.End.Column
                ? (part & BorderPart.OutsideRight) == BorderPart.OutsideRight
                : (part & BorderPart.InsideRight) == BorderPart.InsideRight;

        private static bool IsApplicableForBottom(
            ExcelRangeBase excelRange,
            ExcelRangeBase cell,
            BorderPart part) =>
            excelRange.End.Row == cell.End.Row
                ? (part & BorderPart.OutsideBottom) == BorderPart.OutsideBottom
                : (part & BorderPart.InsideBottom) == BorderPart.InsideBottom;

        private static bool IsApplicableForLeft(
            ExcelRangeBase excelRange,
            ExcelRangeBase cell,
            BorderPart part) =>
            excelRange.Start.Column == cell.Start.Column
                ? (part & BorderPart.OutsideLeft) == BorderPart.OutsideLeft
                : (part & BorderPart.InsideLeft) == BorderPart.InsideLeft;
    }
}