namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    internal static class SheetBoxRender
    {
        public static Tee<ExcelWorksheet> RootRender(this BoxCore box) =>
            FailFast(
                box.RootContentRender(),
                box.TrackSizesRender(),
                box.HideRender(),
                box.FreezeRender());

        private static Tee<ExcelWorksheet> RootContentRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.Placement.IsAbsent)
                {
                    return Unit();
                }

                var excelRange = excelSheet.Cells[
                    FromRow: box.Placement.Position.Row,
                    FromCol: box.Placement.Position.Col,
                    ToRow: box.Placement.ToRow,
                    ToCol: box.Placement.ToCol];

                return box.Render()(excelRange);
            };

        private static Tee<ExcelWorksheet> TrackSizesRender(this BoxCore box) =>
            HarvestErrors(
                HarvestErrors(box.ImmediateDescendants().Map(TrackSizesRender)),
                box.ColSizesRender(),
                box.RowSizesRender());
    }
}