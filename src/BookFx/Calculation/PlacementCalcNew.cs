namespace BookFx.Calculation
{
    using BookFx.Cores;

    // todo rename to PlacementCalc
    internal static class PlacementCalcNew
    {
        public static (Placement Result, Cache Cache) Perform(BoxCore box, Cache cache, Structure structure)
        {
            var firstRow = FirstRowCalc.Perform(box, cache, structure);
            var firstCol = FirstColCalc.Perform(box, firstRow.Cache, structure);
            var height = HeightCalc.Perform(box, firstCol.Cache, structure);
            var width = WidthCalc.Perform(box, height.Cache, structure);

            var result = Placement.At(firstRow.Result, firstCol.Result, height.Result, width.Result);

            return (result, width.Cache);
        }
    }
}