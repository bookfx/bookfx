namespace BookFx.Tests.Renders
{
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;

    public class SheetBoxRenderTests
    {
        [Fact]
        public void RootRender_EmptyInRootRange_EmptySheet() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Row().Get.LayOut();

                box.RootRender()(excelSheet);

                excelSheet.Cells[1, 1].Value.Should().BeNull();
            });

        [Fact]
        public void RootRender_ValueInRootRange_SheetWithValue() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value("A").Get.LayOut();

                box.RootRender()(excelSheet);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
            });

        [Fact]
        public void RootRender_ColSpan2_ExpectedCellValues() =>
            Packer.OnSheet(excelSheet =>
            {
                // AA-
                // ---
                var box = Make.Value("A").SpanCols(2).Get.LayOut();

                box.RootRender()(excelSheet);

                excelSheet.Cells[Row: 1, Col: 1].Value.Should().Be("A");
                excelSheet.Cells[Row: 1, Col: 2].Value.Should().Be("A");
                excelSheet.Cells[Row: 1, Col: 3].Value.Should().Be(null);
                excelSheet.Cells[Row: 2, Col: 1].Value.Should().Be(null);
                excelSheet.Cells[Row: 2, Col: 3].Value.Should().Be(null);
            });

        [Fact]
        public void RootRender_RowSpan2_ExpectedCellValues() =>
            Packer.OnSheet(excelSheet =>
            {
                // A-
                // A-
                // --
                var box = Make.Value("A").SpanRows(2).Get.LayOut();

                box.RootRender()(excelSheet);

                excelSheet.Cells[Row: 1, Col: 1].Value.Should().Be("A");
                excelSheet.Cells[Row: 2, Col: 1].Value.Should().Be("A");
                excelSheet.Cells[Row: 3, Col: 1].Value.Should().Be(null);
                excelSheet.Cells[Row: 1, Col: 2].Value.Should().Be(null);
                excelSheet.Cells[Row: 3, Col: 2].Value.Should().Be(null);
            });
    }
}