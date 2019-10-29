namespace BookFx.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;
    using static BookFx.Functional.F;

    internal static class BoxHideRender
    {
        public static Act<ExcelWorksheet> HideRender(this BoxCore box) =>
            HarvestErrors(
                HarvestErrors(box.ImmediateDescendants().Map(HideRender)),
                box.HideRowsRender(),
                box.HideColsRender());

        private static Act<ExcelWorksheet> HideRowsRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.IsRowsHidden)
                {
                    Enumerable
                        .Range(box.Placement.Position.Row, box.Placement.Dimension.Height)
                        .ForEach(row => excelSheet.Row(row).Hidden = true);
                }

                return Unit();
            };

        private static Act<ExcelWorksheet> HideColsRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.IsColsHidden)
                {
                    Enumerable
                        .Range(box.Placement.Position.Col, box.Placement.Dimension.Width)
                        .ForEach(col => excelSheet.Column(col).Hidden = true);
                }

                return Unit();
            };
    }
}