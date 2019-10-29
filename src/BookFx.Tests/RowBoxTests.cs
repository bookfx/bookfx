namespace BookFx.Tests
{
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;

    public class RowBoxTests
    {
        [Fact]
        public void CreateEnumerable_NullAmongNonNull_3Children() =>
            Make.Row(new[] { "A", null, "C" }.Map(Make.Value)).Get.Children.Should().HaveCount(3);

        [Fact]
        public void CreateParams_NullAmongNonNull_3Children() =>
            Make.Row("A", Make.Value(null), "C").Get.Children.Should().HaveCount(3);
    }
}