namespace BookFx.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    internal static class ActComposition
    {
        public static Act<T> FailFast<T>(params Act<T>[] acts) => FailFast(acts.AsEnumerable());

        public static Act<T> FailFast<T>(IEnumerable<Act<T>> acts) =>
            x => acts
                .Aggregate(
                    Valid(Unit()),
                    (acc, act) => acc.Bind(_ => act(x)));

        public static Act<T> HarvestErrors<T>(params Act<T>[] acts) =>
            HarvestErrors(acts.AsEnumerable());

        public static Act<T> HarvestErrors<T>(IEnumerable<Act<T>> acts) =>
            x => acts
                .Traverse(act => act(x))
                .Map(_ => Unit());
    }
}