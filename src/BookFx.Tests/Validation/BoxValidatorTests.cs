namespace BookFx.Tests.Validation
{
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Functional;
    using BookFx.Tests.Arbitraries;
    using BookFx.Validation;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    public class BoxValidatorTests
    {
        [Fact]
        public void RowSpanSize_None_Valid()
        {
            var box = Make.Value().Get.Place();

            var result = BoxValidator.RowSpanSize(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(16_385)]
        [InlineData(1_000_000)]
        [InlineData(1_048_576)]
        public void RowSpanSize_ValidSizes_Valid(int size)
        {
            var box = Make.Value().SpanRows(size).Get.Place();

            var result = BoxValidator.RowSpanSize(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-20_000)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1_048_577)]
        [InlineData(10_000_000)]
        public void RowSpanSize_InvalidSizes_Invalid(int size)
        {
            var box = Make.Value().SpanRows(size).Get.Place();

            var result = BoxValidator.RowSpanSize(box);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ColSpanSize_None_Valid()
        {
            var box = Make.Value().Get.Place();

            var result = BoxValidator.ColSpanSize(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(16_384)]
        public void ColSpanSize_ValidSizes_Valid(int size)
        {
            var box = Make.Value().SpanCols(size).Get.Place();

            var result = BoxValidator.ColSpanSize(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-20_000)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(16_385)]
        [InlineData(100_000)]
        public void ColSpanSize_InvalidSizes_Invalid(int size)
        {
            var box = Make.Value().SpanCols(size).Get.Place();

            var result = BoxValidator.ColSpanSize(box);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void RowSizeRange_No_Valid()
        {
            var box = Make.Value().Get.Place();

            var result = BoxValidator.RowSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void RowSizeRange_NoneAndFit_Valid()
        {
            var box = Make.Value().SizeRows(TrackSize.None, TrackSize.Fit).Get.Place();

            var result = BoxValidator.RowSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(ValidRowSizeArb) })]
        public void RowSizeRange_ValidSizes_Valid(TrackSize[] sizes)
        {
            var box = Make.Value().SizeRows(sizes).Get.Place();

            var result = BoxValidator.RowSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-20_000d)]
        [InlineData(-1d)]
        [InlineData(410d)]
        [InlineData(100_000d)]
        public void RowSizeRange_InvalidSizes_Invalid(double size)
        {
            var box = Make.Value().SizeRows(size).Get.Place();

            var result = BoxValidator.RowSizeRange(box);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ColSizeRange_No_Valid()
        {
            var box = Make.Value().Get.Place();

            var result = BoxValidator.ColSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ColSizeRange_NoneAndFit_Valid()
        {
            var box = Make.Value().SizeCols(TrackSize.None, TrackSize.Fit).Get.Place();

            var result = BoxValidator.ColSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Property(Arbitrary = new[] { typeof(ValidColSizeArb) })]
        public void ColSizeRange_ValidSizes_Valid(TrackSize[] sizes)
        {
            var box = Make.Value().SizeCols(sizes).Get.Place();

            var result = BoxValidator.ColSizeRange(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(-20_000d)]
        [InlineData(-1d)]
        [InlineData(256d)]
        [InlineData(100_000d)]
        public void ColSizeRange_InvalidSizes_Invalid(double size)
        {
            var box = Make.Value().SizeCols(size).Get.Place();

            var result = BoxValidator.ColSizeRange(box);

            result.IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("_")]
        [InlineData("_1")]
        [InlineData("_123")]
        [InlineData("A")]
        [InlineData("name0")]
        [InlineData("year")]
        [InlineData("год")]
        [InlineData("έτος")]
        [InlineData("年")]
        public void Name_ValidName_ValidResult(string rangeName)
        {
            var box = Make.Value().NameGlobally(rangeName).Get;

            var result = BoxValidator.Name(x => x.GlobalName)(box);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("123")]
        [InlineData("1year")]
        [InlineData("1год")]
        [InlineData("A1")]
        [InlineData("R1C1")]
        [InlineData("rc")]
        [InlineData("rc1")]
        [InlineData("r1c")]
        [InlineData("r")]
        [InlineData("c")]
        [InlineData("XFD1048576")]
        [InlineData("R1048576C16384")]
        [InlineData("-")]
        [InlineData("!")]
        [InlineData("=")]
        public void Name_InvalidName_InvalidResult(string rangeName)
        {
            var box = Make.Value().NameGlobally(rangeName).Get;

            var result = BoxValidator.Name(x => x.GlobalName)(box);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Style_ValidStyle_Valid()
        {
            var box = Make.Value().Style(Make.Style()).Get.Place();

            var result = BoxValidator.Style(box);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Style_InvalidStyle_Invalid()
        {
            const double invalidFontSize = -1d;
            var box = Make.Value().Style(Make.Style().Font(invalidFontSize)).Get.Place();

            var result = BoxValidator.Style(box);

            result.ErrorsUnsafe()
                .Should()
                .BeEquivalentTo(List(Errors.Box.Aggregate(
                    box,
                    inners: List(Errors.Style.FontSizeIsInvalid(invalidFontSize)))));
        }

        [Fact]
        public void Style_InvalidStyles_1ErrorWith2Inners()
        {
            var box = Make
                .Value()
                .Style(Make
                    .Style()
                    .Font(-1)
                    .Indent(-1)
                )
                .Get
                .Place();

            var result = BoxValidator.Style(box);

            result.ErrorsUnsafe().Single().Inners.Should().HaveCount(2);
        }
    }
}