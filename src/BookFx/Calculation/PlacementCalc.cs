namespace BookFx.Calculation
{
    using BookFx.Cores;

    internal static class PlacementCalc
    {
        public static Placement Place(BoxCore box, Structure structure, Cache cache) =>
            Placement.At(
                FirstRowCalc.FirstRow(box, structure, cache),
                FirstColCalc.FirstCol(box, structure, cache),
                HeightCalc.Height(box, structure, cache),
                WidthCalc.Width(box, structure, cache));
    }
}