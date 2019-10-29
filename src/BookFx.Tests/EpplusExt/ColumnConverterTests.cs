namespace BookFx.Tests.EpplusExt
{
    using BookFx.Epplus;
    using BookFx.Functional;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    public class ColumnConverterTests
    {
        [Theory]
        [InlineData(1, "A")]
        [InlineData(2, "B")]
        [InlineData(26, "Z")]
        [InlineData(27, "AA")]
        [InlineData(703, "AAA")]
        [InlineData(Constraint.MaxColumn, "XFD")]
        public void NumberToName_ValidNumber_Expected(int number, string expected) =>
            ColumnConverter.NumberToName(number).ValueUnsafe().Should().Be(expected);

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(Constraint.MaxColumn + 1)]
        [InlineData(int.MaxValue)]
        public void NumberToName_InvalidNumber_None(int number) =>
            ColumnConverter.NumberToName(number).IsNone.Should().BeTrue();

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        [InlineData("Z", 26)]
        [InlineData("AA", 27)]
        [InlineData("AAA", 703)]
        [InlineData("XFD", Constraint.MaxColumn)]
        [InlineData("z", 26)]
        [InlineData("aaa", 703)]
        [InlineData("xfd", Constraint.MaxColumn)]
        public void NameToNumber_ValidName_Expected(string name, int expected) =>
            ColumnConverter.NameToNumber(name).ValueUnsafe().Should().Be(expected);

        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("-")]
        [InlineData("AAAA")]
        [InlineData("XFE")]
        public void NameToNumber_InvalidName_None(string name) =>
            ColumnConverter.NameToNumber(name).IsNone.Should().BeTrue();

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) })]
        public void Both_ValidNumberToNameToNumber_Same(int number)
        {
            var name = ColumnConverter.NumberToName(number).ValueUnsafe();
            var result = ColumnConverter.NameToNumber(name).ValueUnsafe();
            result.Should().Be(number);
        }
    }
}