namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    // todo rename to PlacementCalc
    internal static class PlacementCalcNew
    {
        public static Sc<Cache, Placement> Placement(BoxCore box, Structure structure) =>
            from firstRow in FirstRowCalc.FirstRow(box, structure)
            from firstCol in FirstColCalc.FirstCol(box, structure)
            from height in HeightCalc.Height(box, structure)
            from width in WidthCalc.Width(box, structure)
            select BookFx.Placement.At(firstRow, firstCol, height, width);
    }
}