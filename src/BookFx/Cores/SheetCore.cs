namespace BookFx.Cores
{
    using System.Drawing;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using OfficeOpenXml;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a sheet properties.
    /// </summary>
    [PublicAPI]
    public sealed class SheetCore
    {
        internal static readonly SheetCore Empty = new SheetCore(
            name: None,
            tabColor: None,
            box: None,
            pageView: None,
            orientation: None,
            margins: None,
            fitToHeight: None,
            fitToWidth: None,
            scale: None,
            protoBook: None,
            protoName: None,
            protoSheet: None);

        private SheetCore(
            Option<string> name,
            Option<Color> tabColor,
            Option<BoxCore> box,
            Option<PageView> pageView,
            Option<PageOrientation> orientation,
            Option<PageMarginsCore> margins,
            Option<int> fitToHeight,
            Option<int> fitToWidth,
            Option<int> scale,
            Option<byte[]> protoBook,
            Option<string> protoName,
            Option<ExcelWorksheet> protoSheet)
        {
            Name = name;
            TabColor = tabColor;
            Box = box;
            PageView = pageView;
            Orientation = orientation;
            Margins = margins;
            FitToHeight = fitToHeight;
            FitToWidth = fitToWidth;
            Scale = scale;
            ProtoBook = protoBook;
            ProtoName = protoName;
            ProtoSheet = protoSheet;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public Option<string> Name { get; }

        /// <summary>
        /// Gets the tab color.
        /// </summary>
        public Option<Color> TabColor { get; }

        /// <summary>
        /// Gets the root box.
        /// </summary>
        public Option<BoxCore> Box { get; }

        /// <summary>
        /// Gets the page view.
        /// </summary>
        public Option<PageView> PageView { get; }

        /// <summary>
        /// Gets the page orientation.
        /// </summary>
        public Option<PageOrientation> Orientation { get; }

        /// <summary>
        /// Gets page margins.
        /// </summary>
        public Option<PageMarginsCore> Margins { get; }

        /// <summary>
        /// Gets the number of pages to fit the height of printout.
        /// </summary>
        public Option<int> FitToHeight { get; }

        /// <summary>
        /// Gets the number of pages to fit the width of printout.
        /// </summary>
        public Option<int> FitToWidth { get; }

        /// <summary>
        /// Gets the scale.
        /// </summary>
        public Option<int> Scale { get; }

        /// <summary>
        /// Gets bytes of the prototype book xlsx-file.
        /// </summary>
        public Option<byte[]> ProtoBook { get; }

        /// <summary>
        /// Gets the name of range with the prototype.
        /// </summary>
        public Option<string> ProtoName { get; }

        internal Option<ExcelWorksheet> ProtoSheet { get; }

        [Pure]
        internal static SheetCore Create() => Empty;

        [Pure]
        internal SheetCore WithBox(Option<BoxCore> box) => With(box: box);

        [Pure]
        internal SheetCore With(
            Option<string>? name = null,
            Option<Color>? tabColor = null,
            Option<BoxCore>? box = null,
            Option<PageView>? pageView = null,
            Option<PageOrientation>? orientation = null,
            Option<PageMarginsCore>? margins = null,
            Option<int>? fitToHeight = null,
            Option<int>? fitToWidth = null,
            Option<int>? scale = null,
            Option<byte[]>? protoBook = null,
            Option<string>? protoName = null,
            Option<ExcelWorksheet>? protoSheet = null) =>
            new SheetCore(
                name ?? Name,
                tabColor ?? TabColor,
                box ?? Box,
                pageView ?? PageView,
                orientation ?? Orientation,
                margins ?? Margins,
                fitToHeight ?? FitToHeight,
                fitToWidth ?? FitToWidth,
                scale ?? Scale,
                protoBook ?? ProtoBook,
                protoName ?? ProtoName,
                protoSheet ?? ProtoSheet);
    }
}