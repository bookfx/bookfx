namespace BookFx.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    internal static class TeeComposition
    {
        public static Tee<T> FailFast<T>(params Tee<T>[] tees) => FailFast(tees.AsEnumerable());

        public static Tee<T> FailFast<T>(IEnumerable<Tee<T>> tees) =>
            x => tees
                .Aggregate(
                    Valid(x),
                    (acc, tee) => acc.Bind(_ => tee(x)));

        public static Tee<T> HarvestErrors<T>(params Tee<T>[] tees) =>
            HarvestErrors(tees.AsEnumerable());

        public static Tee<T> HarvestErrors<T>(IEnumerable<Tee<T>> tees) =>
            x => tees
                .Traverse(tee => tee(x))
                .Map(_ => x);
    }
}