namespace BookFx.Tests.Renders
{
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    public class BoxRowSizesRenderTests
    {
        [Theory]
        [InlineData(10f)]
        [InlineData(20f)]
        public void RowSizesRender_SomeRowSize_Set(float size) =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(size).Get.LayOut();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).Height.Should().Be(size);
            });

        [Fact]
        public void RowSizesRender_NoneRowSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.None).Get.LayOut();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).Height.Should().Be(excelSheet.DefaultRowHeight);
            });

        [Fact]
        public void RowSizesRender_FitRowSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.Fit).Get.LayOut();

                box.RowSizesRender()(excelSheet);

                excelSheet.Row(1).CustomHeight.Should().BeFalse();
            });

        [Fact]
        public void RowSizesRender_TooManyRowSizes_Invalid() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeRows(TrackSize.Fit, TrackSize.Fit).Get.LayOut();

                var result = box.RowSizesRender()(excelSheet);

                result.Should().Be(
                    Invalid<Unit>(Errors.Box.RowSizeCountIsInvalid(sizeCount: 2, boxHeight: 1)));
            });
    }
}