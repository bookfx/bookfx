namespace BookFx.Tests.Validation
{
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Functional;
    using BookFx.Validation;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class SheetValidatorTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData("asdf")]
        [InlineData("1234567890123456789012345678901")]
        public void SheetName_ValidNames_Valid(string name)
        {
            var sheet = Make.Sheet().Name(name).Get.Place();

            SheetValidator.SheetName(sheet).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(":")]
        [InlineData(@"\")]
        [InlineData("/")]
        [InlineData("?")]
        [InlineData("*")]
        [InlineData("[")]
        [InlineData("]")]
        [InlineData("[asdf]")]
        [InlineData("12345678901234567890123456789012")]
        [InlineData("12345678901234567890123456789012asdf")]
        public void SheetName_InvalidNames_Invalid(string name)
        {
            var sheet = Make.Sheet().Name(name).Get.Place();

            SheetValidator.SheetName(sheet).IsValid.Should().BeFalse();
        }

        [Fact]
        public void Boxes_ValidBox_Valid()
        {
            var sheet = Make.Sheet(Make.Value()).Get.Place();

            var result = SheetValidator.Boxes(sheet);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Boxes_InvalidBox_Invalid()
        {
            const int invalidSpan = -1;
            var sheet = Make.Sheet(Make.Value().SpanCols(invalidSpan)).Get.Place();

            var result = SheetValidator.Boxes(sheet);

            result.ErrorsUnsafe()
                .Should()
                .BeEquivalentTo(Errors.Sheet.Aggregate(sheet, inners: List(Errors.Box.ColSpanIsInvalid(invalidSpan))));
        }

        [Fact]
        public void Boxes_InvalidBoxes_1ErrorWith2Inners()
        {
            var invalidSize = TrackSize.Some(-1);
            var sheet = Make.Sheet(
                    Make.Row(
                        Make.Value().SizeRows(invalidSize),
                        Make.Value().SizeRows(invalidSize)
                    )
                )
                .Get
                .Place();

            var result = SheetValidator.Boxes(sheet);

            result.ErrorsUnsafe().Single().Inners.Should().HaveCount(2);
        }

        [Fact]
        public void RootBoxWidth_SmallBox_Valid()
        {
            var sheet = Make.Sheet(Make.Value().SpanCols(5)).Get.Place();

            var result = SheetValidator.RootBoxWidth(sheet);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void RootBoxWidth_BigBox_Invalid()
        {
            var sheet = Make.Sheet(
                    Make.Row(
                        Make.Value().SpanCols(16_000),
                        Make.Value().SpanCols(16_000)
                    )
                )
                .Get
                .Place();

            var result = SheetValidator.RootBoxWidth(sheet);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().First().Should().BeOfType<Errors.Sheet.RootBoxWidthTooBigError>();
        }

        [Fact]
        public void RootBoxHeight_SmallBox_Valid()
        {
            var sheet = Make.Sheet(Make.Value().SpanRows(5)).Get.Place();

            var result = SheetValidator.RootBoxHeight(sheet);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void RootBoxHeight_BigBox_Invalid()
        {
            var sheet = Make.Sheet(
                    Make.Col(
                        Make.Value().SpanRows(1_000_000),
                        Make.Value().SpanRows(1_000_000)
                    )
                )
                .Get
                .Place();

            var result = SheetValidator.RootBoxHeight(sheet);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().First().Should().BeOfType<Errors.Sheet.RootBoxHeightTooBigError>();
        }
    }
}