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
    /// A box with a value, with a formula or an empty box.
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

        [Pure]
        public static implicit operator ValueBox(BoxCore core) => new ValueBox(core);

        [Pure]
        public static implicit operator ValueBox(string value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(bool value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(byte value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(sbyte value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(short value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(ushort value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(int value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(uint value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(long value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(ulong value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(float value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(double value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(decimal value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(DateTime value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(Guid value) => Value(value);

        /// <inheritdoc cref="Box.Name"/>
        [Pure]
        public new ValueBox Name(string name) => Get.With(name: Some(name));

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new ValueBox Style(BoxStyle style) => Get.With(style: style.Get);

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

        /// <inheritdoc cref="Box.AutoSpan"/>
        [Pure]
        public new ValueBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <inheritdoc cref="Box.AutoSpanRows"/>
        [Pure]
        public new ValueBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <inheritdoc cref="Box.AutoSpanCols"/>
        [Pure]
        public new ValueBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public ValueBox Merge(bool merge = true) => Get.With(merge: merge);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="sizes">Sizes of rows.</param>
        [Pure]
        public new ValueBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="size">A size of the first row.</param>
        /// <param name="others">Sizes of other rows.</param>
        [Pure]
        public new ValueBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="sizes">Sizes of columns.</param>
        [Pure]
        public new ValueBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="size">A size of the first column.</param>
        /// <param name="others">Sizes of other columns.</param>
        [Pure]
        public new ValueBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

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