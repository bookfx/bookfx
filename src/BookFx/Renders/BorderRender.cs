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
                var parts = border.Part.GetOrElse(BorderParts.All);

                switch (parts)
                {
                    case BorderParts.All:
                        border.ApplyTo(excelRange.Style.Border.Top);
                        border.ApplyTo(excelRange.Style.Border.Right);
                        border.ApplyTo(excelRange.Style.Border.Bottom);
                        border.ApplyTo(excelRange.Style.Border.Left);

                        break;

                    case BorderParts.Outside:
                        border.Color.Match(
                            none: () => excelRange.Style.Border.BorderAround(border.Style.ToExcelBorderStyle()),
                            some: color =>
                                excelRange.Style.Border.BorderAround(border.Style.ToExcelBorderStyle(), color));

                        break;

                    default:
                        foreach (var cell in excelRange.GetCells())
                        {
                            SetCellBorder(cell);
                        }

                        break;
                }

                return Unit();

                void SetCellBorder(ExcelRangeBase cell)
                {
                    if (IsApplicableForTop())
                    {
                        border.ApplyTo(cell.Style.Border.Top);
                    }

                    if (IsApplicableForRight())
                    {
                        border.ApplyTo(cell.Style.Border.Right);
                    }

                    if (IsApplicableForBottom())
                    {
                        border.ApplyTo(cell.Style.Border.Bottom);
                    }

                    if (IsApplicableForLeft())
                    {
                        border.ApplyTo(cell.Style.Border.Left);
                    }

                    bool IsApplicableForTop() =>
                        parts.IsSupersetOf(
                            excelRange.Start.Row == cell.Start.Row
                                ? BorderParts.OutsideTop
                                : BorderParts.InsideTop);

                    bool IsApplicableForRight() =>
                        parts.IsSupersetOf(
                            excelRange.End.Column == cell.End.Column
                                ? BorderParts.OutsideRight
                                : BorderParts.InsideRight);

                    bool IsApplicableForBottom() =>
                        parts.IsSupersetOf(
                            excelRange.End.Row == cell.End.Row
                                ? BorderParts.OutsideBottom
                                : BorderParts.InsideBottom);

                    bool IsApplicableForLeft() =>
                        parts.IsSupersetOf(
                            excelRange.Start.Column == cell.Start.Column
                                ? BorderParts.OutsideLeft
                                : BorderParts.InsideLeft);
                }
            };

        private static void ApplyTo(this BoxBorderCore border, ExcelBorderItem excelBorderItem)
        {
            excelBorderItem.Style = border.Style.ToExcelBorderStyle();
            border.Color.ForEach(color => excelBorderItem.Color.SetColor(color));
        }

        private static ExcelBorderStyle ToExcelBorderStyle(this Option<BorderStyle> style) =>
            (ExcelBorderStyle)style.GetOrElse(BorderStyle.Thin);
    }
}