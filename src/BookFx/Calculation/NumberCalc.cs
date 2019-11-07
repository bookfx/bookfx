namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class NumberCalc
    {
        private static readonly Sc<int, int> GetAndIncement = state => (state, state + 1);

        public static (BoxCore Box, int BoxCount) Number(this BoxCore rootBox) => NumberSc(rootBox)(0);

        private static Sc<int, BoxCore> NumberSc(BoxCore box) =>
            box.Match(
                row: OfComposite,
                col: OfComposite,
                stack: OfComposite,
                value: OfValue,
                proto: OfProto);

        private static Sc<int, BoxCore> OfComposite(BoxCore box) =>
            from number in GetAndIncement
            from children in box.Children.Traverse(NumberSc)
            select box.With(number: number, children: children);

        private static Sc<int, BoxCore> OfValue(BoxCore box) =>
            from number in GetAndIncement
            select box.With(number: number);

        private static Sc<int, BoxCore> OfProto(BoxCore box) =>
            from number in GetAndIncement
            from slots in box.Slots.Traverse(slot =>
                from slotBox in NumberSc(slot.Box)
                select slot.With(box: slotBox))
            select box.With(number: number, slots: slots);
    }
}