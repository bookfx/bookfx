namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using FluentAssertions;
    using Xunit;

    public class MinHeightCalcTests
    {
        [Fact]
        public void MinHeight_ColWith2Children_Expected()
        {
            // A
            // B
            var (box, boxCount) = Make
                .Col(
                    Make.Value("A").SpanRows(3),
                    Make.Value("B").SpanRows(5)
                )
                .Get
                .Number();
            var cache = Cache.Create(boxCount);

            var result = MinHeightCalc.MinHeight(box, cache);

            result.Should().Be(8);
        }
    }
}