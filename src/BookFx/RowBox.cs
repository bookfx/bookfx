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
        public new RowBox HideRows() => Get.With(isRowsHidden: true);

        [Pure]
        public new RowBox HideCols() => Get.With(isColsHidden: true);

        [Pure]
        public new RowBox FreezeRows() => Get.With(isRowsFrozen: true);

        [Pure]
        public new RowBox FreezeCols() => Get.With(isColsFrozen: true);
    }
}