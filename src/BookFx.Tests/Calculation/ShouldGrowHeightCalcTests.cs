namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using FluentAssertions;
    using Xunit;

    public class ShouldGrowHeightCalcTests
    {
        [Fact]
        public void ShouldGrowHeight_AutoSpanAndHigherSibling_True()
        {
            var (root, boxCount) = Make
                .Row(
                    Make.Value(),
                    Make.Value().SpanRows(10))
                .AutoSpanRows()
                .Get
                .Number();
            var layout = Layout.Create(root, boxCount);

            var result = root.Children[0].ShouldGrowHeight(layout);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldGrowHeight_AutoSpanAndHigherUncle_True()
        {
            var (root, boxCount) = Make
                .Row(
                    Make.Col(Make.Value()),
                    Make.Value().SpanRows(10))
                .AutoSpanRows()
                .Get
                .Number();
            var layout = Layout.Create(root, boxCount);

            var result = root.Children[0].Children[0].ShouldGrowHeight(layout);

            result.Should().BeTrue();
        }
    }
}