namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    internal static class ScComposition
    {
        [Pure]
        public static Sc<TS, TV> Compose<TS, TV>(
            this IEnumerable<Sc<TS, TV>> scs,
            TV seed,
            Func<TV, TV, TV> f) =>
            scs.Aggregate(
                seed: Sc<TS>.ScOf(seed),
                func: (accSc, currSc) =>
                    from acc in accSc
                    from curr in currSc
                    select f(acc, curr));
    }
}