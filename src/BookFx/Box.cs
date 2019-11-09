namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;
    using static BookFx.Make;

    [PublicAPI]
    public abstract class Box
    {
        private protected Box(BoxCore core) => Get = core;

        public BoxCore Get { get; }

        [Pure]
        public static implicit operator Box(BoxCore core) =>
            core.Match<Box>(
                row: x => (RowBox)x,
                col: x => (ColBox)x,
                stack: x => (StackBox)x,
                value: x => (ValueBox)x,
                proto: x => (ProtoBox)x);

        [Pure]
        public static implicit operator Box(string value) => Value(value);

        [Pure]
        public static implicit operator Box(bool value) => Value(value);

        [Pure]
        public static implicit operator Box(int value) => Value(value);

        [Pure]
        public static implicit operator Box(long value) => Value(value);

        [Pure]
        public static implicit operator Box(double value) => Value(value);

        [Pure]
        public static implicit operator Box(decimal value) => Value(value);

        [Pure]
        public static implicit operator Box(DateTime value) => Value(value);

        [Pure]
        public Box Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// Enables or disables automatic span when a box can be stretched to its contrainer.
        /// Applies to the box and its descendants, but AutoSpan of descendants has priority.
        /// By default is disabled.
        /// </summary>
        [Pure]
        public Box AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <summary>
        /// <inheritdoc cref="AutoSpan"/>
        /// </summary>
        [Pure]
        public Box AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <summary>
        /// <inheritdoc cref="AutoSpan"/>
        /// </summary>
        [Pure]
        public Box AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public Box Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public Box SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public Box SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public Box SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public Box SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public Box SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public Box HideRows() => Get.With(isRowsHidden: true);

        [Pure]
        public Box HideCols() => Get.With(isColsHidden: true);

        [Pure]
        public Box FreezeRows() => Get.With(isRowsFrozen: true);

        [Pure]
        public Box FreezeCols() => Get.With(isColsFrozen: true);

        [Pure]
        public Sheet ToSheet() => Sheet(this);
    }
}