namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;
    using Processed = System.Collections.Immutable.ImmutableHashSet<BookFx.Cores.BoxCore>;

    internal static class UniquenessCalc
    {
        public static BoxCore MakeUnique(this BoxCore rootBox) => Unique(rootBox).Run(Processed.Empty);

        private static Sc<Processed, BoxCore> Unique(BoxCore box) =>
            box.Match(
                row: OfComposite,
                col: OfComposite,
                stack: OfComposite,
                value: OfValue,
                proto: OfProto);

        private static Sc<Processed, BoxCore> OfComposite(BoxCore box) =>
            from children in box.Children.Traverse(Unique)
            select box.With(children: children);

        private static Sc<Processed, BoxCore> OfValue(BoxCore box) =>
            from processed in Sc<Processed>.GetState
            let unique = processed.Contains(box) ? box.With() : box
            from unused in Sc<Processed>.PutState(processed.Add(unique))
            select unique;

        private static Sc<Processed, BoxCore> OfProto(BoxCore box) =>
            from slots in box.Slots.Traverse(slot =>
                from slotBox in Unique(slot.Box)
                select slot.With(box: slotBox))
            select box.With(slots: slots);
    }
}