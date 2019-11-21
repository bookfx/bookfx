namespace BookFx.Epplus
{
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    internal static class FormulaConverter
    {
        private static readonly Regex Parser = new Regex(
            @"
 (?<String>""[^""]*"")
|(?<Sheet>'[^']*')
|(?<R1C1>
    (?=R|C)
    (?<Row>
         (?:R\[(?<RowRel>-?\d+)\])
        |(?:R(?<RowAbs>\d+))
        |(?:R(?<RowEmpty>))
    )?
    (?<Col>
         (?:C\[(?<ColRel>-?\d+)\])
        |(?:C(?<ColAbs>\d+))
        |(?:C(?<ColEmpty>))
    )?
	(?![\w[_])
)
|(?<Identifier>[a-z_][\w]*)
|(?<Other>[\s\S])
",
            RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        [Pure]
        public static string R1C1ToA1(int row, int col, string formula) =>
            Parser
                .Matches(formula)
                .Cast<Match>()
                .Map(match => GetPart(row, col, match))
                .Join();

        private static string GetPart(int row, int col, Match match) =>
            match.Groups["R1C1"].Success
                ? GetA1(row, col, match)
                : match.Value;

        private static string GetA1(int baseRow, int baseCol, Match match)
        {
            var row = GetRow(baseRow, match);
            var col = GetCol(baseCol, match);

            if (match.Groups["Row"].Success && row.IsNone)
            {
                return match.Value;
            }

            if (match.Groups["Col"].Success && col.IsNone)
            {
                return match.Value;
            }

            return row.Match(
                none: () => col.Match(
                    none: () => match.Value,
                    some: c => $"{c}:{c}"),
                some: r => col.Match(
                    none: () => $"{r}:{r}",
                    some: c => c + r));
        }

        private static Option<string> GetRow(int baseRow, Match match) =>
            GetRowRel(baseRow, match)
                .OrElse(GetRowAbs(match))
                .OrElse(GetRowEmpty(baseRow, match));

        private static Option<string> GetRowAbs(Match match) =>
            GetNumber(match.Groups["RowAbs"])
                .Where(row => row >= 1 && row <= Constraint.MaxRow)
                .Map(row => $"${row}");

        private static Option<string> GetRowRel(int baseRow, Match match) =>
            GetNumber(match.Groups["RowRel"])
                .Map(rel => (baseRow + rel).ToString(CultureInfo.InvariantCulture));

        private static Option<string> GetRowEmpty(int baseRow, Match match) =>
            Some(baseRow.ToString(CultureInfo.InvariantCulture))
                .Where(_ => match.Groups["RowEmpty"].Success);

        private static Option<string> GetCol(int baseCol, Match match) =>
            GetColRel(baseCol, match)
                .OrElse(GetColAbs(match))
                .OrElse(GetColEmpty(baseCol, match));

        private static Option<string> GetColAbs(Match match) =>
            GetNumber(match.Groups["ColAbs"])
                .Bind(ColumnConverter.NumberToName)
                .Map(colName => $"${colName}");

        private static Option<string> GetColRel(int baseCol, Match match) =>
            GetNumber(match.Groups["ColRel"])
                .Map(rel => baseCol + rel)
                .Bind(ColumnConverter.NumberToName);

        private static Option<string> GetColEmpty(int baseCol, Match match) =>
            ColumnConverter.NumberToName(baseCol)
                .Where(_ => match.Groups["ColEmpty"].Success);

        private static Option<int> GetNumber(Group matchGroup) =>
            matchGroup.Success
                ? Some(int.Parse(matchGroup.Value))
                : None;
    }
}