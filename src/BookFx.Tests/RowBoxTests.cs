namespace BookFx.Tests
{
    using FluentAssertions;
    using Xunit;

    public class RowBoxTests
    {
        [Fact]
        public void CreateEnumerable_EmptyAmongNonEmpty_3Children() =>
            Make.Row(new[] { "A", Make.Value(), "C" }).Get.Children.Should().HaveCount(3);

        [Fact]
        public void CreateParams_EmptyAmongNonEmpty_3Children() =>
            Make.Row("A", Make.Value(), "C").Get.Children.Should().HaveCount(3);
    }
}