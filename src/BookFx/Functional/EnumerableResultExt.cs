namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    internal static class EnumerableResultExt
    {
        public static Result<IEnumerable<TR>> Traverse<T, TR>(this IEnumerable<T> xs, Func<T, Result<TR>> f) =>
            TraverseA(xs, f);

        public static Result<IEnumerable<TR>> TraverseM<T, TR>(this IEnumerable<T> xs, Func<T, Result<TR>> f) =>
            xs.Aggregate(
                seed: Valid(Enumerable.Empty<TR>()),
                func: (result, x) =>
                    from rs in result
                    from r in f(x)
                    select rs.Append(r));

        public static Result<IEnumerable<TR>> TraverseA<T, TR>(this IEnumerable<T> xs, Func<T, Result<TR>> f) =>
            xs.Aggregate(
                seed: Valid(Enumerable.Empty<TR>()),
                func: (result, x) => Valid(Append<TR>())
                    .Apply(result)
                    .Apply(f(x)));

        public static Result<IEnumerable<T>> FailFast<T>(this IEnumerable<Result<T>> xs) =>
            xs.TraverseM(x => x);

        public static Result<IEnumerable<T>> HarvestErrors<T>(this IEnumerable<Result<T>> xs) =>
            xs.TraverseA(x => x);

        private static Func<IEnumerable<T>, T, IEnumerable<T>> Append<T>() => (xs, x) => xs.Append(x);
    }
}