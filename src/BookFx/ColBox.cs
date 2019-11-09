namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class ColBox : CompositeBox
    {
        public static readonly ColBox Empty = BoxCore.Create(BoxType.Col);

        private ColBox(BoxCore core)
            : base(core)
        {
        }

        [Pure]
        public static implicit operator ColBox(BoxCore core) => new ColBox(core);

        [Pure]
        public new ColBox Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpan"/>
        /// </summary>
        [Pure]
        public new ColBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanRows"/>
        /// </summary>
        [Pure]
        public new ColBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanCols"/>
        /// </summary>
        [Pure]
        public new ColBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public new ColBox Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public ColBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        [Pure]
        public ColBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        [Pure]
        public new ColBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public new ColBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public new ColBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public new ColBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public new ColBox SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public new ColBox HideRows() => Get.With(isRowsHidden: true);

        [Pure]
        public new ColBox HideCols() => Get.With(isColsHidden: true);

        [Pure]
        public new ColBox FreezeRows() => Get.With(isRowsFrozen: true);

        [Pure]
        public new ColBox FreezeCols() => Get.With(isColsFrozen: true);

        /// <summary>
        /// <inheritdoc cref="Box.AutoFilter"/>
        /// </summary>
        [Pure]
        public new ColBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}