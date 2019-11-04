namespace BookFx.Calculation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class EnumerableResultCacheExt
    {
        public static (Result<int>, Cache) Max<T>(
            this IEnumerable<T> list,
            (Result<int>, Cache) seed,
            Func<T, Cache, (Result<int>, Cache)> selector) =>
            list.Aggregate(
                seed,
                (acc, curr) =>
                {
                    var (accResult, accCache) = acc;
                    var (currResult, newCache) = selector(curr, accCache);
                    var newResult =
                        from accValue in accResult
                        from currValue in currResult
                        select Math.Max(accValue, currValue);
                    return (newResult, newCache);
                });

        public static (Result<int>, Cache) Sum<T>(
            this IEnumerable<T> list,
            (Result<int>, Cache) seed,
            Func<T, Cache, (Result<int>, Cache)> selector) =>
            list.Aggregate(
                seed,
                (acc, curr) =>
                {
                    var (accResult, accCache) = acc;
                    var (currResult, newCache) = selector(curr, accCache);
                    var newResult =
                        from accValue in accResult
                        from currValue in currResult
                        select accValue + currValue;
                    return (newResult, newCache);
                });
    }
}