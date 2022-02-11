namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.TeeComposition;

    internal static class BoxRender
    {
        public static Tee<ExcelRangeBase> Render(this BoxCore box) =>
            HarvestErrors(
                box.ProtoRender(),
                box.MergeRender(),
                box.StyleRender(),
                box.DescendantsRender(),
                box.GlobalNameRender(),
                box.LocalNameRender(),
                box.ContentRender(),
                box.AutoFilterRender(),
                box.PrintAreaRender());
    }
}