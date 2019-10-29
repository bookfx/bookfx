namespace BookFx.Cores
{
    using BookFx.Functional;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class SlotCore
    {
        private SlotCore(Reference reference, BoxCore box, Option<Position> position)
        {
            Reference = reference;
            Box = box;
            Position = position;
        }

        public Reference Reference { get; }

        public BoxCore Box { get; }

        internal Option<Position> Position { get; }

        [Pure]
        internal static SlotCore Create(Reference reference, BoxCore box) =>
            new SlotCore(reference: reference, box: box, position: F.None);

        [Pure]
        internal SlotCore With(
            Reference? reference = null,
            BoxCore? box = null,
            Option<Position>? position = null) =>
            new SlotCore(
                reference ?? Reference,
                box ?? Box,
                position ?? Position);
    }
}