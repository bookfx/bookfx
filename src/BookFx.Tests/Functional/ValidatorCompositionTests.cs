namespace BookFx.Tests.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using static BookFx.Functional.F;
    using static BookFx.Functional.ValidatorComposition;

    public class ValidatorCompositionTests
    {
        [Property]
        public void HarvestErrors_ValidValidators_Valid(bool[] array, int value)
        {
            var validators = array.Map(_ => GetValidValidator());

            var result = HarvestErrors(validators)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void HarvestErrors_InvalidValidators_InvalidWithItsErrors(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var validators = errors.Map(GetInvalidValidator);

            var result = HarvestErrors(validators)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors);
        }

        [Property]
        public void HarvestErrors_ValidAndInvalid_InvalidWithErrors(
            bool[] array,
            NonEmptyArray<string> messages,
            int value)
        {
            var errors = GetErrors(messages.Get);
            var validValidators = array.Map(_ => GetValidValidator());
            var invalidValidators = errors.Map(GetInvalidValidator);
            var validators = validValidators.Concat(invalidValidators);

            var result = HarvestErrors(validators)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors);
        }

        [Property]
        public void FailFast_ValidValidators_Valid(bool[] array, int value)
        {
            var validators = array.Map(_ => GetValidValidator());

            var result = FailFast(validators)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void FailFast_InvalidValidators_InvalidWithFirstError(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var validators = errors.Map(GetInvalidValidator);

            var result = FailFast(validators)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors.First());
        }

        [Property]
        public void FailFast_ValidAndInvalid_InvalidWithFirstError(
            bool[] array,
            NonEmptyArray<string> messages,
            int value)
        {
            var errors = GetErrors(messages.Get);
            var validValidators = array.Map(_ => GetValidValidator());
            var invalidValidators = errors.Map(GetInvalidValidator);
            var validators = validValidators.Concat(invalidValidators);

            var result = FailFast(validators)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors.First());
        }

        private static Error[] GetErrors(IEnumerable<string> messages) => messages.Map(x => new Error(x)).ToArray();

        private static Validator<int> GetValidValidator() => Valid;

        private static Validator<int> GetInvalidValidator(Error error) => _ => Invalid(error);
    }
}