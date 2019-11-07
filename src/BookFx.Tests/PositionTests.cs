namespace BookFx.Tests
{
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck.Xunit;
    using static System.Math;

    public class PositionTests
    {
        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void RelatingToAbsoluteFrom_SameBase_SamePosition(int dRow1, int dCol1, int dRow2, int dCol2)
        {
            var relPosition = Position.At(Max(1, dRow1 / 2), Max(1, dCol1 / 2));
            var basePosition = Position.At(Max(1, dRow2 / 2), Max(1, dCol2 / 2));

            var abs = relPosition.AbsoluteFrom(basePosition);
            var result = abs.RelatingTo(basePosition);

            result.Should().Be(relPosition);
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void AbsoluteFromRelatingTo_SameBase_SamePosition(int row1, int col1, int row2, int col2)
        {
            var absPosition = Position.At(Max(row1, row2), Max(col1, col2));
            var basePosition = Position.At(Min(row1, row2), Min(col1, col2));

            var rel = absPosition.RelatingTo(basePosition);
            var result = rel.AbsoluteFrom(basePosition);

            result.Should().Be(absPosition);
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void RelatingTo_AnyPositionAndInitialBase_Same(int row, int col)
        {
            var position = Position.At(row, col);

            var result = position.RelatingTo(Position.Initial);

            result.Should().Be(Position.At(row, col));
        }

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void AbsoluteFrom_InitialPositionAndAnyBase_Base(int baseRow, int baseCol)
        {
            var basePosition = Position.At(baseRow, baseCol);

            var result = Position.Initial.AbsoluteFrom(basePosition);

            result.Should().Be(Position.At(baseRow, baseCol));
        }
    }
}