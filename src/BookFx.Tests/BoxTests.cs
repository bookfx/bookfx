namespace BookFx.Tests
{
    using BookFx.Cores;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    public class BoxTests
    {
        [Fact]
        public void Implicit_Row_RowBox()
        {
            var core = BoxCore.Create(BoxType.Row);

            Box box = core;

            box.Should().BeOfType<RowBox>();
        }

        [Property]
        public void Name_NonNull_SetName(NonNull<string> name)
        {
            Box box = Make.Row();

            var result = box.Name(name.Get);

            result.Get.Name.Should().Be(Some(name.Get));
        }
    }
}