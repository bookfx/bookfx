namespace BookFx.Tests.EpplusExt
{
    using System.Linq;
    using BookFx.Epplus;
    using FluentAssertions;
    using Xunit;

    public class ExcelRangeBaseExtTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 5)]
        public void GetCells_RangeWithSize_CountIsProduct(int rows, int columns) =>
            Packer.OnSheet(excelSheet =>
            {
                var range = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: rows, ToCol: columns];

                var result = range.GetCells();

                result.Should().HaveCount(rows * columns);
            });

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 5)]
        public void GetCells_RangeWithSize_FirstIsStart(int rows, int columns) =>
            Packer.OnSheet(excelSheet =>
            {
                var range = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: rows, ToCol: columns];

                var result = range.GetCells();

                result.First().Address.Should().Be(range.Start.Address);
            });

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 5)]
        public void GetCells_RangeWithSize_LastIsEnd(int rows, int columns) =>
            Packer.OnSheet(excelSheet =>
            {
                var range = excelSheet.Cells[FromRow: 1, FromCol: 1, ToRow: rows, ToCol: columns];

                var result = range.GetCells();

                result.Last().Address.Should().Be(range.End.Address);
            });

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void CopyColSizes_Cell_Copied(int size) =>
            Packer.OnSheet(excelSheet =>
            {
                var source = excelSheet.Cells[1, 1];
                var target = excelSheet.Cells[2, 2];
                excelSheet.Column(1).Width = size;

                source.CopyColSizes(target);

                excelSheet.Column(2).Width.Should().Be(excelSheet.Column(1).Width);
            });

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void CopyRowSizes_Cell_Copied(int size) =>
            Packer.OnSheet(excelSheet =>
            {
                var source = excelSheet.Cells[1, 1];
                var target = excelSheet.Cells[2, 2];
                excelSheet.Row(1).Height = size;

                source.CopyRowSizes(target);

                excelSheet.Row(2).Height.Should().Be(excelSheet.Row(1).Height);
            });
    }
}