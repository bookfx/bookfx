namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;
    using static BookFx.Make;

    /// <summary>
    /// A box with a content (a value or a formula) or an empty box.
    /// </summary>
    [PublicAPI]
    public sealed class ValueBox : Box
    {
        /// <summary>
        /// The empty <see cref="ValueBox"/>.
        /// </summary>
        public static readonly ValueBox Empty = BoxCore.Create(BoxType.Value);

        private ValueBox(BoxCore core)
            : base(core)
        {
        }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(BoxCore core) => new ValueBox(core);

        /// <summary>
        /// Implicit convert from <see cref="string"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(string content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="bool"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(bool content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="byte"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(byte content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="short"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(short content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="int"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(int content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="long"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(long content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="float"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(float content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="double"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(double content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="decimal"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(decimal content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="DateTime"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(DateTime content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="Guid"/> to <see cref="ValueBox"/>.
        /// </summary>
        [Pure]
        public static implicit operator ValueBox(Guid content) => Value(content);

        /// <inheritdoc cref="Box.NameGlobally"/>
        [Pure]
        public new ValueBox NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <inheritdoc cref="Box.NameLocally"/>
        [Pure]
        public new ValueBox NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="Box.Name"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public new ValueBox Name(string rangeName) => NameGlobally(rangeName);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new ValueBox Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

        /// <summary>
        /// Span rows and columns.
        /// </summary>
        /// <param name="rows">A number of rows to span.</param>
        /// <param name="cols">A number of columns to span.</param>
        [Pure]
        public ValueBox Span(int rows, int cols) => Get.With(rowSpan: rows, colSpan: cols);

        /// <summary>
        /// Span rows.
        /// </summary>
        /// <param name="count">A number of rows to span.</param>
        [Pure]
        public ValueBox SpanRows(int count) => Get.With(rowSpan: count);

        /// <summary>
        /// Span columns.
        /// </summary>
        /// <param name="count">A number of columns to span.</param>
        [Pure]
        public ValueBox SpanCols(int count) => Get.With(colSpan: count);

        /// <inheritdoc cref="Box.AutoSpan()"/>
        [Pure]
        public new ValueBox AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpan(bool)"/>
        [Pure]
        public new ValueBox AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanRows()"/>
        [Pure]
        public new ValueBox AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanRows(bool)"/>
        [Pure]
        public new ValueBox AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="Box.AutoSpanCols()"/>
        [Pure]
        public new ValueBox AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="Box.AutoSpanCols(bool)"/>
        [Pure]
        public new ValueBox AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <summary>
        /// Merge cells.
        /// </summary>
        [Pure]
        public ValueBox Merge() => Get.With(merge: true);

        /// <summary>
        /// Merge or unmerge cells.
        /// </summary>
        /// <param name="isMerged">true - merge cells; false - unmerge cells.</param>
        [Pure]
        public ValueBox Merge(bool isMerged) => Get.With(merge: isMerged);

        /// <inheritdoc cref="Box.SizeRows(IEnumerable{TrackSize})"/>
        [Pure]
        public new ValueBox SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <inheritdoc cref="Box.SizeRows(TrackSize, TrackSize[])"/>
        [Pure]
        public new ValueBox SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SizeCols(IEnumerable{TrackSize})"/>
        [Pure]
        public new ValueBox SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <inheritdoc cref="Box.SizeCols(TrackSize, TrackSize[])"/>
        [Pure]
        public new ValueBox SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <inheritdoc cref="Box.SetPrintArea"/>
        [Pure]
        public new ValueBox SetPrintArea() => Get.With(isPrintArea: true);

        /// <inheritdoc cref="Box.HideRows"/>
        [Pure]
        public new ValueBox HideRows() => Get.With(areRowsHidden: true);

        /// <inheritdoc cref="Box.HideCols"/>
        [Pure]
        public new ValueBox HideCols() => Get.With(areColsHidden: true);

        /// <inheritdoc cref="Box.Freeze"/>
        [Pure]
        public new ValueBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <inheritdoc cref="Box.FreezeRows"/>
        [Pure]
        public new ValueBox FreezeRows() => Get.With(areRowsFrozen: true);

        /// <inheritdoc cref="Box.FreezeCols"/>
        [Pure]
        public new ValueBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <inheritdoc cref="Box.AutoFilter"/>
        [Pure]
        public new ValueBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}