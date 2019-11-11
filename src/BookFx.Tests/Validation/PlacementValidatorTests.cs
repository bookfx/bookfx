namespace BookFx.Tests.Validation
{
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Validation;
    using FluentAssertions;
    using Xunit;

    public class PlacementValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Constraint.MaxRow + 1)]
        public void Position_InvalidRow_Invalid(int value) =>
            PlacementValidator.Position(GetBoxAt(value, 1)).IsValid.Should().BeFalse();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Constraint.MaxRow + 1)]
        public void Position_InvalidCol_Invalid(int value) =>
            PlacementValidator.Position(GetBoxAt(1, value)).IsValid.Should().BeFalse();

        [Fact]
        public void RootBoxWidth_SmallBox_Valid()
        {
            var box = Make.Value().SpanCols(5).Get.Place();

            var result = PlacementValidator.RootBoxWidth(box);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void RootBoxWidth_BigBox_Invalid()
        {
            var box = Make
                .Row(
                    Make.Value().SpanCols(16_000),
                    Make.Value().SpanCols(16_000)
                )
                .Get
                .Place();

            var result = PlacementValidator.RootBoxWidth(box);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().First().Should().BeOfType<Errors.Placement.RootBoxWidthTooBigError>();
        }

        [Fact]
        public void RootBoxHeight_SmallBox_Valid()
        {
            var box = Make.Value().SpanRows(5).Get.Place();

            var result = PlacementValidator.RootBoxHeight(box);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void RootBoxHeight_BigBox_Invalid()
        {
            var box = Make
                .Col(
                    Make.Value().SpanRows(1_000_000),
                    Make.Value().SpanRows(1_000_000)
                )

                .Get
                .Place();

            var result = PlacementValidator.RootBoxHeight(box);

            result.IsValid.Should().BeFalse();
            result.ErrorsUnsafe().First().Should().BeOfType<Errors.Placement.RootBoxHeightTooBigError>();
        }

        private static BoxCore GetBoxAt(int row, int col)
        {
            var position = Position.At(row, col);
            var dimension = Dimension.Of(1, 1);
            return BoxCore.Create(BoxType.Value).With(placement: Placement.At(position, dimension));
        }
    }
}