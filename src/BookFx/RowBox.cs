namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// One of the composite <see cref="Box"/> types. Inner boxes are placed in row from left to right.
    /// </summary>
    [PublicAPI]
    public sealed class RowBox : Box
    {
        /// <summary>
        /// The empty <see cref="RowBox"/>.
        /// </summary>
        public static readonly RowBox Empty = BoxCore.Create(BoxType.Row);

        private RowBox(BoxCore core)
            : base(core)
        {
        }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="RowBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator RowBox(BoxCore core) => new RowBox(core);

        /// <inheritdoc cref="Box.NameGlobally"/>
        [Pure]
        public new RowBox NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <inheritdoc cref="Box.NameLocally"/>
        [Pure]
        public new RowBox NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="Box.Name"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public new RowBox Name(string rangeName) => NameGlobally(rangeName);

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new RowBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new RowBox AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new RowBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new RowBox AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new RowBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new RowBox AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new RowBox Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

        /// <summary>
        /// Add box(es) in row.
        /// </summary>
        /// <param name="child">The first box.</param>
        /// <param name="others">Other boxes.</param>
        [Pure]
        public RowBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        /// <summary>
        /// Add box(es) in row.
        /// </summary>
        [Pure]
        public RowBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        /// <inheritdoc cref="Box.SizeRows(IEnumerable{TrackSize})"/>
        [Pure]
        public new RowBox SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <inheritdoc cref="Box.SizeRows(TrackSize, TrackSize[])"/>
        [Pure]
        public new RowBox SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SizeCols(IEnumerable{TrackSize})"/>
        [Pure]
        public new RowBox SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <inheritdoc cref="Box.SizeCols(TrackSize, TrackSize[])"/>
        [Pure]
        public new RowBox SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SetPrintArea"/>
        [Pure]
        public new RowBox SetPrintArea() => Get.With(isPrintArea: true);

        /// <inheritdoc cref="Box.HideRows"/>
        [Pure]
        public new RowBox HideRows() => Get.With(areRowsHidden: true);

        /// <inheritdoc cref="Box.HideCols"/>
        [Pure]
        public new RowBox HideCols() => Get.With(areColsHidden: true);

        /// <inheritdoc cref="Box.Freeze"/>
        [Pure]
        public new RowBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <inheritdoc cref="Box.FreezeRows"/>
        [Pure]
        public new RowBox FreezeRows() => Get.With(areRowsFrozen: true);

        /// <inheritdoc cref="Box.FreezeCols"/>
        [Pure]
        public new RowBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <inheritdoc cref="Box.AutoFilter"/>
        [Pure]
        public new RowBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}