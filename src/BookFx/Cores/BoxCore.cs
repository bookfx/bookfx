namespace BookFx.Cores
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a box properties.
    /// </summary>
    [PublicAPI]
    public sealed class BoxCore
    {
        internal static readonly BoxCore Empty = Create(BoxType.Row);

        private BoxCore(
            BoxType type,
            Option<string> globalName,
            Option<string> localName,
            Option<BoxStyleCore> style,
            IEnumerable<TrackSize> rowSizes,
            IEnumerable<TrackSize> colSizes,
            bool isPrintArea,
            bool areRowsHidden,
            bool areColsHidden,
            bool areRowsFrozen,
            bool areColsFrozen,
            bool isAutoFilter,
            Option<object> content,
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
            GlobalName = globalName;
            LocalName = localName;
            Style = style;
            IsPrintArea = isPrintArea;
            AreRowsHidden = areRowsHidden;
            AreColsHidden = areColsHidden;
            AreRowsFrozen = areRowsFrozen;
            AreColsFrozen = areColsFrozen;
            IsAutoFilter = isAutoFilter;
            RowSizes = rowSizes.ToImmutableList();
            ColSizes = colSizes.ToImmutableList();
            Content = content;
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

        /// <summary>
        /// Gets the type.
        /// </summary>
        public BoxType Type { get; }

        /// <summary>
        /// Gets the book scoped range name.
        /// </summary>
        public Option<string> GlobalName { get; }

        /// <summary>
        /// Gets the sheet scoped range name.
        /// </summary>
        public Option<string> LocalName { get; }

        /// <inheritdoc cref="GlobalName"/>
        [Obsolete("Use GlobalName or LocalName instead.")]
        public Option<string> Name => GlobalName;

        /// <summary>
        /// Gets the style.
        /// </summary>
        public Option<BoxStyleCore> Style { get; }

        /// <summary>
        /// Gets row heights.
        /// </summary>
        public ImmutableList<TrackSize> RowSizes { get; }

        /// <summary>
        /// Gets column widths.
        /// </summary>
        public ImmutableList<TrackSize> ColSizes { get; }

        /// <summary>
        /// Gets a value indicating whether is print area defined by the box.
        /// </summary>
        public bool IsPrintArea { get; }

        /// <summary>
        /// Gets a value indicating whether is rows of the box are hidden.
        /// </summary>
        public bool AreRowsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether is columns of the box are hidden.
        /// </summary>
        public bool AreColsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether is rows of the box are frozen.
        /// </summary>
        public bool AreRowsFrozen { get; }

        /// <summary>
        /// Gets a value indicating whether is columns of the box are frozen.
        /// </summary>
        public bool AreColsFrozen { get; }

        /// <summary>
        /// Gets a value indicating whether is auto filter added to the lower row of the box.
        /// </summary>
        public bool IsAutoFilter { get; }

        /// <summary>
        /// Gets the content (the value or the formula).
        /// </summary>
        public Option<object> Content { get; }

        /// <summary>
        /// Gets the number of rows to span.
        /// </summary>
        public Option<int> RowSpan { get; }

        /// <summary>
        /// Gets the number of columns to span.
        /// </summary>
        public Option<int> ColSpan { get; }

        /// <summary>
        /// Gets a value indicating whether is automatic span of rows activated.
        /// </summary>
        public Option<bool> RowAutoSpan { get; }

        /// <summary>
        /// Gets a value indicating whether is automatic span of columns activated.
        /// </summary>
        public Option<bool> ColAutoSpan { get; }

        /// <summary>
        /// Gets a value indicating whether are cells merged.
        /// </summary>
        public Option<bool> Merge { get; }

        /// <summary>
        /// Gets children.
        /// </summary>
        public ImmutableList<BoxCore> Children { get; }

        /// <summary>
        /// Gets the prototype.
        /// </summary>
        public Option<ProtoCore> Proto { get; }

        /// <summary>
        /// Gets slots.
        /// </summary>
        public ImmutableList<SlotCore> Slots { get; }

        internal int Number { get; }

        internal Placement Placement { get; }

        [Pure]
        internal static BoxCore Create(
            BoxType type,
            Option<object>? content = null,
            Option<BoxStyleCore>? style = null) =>
            new BoxCore(
                type: type,
                globalName: None,
                localName: None,
                style: style ?? None,
                rowSizes: Enumerable.Empty<TrackSize>(),
                colSizes: Enumerable.Empty<TrackSize>(),
                isPrintArea: false,
                areRowsHidden: false,
                areColsHidden: false,
                areRowsFrozen: false,
                areColsFrozen: false,
                isAutoFilter: false,
                content: content ?? None,
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
            Option<string>? globalName = null,
            Option<string>? localName = null,
            Option<BoxStyleCore>? style = null,
            IEnumerable<TrackSize>? rowSizes = null,
            IEnumerable<TrackSize>? colSizes = null,
            bool? isPrintArea = null,
            bool? areRowsHidden = null,
            bool? areColsHidden = null,
            bool? areRowsFrozen = null,
            bool? areColsFrozen = null,
            bool? isAutoFilter = null,
            Option<object>? content = null,
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
                globalName ?? GlobalName,
                localName ?? LocalName,
                style ?? Style,
                rowSizes ?? RowSizes,
                colSizes ?? ColSizes,
                isPrintArea ?? IsPrintArea,
                areRowsHidden ?? AreRowsHidden,
                areColsHidden ?? AreColsHidden,
                areRowsFrozen ?? AreRowsFrozen,
                areColsFrozen ?? AreColsFrozen,
                isAutoFilter ?? IsAutoFilter,
                content ?? Content,
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