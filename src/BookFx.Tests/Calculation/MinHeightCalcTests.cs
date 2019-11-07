namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using BookFx.Functional;
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

            var result = MinHeightCalc.MinHeight(box).Run(Cache.Create(boxCount));

            result.Should().Be(8);
        }
    }
}