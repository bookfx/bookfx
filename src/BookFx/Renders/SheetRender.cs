namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;
    using static BookFx.Functional.F;

    internal static class SheetRender
    {
        public static Act<ExcelWorksheet> Render(this SheetCore sheet) =>
            HarvestErrors(
                sheet.Box.Match(
                    none: NoAct,
                    some: SheetBoxRender.RootRender),
                SettingsRender(sheet));

        private static Act<ExcelWorksheet> SettingsRender(this SheetCore sheet) =>
            excelSheet =>
            {
                var printSettings = excelSheet.PrinterSettings;

                sheet.TabColor.ForEach(color => excelSheet.TabColor = color);

                sheet.PageView.ForEach(pageView =>
                {
                    excelSheet.View.PageLayoutView = pageView == PageView.Layout;
                    excelSheet.View.PageBreakView = pageView == PageView.Break;
                });

                sheet.Orientation.ForEach(orientation =>
                    printSettings.Orientation = (eOrientation)orientation);

                sheet
                    .Margins
                    .Map(x => x.ToInches())
                    .ForEach(margins =>
                    {
                        margins.Top.ForEach(margin => printSettings.TopMargin = margin);
                        margins.Right.ForEach(margin => printSettings.RightMargin = margin);
                        margins.Bottom.ForEach(margin => printSettings.BottomMargin = margin);
                        margins.Left.ForEach(margin => printSettings.LeftMargin = margin);
                        margins.Header.ForEach(margin => printSettings.HeaderMargin = margin);
                        margins.Footer.ForEach(margin => printSettings.FooterMargin = margin);
                    });

                printSettings.FitToHeight = sheet.FitToHeight.GetOrElse(0);
                printSettings.FitToWidth = sheet.FitToWidth.GetOrElse(0);
                printSettings.FitToPage = sheet.FitToHeight.IsSome || sheet.FitToWidth.IsSome;

                sheet.Scale.ForEach(scale => excelSheet.View.ZoomScale = scale);

                return Unit();
            };
    }
}