namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    internal static class ScComposition
    {
        [Pure]
        public static Sc<TS, TV> Compose<TS, TV>(
            this IEnumerable<Sc<TS, TV>> scs,
            TV seed,
            Func<TV, TV, TV> f) =>
            state =>
            {
                var accValue = seed;
                var accState = state;

                foreach (var sc in scs)
                {
                    var (scValue, newState) = sc(accState);
                    accValue = f(accValue, scValue);
                    accState = newState;
                }

                return (accValue, accState);
            };
    }
}