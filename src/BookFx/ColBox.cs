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

        /// <inheritdoc cref="Box.NameGlobally"/>
        [Pure]
        public new ColBox NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <inheritdoc cref="Box.NameLocally"/>
        [Pure]
        public new ColBox NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="Box.Name"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public new ColBox Name(string rangeName) => NameGlobally(rangeName);

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new ColBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new ColBox AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new ColBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new ColBox AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new ColBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new ColBox AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new ColBox Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

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

        /// <inheritdoc cref="Box.SizeRows(IEnumerable{TrackSize})"/>
        [Pure]
        public new ColBox SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <inheritdoc cref="Box.SizeRows(TrackSize, TrackSize[])"/>
        [Pure]
        public new ColBox SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SizeCols(IEnumerable{TrackSize})"/>
        [Pure]
        public new ColBox SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <inheritdoc cref="Box.SizeCols(TrackSize, TrackSize[])"/>
        [Pure]
        public new ColBox SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

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