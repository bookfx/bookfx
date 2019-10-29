namespace BookFx.Calculation
{
    using System;
    using System.Collections.Generic;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.F;

    internal static class PlacementCalc
    {
        private static readonly Mover MoveRight =
            placement => Position.At(row: placement.Position.Row, col: placement.ToCol + 1);

        private static readonly Mover MoveDown =
            placement => Position.At(row: placement.ToRow + 1, col: placement.Position.Col);

        private static readonly Mover DontMove = placement => placement.Position;

        private delegate Result<Position> Mover(Placement placement);

        public static Result<BoxCore> WithPlacement(this BoxCore box) => box.WithPlacement(None, Position.Initial);

        private static Result<BoxCore> WithPlacement(this BoxCore box, Option<BoxCore> parent, Position position) =>
            box.Match(
                row: x => x.CompositeWithPlacement(position, MoveRight),
                col: x => x.CompositeWithPlacement(position, MoveDown),
                stack: x => x.CompositeWithPlacement(position, DontMove),
                value: x => x.ValueWithPlacement(position, parent),
                proto: x => x.ProtoWithPlacement(position));

        private static Result<BoxCore> CompositeWithPlacement(this BoxCore box, Position position, Mover move)
        {
            return PlaceChildren()
                .HarvestErrors()
                .Map(children => box
                    .With(children: children)
                    .With(placement: Placement.At(position, box.MinDimension)));

            IEnumerable<Result<BoxCore>> PlaceChildren()
            {
                var nextPosition = Valid(position);

                foreach (var child in box.Children)
                {
                    var placed = nextPosition.Bind(childPosition => child.WithPlacement(box, childPosition));

                    yield return placed;

                    if (!placed.IsValid)
                    {
                        yield break;
                    }

                    nextPosition = placed.Map(x => x.Placement).Bind(move.Invoke);
                }
            }
        }

        private static Result<BoxCore> ValueWithPlacement(
            this BoxCore box,
            Position position,
            Option<BoxCore> parent) =>
            box.With(placement: Placement.At(
                position,
                Dimension.Of(
                    height: box
                        .RowSpan
                        .OrElse(() => parent.Map(
                            row: x => x.MinDimension.Height,
                            col: _ => 1,
                            stack: x => x.MinDimension.Height,
                            value: _ => throw new InvalidOperationException(),
                            proto: _ => 1))
                        .GetOrElse(1),
                    width: box
                        .ColSpan
                        .OrElse(() => parent.Map(
                            row: _ => 1,
                            col: x => x.MinDimension.Width,
                            stack: x => x.MinDimension.Width,
                            value: _ => throw new InvalidOperationException(),
                            proto: _ => 1))
                        .GetOrElse(1))));

        private static Result<BoxCore> ProtoWithPlacement(this BoxCore box, Position position)
        {
            return box
                .Slots
                .Map(PlaceSlot)
                .HarvestErrors()
                .Map(PlaceBox);

            Result<SlotCore> PlaceSlot(SlotCore slot) =>
                from absolutePosition in slot.Position.ValueUnsafe().AbsoluteFrom(position)
                from slotBox in slot.Box.WithPlacement(parent: box, absolutePosition)
                select slot.With(box: slotBox);

            BoxCore PlaceBox(IEnumerable<SlotCore> slots) =>
                box.With(
                    slots: slots,
                    placement: Placement.At(position, box.MinDimension));
        }
    }
}