namespace BookFx.Tests.Renders
{
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class BoxDescendantsRenderTests
    {
        private const string OldValue = "old value";

        [Fact]
        public void DescendantsRender_RowBoxAnd1EmptyCompositeChild_OneSkipped() =>
            Packer.OnSheet(excelSheet =>
            {
                // AC
                var box = Make
                    .Row(
                        Make.Value("A"),
                        RowBox.Empty,
                        Make.Value("C"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 2];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[1, 2].Value.Should().Be("C");
            });

        [Fact]
        public void DescendantsRender_RowBoxAnd1EmptyValueChild_OneNotChanged() =>
            Packer.OnSheet(excelSheet =>
            {
                // A_C
                var box = Make
                    .Row(
                        Make.Value("A"),
                        Make.Value(),
                        Make.Value("C"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 3];
                excelSheet.Cells[1, 2].Value = OldValue;

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[1, 2].Value.Should().Be(OldValue);
                excelSheet.Cells[1, 3].Value.Should().Be("C");
            });

        [Fact]
        public void DescendantsRender_RowBoxAnd1UnitValueChild_OneSetNull() =>
            Packer.OnSheet(excelSheet =>
            {
                // A_C
                var box = Make.Row(
                        Make.Value("A"),
                        Make.Value(Unit()),
                        Make.Value("C"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 3];
                excelSheet.Cells[1, 2].Value = OldValue;

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[1, 2].Value.Should().BeNull();
                excelSheet.Cells[1, 3].Value.Should().Be("C");
            });

        [Fact]
        public void DescendantsRender_RowBoxAnd2Children_BothChanged() =>
            Packer.OnSheet(excelSheet =>
            {
                // AB
                var box = Make.Row(
                        Make.Value("A"),
                        Make.Value("B"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 1, ToCol: 2];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[1, 2].Value.Should().Be("B");
            });

        [Fact]
        public void DescendantsRender_RowBoxAndAutoSpan_Spanned() =>
            Packer.OnSheet(excelSheet =>
            {
                // AB
                // AB
                var box = Make.Row(
                        Make.Value("A"),
                        Make.Value("B").SpanRows(2))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[Row: 1, Col: 1].Value.Should().Be("A");
                excelSheet.Cells[Row: 2, Col: 1].Value.Should().Be("A");
            });

        [Fact]
        public void DescendantsRender_ColBoxAnd1EmptyChild_OneSkipped() =>
            Packer.OnSheet(excelSheet =>
            {
                // A
                // _
                // B
                var box = Make
                    .Col(
                        Make.Value("A"),
                        RowBox.Empty,
                        Make.Value("B"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 1];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[2, 1].Value.Should().Be("B");
            });

        [Fact]
        public void DescendantsRender_ColBoxAnd2Children_BothChanged() =>
            Packer.OnSheet(excelSheet =>
            {
                // A
                // B
                var box = Make
                    .Col(
                        Make.Value("A"),
                        Make.Value("B"))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 1];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[1, 1].Value.Should().Be("A");
                excelSheet.Cells[2, 1].Value.Should().Be("B");
            });

        [Fact]
        public void DescendantsRender_ColBoxAndAutoSpan_Spanned() =>
            Packer.OnSheet(excelSheet =>
            {
                // AA
                // BB
                var box = Make
                    .Col(
                        Make.Value("A"),
                        Make.Value("B").SpanCols(2))
                    .Get
                    .Place();
                var excelRange = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: 2, ToCol: 2];

                box.DescendantsRender()(excelRange);

                excelSheet.Cells[Row: 1, Col: 1].Value.Should().Be("A");
                excelSheet.Cells[Row: 1, Col: 2].Value.Should().Be("A");
            });
    }
}