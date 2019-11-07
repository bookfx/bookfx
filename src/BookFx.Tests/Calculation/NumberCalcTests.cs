namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using FluentAssertions;
    using Xunit;

    public class NumberCalcTests
    {
        [Fact]
        public void Number_Value_OneNumbered()
        {
            var (box, count) = Make.Value().Get.Number();

            box.Number.Should().Be(0);
            count.Should().Be(1);
        }

        [Fact]
        public void Number_3Gens_AllNumbered()
        {
            var box = Make.Row(Make.Row(Make.Value()));

            var (genX, count) = box.Get.Number();

            var genY = genX.Children[0];
            var genZ = genY.Children[0];

            genX.Number.Should().Be(0);
            genY.Number.Should().Be(1);
            genZ.Number.Should().Be(2);
            count.Should().Be(3);
        }

        [Fact]
        public void Number_3Children_AllNumbered()
        {
            var box = Make.Row(Make.Value(), Make.Value(), Make.Value());

            var (parent, count) = box.Get.Number();

            parent.Number.Should().Be(0);
            parent.Children[0].Number.Should().Be(1);
            parent.Children[1].Number.Should().Be(2);
            parent.Children[2].Number.Should().Be(3);
            count.Should().Be(4);
        }
    }
}