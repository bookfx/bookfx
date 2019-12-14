namespace BookFx.Renders
{
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxRowSizesRender
    {
        public static Tee<ExcelWorksheet> RowSizesRender(this BoxCore box) =>
            excelSheet =>
            {
                if (box.RowSizes.IsEmpty)
                {
                    return Unit();
                }

                if (box.RowSizes.Count > box.Placement.Dimension.Height)
                {
                    return Errors.Box.RowSizeCountIsInvalid(
                        sizeCount: box.RowSizes.Count,
                        boxHeight: box.Placement.Dimension.Height);
                }

                Enumerable
                    .Range(0, box.Placement.Dimension.Height)
                    .ForEach(offset => box
                        .RowSizes[offset % box.RowSizes.Count]
                        .ForEach(
                            fit: () => excelSheet.Row(box.Placement.Position.Row + offset).CustomHeight = false,
                            some: value => excelSheet.Row(box.Placement.Position.Row + offset).Height = value));

                return Unit();
            };
    }
}