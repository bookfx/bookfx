namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static MinWidthCalc;

    internal static class WidthCalc
    {
        public static Sc<Cache, int> Width(BoxCore box, Structure structure) =>
            from cache in GetState
            from result in cache.GetOrCompute(
                key: (box, Measure.Width),
                sc: () => box.Match(
                    row: _ => OfComposite(box),
                    col: _ => OfComposite(box),
                    stack: _ => OfComposite(box),
                    value: _ => OfValue(box, structure),
                    proto: _ => OfComposite(box)))
            select result;

        private static Sc<Cache, int> OfComposite(BoxCore box) => MinWidth(box);

        private static Sc<Cache, int> OfValue(BoxCore box, Structure structure) =>
            box
                .ColSpan
                .Map(ScOf)
                .OrElse(() => structure
                    .Parent(box)
                    .Map(
                        row: _ => ScOf(1),
                        col: MinWidth,
                        stack: MinWidth,
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => ScOf(1)))
                .GetOrElse(ScOf(1));
    }
}