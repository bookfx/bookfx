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
        [InlineData(0f, 0f)]
        [InlineData(0.01f, 0f)]
        [InlineData(0.04f, 0f)]
        [InlineData(0.05f, 0.140625f)]
        [InlineData(0.09f, 0.140625f)]
        [InlineData(0.12f, 0.140625f)]
        [InlineData(0.13f, 0.28515625f)]
        [InlineData(0.2f, 0.28515625f)]
        [InlineData(0.21f, 0.42578125f)]
        [InlineData(0.29f, 0.42578125f)]
        [InlineData(1f, 1.7109375f)]
        [InlineData(1.01f, 1.7109375f)]
        [InlineData(1.09f, 1.85546875f)]
        [InlineData(1.1f, 1.85546875f)]
        [InlineData(1.92f, 2.5703125f)]
        [InlineData(1.93f, 2.7109375f)]
        [InlineData(2f, 2.7109375f)]
        [InlineData(3f, 3.7109375f)]
        [InlineData(9f, 9.7109375f)]
        [InlineData(100.01f, 100.7109375f)]
        [InlineData(100.09f, 100.85546875f)]
        [InlineData(100.1f, 100.85546875f)]
        [InlineData(107f, 107.7109375f)]
        [InlineData(107.3f, 108f)]
        [InlineData(107.5f, 108.28515625f)]
        [InlineData(107.7f, 108.42578125f)]
        public void ColSizesRender_SomeColSize_Set(float size, double expected) =>
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