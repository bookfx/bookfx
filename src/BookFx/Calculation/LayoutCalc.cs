namespace BookFx.Calculation
{
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
            from placement in PlacementCalcNew.Placement(box, structure)
            from placed in box.Match(
                row: x => LayOutComposite(x, placement, structure),
                col: x => LayOutComposite(x, placement, structure),
                stack: x => LayOutComposite(x, placement, structure),
                value: x => LayOutValue(x, placement),
                proto: x => LayOutProto(x, placement, structure))
            select placed;

        private static Sc<Cache, BoxCore> LayOutComposite(BoxCore box, Placement placement, Structure structure) =>
            from children in box.Children.Traverse(child => LayOut(child, structure))
            select box
                .With(children: children)
                .With(placement: placement);

        private static Sc<Cache, BoxCore> LayOutValue(BoxCore box, Placement placement) =>
            Sc<Cache>.Return(box.With(placement: placement));

        private static Sc<Cache, BoxCore> LayOutProto(BoxCore box, Placement placement, Structure structure) =>
            from slots in box.Slots.Traverse(slot =>
                from slotBox in LayOut(slot.Box, structure)
                select slot.With(box: slotBox))
            select box.With(
                slots: slots,
                placement: placement);
    }
}