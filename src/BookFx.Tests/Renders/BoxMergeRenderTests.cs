namespace BookFx.Tests.Renders
{
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;

    public class BoxMergeRenderTests
    {
        [Fact]
        public void MergeRender_Empty_NotMerged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().Get;
                var excelRange = excelSheet.Cells[1, 1];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeFalse();
            });

        [Fact]
        public void MergeRender_ColSpanAndNoValue_NotMerged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SpanCols(2).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 2];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeFalse();
            });

        [Fact]
        public void MergeRender_ValueAndColSpan_Merged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value("A").SpanCols(2).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 2];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeTrue();
            });

        [Fact]
        public void MergeRender_RowSpanAndNoValue_NotMerged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().SpanRows(2).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 1];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeFalse();
            });

        [Fact]
        public void MergeRender_ValueAndRowSpan_Merged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value("A").SpanRows(2).Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 1];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeTrue();
            });

        [Fact]
        public void MergeRender_WiderSiblingAndValue_Merged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value("A").Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 2];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeTrue();
            });

        [Fact]
        public void MergeRender_HigherSiblingAndValue_Merged() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value("A").Get;
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 1];

                box.MergeRender()(excelRange);

                excelRange.Merge.Should().BeTrue();
            });
    }
}