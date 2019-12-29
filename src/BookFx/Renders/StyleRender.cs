namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    internal static class StyleRender
    {
        public static Tee<ExcelRangeBase> Render(this BoxStyleCore style) =>
            HarvestErrors(
                style.BordersRender(),
                style.OthersRender());

        private static Tee<ExcelRangeBase> BordersRender(this BoxStyleCore style) =>
            HarvestErrors(style.Borders.Map(BorderRender.Render));

        private static Tee<ExcelRangeBase> OthersRender(this BoxStyleCore style) =>
            excelRange =>
            {
                style.FontSize.ForEach(size => excelRange.Style.Font.Size = (float)size);
                style.FontName.ForEach(name => excelRange.Style.Font.Name = name);
                style.FontColor.ForEach(color => excelRange.Style.Font.Color.SetColor(color));
                style.BackColor.ForEach(color =>
                {
                    excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    excelRange.Style.Fill.BackgroundColor.SetColor(color);
                });
                style.IsBold.ForEach(bold => excelRange.Style.Font.Bold = bold);
                style.IsItalic.ForEach(italic => excelRange.Style.Font.Italic = italic);
                style.IsUnderline.ForEach(underline => excelRange.Style.Font.UnderLine = underline);
                style.IsStrike.ForEach(strike => excelRange.Style.Font.Strike = strike);
                style.HAlign.ForEach(
                    alignment => excelRange.Style.HorizontalAlignment = (ExcelHorizontalAlignment)alignment);
                style.VAlign.ForEach(
                    alignment => excelRange.Style.VerticalAlignment = (ExcelVerticalAlignment)alignment);
                style.IsWrap.ForEach(wrap => excelRange.Style.WrapText = wrap);
                style.IsShrink.ForEach(shrink => excelRange.Style.ShrinkToFit = shrink);
                style.Rotation.ForEach(rotation => excelRange.Style.TextRotation = rotation);
                style.Indent.ForEach(size => excelRange.Style.Indent = size);
                style.Format.ForEach(format => excelRange.Style.Numberformat.Format = format);

                return Unit();
            };
    }
}