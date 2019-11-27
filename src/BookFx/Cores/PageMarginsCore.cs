namespace BookFx.Cores
{
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a page margins.
    /// </summary>
    [PublicAPI]
    public class PageMarginsCore
    {
        internal static readonly PageMarginsCore InCentimetres = Create(UnitOfMeasurement.Centimetre);

        internal static readonly PageMarginsCore InInches = Create(UnitOfMeasurement.Inch);

        private const decimal CentimetresInInch = 2.54m;

        private PageMarginsCore(
            UnitOfMeasurement unit,
            Option<decimal> top,
            Option<decimal> right,
            Option<decimal> bottom,
            Option<decimal> left,
            Option<decimal> header,
            Option<decimal> footer)
        {
            Unit = unit;
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
            Header = header;
            Footer = footer;
        }

        /// <summary>
        /// Unit of measurement of page margins.
        /// </summary>
        public enum UnitOfMeasurement
        {
            /// <summary>
            /// Inches.
            /// </summary>
            Inch,

            /// <summary>
            /// Centimetres.
            /// </summary>
            Centimetre,
        }

        /// <summary>
        /// Gets the unit of measurement of page margins.
        /// </summary>
        public UnitOfMeasurement Unit { get; }

        /// <summary>
        /// Gets the top margin.
        /// </summary>
        public Option<decimal> Top { get; }

        /// <summary>
        /// Gets the right margin.
        /// </summary>
        public Option<decimal> Right { get; }

        /// <summary>
        /// Gets the bottom margin.
        /// </summary>
        public Option<decimal> Bottom { get; }

        /// <summary>
        /// Gets the left margin.
        /// </summary>
        public Option<decimal> Left { get; }

        /// <summary>
        /// Gets the header margin.
        /// </summary>
        public Option<decimal> Header { get; }

        /// <summary>
        /// Gets the footer margin.
        /// </summary>
        public Option<decimal> Footer { get; }

        [Pure]
        internal PageMarginsCore ToInches() =>
            Unit == UnitOfMeasurement.Inch
                ? this
                : With(
                    unit: UnitOfMeasurement.Inch,
                    top: Top.Map(x => x / CentimetresInInch),
                    right: Right.Map(x => x / CentimetresInInch),
                    bottom: Bottom.Map(x => x / CentimetresInInch),
                    left: Left.Map(x => x / CentimetresInInch),
                    header: Header.Map(x => x / CentimetresInInch),
                    footer: Footer.Map(x => x / CentimetresInInch));

        [Pure]
        internal PageMarginsCore With(
            UnitOfMeasurement? unit = null,
            Option<decimal>? top = null,
            Option<decimal>? right = null,
            Option<decimal>? bottom = null,
            Option<decimal>? left = null,
            Option<decimal>? header = null,
            Option<decimal>? footer = null) => new PageMarginsCore(
            unit ?? Unit,
            top ?? Top,
            right ?? Right,
            bottom ?? Bottom,
            left ?? Left,
            header ?? Header,
            footer ?? Footer);

        [Pure]
        private static PageMarginsCore Create(UnitOfMeasurement unit) =>
            new PageMarginsCore(unit, None, None, None, None, None, None);
    }
}