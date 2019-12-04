namespace BookFx.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    internal static class ValidatorComposition
    {
        public static Validator<T> FailFast<T>(params Validator<T>[] validators) => FailFast(validators.AsEnumerable());

        public static Validator<T> FailFast<T>(IEnumerable<Validator<T>> validators) =>
            x => validators
                .Aggregate(
                    Valid(x),
                    (acc, validator) => acc.Bind(_ => validator(x)));

        public static Validator<T> HarvestErrors<T>(params Validator<T>[] validators) =>
            HarvestErrors(validators.AsEnumerable());

        public static Validator<T> HarvestErrors<T>(IEnumerable<Validator<T>> validators) =>
            x => validators
                .Traverse(validator => validator(x))
                .Map(_ => x);
    }
}