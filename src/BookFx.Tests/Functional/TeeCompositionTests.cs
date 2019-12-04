namespace BookFx.Tests.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using static BookFx.Functional.F;
    using static BookFx.Functional.TeeComposition;

    public class TeeCompositionTests
    {
        [Property]
        public void HarvestErrors_ValidTees_Valid(bool[] array, int value)
        {
            var tees = array.Map(_ => GetValidTee());

            var result = HarvestErrors(tees)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void HarvestErrors_InvalidTees_InvalidWithItsErrors(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var tees = errors.Map(GetInvalidTee);

            var result = HarvestErrors(tees)(value);

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
            var validTees = array.Map(_ => GetValidTee());
            var invalidTees = errors.Map(GetInvalidTee);
            var tees = validTees.Concat(invalidTees);

            var result = HarvestErrors(tees)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors);
        }

        [Property]
        public void FailFast_ValidTees_Valid(bool[] array, int value)
        {
            var tees = array.Map(_ => GetValidTee());

            var result = FailFast(tees)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void FailFast_InvalidTees_InvalidWithFirstError(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var tees = errors.Map(GetInvalidTee);

            var result = FailFast(tees)(value);

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
            var validTees = array.Map(_ => GetValidTee());
            var invalidTees = errors.Map(GetInvalidTee);
            var tees = validTees.Concat(invalidTees);

            var result = FailFast(tees)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors.First());
        }

        private static Error[] GetErrors(IEnumerable<string> messages) => messages.Map(x => new Error(x)).ToArray();

        private static Tee<int> GetValidTee() => _ => Valid(Unit());

        private static Tee<int> GetInvalidTee(Error error) => _ => Invalid(error);
    }
}