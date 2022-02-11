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
    /// One of the composite <see cref="Box"/> types. Inner boxes are placed in stack one above the other.
    /// </summary>
    [PublicAPI]
    public sealed class StackBox : Box
    {
        /// <summary>
        /// The empty <see cref="StackBox"/>.
        /// </summary>
        public static readonly StackBox Empty = BoxCore.Create(BoxType.Stack);

        private StackBox(BoxCore core)
            : base(core)
        {
        }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="StackBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator StackBox(BoxCore core) => new StackBox(core);

        /// <inheritdoc cref="Box.NameGlobally"/>
        [Pure]
        public new StackBox NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <inheritdoc cref="Box.NameLocally"/>
        [Pure]
        public new StackBox NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="Box.Name"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public new StackBox Name(string rangeName) => NameGlobally(rangeName);

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new StackBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new StackBox AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new StackBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new StackBox AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new StackBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new StackBox AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new StackBox Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

        /// <summary>
        /// Add box(es) in stack.
        /// </summary>
        /// <param name="child">The first box.</param>
        /// <param name="others">Other boxes.</param>
        [Pure]
        public StackBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        /// <summary>
        /// Add box(es) in stack.
        /// </summary>
        [Pure]
        public StackBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        /// <inheritdoc cref="Box.SizeRows(IEnumerable{TrackSize})"/>
        [Pure]
        public new StackBox SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <inheritdoc cref="Box.SizeRows(TrackSize, TrackSize[])"/>
        [Pure]
        public new StackBox SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SizeCols(IEnumerable{TrackSize})"/>
        [Pure]
        public new StackBox SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <inheritdoc cref="Box.SizeCols(TrackSize, TrackSize[])"/>
        [Pure]
        public new StackBox SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SetPrintArea"/>
        [Pure]
        public new StackBox SetPrintArea() => Get.With(isPrintArea: true);

        /// <inheritdoc cref="Box.HideRows"/>
        [Pure]
        public new StackBox HideRows() => Get.With(areRowsHidden: true);

        /// <inheritdoc cref="Box.HideCols"/>
        [Pure]
        public new StackBox HideCols() => Get.With(areColsHidden: true);

        /// <inheritdoc cref="Box.Freeze"/>
        [Pure]
        public new StackBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <inheritdoc cref="Box.FreezeRows"/>
        [Pure]
        public new StackBox FreezeRows() => Get.With(areRowsFrozen: true);

        /// <inheritdoc cref="Box.FreezeCols"/>
        [Pure]
        public new StackBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <inheritdoc cref="Box.AutoFilter"/>
        [Pure]
        public new StackBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}