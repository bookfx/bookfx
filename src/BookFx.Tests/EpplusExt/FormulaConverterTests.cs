namespace BookFx.Tests.EpplusExt
{
    using BookFx.Epplus;
    using FluentAssertions;
    using Xunit;

    public class FormulaConverterTests
    {
        [Theory]
        [InlineData("R1C1", "$A$1")]
        [InlineData("RC", "E4")]
        [InlineData("rc", "E4")]
        [InlineData("R3C5", "$E$3")]
        [InlineData("R3C", "E$3")]
        [InlineData("R[-1]C5", "$E3")]
        [InlineData("R[-1]C", "E3")]
        [InlineData("R4C6", "$F$4")]
        [InlineData("R4C[1]", "F$4")]
        [InlineData("RC6", "$F4")]
        [InlineData("RC[1]", "F4")]
        [InlineData("SUM(R1C4:R3C6)", "SUM($D$1:$F$3)")]
        [InlineData("SUM(R[-3]C[-1]:R[-1]C[1])", "SUM(D1:F3)")]
        [InlineData("SUM(Data C)", "SUM(Data E:E)")]
        [InlineData("SUM(Data R1)", "SUM(Data $1:$1)")]
        [InlineData("1 + RC", "1 + E4")]
        [InlineData("Sheet1!R1C1", "Sheet1!$A$1")]
        [InlineData("Sheet1!R[1]C[1]", "Sheet1!F5")]
        [InlineData("RC:R[1]C:RC", "E4:E5:E4")]
        [InlineData("IF(R1C1>R1C2,3%,0%)", "IF($A$1>$B$1,3%,0%)")]
        public void R1C1ToA1_HasR1C1_Expected(string formula, string expected) =>
            FormulaConverter.R1C1ToA1(4, 5, formula).Should().Be(expected);

        [Theory]
        [InlineData("IF(AND(R11=1,R14=TRUE),G19,0)", "IF(AND($11:$11=1,$14:$14=TRUE),G19,0)")]
        [InlineData("SQRT(_eoq2(C5,C4,C6,C7))", "SQRT(_eoq2($E:$E,$D:$D,$F:$F,$G:$G))")]
        public void R1C1ToA1_HasBothR1C1AndA1_Breaked(string formula, string expected) =>
            FormulaConverter.R1C1ToA1(4, 5, formula).Should().Be(expected);

        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("Value")]
        [InlineData("1+2")]
        [InlineData("A1")]
        [InlineData("SUM(A1)")]
        [InlineData("SUM(A1:D2)")]
        [InlineData("SUM(A1,A2)")]
        [InlineData("SUM(A1:A2:A1)")]
        [InlineData("SUM(A:A)")]
        [InlineData("SUM(A1:D2 A:A)")]
        [InlineData("SUM(Data)")]
        [InlineData("SUM(Data B:B)")]
        [InlineData("SUM(Data $1:$1")]
        [InlineData("Sheet1!A1")]
        [InlineData("SUM(Sheet1!A1)")]
        [InlineData("\"\"")]
        [InlineData("\"as\"&\"df\"")]
        [InlineData("\"\"\"\"")]
        [InlineData("\"RC\"")]
        [InlineData("\" RC \"")]
        [InlineData("\"RC\"\"RC\"")]
        [InlineData("\"R1C1\"")]
        [InlineData("\"R[1]C[1]\"")]
        [InlineData("1 + A1")]
        [InlineData("1 + 2")]
        [InlineData("rca")]
        [InlineData("r1ca")]
        [InlineData("r[1]ca")]
        [InlineData("r[1]c2a")]
        [InlineData("r[1]c[2]a")]
        [InlineData("r1c[2]a")]
        [InlineData("rc[2]a")]
        [InlineData("rc0")]
        [InlineData("rc20000")]
        [InlineData("r0c")]
        [InlineData("r2000000c")]
        [InlineData("E9/E10")]
        [InlineData("(B8/48)*15")]
        [InlineData("+B11+1")]
        [InlineData("COUNTIF(B$4:B$46,\">=90\")")]
        [InlineData("('[2]Detail I&E'!D62)/1000")]
        [InlineData("[1]!today")]
        [InlineData("AVERAGE(#REF!)")]
        [InlineData("MATCH(F3,Prices!2:2,0)")]
        [InlineData("DCOUNT(Lettergrades,,I80:I81)")]
        [InlineData("SUM(namedRangeA1A2)")]
        [InlineData("FVSCHEDULE(1,0.09;0.11;0.1)")]
        public void R1C1ToA1_HasNotR1C1_Same(string formula) =>
            FormulaConverter.R1C1ToA1(3, 5, formula).Should().Be(formula);
    }
}