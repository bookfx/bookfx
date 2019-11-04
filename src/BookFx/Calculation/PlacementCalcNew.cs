namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    // todo rename to PlacementCalc
    internal static class PlacementCalcNew
    {
        public static (Result<Placement> Result, Cache Cache) Perform(BoxCore box, Cache cache, Structure structure)
        {
            var firstRow = FirstRowCalc.Perform(box, cache, structure);
            var firstCol = FirstColCalc.Perform(box, firstRow.Cache, structure);
            var height = HeightCalc.Perform(box, firstCol.Cache, structure);
            var width = WidthCalc.Perform(box, height.Cache, structure);

            var result =
                from r in firstRow.Result
                from c in firstCol.Result
                from h in height.Result
                from w in width.Result
                from placement in Placement.At(r, c, h, w)
                select placement;

            return (result, width.Cache);
        }
    }
}