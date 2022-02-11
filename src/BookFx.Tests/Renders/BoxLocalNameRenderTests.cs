namespace BookFx.Tests.Renders
{
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class BoxLocalNameRenderTests
    {
        private const string TheValidName = "TheName";

        [Fact]
        public void LocalNameRender_NoName_NotSet() =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[1, 1];

                Make.Value().Get.LocalNameRender()(excelRange);

                excelRange.Worksheet.Names.Should().BeEmpty();
            });

        [Fact]
        public void LocalNameRender_NonEmptyName_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().NameLocally(TheValidName).Get;
                var excelRange = excelSheet.Cells[1, 1];

                box.LocalNameRender()(excelRange);

                excelRange.Worksheet.Names
                    .Should()
                    .HaveCount(1)
                    .And.OnlyContain(x => x.Name == TheValidName && x.Address == excelRange.Address);
            });

        [Fact]
        public void LocalNameRender_NameIsAlreadyExists_Error() =>
            Packer.OnSheet(excelSheet =>
            {
                excelSheet.Names.Add(TheValidName, excelSheet.Cells[2, 2]);
                var box = Make.Value().NameLocally(TheValidName).Get;
                var excelRange = excelSheet.Cells[1, 1];

                var result = box.LocalNameRender()(excelRange);

                result.IsValid.Should().BeFalse();
            });
    }
}