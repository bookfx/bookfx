namespace BookFx.Usage.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    public static class EnumerableExt
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> xs, int chunkSize) =>
            xs
                .Select((x, i) => (x, i))
                .GroupBy(t => t.i / chunkSize)
                .Select(g => g.Select(t => t.x).ToList())
                .ToList();

        public static IEnumerable<T> Delimit<T>(this IEnumerable<T> xs, T delimiter) =>
            xs.SelectMany(x => List(delimiter, x)).Skip(1);
    }
}