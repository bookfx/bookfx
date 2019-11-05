namespace BookFx.Calculation
{
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        int
    >;

    // todo del
    internal static class ResultCacheExt
    {
        //public static (TR, Cache) Map<T, TR>(
        //    this (T Result, Cache Cache) rc,
        //    Func<T, Cache, (TR, Cache)> mapper) =>
        //    rc.Result.Match(
        //        invalid: errors => (Invalid(errors), rc.Cache),
        //        valid: value =>
        //        {
        //            var (newValue, newCache) = mapper(value, rc.Cache);
        //            return (Valid(newValue), newCache);
        //        });

        //public static (TR, Cache) Bind<T, TR>(
        //    this (T Result, Cache Cache) rc,
        //    Func<T, Cache, (TR, Cache)> binder) =>
        //    rc.Result.Match(
        //        invalid: errors => (Invalid(errors), rc.Cache),
        //        valid: value => binder(value, rc.Cache));
    }
}