namespace BookFx.Tests.Renders
{
    using System;
    using BookFx.Epplus;
    using BookFx.Renders;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using OfficeOpenXml;
    using Xunit;
    using static BookFx.Functional.F;

    [Properties(MaxTest = 10)]
    public class BoxValueRenderTests
    {
        private const string OldValue = "old value";
        private const string OldFormula = "\"old formula\"";

        [Fact]
        public void ValueRender_NoValue_ValueNotSet() =>
            CheckValue(
                Make.Value(),
                range => range.Value.Should().Be(OldValue));

        [Fact]
        public void ValueRender_NoValue_FormulaNotSet() =>
            CheckFormula(
                Make.Value(),
                range => range.Formula.Should().Be(OldFormula));

        [Fact]
        public void ValueRender_NullValue_Null() =>
            CheckValue(
                Make.Value(null),
                range => range.Value.Should().BeNull());

        [Fact]
        public void ValueRender_NoneValue_Null() =>
            CheckValue(
                Make.Value(None),
                range => range.Value.Should().BeNull());

        [Property]
        public void ValueRender_EscapedValue_Set(NonNull<string> value) =>
            CheckValue(
                Make.Value($"'{value.Get}"),
                range => range.Value.Should().Be(value.Get));

        [Property(Arbitrary = new[] { typeof(GeneralValueArb) })]
        public void ValueRender_Object_Set(object value) =>
            CheckValue(
                Make.Value(value),
                range => range.Value.Should().Be(value));

        [Property]
        public void ValueRender_Int_Set(int value) =>
            CheckValue(
                Make.Value(value),
                range => range.Value.Should().Be(value));

        [Theory]
        [InlineData("=1+2", "1+2")]
        [InlineData("=1 + 2", "1 + 2")]
        [InlineData("=RC", "E4")]
        [InlineData("=R2C3", "$C$2")]
        [InlineData("=R[2]C[3]", "H6")]
        [InlineData("=SUM(R[1]C,R[10]C)", "SUM(E5,E14)")]
        [InlineData("=SUM(R[1]C:R[10]C)", "SUM(E5:E14)")]
        [InlineData("=SUM(Data C)", "SUM(Data E:E)")]
        [InlineData("=SUM(Data1 Data2 Data3)", "SUM(Data1 Data2 Data3)")]
        [InlineData("=SUM(R Data1 Data2 Data3)", "SUM(4:4 Data1 Data2 Data3)")]
        [InlineData("=SUM(R1 Data1 Data2 Data3)", "SUM($1:$1 Data1 Data2 Data3)")]
        public void ValueRender_ValidFormulas_SetExpected(string formula, string expected) =>
            CheckFormula(
                Make.Value(formula),
                range => range.Formula.Should().Be(expected));

        private static void CheckValue(ValueBox box, Action<ExcelRange> assertion) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[1, 1];
                excelRange.Value = OldValue;

                box.Get.ValueRender()(excelRange);

                assertion(excelRange);
            });

        private static void CheckFormula(ValueBox box, Action<ExcelRange> assertion) =>
            Packer.OnSheet(excelSheet =>
            {
                var excelRange = excelSheet.Cells[Row: 4, Col: 5];
                excelRange.Formula = OldFormula;

                box.Get.ValueRender()(excelRange);

                assertion(excelRange);
            });
    }
}