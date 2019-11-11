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
            bool areRowsHidden,
            bool areColsHidden,
            bool areRowsFrozen,
            bool areColsFrozen,
            bool isAutoFilter,
            Option<object> value,
            Option<int> rowSpan,
            Option<int> colSpan,
            Option<bool> rowAutoSpan,
            Option<bool> colAutoSpan,
            Option<bool> merge,
            IEnumerable<BoxCore> children,
            Option<ProtoCore> proto,
            IEnumerable<SlotCore> slots,
            int number,
            Placement placement)
        {
            Type = type;
            Name = name;
            Style = style;
            IsPrintArea = isPrintArea;
            AreRowsHidden = areRowsHidden;
            AreColsHidden = areColsHidden;
            AreRowsFrozen = areRowsFrozen;
            AreColsFrozen = areColsFrozen;
            IsAutoFilter = isAutoFilter;
            RowSizes = rowSizes.ToImmutableList();
            ColSizes = colSizes.ToImmutableList();
            Value = value;
            RowSpan = rowSpan;
            ColSpan = colSpan;
            RowAutoSpan = rowAutoSpan;
            ColAutoSpan = colAutoSpan;
            Merge = merge;
            Children = children.ToImmutableList();
            Proto = proto;
            Slots = slots.ToImmutableList();
            Number = number;
            Placement = placement;
        }

        public BoxType Type { get; }

        public Option<string> Name { get; }

        public Option<BoxStyleCore> Style { get; }

        public ImmutableList<TrackSize> RowSizes { get; }

        public ImmutableList<TrackSize> ColSizes { get; }

        public bool IsPrintArea { get; }

        public bool AreRowsHidden { get; }

        public bool AreColsHidden { get; }

        public bool AreRowsFrozen { get; }

        public bool AreColsFrozen { get; }

        public bool IsAutoFilter { get; }

        public Option<object> Value { get; }

        public Option<int> RowSpan { get; }

        public Option<int> ColSpan { get; }

        public Option<bool> RowAutoSpan { get; }

        public Option<bool> ColAutoSpan { get; }

        public Option<bool> Merge { get; }

        public ImmutableList<BoxCore> Children { get; }

        public Option<ProtoCore> Proto { get; }

        public ImmutableList<SlotCore> Slots { get; }

        internal int Number { get; }

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
                areRowsHidden: false,
                areColsHidden: false,
                areRowsFrozen: false,
                areColsFrozen: false,
                isAutoFilter: false,
                value: value ?? None,
                rowSpan: None,
                colSpan: None,
                rowAutoSpan: None,
                colAutoSpan: None,
                merge: None,
                children: Enumerable.Empty<BoxCore>(),
                proto: None,
                slots: Enumerable.Empty<SlotCore>(),
                number: -1,
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
            bool? areRowsHidden = null,
            bool? areColsHidden = null,
            bool? areRowsFrozen = null,
            bool? areColsFrozen = null,
            bool? isAutoFilter = null,
            Option<object>? value = null,
            Option<int>? rowSpan = null,
            Option<int>? colSpan = null,
            Option<bool>? rowAutoSpan = null,
            Option<bool>? colAutoSpan = null,
            Option<bool>? merge = null,
            IEnumerable<BoxCore>? children = null,
            Option<ProtoCore>? proto = null,
            IEnumerable<SlotCore>? slots = null,
            int? number = null,
            Placement? placement = null) =>
            new BoxCore(
                type ?? Type,
                name ?? Name,
                style ?? Style,
                rowSizes ?? RowSizes,
                colSizes ?? ColSizes,
                isPrintArea ?? IsPrintArea,
                areRowsHidden ?? AreRowsHidden,
                areColsHidden ?? AreColsHidden,
                areRowsFrozen ?? AreRowsFrozen,
                areColsFrozen ?? AreColsFrozen,
                isAutoFilter ?? IsAutoFilter,
                value ?? Value,
                rowSpan ?? RowSpan,
                colSpan ?? ColSpan,
                rowAutoSpan ?? RowAutoSpan,
                colAutoSpan ?? ColAutoSpan,
                merge ?? Merge,
                children ?? Children,
                proto ?? Proto,
                slots ?? Slots,
                number ?? Number,
                placement ?? Placement);
    }
}