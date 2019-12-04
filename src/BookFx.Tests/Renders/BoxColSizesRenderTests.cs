namespace BookFx.Tests.Renders
{
    using BookFx.Calculation;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    public class BoxColSizesRenderTests
    {
        [Theory]
        [InlineData(0d, 0d)]
        [InlineData(0.01d, 0d)]
        [InlineData(0.04d, 0d)]
        [InlineData(0.05d, 0.140625d)]
        [InlineData(0.09d, 0.140625d)]
        [InlineData(0.12d, 0.140625d)]
        [InlineData(0.13d, 0.28515625d)]
        [InlineData(0.2d, 0.28515625d)]
        [InlineData(0.21d, 0.42578125d)]
        [InlineData(0.29d, 0.42578125d)]
        [InlineData(1d, 1.7109375d)]
        [InlineData(1.01d, 1.7109375d)]
        [InlineData(1.09d, 1.85546875d)]
        [InlineData(1.1d, 1.85546875d)]
        [InlineData(1.92d, 2.5703125d)]
        [InlineData(1.93d, 2.7109375d)]
        [InlineData(2d, 2.7109375d)]
        [InlineData(3d, 3.7109375d)]
        [InlineData(9d, 9.7109375d)]
        [InlineData(100.01d, 100.7109375d)]
        [InlineData(100.09d, 100.85546875d)]
        [InlineData(100.1d, 100.85546875d)]
        [InlineData(107d, 107.7109375d)]
        [InlineData(107.3d, 108d)]
        [InlineData(107.5d, 108.28515625d)]
        [InlineData(107.7d, 108.42578125d)]
        public void ColSizesRender_SomeColSize_Set(double size, double expected) =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeCols(size).Get.Place();

                box.ColSizesRender()(excelSheet);

                excelSheet.Column(1).Width.Should().Be(expected);
            });

        [Fact]
        public void ColSizesRender_NoneColSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeCols(TrackSize.None).Get.Place();

                box.ColSizesRender()(excelSheet);

                excelSheet.Column(1).Width.Should().Be(excelSheet.DefaultColWidth);
            });

        [Fact]
        public void ColSizesRender_FitColSize_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                excelSheet.Cells[1, 1].Value = "the long long long long string";
                var box = Make.Value().SizeCols(TrackSize.Fit).Get.Place();

                box.ColSizesRender()(excelSheet);

                excelSheet.Column(1).Width.Should().NotBe(excelSheet.DefaultColWidth);
            });

        [Fact]
        public void ColSizesRender_FitOverMerged_NotSet() =>
            Packer.OnSheet(excelSheet =>
            {
                excelSheet.Cells[1, 1].Value = "A";
                excelSheet.Cells[1, 1, 2, 1].Merge = true;
                var box = Make.Value().SizeCols(TrackSize.Fit).Get.Place();

                box.ColSizesRender()(excelSheet);

                excelSheet.Column(1).Width.Should().Be(excelSheet.DefaultColWidth);
            });

        [Fact]
        public void ColSizesRender_TooManyColSizes_Invalid() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SizeCols(TrackSize.Fit, TrackSize.Fit).Get.Place();

                var result = box.ColSizesRender()(excelSheet);

                result.Should().Be(
                    Invalid<Unit>(Errors.Box.ColSizeCountIsInvalid(sizeCount: 2, boxWidth: 1)));
            });
    }
}