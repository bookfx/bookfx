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
        /// <summary>
        /// The empty sheet.
        /// </summary>
        public static readonly Sheet Empty = SheetCore.Empty;

        private Sheet(SheetCore core) => Get = core;

        /// <summary>
        /// Gets properties of the sheet.
        /// </summary>
        public SheetCore Get { get; }

        /// <summary>
        /// Implicit convert from <see cref="SheetCore"/> to <see cref="Sheet"/>.
        /// </summary>
        [Pure]
        public static implicit operator Sheet(SheetCore core) => new Sheet(core);

        /// <summary>
        /// Define a sheet name.
        /// </summary>
        /// <param name="sheetName">
        /// A sheet name.
        /// Take care that the name is not empty,
        /// the name length is not longer than 31 and
        /// the name is free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        [Pure]
        public Sheet Name(string sheetName) => Get.With(name: Some(sheetName));

        /// <summary>
        /// Define a tab color.
        /// </summary>
        [Pure]
        public Sheet TabColor(Color color) => Get.With(tabColor: color);

        /// <summary>
        /// Define page view.
        /// </summary>
        [Pure]
        public Sheet SetPageView(PageView pageView) => Get.With(pageView: pageView);

        /// <summary>
        /// Fit the height and the width of printout to the one page.
        /// </summary>
        [Pure]
        public Sheet Fit() => Get.With(fitToHeight: 1, fitToWidth: 1);

        /// <summary>
        /// Fit the height and the width of printout to pages.
        /// </summary>
        /// <param name="height">Number of pages to fit height.</param>
        /// <param name="width">Number of pages to fit width.</param>
        [Pure]
        public Sheet Fit(int height, int width) => Get.With(fitToHeight: height, fitToWidth: width);

        /// <summary>
        /// Fit the height of printout to the one page.
        /// </summary>
        [Pure]
        public Sheet FitToHeight() => Get.With(fitToHeight: 1);

        /// <summary>
        /// Fit the height of printout to pages.
        /// </summary>
        /// <param name="pages">Number of pages to fit.</param>
        [Pure]
        public Sheet FitToHeight(int pages) => Get.With(fitToHeight: pages);

        /// <summary>
        /// Fit the width of printout to the one page.
        /// </summary>
        [Pure]
        public Sheet FitToWidth() => Get.With(fitToWidth: 1);

        /// <summary>
        /// Fit the width of printout to pages.
        /// </summary>
        /// <param name="pages">Number of pages to fit.</param>
        [Pure]
        public Sheet FitToWidth(int pages) => Get.With(fitToWidth: pages);

        /// <summary>
        /// Define a scale.
        /// </summary>
        /// <param name="percents">A scale in percents.</param>
        [Pure]
        public Sheet Scale(int percents) => Get.With(scale: percents);

        /// <summary>
        /// Make a <see cref="Book"/> with one sheet.
        /// </summary>
        [Pure]
        public Book ToBook() => Make.Book(this);
    }
}