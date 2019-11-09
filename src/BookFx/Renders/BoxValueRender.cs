namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    internal static class BoxValueRender
    {
        public static Act<ExcelRangeBase> ValueRender(this BoxCore box) =>
            excelRange =>
            {
                box.Value.ForEach(value =>
                {
                    var isMerge = box.Merge.GetOrElse(true) && (excelRange.Columns > 1 || excelRange.Rows > 1);
                    var valueRange = isMerge ? excelRange.Offset(0, 0, 1, 1) : excelRange;

                    switch (value)
                    {
                        case Unit _:
                            valueRange.Value = null;
                            break;

                        case string formula when formula.StartsWith("="):
                            valueRange.Formula = FormulaConverter.R1C1ToA1(
                                valueRange.Start.Row,
                                valueRange.Start.Column,
                                formula.Substring(1));
                            break;

                        case string escaped when escaped.StartsWith("'"):
                            valueRange.Value = escaped.Substring(1);
                            break;

                        default:
                            valueRange.Value = value;
                            break;
                    }
                });

                return Unit();
            };
    }
}