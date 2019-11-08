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
            var layout = Layout.Create(numberedBox, boxCount);

            return Place(numberedBox, layout);
        }

        private static BoxCore Place(BoxCore box, Layout layout)
        {
            var placement = Placement(box, layout);

            return box.Match(
                row: x => PlaceComposite(x, placement, layout),
                col: x => PlaceComposite(x, placement, layout),
                stack: x => PlaceComposite(x, placement, layout),
                value: x => PlaceValue(x, placement),
                proto: x => PlaceProto(x, placement, layout));
        }

        private static Placement Placement(BoxCore box, Layout layout) =>
            BookFx.Placement.At(
                box.FirstRow(layout),
                box.FirstCol(layout),
                box.Height(layout),
                box.Width(layout));

        private static BoxCore PlaceComposite(BoxCore box, Placement placement, Layout layout) =>
            box.With(
                placement: placement,
                children: box.Children.Map(child => Place(child, layout)));

        private static BoxCore PlaceValue(BoxCore box, Placement placement) => box.With(placement: placement);

        private static BoxCore PlaceProto(BoxCore box, Placement placement, Layout layout) =>
            box.With(
                placement: placement,
                slots: box.Slots.Map(slot => slot.With(box: Place(slot.Box, layout))));
    }
}