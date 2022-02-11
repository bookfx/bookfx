namespace BookFx.Tests.Validation
{
    using System.Linq;
    using BookFx.Calculation;
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
        public void BoxLocalNameUniqueness_UniqueBoxNames_Valid()
        {
            var sheet = Make.Sheet(Make.Row(Make.Value().NameLocally("A"), Make.Value().NameLocally("B"))).Get;

            var result = SheetValidator.BoxLocalNameUniqueness(sheet);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void BoxLocalNameUniqueness_NonUniqueBoxNames_Invalid()
        {
            var sheet = Make.Sheet(Make.Row(Make.Value().NameLocally("A"), Make.Value().NameLocally("A"))).Get;

            var result = SheetValidator.BoxLocalNameUniqueness(sheet);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Margins_Empty_Valid()
        {
            var sheet = Make.Sheet().Get;

            SheetValidator.Margins(sheet).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void Margins_ValidMargins_Valid(double margin)
        {
            var sheet = Make.Sheet().Margin(PageMargins.InCentimetres(margin)).Get;

            SheetValidator.Margins(sheet).IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void Margins_InvalidMargins_Invalid(double margin)
        {
            var sheet = Make.Sheet().Margin(PageMargins.InCentimetres(margin)).Get;

            SheetValidator.Margins(sheet).IsValid.Should().BeFalse();
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
                .BeEquivalentTo(List(Errors.Sheet.Aggregate(
                    sheet,
                    inners: List(Errors.Box.ColSpanIsInvalid(invalidSpan)))));
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
    }
}