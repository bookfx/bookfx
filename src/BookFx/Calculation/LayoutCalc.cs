namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class LayoutCalc
    {
        public static BookCore LayOut(this BookCore book) =>
            book.WithSheets(book.Sheets.Map(LayOut));

        public static SheetCore LayOut(this SheetCore sheet) =>
            sheet.WithBox(sheet.Box.Map(LayOut));

        public static BoxCore LayOut(this BoxCore box) =>
            box.WithMinDimension().WithPlacement();

        // todo rename to LayOut
        public static Result<BoxCore> LayOutNew(this BoxCore rootBox)
        {
            var structure = Structure.Create(rootBox);
            var cache = Cache.Empty;
            var (result, _) = LayOut(rootBox, cache, structure);
            return result;
        }

        private static (Result<BoxCore>, Cache) LayOut(BoxCore box, Cache cache, Structure structure) =>
            PlacementCalcNew
                .Perform(box, cache, structure)
                .Bind((placement, newCache) => box.Match(
                    row: x => LayOutComposite(x, placement, newCache, structure),
                    col: x => LayOutComposite(x, placement, newCache, structure),
                    stack: x => LayOutComposite(x, placement, newCache, structure),
                    value: x => (LayOutValue(x, placement), newCache),
                    proto: x => (LayOutProto(x, placement), newCache)));

        private static (Result<BoxCore>, Cache) LayOutComposite(
            BoxCore box,
            Placement placement,
            Cache cache,
            Structure structure)
        {
            var (childResults, newCache) = box
                .Children
                .AggMap(cache, (child, accCache) => LayOut(child, accCache, structure));

            var result = childResults
                .HarvestErrors()
                .Map(children => box
                    .With(children: children)
                    .With(placement: placement));

            return (result, newCache);
        }

        private static Result<BoxCore> LayOutValue(BoxCore box, Placement placement) =>
            box.With(placement: placement);

        private static Result<BoxCore> LayOutProto(BoxCore box, Placement placement) =>
            throw new NotImplementedException(); // todo
    }
}