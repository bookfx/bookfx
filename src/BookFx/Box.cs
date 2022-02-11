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
    public class Box
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
        public static implicit operator Box(string content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="bool"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(bool content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="byte"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(byte content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="short"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(short content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="int"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(int content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="long"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(long content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="float"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(float content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="double"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(double content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="decimal"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(decimal content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="DateTime"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(DateTime content) => Value(content);

        /// <summary>
        /// Implicit convert from <see cref="Guid"/> to <see cref="Box"/>.
        /// </summary>
        [Pure]
        public static implicit operator Box(Guid content) => Value(content);

        /// <summary>
        /// Define a book scoped name of the range.
        /// </summary>
        /// <param name="rangeName">A range name.</param>
        [Pure]
        public Box NameGlobally(string rangeName) => Get.With(globalName: Some(rangeName));

        /// <summary>
        /// Define a sheet scoped name of the range.
        /// </summary>
        /// <param name="rangeName">A range name.</param>
        [Pure]
        public Box NameLocally(string rangeName) => Get.With(localName: Some(rangeName));

        /// <inheritdoc cref="NameGlobally"/>
        [Obsolete("Use NameGlobally or NameLocally instead.")]
        [Pure]
        public Box Name(string rangeName) => NameGlobally(rangeName);

        /// <summary>
        /// Enables automatic span when a box can be stretched to its contrainer.
        /// Applies to the box and its descendants, but AutoSpan of descendants has priority.
        /// By default is disabled.
        /// </summary>
        [Pure]
        public Box AutoSpan() => Get.With(rowAutoSpan: true, colAutoSpan: true);

        /// <summary>
        /// Enables or disables automatic span when a box can be stretched to its contrainer.
        /// Applies to the box and its descendants, but AutoSpan of descendants has priority.
        /// By default is disabled.
        /// </summary>
        [Pure]
        public Box AutoSpan(bool isEnabled) => Get.With(rowAutoSpan: isEnabled, colAutoSpan: isEnabled);

        /// <inheritdoc cref="AutoSpan()"/>
        [Pure]
        public Box AutoSpanRows() => Get.With(rowAutoSpan: true);

        /// <inheritdoc cref="AutoSpan(bool)"/>
        [Pure]
        public Box AutoSpanRows(bool isEnabled) => Get.With(rowAutoSpan: isEnabled);

        /// <inheritdoc cref="AutoSpan()"/>
        [Pure]
        public Box AutoSpanCols() => Get.With(colAutoSpan: true);

        /// <inheritdoc cref="AutoSpan(bool)"/>
        [Pure]
        public Box AutoSpanCols(bool isEnabled) => Get.With(colAutoSpan: isEnabled);

        /// <summary>
        /// Define a style.
        /// </summary>
        [Pure]
        public Box Style(BoxStyle boxStyle) => Get.With(style: boxStyle.Get);

        /// <summary>
        /// <para>Define heights of rows.</para>
        /// <para>
        /// Since a box can cover a few rows, the method takes a pattern of sizes.
        /// The pattern repeats throughout the box height and defines all row heights of the box.
        /// <code>Make.Value().SizeRows(new[] { 10, 20 }).SpanRows(5)</code>
        /// will define the following row sizes: 10, 20, 10, 20, 10.
        /// </para>
        /// <para>A row size of an outer box has priority over a row size of an inner box.</para>
        /// </summary>
        /// <param name="pattern">A pattern of row sizes.</param>
        [Pure]
        public Box SizeRows(IEnumerable<TrackSize> pattern) => Get.With(rowSizes: pattern);

        /// <summary>
        /// <para>Define heights of rows.</para>
        /// <para>
        /// Since a box can cover a few rows, the method takes a pattern of sizes.
        /// The pattern repeats throughout the box height and defines all row heights of the box.
        /// <code>Make.Value().SizeRows(10, 20).SpanRows(5)</code>
        /// will define the following row sizes: 10, 20, 10, 20, 10.
        /// </para>
        /// <para>A row size of an outer box has priority over a row size of an inner box.</para>
        /// </summary>
        /// <param name="firstPatternPart">The first part of a pattern of row sizes.</param>
        /// <param name="otherPatternParts">Other parts of a pattern of row sizes.</param>
        [Pure]
        public Box SizeRows(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(rowSizes: otherPatternParts.Prepend(firstPatternPart));

        /// <summary>
        /// <para>Define widths of columns.</para>
        /// <para>
        /// Since a box can cover a few columns, the method takes a pattern of sizes.
        /// The pattern repeats throughout the box width and defines all column widths of the box.
        /// <code>Make.Value().SizeCols(new[] { 10, 20 }).SpanCols(5)</code>
        /// will define the following column sizes: 10, 20, 10, 20, 10.
        /// </para>
        /// <para>A column size of an outer box has priority over a column size of an inner box.</para>
        /// </summary>
        /// <param name="pattern">A pattern of column sizes.</param>
        [Pure]
        public Box SizeCols(IEnumerable<TrackSize> pattern) => Get.With(colSizes: pattern);

        /// <summary>
        /// <para>Define widths of columns.</para>
        /// <para>
        /// Since a box can cover a few columns, the method takes a pattern of sizes.
        /// The pattern repeats throughout the box width and defines all column widths of the box.
        /// <code>Make.Value().SizeCols(10, 20).SpanCols(5)</code>
        /// will define the following column sizes: 10, 20, 10, 20, 10.
        /// </para>
        /// <para>A column size of an outer box has priority over a column size of an inner box.</para>
        /// </summary>
        /// <param name="firstPatternPart">The first part of a pattern of column sizes.</param>
        /// <param name="otherPatternParts">Other parts of a pattern of column sizes.</param>
        [Pure]
        public Box SizeCols(TrackSize firstPatternPart, params TrackSize[] otherPatternParts) =>
            Get.With(colSizes: otherPatternParts.Prepend(firstPatternPart));

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