namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class HeightCalc
    {
        public static (Result<int> Result, Cache Cache) Perform(BoxCore box, Cache cache, Structure structure) =>
            throw new NotImplementedException();
    }
}