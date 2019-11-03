namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using JetBrains.Annotations;
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
            var collection = list.ToCollection();
            return collection.Any() ? more(collection.First(), collection.Skip(1)) : empty();
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

        // todo test
        [Pure]
        public static IEnumerable<(T Prev, T Next)> Neighbors<T>(this IEnumerable<T> list)
        {
            var collection = list.ToCollection();

            return Enumerable.Zip(
                collection,
                collection.Skip(1),
                (x1, x2) => (x1, x2));
        }

        private static IReadOnlyCollection<T> ToCollection<T>(this IEnumerable<T> enumerable) =>
            enumerable is IReadOnlyCollection<T> collection
                ? collection
                : enumerable.ToArray();
    }
}