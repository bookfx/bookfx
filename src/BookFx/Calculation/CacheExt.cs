namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class CacheExt
    {
        // todo test
        public static Sc<Cache, int> GetOrCompute(
            this Cache cache,
            (BoxCore Box, Measure Measure) key,
            Func<Sc<Cache, int>> computation) =>
            cache
                .TryGetValue(key)
                .Map(Sc<Cache>.Return)
                .GetOrElse(computation);
    }
}