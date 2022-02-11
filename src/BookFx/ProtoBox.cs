namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// A prototype. Inner boxes are placed in slots.
    /// </summary>
    [PublicAPI]
    public sealed class ProtoBox : Box
    {
        private ProtoBox(BoxCore core)
            : base(core)
        {
        }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="ProtoBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ProtoBox(BoxCore core) => new ProtoBox(core);

        /// <summary>
        /// Add a box into a slot.
        /// </summary>
        /// <param name="slot">A name of range in prototype, in which the <paramref name="box"/> will be placed.</param>
        /// <param name="box">A box to place in the <paramref name="slot"/>.</param>
        [Pure]
        public ProtoBox Add(Reference slot, Box box) => Get.Add(slot, box.Get);

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new ProtoBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new ProtoBox AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new ProtoBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new ProtoBox AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new ProtoBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new ProtoBox AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.NameGlobally"/>
        [Pure]
        public new ProtoBox NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <inheritdoc cref="Box.NameLocally"/>
        [Pure]
        public new ProtoBox NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="Box.Name"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public new ProtoBox Name(string rangeName) => NameGlobally(rangeName);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new ProtoBox Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

        /// <inheritdoc cref="Box.SizeRows(IEnumerable{TrackSize})"/>
        [Pure]
        public new ProtoBox SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <inheritdoc cref="Box.SizeRows(TrackSize, TrackSize[])"/>
        [Pure]
        public new ProtoBox SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SizeCols(IEnumerable{TrackSize})"/>
        [Pure]
        public new ProtoBox SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <inheritdoc cref="Box.SizeCols(TrackSize, TrackSize[])"/>
        [Pure]
        public new ProtoBox SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SetPrintArea"/>
        [Pure]
        public new ProtoBox SetPrintArea() => Get.With(isPrintArea: true);

        /// <inheritdoc cref="Box.HideRows"/>
        [Pure]
        public new ProtoBox HideRows() => Get.With(areRowsHidden: true);

        /// <inheritdoc cref="Box.HideCols"/>
        [Pure]
        public new ProtoBox HideCols() => Get.With(areColsHidden: true);

        /// <inheritdoc cref="Box.Freeze"/>
        [Pure]
        public new ProtoBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <inheritdoc cref="Box.FreezeRows"/>
        [Pure]
        public new ProtoBox FreezeRows() => Get.With(areRowsFrozen: true);

        /// <inheritdoc cref="Box.FreezeCols"/>
        [Pure]
        public new ProtoBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <inheritdoc cref="Box.AutoFilter"/>
        [Pure]
        public new ProtoBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}