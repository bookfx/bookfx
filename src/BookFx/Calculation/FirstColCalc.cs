namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        int
    >;

    internal static class FirstColCalc
    {
        public static (int Result, Cache Cache) Perform(BoxCore box, Cache cache, Structure structure) =>
            throw new NotImplementedException();
    }
}