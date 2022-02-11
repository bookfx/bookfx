namespace BookFx.Tests.Renders
{
    using BookFx.Epplus;
    using BookFx.Renders;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    [Properties(MaxTest = 10)]
    public class BoxGlobalNameRenderTests
    {
        private const string TheValidName = "TheName";

        [Fact]
        public void GlobalNameRender_NoName_NotSet() =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[1, 1];

                Make.Value().Get.GlobalNameRender()(excelRange);

                excelRange.Worksheet.Workbook.Names.Should().BeEmpty();
            });

        [Fact]
        public void GlobalNameRender_NonEmptyName_Set() =>
            Packer.OnSheet(excelSheet =>
            {
                var box = Make.Value().NameGlobally(TheValidName).Get;
                var excelRange = excelSheet.Cells[1, 1];

                box.GlobalNameRender()(excelRange);

                excelRange.Worksheet.Workbook.Names
                    .Should()
                    .HaveCount(1)
                    .And.OnlyContain(x => x.Name == TheValidName && x.Address == excelRange.Address);
            });

        [Fact]
        public void GlobalNameRender_NameIsAlreadyExists_Error() =>
            Packer.OnSheet(excelSheet =>
            {
                excelSheet.Workbook.Names.Add(TheValidName, excelSheet.Cells[2, 2]);
                var box = Make.Value().NameGlobally(TheValidName).Get;
                var excelRange = excelSheet.Cells[1, 1];

                var result = box.GlobalNameRender()(excelRange);

                result.IsValid.Should().BeFalse();
            });
    }
}