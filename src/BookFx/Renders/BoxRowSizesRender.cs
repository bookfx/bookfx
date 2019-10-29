namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxRowSizesRender
    {
        public static Act<ExcelWorksheet> RowSizesRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.RowSizes.Count > box.Placement.Dimension.Height)
                {
                    return Errors.Box.RowSizeCountIsInvalid(
                        sizeCount: box.RowSizes.Count,
                        boxHeight: box.Placement.Dimension.Height);
                }

                box.RowSizes.ForEach((size, i) => size.ForEach(
                    fit: () => excelSheet.Row(box.Placement.Position.Row + i).CustomHeight = false,
                    some: value => excelSheet.Row(box.Placement.Position.Row + i).Height = value));

                return Unit();
            };
    }
}