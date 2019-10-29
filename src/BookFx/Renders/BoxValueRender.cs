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
                    switch (value)
                    {
                        case Unit _:
                            excelRange.Value = null;
                            break;

                        case string formula when formula.StartsWith("="):
                            excelRange.Formula = FormulaConverter.R1C1ToA1(
                                excelRange.Start.Row,
                                excelRange.Start.Column,
                                formula.Substring(1));
                            break;

                        case string escaped when escaped.StartsWith("'"):
                            excelRange.Value = escaped.Substring(1);
                            break;

                        default:
                            excelRange.Value = value;
                            break;
                    }
                });

                return Unit();
            };
    }
}