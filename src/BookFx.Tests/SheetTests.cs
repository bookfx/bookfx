namespace BookFx.Tests
{
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;

    public class SheetTests
    {
        [Fact]
        public void Empty_Always_Empty()
        {
            Sheet.Empty.Should().BeSameAs(Sheet.Empty);
        }

        [Fact]
        public void Empty_Always_NameIsNone()
        {
            Sheet.Empty.Get.Name.IsNone.Should().BeTrue();
        }

        [Fact]
        public void Create_Always_Empty()
        {
            Make.Sheet().Should().BeSameAs(Sheet.Empty);
        }

        [Fact]
        public void Name_NotNull_Name()
        {
            const string name = "Sheet Name";
            var sut = Make.Sheet();

            var result = sut.Name(name);

            result.Get.Name.ValueUnsafe().Should().Be(name);
        }
    }
}