namespace BookFx.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.TeeComposition;

    internal static class BoxDescendantsRender
    {
        public static Tee<ExcelRangeBase> DescendantsRender(this BoxCore box) =>
            HarvestErrors(box.ImmediateDescendants().Where(x => !x.Placement.IsAbsent).Map(DescendantRender));

        private static Tee<ExcelRangeBase> DescendantRender(BoxCore box) =>
            parentExcelRange => box.Render()(parentExcelRange.Worksheet.Cells[
                FromRow: box.Placement.Position.Row,
                FromCol: box.Placement.Position.Col,
                ToRow: box.Placement.ToRow,
                ToCol: box.Placement.ToCol]);
    }
}