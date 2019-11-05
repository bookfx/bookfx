namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    // todo rename to PlacementCalc
    internal static class PlacementCalcNew
    {
        public static Sc<Cache, Placement> Perform(BoxCore box, Structure structure) =>
            from firstRow in FirstRowCalc.Perform(box, structure)
            from firstCol in FirstColCalc.Perform(box, structure)
            from height in HeightCalc.Perform(box, structure)
            from width in WidthCalc.Perform(box, structure)
            select Placement.At(firstRow, firstCol, height, width);
    }
}