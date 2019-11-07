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

        public static BoxCore LayOut(this BoxCore box)
        {
            var (numberedBox, boxCount) = box.Number();

            return LayOut(numberedBox, Structure.Create(numberedBox), Cache.Create(boxCount));
        }

        private static BoxCore LayOut(BoxCore box, Structure structure, Cache cache)
        {
            var placement = PlacementCalc.Place(box, structure, cache);

            return box.Match(
                row: x => LayOutComposite(x, placement, structure, cache),
                col: x => LayOutComposite(x, placement, structure, cache),
                stack: x => LayOutComposite(x, placement, structure, cache),
                value: x => LayOutValue(x, placement),
                proto: x => LayOutProto(x, placement, structure, cache));
        }

        private static BoxCore LayOutComposite(
            BoxCore box,
            Placement placement,
            Structure structure,
            Cache cache) =>
            box.With(
                placement: placement,
                children: box.Children.Map(child => LayOut(child, structure, cache)));

        private static BoxCore LayOutValue(BoxCore box, Placement placement) => box.With(placement: placement);

        private static BoxCore LayOutProto(
            BoxCore box,
            Placement placement,
            Structure structure,
            Cache cache) =>
            box.With(
                placement: placement,
                slots: box.Slots.Map(slot => slot.With(box: LayOut(slot.Box, structure, cache))));
    }
}