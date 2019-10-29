namespace BookFx.Tests
{
    using BookFx.Cores;
    using FluentAssertions;
    using Xunit;

    public class BoxCoreTests
    {
        [Fact]
        public void With_EmptyWithNoParams_Empty() => BoxCore.Empty.With().Should().BeEquivalentTo(BoxCore.Empty);
    }
}