namespace BookFx.Calculation
{
    using System;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using Cache = System.Collections.Immutable.ImmutableDictionary<
        (BookFx.Cores.BoxCore, BookFx.Calculation.Measure),
        BookFx.Functional.Result<int>
    >;

    internal static class ResultCacheExt
    {
        public static (Result<TR>, Cache) Map<T, TR>(
            this (Result<T> Result, Cache Cache) rc,
            Func<T, Cache, (TR, Cache)> mapper) =>
            rc.Result.Match(
                invalid: errors => (Invalid(errors), rc.Cache),
                valid: value =>
                {
                    var (newValue, newCache) = mapper(value, rc.Cache);
                    return (Valid(newValue), newCache);
                });

        public static (Result<TR>, Cache) Bind<T, TR>(
            this (Result<T> Result, Cache Cache) rc,
            Func<T, Cache, (Result<TR>, Cache)> binder) =>
            rc.Result.Match(
                invalid: errors => (Invalid(errors), rc.Cache),
                valid: value => binder(value, rc.Cache));
    }
}