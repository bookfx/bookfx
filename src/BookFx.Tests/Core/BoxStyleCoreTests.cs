namespace BookFx.Tests.Core
{
    using BookFx.Cores;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class BoxStyleCoreTests
    {
        [Fact]
        public void Mix_EmptyAndEmpty_EqEmpty() =>
            BoxStyleCore.Mix(BoxStyleCore.Empty, BoxStyleCore.Empty).Should().BeEquivalentTo(BoxStyleCore.Empty);

        [Fact]
        public void Mix_BothWithBorder_BordersAreConcats()
        {
            var border1 = Make.Border();
            var border2 = Make.Border();

            var result = BoxStyleCore.Mix(
                Make.Style().Borders(border1).Get,
                Make.Style().Borders(border2).Get);

            result.Borders.Should().Equal(border1.Get, border2.Get);
        }

        [Fact]
        public void Mix_BothWithFontSize_SecondWins()
        {
            var result = BoxStyleCore.Mix(Make.Style().Font(1).Get, Make.Style().Font(2).Get);

            result.FontSize.ValueUnsafe().Should().Be(2);
        }

        [Fact]
        public void Mix_ThreeWithBorder_BordersAreConcats()
        {
            var border1 = Make.Border();
            var border2 = Make.Border();
            var border3 = Make.Border();

            var result = BoxStyleCore.Mix(List(
                Make.Style().Borders(border1).Get,
                Make.Style().Borders(border2).Get,
                Make.Style().Borders(border3).Get));

            result.Borders.Should().Equal(border1.Get, border2.Get, border3.Get);
        }
    }
}