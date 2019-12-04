namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.TeeComposition;

    internal static class BoxStyleRender
    {
        public static Tee<ExcelRangeBase> StyleRender(this BoxCore box) =>
            HarvestErrors(box.Style.Map(Renders.StyleRender.Render).AsEnumerable());
    }
}