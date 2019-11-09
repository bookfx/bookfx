namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class ProtoBox : Box
    {
        private ProtoBox(BoxCore core)
            : base(core)
        {
        }

        [Pure]
        public static implicit operator ProtoBox(BoxCore core) => new ProtoBox(core);

        [Pure]
        public ProtoBox Add(Reference slot, Box box) => Get.Add(slot, box.Get);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpan"/>
        /// </summary>
        [Pure]
        public new ProtoBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanRows"/>
        /// </summary>
        [Pure]
        public new ProtoBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanCols"/>
        /// </summary>
        [Pure]
        public new ProtoBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public new ProtoBox Name(string name) => Get.With(name: Some(name));

        [Pure]
        public new ProtoBox Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public new ProtoBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public new ProtoBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public new ProtoBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public new ProtoBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public new ProtoBox SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public new ProtoBox HideRows() => Get.With(isRowsHidden: true);

        [Pure]
        public new ProtoBox HideCols() => Get.With(isColsHidden: true);

        [Pure]
        public new ProtoBox FreezeRows() => Get.With(isRowsFrozen: true);

        [Pure]
        public new ProtoBox FreezeCols() => Get.With(isColsFrozen: true);

        /// <summary>
        /// <inheritdoc cref="Box.AutoFilter"/>
        /// </summary>
        [Pure]
        public new ProtoBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}