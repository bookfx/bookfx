namespace BookFx.Cores
{
    using BookFx.Functional;
    using JetBrains.Annotations;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class SheetCore
    {
        internal static readonly SheetCore Empty = new SheetCore(
            name: None,
            box: None,
            pageView: None,
            fitToHeight: None,
            fitToWidth: None,
            scale: None,
            protoBook: None,
            protoName: None,
            protoSheet: None);

        private SheetCore(
            Option<string> name,
            Option<BoxCore> box,
            Option<PageView> pageView,
            Option<int> fitToHeight,
            Option<int> fitToWidth,
            Option<int> scale,
            Option<byte[]> protoBook,
            Option<string> protoName,
            Option<ExcelWorksheet> protoSheet)
        {
            Name = name;
            Box = box;
            PageView = pageView;
            FitToHeight = fitToHeight;
            FitToWidth = fitToWidth;
            Scale = scale;
            ProtoBook = protoBook;
            ProtoName = protoName;
            ProtoSheet = protoSheet;
        }

        public Option<string> Name { get; }

        public Option<BoxCore> Box { get; }

        public Option<PageView> PageView { get; }

        public Option<int> FitToHeight { get; }

        public Option<int> FitToWidth { get; }

        public Option<int> Scale { get; }

        public Option<byte[]> ProtoBook { get; }

        public Option<string> ProtoName { get; }

        internal Option<ExcelWorksheet> ProtoSheet { get; }

        [Pure]
        internal static SheetCore Create() => Empty;

        [Pure]
        internal SheetCore WithBox(Option<BoxCore> box) => With(box: box);

        [Pure]
        internal SheetCore With(
            Option<string>? name = null,
            Option<BoxCore>? box = null,
            Option<PageView>? pageView = null,
            Option<int>? fitToHeight = null,
            Option<int>? fitToWidth = null,
            Option<int>? scale = null,
            Option<byte[]>? protoBook = null,
            Option<string>? protoName = null,
            Option<ExcelWorksheet>? protoSheet = null) =>
            new SheetCore(
                name ?? Name,
                box ?? Box,
                pageView ?? PageView,
                fitToHeight ?? FitToHeight,
                fitToWidth ?? FitToWidth,
                scale ?? Scale,
                protoBook ?? ProtoBook,
                protoName ?? ProtoName,
                protoSheet ?? ProtoSheet);
    }
}