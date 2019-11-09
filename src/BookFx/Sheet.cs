namespace BookFx
{
    using System.Drawing;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Sheet is a class to describe worksheet.
    /// </summary>
    [PublicAPI]
    public sealed class Sheet
    {
        public static readonly Sheet Empty = SheetCore.Empty;

        private Sheet(SheetCore core) => Get = core;

        public SheetCore Get { get; }

        [Pure]
        public static implicit operator Sheet(SheetCore core) => new Sheet(core);

        [Pure]
        public Sheet Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// Set sheet tab color.
        /// </summary>
        [Pure]
        public Sheet TabColor(Color color) => Get.With(tabColor: color);

        [Pure]
        public Sheet SetPageView(PageView pageView) => Get.With(pageView: pageView);

        /// <summary>
        /// Fit to height and width in pages.
        /// </summary>
        /// <param name="height">Number of pages to fit height.</param>
        /// <param name="width">Number of pages to fit width.</param>
        [Pure]
        public Sheet Fit(int height = 1, int width = 1) => Get.With(fitToHeight: height, fitToWidth: width);

        /// <summary>
        /// Fit to height in pages.
        /// </summary>
        /// <param name="pages">Number of pages to fit.</param>
        [Pure]
        public Sheet FitToHeight(int pages = 1) => Get.With(fitToHeight: pages);

        /// <summary>
        /// Fit to width in pages.
        /// </summary>
        /// <param name="pages">Number of pages to fit.</param>
        [Pure]
        public Sheet FitToWidth(int pages = 1) => Get.With(fitToWidth: pages);

        [Pure]
        public Sheet Scale(int scale) => Get.With(scale: scale);

        [Pure]
        public Book ToBook() => Make.Book(this);
    }
}