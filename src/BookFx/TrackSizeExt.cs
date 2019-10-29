namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    internal static class TrackSizeExt
    {
        public static Unit ForEach(this TrackSize size, Action fit, Action<float> some) =>
            size.Match(
                none: () => Unit(),
                fit: fit.ToFunc(),
                some: value => some.ToFunc()(value));

        public static Unit Match(this TrackSize size, Action none, Action fit, Action<float> some) =>
            size.Match(none.ToFunc(), fit.ToFunc(), some.ToFunc());

        public static TrackSize OrElse(this TrackSize size, TrackSize another) =>
            size.Match(
                none: () => another,
                fit: () => size,
                some: _ => size);

        public static TrackSize OrElse(this TrackSize size, Func<TrackSize> fallback) =>
            size.Match(
                none: fallback,
                fit: () => size,
                some: _ => size);

        public static IEnumerable<TrackSize> TakeExact(this IEnumerable<TrackSize> sizes, int count) =>
            sizes
                .Concat(Enumerable.Repeat(TrackSize.None, count))
                .Take(count);

        public static IEnumerable<TrackSize> Complement(
            this IEnumerable<TrackSize> sizes,
            IEnumerable<TrackSize> complementation) =>
            sizes.Zip(complementation, (s, c) => s.OrElse(c));

        public static IEnumerable<float> Values(this IEnumerable<TrackSize> sizes) =>
            sizes.Bind(x => x.ValueAsEnumerable());
    }
}