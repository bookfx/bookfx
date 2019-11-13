namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class StackBox : CompositeBox
    {
        public static readonly StackBox Empty = BoxCore.Create(BoxType.Stack);

        private StackBox(BoxCore core)
            : base(core)
        {
        }

        [Pure]
        public static implicit operator StackBox(BoxCore core) => new StackBox(core);

        [Pure]
        public new StackBox Name(string name) => Get.With(name: Some(name));

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpan"/>
        /// </summary>
        [Pure]
        public new StackBox AutoSpan(bool autoSpan = true) => Get.With(rowAutoSpan: autoSpan, colAutoSpan: autoSpan);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanRows"/>
        /// </summary>
        [Pure]
        public new StackBox AutoSpanRows(bool autoSpanRows = true) => Get.With(rowAutoSpan: autoSpanRows);

        /// <summary>
        /// <inheritdoc cref="Box.AutoSpanCols"/>
        /// </summary>
        [Pure]
        public new StackBox AutoSpanCols(bool autoSpanCols) => Get.With(colAutoSpan: autoSpanCols);

        [Pure]
        public new StackBox Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public StackBox Add(Box child, params Box[] others) =>
            Add(others.Prepend(child));

        [Pure]
        public StackBox Add(IEnumerable<Box> children) =>
            Get.Add(children.Map(x => x.Get));

        [Pure]
        public new StackBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public new StackBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public new StackBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public new StackBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public new StackBox SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public new StackBox HideRows() => Get.With(areRowsHidden: true);

        [Pure]
        public new StackBox HideCols() => Get.With(areColsHidden: true);

        [Pure]
        public new StackBox Freeze() => Get.With(areRowsFrozen: true, areColsFrozen: true);

        [Pure]
        public new StackBox FreezeRows() => Get.With(areRowsFrozen: true);

        [Pure]
        public new StackBox FreezeCols() => Get.With(areColsFrozen: true);

        /// <summary>
        /// <inheritdoc cref="Box.AutoFilter"/>
        /// </summary>
        [Pure]
        public new StackBox AutoFilter() => Get.With(isAutoFilter: true);
    }
}