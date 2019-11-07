namespace BookFx.Calculation
{
    using System.Collections.Immutable;
    using BookFx.Cores;
    using BookFx.Functional;

    internal class Structure
    {
        private readonly ImmutableDictionary<BoxCore, BoxCore> _parent;
        private readonly ImmutableDictionary<BoxCore, SlotCore> _slot;
        private readonly ImmutableDictionary<BoxCore, BoxCore> _prev;

        private Structure(
            ImmutableDictionary<BoxCore, BoxCore> parent,
            ImmutableDictionary<BoxCore, SlotCore> slot,
            ImmutableDictionary<BoxCore, BoxCore> prev)
        {
            _parent = parent;
            _slot = slot;
            _prev = prev;
        }

        public static Structure Create(BoxCore rootBox) =>
            new Structure(
                parent: rootBox
                    .SelfAndDescendants()
                    .Bind(parent => parent
                        .ImmediateDescendants()
                        .Map(descendant => (descendant, parent)))
                    .ToImmutableDictionary(x => x.descendant, x => x.parent),
                slot: rootBox
                    .SelfAndDescendants()
                    .Bind(proto => proto.Slots.Map(slot => (SlotBox: slot.Box, Slot: slot)))
                    .ToImmutableDictionary(x => x.SlotBox, x => x.Slot),
                prev: rootBox
                    .SelfAndDescendants()
                    .Bind(parent => parent
                        .Children
                        .Neighbors())
                    .ToImmutableDictionary(x => x.Next, x => x.Prev));

        public Option<BoxCore> Parent(BoxCore box) => _parent.TryGetValue(box);

        public SlotCore Slot(BoxCore protoBox) => _slot[protoBox];

        public Option<BoxCore> Prev(BoxCore box) => _prev.TryGetValue(box);
    }
}