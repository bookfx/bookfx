namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    internal static class BoxContentRender
    {
        public static Tee<ExcelRangeBase> ContentRender(this BoxCore box) =>
            excelRange =>
            {
                box.Content.ForEach(content =>
                {
                    var contentRange = excelRange.Offset(0, 0, 1, 1);

                    switch (content)
                    {
                        case Unit _:
                            contentRange.Value = null;
                            break;

                        case string formula when formula.StartsWith("="):
                            contentRange.Formula = FormulaConverter.R1C1ToA1(
                                contentRange.Start.Row,
                                contentRange.Start.Column,
                                formula.Substring(1));
                            break;

                        case string escaped when escaped.StartsWith("'"):
                            contentRange.Value = escaped.Substring(1);
                            break;

                        default:
                            contentRange.Value = content;
                            break;
                    }
                });

                return Unit();
            };
    }
}