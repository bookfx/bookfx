namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        int
    >;

    internal static class CacheExt
    {
        public static (int, Cache) GetOrEval(
            this Cache cache,
            BoxCore box,
            Measure measure,
            Func<int> eval) =>
            cache.GetOrEval(box, measure, () => (eval(), cache));

        public static (int, Cache) GetOrEval(
            this Cache cache,
            BoxCore box,
            Measure measure,
            Func<(int, Cache)> eval) =>
            cache
                .TryGetValue((box, measure))
                .Map(result => (result, cache))
                .GetOrElse(() =>
                {
                    var (result, newCache) = eval();
                    return (result, newCache.Add((box, measure), result));
                });
    }
}