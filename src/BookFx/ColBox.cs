namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// One of the composite <see cref="Box"/> types. Inner boxes are placed in column from top to bottom.
    /// </summary>
    [PublicAPI]
    public sealed class ColBox : Box
    {
        /// <summary>
        /// The empty <see cref="ColBox"/>.
        /// </summary>
        public static readonly ColBox Empty = BoxCore.Create(BoxType.Col);

        private ColBox(BoxCore core)
            : base(core)
        {
        }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="ColBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ColBox(BoxCore core) => new ColBox(core);

        /// <inheritdoc cref="Box.Name"/>
        [Pure]
        public new ColBox Name(string name) => Get.With(name: Some(name));

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new ColBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new ColBox AutoSpan(bool autoSpan) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new ColBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new ColBox AutoSpanRows(bool autoSpanRows) => Get.With(rowAutoSpan: autoSpanRows);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new ColBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new ColBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new ColBox Style(BoxStyle style) => Get.With(style: style.Get);

        /// <summary>
        /// Add box(es) in column.
        /// </summary>
        /// <param name="child">The first box.</param>
        /// <param name="others">Other boxes.</param>
        [Pure]
        public ColBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        /// <summary>
        /// Add box(es) in column.
        /// </summary>
        [Pure]
        public ColBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="sizes">Sizes of rows.</param>
        [Pure]
        public new ColBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="size">A size of the first row.</param>
        /// <param name="others">Sizes of other rows.</param>
        [Pure]
        public new ColBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="sizes">Sizes of columns.</param>
        [Pure]
        public new ColBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="size">A size of the first column.</param>
        /// <param name="others">Sizes of other columns.</param>
        [Pure]
        public new ColBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        /// <inheritdoc cref="Box.SetPrintArea"/>
        [Pure]
        public new ColBox SetPrintArea() => Get.With(isPrintArea: true);

        /// <inheritdoc cref="Box.HideRows"/>
        [Pure]
        public new ColBox HideRows() => Get.With(areRowsHidden: true);

        /// <inheritdoc cref="Box.HideCols"/>
        [Pure]
        public new ColBox HideCols() => Get.With(areColsHidden: true);

        /// <inheritdoc cref="Box.Freeze"/>
        [Pure]
        public new ColBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <inheritdoc cref="Box.FreezeRows"/>
        [Pure]
        public new ColBox FreezeRows() => Get.With(areRowsFrozen: true);

        /// <inheritdoc cref="Box.FreezeCols"/>
        [Pure]
        public new ColBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <inheritdoc cref="Box.AutoFilter"/>
        [Pure]
        public new ColBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}