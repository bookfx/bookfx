namespace BookFx.Tests.Arbitraries
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using FsCheck;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    public static class PlacingBoxArb
    {
        [UsedImplicitly]
        public static Arbitrary<BoxCore> Box() => BoxGen().ToArbitrary();

        private static Gen<BoxCore> BoxGen() => Gen.Sized(BoxGen);

        private static Gen<BoxCore> BoxGen(int size) =>
            size == 0
                ? Gen.Constant(Make.Value().Get)
                : BoxNonStopGen(size);

        private static Gen<BoxCore> BoxNonStopGen(int size) =>
            from children in Gen.ArrayOf(BoxGen(size - 1))
            from spans in Gen.OneOf(Gen.Choose(1, 5).Select(Some), Gen.Constant((Option<int>)None)).Two()
            from box in Gen.Frequency(
                Tuple.Create(2, Gen.Constant(Make.Row().Get.With(children: children))),
                Tuple.Create(2, Gen.Constant(Make.Col().Get.With(children: children))),
                Tuple.Create(1, Gen.Constant(Make.Stack().Get.With(children: children))),
                Tuple.Create(5, Gen.Constant(Make.Value().Get.With(rowSpan: spans.Item2, colSpan: spans.Item1))))
            select box;
    }
}