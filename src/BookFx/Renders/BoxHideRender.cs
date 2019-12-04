namespace BookFx.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    internal static class BoxHideRender
    {
        public static Tee<ExcelWorksheet> HideRender(this BoxCore box) =>
            HarvestErrors(
                HarvestErrors(box.ImmediateDescendants().Map(HideRender)),
                box.HideRowsRender(),
                box.HideColsRender());

        private static Tee<ExcelWorksheet> HideRowsRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.AreRowsHidden)
                {
                    Enumerable
                        .Range(box.Placement.Position.Row, box.Placement.Dimension.Height)
                        .ForEach(row => excelSheet.Row(row).Hidden = true);
                }

                return Unit();
            };

        private static Tee<ExcelWorksheet> HideColsRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.AreColsHidden)
                {
                    Enumerable
                        .Range(box.Placement.Position.Col, box.Placement.Dimension.Width)
                        .ForEach(col => excelSheet.Column(col).Hidden = true);
                }

                return Unit();
            };
    }
}