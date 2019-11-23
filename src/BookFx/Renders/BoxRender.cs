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
                box.MergeRender(),
                box.StyleRender(),
                box.DescendantsRender(),
                box.NameRender(),
                box.ContentRender(),
                box.AutoFilterRender(),
                box.PrintAreaRender());
    }
}