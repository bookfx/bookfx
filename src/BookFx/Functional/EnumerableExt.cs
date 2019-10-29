namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using static F;
    using Unit = System.ValueTuple;

    internal static class EnumerableExt
    {
        public static IEnumerable<TR> Map<T, TR>(this IEnumerable<T> list, Func<T, TR> f) =>
            list.Select(f);

        public static IEnumerable<TR> Map<T, TR>(this IEnumerable<T> list, Func<T, int, TR> f) =>
            list.Select(f);

        public static IEnumerable<TR> Bind<T, TR>(this IEnumerable<T> list, Func<T, IEnumerable<TR>> f) =>
            list.SelectMany(f);

        public static IEnumerable<TR> Bind<T, TR>(this IEnumerable<T> list, Func<T, Option<TR>> f) =>
            list.SelectMany(x => f(x).AsEnumerable());

        public static IEnumerable<Unit> ForEach<T>(this IEnumerable<T> list, Action<T> action) =>
            list.Map(action.ToFunc()).ToImmutableList();

        public static IEnumerable<Unit> ForEach<T>(this IEnumerable<T> list, Action<T, int> action) =>
            list.Map(action.ToFunc()).ToImmutableList();

        public static TR Match<T, TR>(this IEnumerable<T> list, Func<TR> empty, Func<T, IEnumerable<T>, TR> more)
        {
            using var enumerator = list.GetEnumerator();

            return enumerator.MoveNext()
                ? more(enumerator.Current, Others(enumerator))
                : empty();

            IEnumerable<T> Others(IEnumerator<T> e)
            {
                while (e.MoveNext())
                {
                    yield return e.Current;
                }
            }
        }

        public static TR Match<T, TR>(
            this IEnumerable<T> list,
            Func<TR> empty,
            Func<T, TR> one,
            Func<T, IEnumerable<T>, TR> more) =>
            list.Match(
                empty: empty,
                more: (first, afterFirst) => afterFirst.Match(
                    empty: () => one(first),
                    more: (second, afterSecond) => more(first, afterSecond.Prepend(second)))
            );

        public static Option<T> Find<T>(this IEnumerable<T> list, Func<T, bool> predicate) =>
            list.Where(predicate).Head();

        public static Option<T> Head<T>(this IEnumerable<T> list) =>
            list.Match(
                empty: () => None,
                more: (x, xs) => Some(x));

        public static IEnumerable<T> NonUnique<T>(this IEnumerable<T> list) =>
            list
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key);
    }
}