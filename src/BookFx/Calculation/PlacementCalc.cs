namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class PlacementCalc
    {
        public static BookCore Place(this BookCore book) =>
            book.WithSheets(book.Sheets.Map(Place));

        public static SheetCore Place(this SheetCore sheet) =>
            sheet.WithBox(sheet.Box.Map(Place));

        public static BoxCore Place(this BoxCore box)
        {
            var (numberedBox, boxCount) = box.Number();

            return Place(numberedBox, Structure.Create(numberedBox), Cache.Create(boxCount));
        }

        private static BoxCore Place(BoxCore box, Structure structure, Cache cache)
        {
            var placement = Placement(box, structure, cache);

            return box.Match(
                row: x => PlaceComposite(x, placement, structure, cache),
                col: x => PlaceComposite(x, placement, structure, cache),
                stack: x => PlaceComposite(x, placement, structure, cache),
                value: x => PlaceValue(x, placement),
                proto: x => PlaceProto(x, placement, structure, cache));
        }

        private static Placement Placement(BoxCore box, Structure structure, Cache cache) =>
            BookFx.Placement.At(
                FirstRowCalc.FirstRow(box, structure, cache),
                FirstColCalc.FirstCol(box, structure, cache),
                HeightCalc.Height(box, structure, cache),
                WidthCalc.Width(box, structure, cache));

        private static BoxCore PlaceComposite(
            BoxCore box,
            Placement placement,
            Structure structure,
            Cache cache) =>
            box.With(
                placement: placement,
                children: box.Children.Map(child => Place(child, structure, cache)));

        private static BoxCore PlaceValue(BoxCore box, Placement placement) => box.With(placement: placement);

        private static BoxCore PlaceProto(
            BoxCore box,
            Placement placement,
            Structure structure,
            Cache cache) =>
            box.With(
                placement: placement,
                slots: box.Slots.Map(slot => slot.With(box: Place(slot.Box, structure, cache))));
    }
}