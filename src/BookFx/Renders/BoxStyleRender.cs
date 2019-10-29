namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;

    internal static class BoxStyleRender
    {
        public static Act<ExcelRangeBase> StyleRender(this BoxCore box) =>
            HarvestErrors(box.Style.Map(Renders.StyleRender.Render).AsEnumerable());
    }
}