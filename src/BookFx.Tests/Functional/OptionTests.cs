namespace BookFx.Tests.Functional
{
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class OptionTests
    {
        [Fact]
        public void Equals_BothAreNone_True()
        {
            Option<int> sut = None;

            var result = sut.Equals(None);

            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_BothValuesAreEquals_True()
        {
            var sut = Some(1);

            var result = sut.Equals(Some(1));

            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_BothValuesAreNotEquals_False()
        {
            var sut = Some(1);

            var result = sut.Equals(Some(2));

            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsOp_BothAreNone_True()
        {
            Option<int> sut = None;

            var result = sut == None;

            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsOp_BothValuesAreEquals_True()
        {
            var sut = Some(1);

            var result = sut == Some(1);

            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsOp_BothValuesAreNotEquals_False()
        {
            var sut = Some(1);

            var result = sut == Some(2);

            result.Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOp_BothValuesAreEquals_False()
        {
            var sut = Some(1);

            var result = sut != Some(1);

            result.Should().BeFalse();
        }

        [Fact]
        public void Match_None_NoneFuncResult()
        {
            var sut = None;

            var result = MatchConsumer(sut);

            result.Should().Be("No value");
        }

        [Fact]
        public void Match_Some_SomeFuncResult()
        {
            var sut = Some("some string");

            var result = MatchConsumer(sut);

            result.Should().Be("Value is some string");
        }

        private static string MatchConsumer(Option<string> value)
        {
            return value.Match(
                none: () => "No value",
                some: x => $"Value is {x}");
        }
    }
}