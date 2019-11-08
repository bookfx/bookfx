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

            return Place(numberedBox, Relations.Create(numberedBox), Cache.Create(boxCount));
        }

        private static BoxCore Place(BoxCore box, Relations relations, Cache cache)
        {
            var placement = Placement(box, relations, cache);

            return box.Match(
                row: x => PlaceComposite(x, placement, relations, cache),
                col: x => PlaceComposite(x, placement, relations, cache),
                stack: x => PlaceComposite(x, placement, relations, cache),
                value: x => PlaceValue(x, placement),
                proto: x => PlaceProto(x, placement, relations, cache));
        }

        private static Placement Placement(BoxCore box, Relations relations, Cache cache) =>
            BookFx.Placement.At(
                FirstRowCalc.FirstRow(box, relations, cache),
                FirstColCalc.FirstCol(box, relations, cache),
                HeightCalc.Height(box, relations, cache),
                WidthCalc.Width(box, relations, cache));

        private static BoxCore PlaceComposite(
            BoxCore box,
            Placement placement,
            Relations relations,
            Cache cache) =>
            box.With(
                placement: placement,
                children: box.Children.Map(child => Place(child, relations, cache)));

        private static BoxCore PlaceValue(BoxCore box, Placement placement) => box.With(placement: placement);

        private static BoxCore PlaceProto(
            BoxCore box,
            Placement placement,
            Relations relations,
            Cache cache) =>
            box.With(
                placement: placement,
                slots: box.Slots.Map(slot => slot.With(box: Place(slot.Box, relations, cache))));
    }
}