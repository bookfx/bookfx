namespace BookFx.Calculation
{
    using System.Collections.Immutable;
    using BookFx.Cores;
    using BookFx.Functional;

    internal class Structure
    {
        private readonly ImmutableDictionary<BoxCore, BoxCore> _parent;
        private readonly ImmutableDictionary<BoxCore, BoxCore> _prev;

        private Structure(
            ImmutableDictionary<BoxCore, BoxCore> parent,
            ImmutableDictionary<BoxCore, BoxCore> prev)
        {
            _parent = parent;
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
                prev: rootBox
                    .SelfAndDescendants()
                    .Bind(parent => parent
                        .ImmediateDescendants()
                        .Neighbors())
                    .ToImmutableDictionary(x => x.Next, x => x.Prev));

        public Option<BoxCore> Parent(BoxCore box) => _parent.TryGetValue(box);

        public Option<BoxCore> Prev(BoxCore box) => _prev.TryGetValue(box);
    }
}