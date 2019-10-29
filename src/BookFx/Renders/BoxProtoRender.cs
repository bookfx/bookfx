namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    internal static class BoxProtoRender
    {
        public static Act<ExcelRangeBase> ProtoRender(this BoxCore box) =>
            excelRange =>
            {
                box.Proto.ForEach(proto =>
                {
                    var protoRange = proto.Range.ValueUnsafe();

                    protoRange.Copy(excelRange);
                    protoRange.CopyColSizes(excelRange);
                    protoRange.CopyRowSizes(excelRange);
                });

                return Unit();
            };
    }
}