namespace BookFx.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxFreezeRender
    {
        public static Tee<ExcelWorksheet> FreezeRender(this BoxCore box) =>
            excelSheet =>
            {
                var frozenRow = box
                    .SelfAndDescendants()
                    .Where(x => x.AreRowsFrozen)
                    .Map(x => x.Placement.ToRow)
                    .Head()
                    .GetOrElse(0);

                var frozenCol = box
                    .SelfAndDescendants()
                    .Where(x => x.AreColsFrozen)
                    .Map(x => x.Placement.ToCol)
                    .Head()
                    .GetOrElse(0);

                if (frozenRow > 0 || frozenCol > 0)
                {
                    excelSheet.View.FreezePanes(frozenRow + 1, frozenCol + 1);
                }

                return Unit();
            };
    }
}