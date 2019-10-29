namespace BookFx.Usage.Functional
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public static class F
    {
        public static IEnumerable<T> List<T>(T item, params T[] others) => others.Prepend(item).ToImmutableList();
    }
}