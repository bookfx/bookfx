namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using FluentAssertions;
    using Xunit;

    public class LayoutValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Constraint.MaxRow + 1)]
        public void Position_InvalidRow_Invalid(int value) =>
            LayoutValidator.Position(GetBoxAt(value, 1)).IsValid.Should().BeFalse();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Constraint.MaxRow + 1)]
        public void Position_InvalidCol_Invalid(int value) =>
            LayoutValidator.Position(GetBoxAt(1, value)).IsValid.Should().BeFalse();

        private static BoxCore GetBoxAt(int row, int col)
        {
            var position = Position.At(row, col);
            var dimension = Dimension.Of(1, 1);
            return BoxCore.Create(BoxType.Value).With(placement: Placement.At(position, dimension));
        }
    }
}