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
            state =>
            {
                var rs = Enumerable.Empty<TR>();
                var accState = state;

                foreach (var x in xs)
                {
                    var sc = f(x);
                    var (r, newState) = sc(accState);
                    accState = newState;
                    rs = rs.Append(r);
                }

                return (rs, accState);
            };
    }
}