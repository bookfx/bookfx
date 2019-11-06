namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;

    internal static class CacheExt
    {
        public static Sc<Cache, int> GetOrCompute(
            this Cache cache,
            (BoxCore Box, Measure Measure) key,
            Func<Sc<Cache, int>> sc) =>
            cache
                .TryGetValue(key)
                .Map(Sc<Cache>.ScOf)
                .GetOrElse(() =>
                    from result in sc()
                    from unused in Sc.PutState(cache.Add(key, result))
                    select result);
    }
}