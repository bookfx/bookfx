namespace BookFx.Calculation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class EnumerableResultCacheExt
    {
        // todo via AggMap?
        public static (int, Cache) Max<T>(
            this IEnumerable<T> list,
            (int, Cache) seed,
            Func<T, Cache, (int, Cache)> selector) =>
            list.Aggregate(
                seed,
                (acc, curr) =>
                {
                    var (accValue, accCache) = acc;
                    var (currValue, newCache) = selector(curr, accCache);
                    return (Math.Max(accValue, currValue), newCache);
                });

        // todo via AggMap?
        public static (int, Cache) Sum<T>(
            this IEnumerable<T> list,
            (int, Cache) seed,
            Func<T, Cache, (int, Cache)> selector) =>
            list.Aggregate(
                seed,
                (acc, curr) =>
                {
                    var (accValue, accCache) = acc;
                    var (currValue, newCache) = selector(curr, accCache);
                    return (accValue + currValue, newCache);
                });

        // todo AggMap?
        public static (IEnumerable<TR>, Cache) AggMap<T, TR>(
            this IEnumerable<T> list,
            Cache cache,
            Func<T, Cache, (TR, Cache)> selector) =>
            list.Aggregate(
                (Enumerable.Empty<TR>(), cache),
                (acc, curr) =>
                {
                    var (accValue, accCache) = acc;
                    var (currValue, newCache) = selector(curr, accCache);
                    return (accValue.Append(currValue), newCache);
                });
    }
}