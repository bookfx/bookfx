namespace BookFx.Tests.Functional
{
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class OptionResultExtTests
    {
        [Fact]
        public void Traverse_SomeValid_ValidSome() => Some(0).Traverse(Valid).Should().Be(Valid(Some(0)));

        [Fact]
        public void Traverse_SomeInvalid_Invalid() =>
            Some(0).Traverse(_ => Invalid<int>()).Should().Be(Invalid<Option<int>>());

        [Fact]
        public void Traverse_NoneValid_ValidNone() =>
            ((Option<int>)None).Traverse(Valid).Should().Be(Valid((Option<int>)None));

        [Fact]
        public void Traverse_NoneInvalid_ValidNone() =>
            ((Option<int>)None).Traverse(_ => Invalid<int>()).Should().Be(Valid((Option<int>)None));
    }
}