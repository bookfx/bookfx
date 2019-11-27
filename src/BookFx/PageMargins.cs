namespace BookFx
{
    using BookFx.Cores;
    using JetBrains.Annotations;

    /// <summary>
    /// Page margins.
    /// </summary>
    [PublicAPI]
    public class PageMargins
    {
        private PageMargins(PageMarginsCore core) => Get = core;

        /// <summary>
        /// Gets page margins.
        /// </summary>
        public PageMarginsCore Get { get; }

        /// <summary>
        /// Implicit convert from <see cref="PageMarginsCore"/> to <see cref="PageMargins"/>.
        /// </summary>
        [Pure]
        public static implicit operator PageMargins(PageMarginsCore core) => new PageMargins(core);

        /// <summary>
        /// Creates page margins in centimetres without values.
        /// </summary>
        [Pure]
        public static PageMargins InCentimetres() => PageMarginsCore.InCentimetres;

        /// <summary>
        /// Creates page margins in centimetres with the same top, right, bottom and left margins.
        /// </summary>
        [Pure]
        public static PageMargins InCentimetres(double margin) =>
            PageMarginsCore.InCentimetres.With(top: margin, right: margin, bottom: margin, left: margin);

        /// <summary>
        /// Creates page margins in centimetres with the same vertical (top, bottom)
        /// and horizontal (left, right) margins.
        /// </summary>
        /// <param name="vertical">Top and bottom margin.</param>
        /// <param name="horizontal">Left and right margin.</param>
        [Pure]
        public static PageMargins InCentimetres(double vertical, double horizontal) =>
            PageMarginsCore.InCentimetres.With(top: vertical, right: horizontal, bottom: vertical, left: horizontal);

        /// <summary>
        /// Creates page margins in centimetres.
        /// </summary>
        /// <param name="top">A top margin.</param>
        /// <param name="right">A right margin.</param>
        /// <param name="bottom">A bottom margin.</param>
        /// <param name="left">A left margin.</param>
        [Pure]
        public static PageMargins InCentimetres(double top, double right, double bottom, double left) =>
            PageMarginsCore.InCentimetres.With(top: top, right: right, bottom: bottom, left: left);

        /// <summary>
        /// Creates page margins in inches without values.
        /// </summary>
        [Pure]
        public static PageMargins InInches() => PageMarginsCore.InInches;

        /// <summary>
        /// Creates page margins in inches with the same top, right, bottom and left margins.
        /// </summary>
        [Pure]
        public static PageMargins InInches(double margin) =>
            PageMarginsCore.InInches.With(top: margin, right: margin, bottom: margin, left: margin);

        /// <summary>
        /// Creates page margins in inches with the same vertical (top, bottom)
        /// and horizontal (left, right) margins.
        /// </summary>
        /// <param name="vertical">Top and bottom margin.</param>
        /// <param name="horizontal">Left and right margin.</param>
        [Pure]
        public static PageMargins InInches(double vertical, double horizontal) =>
            PageMarginsCore.InInches.With(top: vertical, right: horizontal, bottom: vertical, left: horizontal);

        /// <summary>
        /// Creates page margins in inches.
        /// </summary>
        /// <param name="top">A top margin.</param>
        /// <param name="right">A right margin.</param>
        /// <param name="bottom">A bottom margin.</param>
        /// <param name="left">A left margin.</param>
        [Pure]
        public static PageMargins InInches(double top, double right, double bottom, double left) =>
            PageMarginsCore.InInches.With(top: top, right: right, bottom: bottom, left: left);

        /// <summary>
        /// Define a top margin.
        /// </summary>
        [Pure]
        public PageMargins Top(double margin) => Get.With(top: margin);

        /// <summary>
        /// Define a right margin.
        /// </summary>
        [Pure]
        public PageMargins Right(double margin) => Get.With(right: margin);

        /// <summary>
        /// Define a bottom margin.
        /// </summary>
        [Pure]
        public PageMargins Bottom(double margin) => Get.With(bottom: margin);

        /// <summary>
        /// Define a left margin.
        /// </summary>
        [Pure]
        public PageMargins Left(double margin) => Get.With(left: margin);

        /// <summary>
        /// Define a header margin.
        /// </summary>
        [Pure]
        public PageMargins Header(double margin) => Get.With(header: margin);

        /// <summary>
        /// Define a footer margin.
        /// </summary>
        [Pure]
        public PageMargins Footer(double margin) => Get.With(footer: margin);
    }
}