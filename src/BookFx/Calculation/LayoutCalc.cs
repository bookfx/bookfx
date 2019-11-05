namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class LayoutCalc
    {
        public static BookCore LayOut(this BookCore book) =>
            book.WithSheets(book.Sheets.Map(LayOut));

        public static SheetCore LayOut(this SheetCore sheet) =>
            sheet.WithBox(sheet.Box.Map(LayOut));

        public static BoxCore LayOut(this BoxCore box) =>
            box.WithMinDimension().WithPlacement();

        // todo rename to LayOut
        public static BoxCore LayOutNew(this BoxCore rootBox)
        {
            var structure = Structure.Create(rootBox);
            return LayOut(rootBox, structure).Run(Cache.Empty);
        }

        private static Sc<Cache, BoxCore> LayOut(BoxCore box, Structure structure) =>
            from placement in PlacementCalcNew.Perform(box, structure)
            from placed in box.Match(
                row: x => LayOutComposite(x, placement, structure),
                col: x => LayOutComposite(x, placement, structure),
                stack: x => LayOutComposite(x, placement, structure),
                value: x => Sc<Cache>.Return(LayOutValue(x, placement)),
                proto: x => Sc<Cache>.Return(LayOutProto(x, placement)))
            select placed;

        private static Sc<Cache, BoxCore> LayOutComposite(BoxCore box, Placement placement, Structure structure) =>
            from children in box.Children.Traverse(child => LayOut(child, structure))
            select box
                .With(children: children)
                .With(placement: placement);

        private static BoxCore LayOutValue(BoxCore box, Placement placement) =>
            box.With(placement: placement);

        private static BoxCore LayOutProto(BoxCore box, Placement placement) =>
            throw new NotImplementedException(); // todo
    }
}