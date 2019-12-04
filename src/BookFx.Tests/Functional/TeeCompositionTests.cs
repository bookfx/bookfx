namespace BookFx.Tests.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using static BookFx.Functional.ActComposition;
    using static BookFx.Functional.F;

    public class ActCompositionTests
    {
        [Property]
        public void HarvestErrors_ValidActs_Valid(bool[] array, int value)
        {
            var acts = array.Map(_ => GetValidAct());

            var result = HarvestErrors(acts)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void HarvestErrors_InvalidActs_InvalidWithItsErrors(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var acts = errors.Map(GetInvalidAct);

            var result = HarvestErrors(acts)(value);

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
            var validActs = array.Map(_ => GetValidAct());
            var invalidActs = errors.Map(GetInvalidAct);
            var acts = validActs.Concat(invalidActs);

            var result = HarvestErrors(acts)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors);
        }

        [Property]
        public void FailFast_ValidActs_Valid(bool[] array, int value)
        {
            var acts = array.Map(_ => GetValidAct());

            var result = FailFast(acts)(value);

            result.IsValid.Should().BeTrue();
        }

        [Property]
        public void FailFast_InvalidActs_InvalidWithFirstError(NonEmptyArray<string> messages, int value)
        {
            var errors = GetErrors(messages.Get);
            var acts = errors.Map(GetInvalidAct);

            var result = FailFast(acts)(value);

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
            var validActs = array.Map(_ => GetValidAct());
            var invalidActs = errors.Map(GetInvalidAct);
            var acts = validActs.Concat(invalidActs);

            var result = FailFast(acts)(value);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().Should().Equal(errors.First());
        }

        private static Error[] GetErrors(IEnumerable<string> messages) => messages.Map(x => new Error(x)).ToArray();

        private static Act<int> GetValidAct() => _ => Valid(Unit());

        private static Act<int> GetInvalidAct(Error error) => _ => Invalid(error);
    }
}