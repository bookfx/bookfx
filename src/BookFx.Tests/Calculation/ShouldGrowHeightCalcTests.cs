namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using FluentAssertions;
    using Xunit;

    public class ShouldGrowHeightCalcTests
    {
        [Fact]
        public void ShouldGrowHeight_HigherSibling_True()
        {
            var (root, boxCount) = Make
                .Row(
                    Make.Value(),
                    Make.Value().SpanRows(10))
                .Get
                .Number();
            var layout = Layout.Create(root, boxCount);

            var result = root.Children[0].ShouldGrowHeight(layout);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldGrowHeight_HigherUncle_True()
        {
            var (root, boxCount) = Make
                .Row(
                    Make.Col(Make.Value()),
                    Make.Value().SpanRows(10))
                .Get
                .Number();
            var layout = Layout.Create(root, boxCount);

            var result = root.Children[0].Children[0].ShouldGrowHeight(layout);

            result.Should().BeTrue();
        }
    }
}