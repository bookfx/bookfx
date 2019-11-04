namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class CacheExt
    {
        public static (Result<int>, Cache) GetOrEval(
            this Cache cache,
            BoxCore box,
            Measure measure,
            Func<Result<int>> eval) =>
            cache.GetOrEval(box, measure, () => (eval(), cache));

        public static (Result<int>, Cache) GetOrEval(
            this Cache cache,
            BoxCore box,
            Measure measure,
            Func<(Result<int>, Cache)> eval) =>
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