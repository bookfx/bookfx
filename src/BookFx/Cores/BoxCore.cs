namespace BookFx.Cores
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    [PublicAPI]
    public sealed class BoxCore
    {
        internal static readonly BoxCore Empty = Create(BoxType.Row);

        private BoxCore(
            BoxType type,
            Option<string> name,
            Option<BoxStyleCore> style,
            IEnumerable<TrackSize> rowSizes,
            IEnumerable<TrackSize> colSizes,
            bool isPrintArea,
            bool isRowsHidden,
            bool isColsHidden,
            bool isRowsFrozen,
            bool isColsFrozen,
            Option<object> value,
            Option<int> rowSpan,
            Option<int> colSpan,
            Option<bool> merge,
            IEnumerable<BoxCore> children,
            Option<ProtoCore> proto,
            IEnumerable<SlotCore> slots,
            Dimension minDimension,
            Placement placement)
        {
            Type = type;
            Name = name;
            Style = style;
            IsPrintArea = isPrintArea;
            IsRowsHidden = isRowsHidden;
            IsColsHidden = isColsHidden;
            IsRowsFrozen = isRowsFrozen;
            IsColsFrozen = isColsFrozen;
            RowSizes = rowSizes.ToImmutableList();
            ColSizes = colSizes.ToImmutableList();
            Value = value;
            RowSpan = rowSpan;
            ColSpan = colSpan;
            Merge = merge;
            Children = children.ToImmutableList();
            Proto = proto;
            Slots = slots.ToImmutableList();
            MinDimension = minDimension;
            Placement = placement;
        }

        public BoxType Type { get; }

        public Option<string> Name { get; }

        public Option<BoxStyleCore> Style { get; }

        public ImmutableList<TrackSize> RowSizes { get; }

        public ImmutableList<TrackSize> ColSizes { get; }

        public bool IsPrintArea { get; }

        public bool IsRowsHidden { get; }

        public bool IsColsHidden { get; }

        public bool IsRowsFrozen { get; }

        public bool IsColsFrozen { get; }

        public Option<object> Value { get; }

        public Option<int> RowSpan { get; }

        public Option<int> ColSpan { get; }

        public Option<bool> Merge { get; }

        public ImmutableList<BoxCore> Children { get; }

        public Option<ProtoCore> Proto { get; }

        public ImmutableList<SlotCore> Slots { get; }

        internal Dimension MinDimension { get; }

        internal Placement Placement { get; }

        [Pure]
        internal static BoxCore Create(
            BoxType type,
            Option<object>? value = null,
            Option<BoxStyleCore>? style = null) =>
            new BoxCore(
                type: type,
                name: None,
                style: style ?? None,
                rowSizes: Enumerable.Empty<TrackSize>(),
                colSizes: Enumerable.Empty<TrackSize>(),
                isPrintArea: false,
                isRowsHidden: false,
                isColsHidden: false,
                isRowsFrozen: false,
                isColsFrozen: false,
                value: value ?? None,
                rowSpan: None,
                colSpan: None,
                merge: None,
                children: Enumerable.Empty<BoxCore>(),
                proto: None,
                slots: Enumerable.Empty<SlotCore>(),
                minDimension: Dimension.Empty,
                placement: Placement.Empty);

        [Pure]
        internal BoxCore Add(IEnumerable<BoxCore> children) =>
            With(children: Children.AddRange(children));

        [Pure]
        internal BoxCore Add(Reference slotRef, BoxCore box) =>
            With(slots: Slots.Add(SlotCore.Create(slotRef, box)));

        [Pure]
        internal BoxCore With(
            BoxType? type = null,
            Option<string>? name = null,
            Option<BoxStyleCore>? style = null,
            IEnumerable<TrackSize>? rowSizes = null,
            IEnumerable<TrackSize>? colSizes = null,
            bool? isPrintArea = null,
            bool? isRowsHidden = null,
            bool? isColsHidden = null,
            bool? isRowsFrozen = null,
            bool? isColsFrozen = null,
            Option<object>? value = null,
            Option<int>? rowSpan = null,
            Option<int>? colSpan = null,
            Option<bool>? merge = null,
            IEnumerable<BoxCore>? children = null,
            Option<ProtoCore>? proto = null,
            IEnumerable<SlotCore>? slots = null,
            Dimension? minDimension = null,
            Placement? placement = null) =>
            new BoxCore(
                type ?? Type,
                name ?? Name,
                style ?? Style,
                rowSizes ?? RowSizes,
                colSizes ?? ColSizes,
                isPrintArea ?? IsPrintArea,
                isRowsHidden ?? IsRowsHidden,
                isColsHidden ?? IsColsHidden,
                isRowsFrozen ?? IsRowsFrozen,
                isColsFrozen ?? IsColsFrozen,
                value ?? Value,
                rowSpan ?? RowSpan,
                colSpan ?? ColSpan,
                merge ?? Merge,
                children ?? Children,
                proto ?? Proto,
                slots ?? Slots,
                minDimension ?? MinDimension,
                placement ?? Placement);
    }
}