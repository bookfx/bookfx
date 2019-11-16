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
    /// Box is the building block of a sheet.
    /// </summary>
    [PublicAPI]
    public abstract class Box
    {
        private protected Box(BoxCore core) => Get = core;

        /// <summary>
        /// Gets properties of the box.
        /// </summary>
        public BoxCore Get { get; }

        /// <summary>
        /// Implicit convert from <see cref="BoxCore"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(BoxCore core) =>
            core.Match<Box>(
                row: x => (RowBox)x,
                col: x => (ColBox)x,
                stack: x => (StackBox)x,
                value: x => (ValueBox)x,
                proto: x => (ProtoBox)x);

        /// <summary>
        /// Implicit convert from <see cref="string"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(string value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="bool"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(bool value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="byte"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(byte value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="sbyte"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(sbyte value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="short"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(short value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="ushort"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(ushort value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="int"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(int value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="uint"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(uint value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="long"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(long value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="ulong"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(ulong value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="float"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(float value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="double"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(double value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="decimal"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(decimal value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="DateTime"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(DateTime value) => Value(value);

        /// <summary>
        /// Implicit convert from <see cref="Guid"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(Guid value) => Value(value);

        /// <summary>
        /// Define a name of the range.
        /// </summary>
        /// <param name="name">A range name.</param>
        [Pure]
        public Box Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// Enables or disables automatic span when a box can be stretched to its contrainer.
        /// Applies to the box and its descendants, but AutoSpan of descendants has priority.
        /// By default is disabled.
        /// </summary>
        [Pure]
        public Box AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <inheritdoc cref="AutoSpan"/>
        [Pure]
        public Box AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <inheritdoc cref="AutoSpan"/>
        [Pure]
        public Box AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        /// <summary>
        /// Define a style.
        /// </summary>
        [Pure]
        public Box Style(BoxStyle style) => Get.With(style: style.Get);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="sizes">Sizes of rows.</param>
        [Pure]
        public Box SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="size">A size of the first row.</param>
        /// <param name="others">Sizes of other rows.</param>
        [Pure]
        public Box SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="sizes">Sizes of columns.</param>
        [Pure]
        public Box SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="size">A size of the first column.</param>
        /// <param name="others">Sizes of other columns.</param>
        [Pure]
        public Box SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        /// <summary>
        /// Define print area by the box.
        /// </summary>
        [Pure]
        public Box SetPrintArea() => Get.With(isPrintArea: true);

        /// <summary>
        /// Hide rows.
        /// </summary>
        [Pure]
        public Box HideRows() => Get.With(areRowsHidden: true);

        /// <summary>
        /// Hide columns.
        /// </summary>
        [Pure]
        public Box HideCols() => Get.With(areColsHidden: true);

        /// <summary>
        /// Freeze the box range.
        /// </summary>
        [Pure]
        public Box Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        /// <summary>
        /// Freeze rows of the box.
        /// </summary>
        [Pure]
        public Box FreezeRows() => Get.With(areRowsFrozen: true);

        /// <summary>
        /// Freeze columns of the box.
        /// </summary>
        [Pure]
        public Box FreezeCols() => Get.With(areColsFrozen: true);

        /// <summary>
        /// Add auto filter to the lower row of the box.
        /// </summary>
        [Pure]
        public Box AutoFilter() => Get.With(isAutoFilter: true);

        /// <summary>
        /// Make a <see cref="Sheet"/> with the root box.
        /// </summary>
        [Pure]
        public Sheet ToSheet() => Sheet(this);
    }
}