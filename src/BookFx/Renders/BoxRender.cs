namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;

    internal static class BoxRender
    {
        public static Act<ExcelRangeBase> Render(this BoxCore box) =>
            HarvestErrors(
                box.ProtoRender(),
                box.DescendantsRender(),
                box.MergeRender(),
                box.StyleRender(),
                box.NameRender(),
                box.ValueRender(),
                box.PrintAreaRender());
    }
}