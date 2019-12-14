namespace BookFx.Tests.Renders
{
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    public class BoxRowSizesRenderTests
    {
        [Theory]
        [InlineData(10d)]
        [InlineData(20d)]
        public void RowSizesRender_SomeRowSize_Set(double size) =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(size).Get.Place();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).Height.Should().Be(size);
            });

        [Fact]
        public void RowSizesRender_NoRowSizes_NotSet() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().Get.Place();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).Height.Should().Be(excelSheet.DefaultRowHeight);
            });

        [Fact]
        public void RowSizesRender_NoneRowSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.None).Get.Place();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).Height.Should().Be(excelSheet.DefaultRowHeight);
            });

        [Fact]
        public void RowSizesRender_FitRowSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.Fit).Get.Place();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).CustomHeight.Should().BeFalse();
            });

        [Fact]
        public void RowSizesRender_TooManyRowSizes_Invalid() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.Fit, TrackSize.Fit).Get.Place();

                var result = box.RowSizesRender()(excelSheet);

                result.Should().Be(
                    Invalid<Unit>(Errors.Box.RowSizeCountIsInvalid(sizeCount: 2, boxHeight: 1)));
            });

        [Fact]
        public void RowSizesRender_PatternLessThanBoxHeight_Repeats() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(10, 20).SpanRows(5).Get.Place();

                box.RowSizesRender()(excelSheet);

                Enumerable.Range(1, 5).Map(excelSheet.Row).Map(x => x.Height).Should().Equal(10, 20, 10, 20, 10);
            });
    }
}