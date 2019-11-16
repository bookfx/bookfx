namespace BookFx
{
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

        /// <inheritdoc cref="Box.Name"/>
        [Pure]
        public new StackBox Name(string name) => Get.With(name: Some(name));

        /// <inheritdoc cref="Box.AutoSpan"/>
        [Pure]
        public new StackBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <inheritdoc cref="Box.AutoSpanRows"/>
        [Pure]
        public new StackBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <inheritdoc cref="Box.AutoSpanCols"/>
        [Pure]
        public new StackBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        /// <inheritdoc cref="Box.Style"/>
        [Pure]
        public new StackBox Style(BoxStyle style) => Get.With(style: style.Get);

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

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="sizes">Sizes of rows.</param>
        [Pure]
        public new StackBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        /// <summary>
        /// Define heights of rows.
        /// </summary>
        /// <param name="size">A size of the first row.</param>
        /// <param name="others">Sizes of other rows.</param>
        [Pure]
        public new StackBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="sizes">Sizes of columns.</param>
        [Pure]
        public new StackBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        /// <summary>
        /// Define widths of columns.
        /// </summary>
        /// <param name="size">A size of the first column.</param>
        /// <param name="others">Sizes of other columns.</param>
        [Pure]
        public new StackBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

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