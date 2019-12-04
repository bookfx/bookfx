namespace BookFx.Tests.Validation
{
    using BookFx.Tests.Arbitraries;
    using BookFx.Validation;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    public class StyleValidatorTests
    {
        [Fact]
        public void FontSize_None_Valid()
        {
            var style = Make.Style().Get;

            var result = StyleValidator.FontSize(style);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(ValidFontSizeArb) })]
        public void FontSize_ValidSizes_Valid(double size)
        {
            var style = Make.Style().Font(size).Get;

            var result = StyleValidator.FontSize(style);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(InvalidFontSizeArb) })]
        public void FontSize_InvalidSizes_Invalid(double size)
        {
            var style = Make.Style().Font(size).Get;

            var result = StyleValidator.FontSize(style);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IndentSize_None_Valid()
        {
            var style = Make.Style().Get;

            var result = StyleValidator.IndentSize(style);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(ValidIndentSizeArb) })]
        public void IndentSize_ValidSizes_Valid(int size)
        {
            var style = Make.Style().Indent(size).Get;

            var result = StyleValidator.IndentSize(style);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(InvalidIndentSizeArb) })]
        public void IndentSize_InvalidSizes_Invalid(int size)
        {
            var style = Make.Style().Indent(size).Get;

            var result = StyleValidator.IndentSize(style);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void FontName_Empty_Invalid()
        {
            var style = Make.Style().Font(string.Empty).Get;

            var result = StyleValidator.FontName(style);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Format_Empty_Invalid()
        {
            var style = Make.Style().Format(string.Empty).Get;

            var result = StyleValidator.Format(style);

            result.IsValid.Should().BeFalse();
        }
    }
}