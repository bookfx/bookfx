namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    internal static class ScComposition
    {
        // todo test
        [Pure]
        public static Sc<TS, TV> Compose<TS, TV>(
            this IEnumerable<Sc<TS, TV>> computations,
            TV seed,
            Func<TV, TV, TV> f) =>
            computations.Aggregate(
                seed: Sc<TS>.Return(seed),
                func: (accSc, currSc) =>
                    from acc in accSc
                    from curr in currSc
                    select f(acc, curr));
    }
}