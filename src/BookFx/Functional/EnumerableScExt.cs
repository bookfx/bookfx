namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    internal static class EnumerableScExt
    {
        [Pure]
        public static Sc<TS, IEnumerable<TR>> Traverse<TS, TV, TR>(this IEnumerable<TV> xs, Func<TV, Sc<TS, TR>> f) =>
            xs.Aggregate(
                seed: Sc<TS>.Return(Enumerable.Empty<TR>()),
                func: (sc, x) =>
                    from rs in sc
                    from r in f(x)
                    select rs.Append(r));
    }
}