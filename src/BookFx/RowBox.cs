namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class RowBox : CompositeBox
    {
        public static readonly RowBox Empty = BoxCore.Create(BoxType.Row);

        private RowBox(BoxCore core)
            : base(core)
        {
        }

        [Pure]
        public static implicit operator RowBox(BoxCore core) => new RowBox(core);

        [Pure]
        public new RowBox Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpan"/>
        /// </summary>
        [Pure]
        public new RowBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanRows"/>
        /// </summary>
        [Pure]
        public new RowBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanCols"/>
        /// </summary>
        [Pure]
        public new RowBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public new RowBox Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public RowBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        [Pure]
        public RowBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        [Pure]
        public new RowBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public new RowBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public new RowBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public new RowBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public new RowBox SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public new RowBox HideRows() => Get.With(areRowsHidden: true);

        [Pure]
        public new RowBox HideCols() => Get.With(areColsHidden: true);

        [Pure]
        public new RowBox FreezeRows() => Get.With(areRowsFrozen: true);

        [Pure]
        public new RowBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <summary>
        /// <inheritdoc cref="Box.AutoFilter"/>
        /// </summary>
        [Pure]
        public new RowBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}