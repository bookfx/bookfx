namespace BookFx.Tests
{
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;
    using static System.Math;

    public class PositionTests
    {
        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void RelatingToAbsoluteFrom_SameBase_SamePosition(int dRow1, int dCol1, int dRow2, int dCol2)
        {
            var relPosition = Position.At(Max(1, dRow1 / 2), Max(1, dCol1 / 2)).ValueUnsafe();
            var basePosition = Position.At(Max(1, dRow2 / 2), Max(1, dCol2 / 2)).ValueUnsafe();

            var abs = relPosition.AbsoluteFrom(basePosition).ValueUnsafe();
            var result = abs.RelatingTo(basePosition).ValueUnsafe();

            result.Should().Be(relPosition);
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void AbsoluteFromRelatingTo_SameBase_SamePosition(int row1, int col1, int row2, int col2)
        {
            var absPosition = Position.At(Max(row1, row2), Max(col1, col2)).ValueUnsafe();
            var basePosition = Position.At(Min(row1, row2), Min(col1, col2)).ValueUnsafe();

            var rel = absPosition.RelatingTo(basePosition).ValueUnsafe();
            var result = rel.AbsoluteFrom(basePosition);

            result.ValueUnsafe().Should().Be(absPosition);
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void RelatingTo_AnyPositionAndInitialBase_Same(int row, int col)
        {
            var position = Position.At(row, col).ValueUnsafe();

            var result = position.RelatingTo(Position.Initial);

            result.Should().Be(Position.At(row, col));
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void AbsoluteFrom_InitialPositionAndAnyBase_Base(int baseRow, int baseCol)
        {
            var basePosition = Position.At(baseRow, baseCol).ValueUnsafe();

            var result = Position.Initial.AbsoluteFrom(basePosition);

            result.Should().Be(Position.At(baseRow, baseCol));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Constraint.MaxRow + 1)]
        public void At_Invalid_Invalid(int value)
        {
            Position.At(value, 1).IsValid.Should().BeFalse();
            Position.At(1, value).IsValid.Should().BeFalse();
        }
    }
}