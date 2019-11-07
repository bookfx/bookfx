namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static MinWidthCalc;

    internal static class WidthCalc
    {
        public static int Width(BoxCore box, Structure structure, Cache cache) =>
            cache.GetOrCompute(
                key: (box, Measure.Width),
                f: () => box.Match(
                    row: _ => OfComposite(box, cache),
                    col: _ => OfComposite(box, cache),
                    stack: _ => OfComposite(box, cache),
                    value: _ => OfValue(box, structure, cache),
                    proto: _ => OfComposite(box, cache)));

        private static int OfComposite(BoxCore box, Cache cache) => MinWidth(box, cache);

        private static int OfValue(BoxCore box, Structure structure, Cache cache) =>
            box
                .ColSpan
                .OrElse(() => structure
                    .Parent(box)
                    .Map(
                        row: _ => 1,
                        col: parent => MinWidth(parent, cache),
                        stack: parent => MinWidth(parent, cache),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ => 1))
                .GetOrElse(1);
    }
}