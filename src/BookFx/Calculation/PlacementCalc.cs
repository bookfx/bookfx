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

        private delegate Position Mover(Placement placement);

        public static BoxCore WithPlacement(this BoxCore box) => box.WithPlacement(None, Position.Initial);

        private static BoxCore WithPlacement(this BoxCore box, Option<BoxCore> parent, Position position) =>
            box.Match(
                row: x => x.CompositeWithPlacement(position, MoveRight),
                col: x => x.CompositeWithPlacement(position, MoveDown),
                stack: x => x.CompositeWithPlacement(position, DontMove),
                value: x => x.ValueWithPlacement(position, parent),
                proto: x => x.ProtoWithPlacement(position));

        private static BoxCore CompositeWithPlacement(this BoxCore box, Position position, Mover move)
        {
            return box
                    .With(children: PlaceChildren())
                    .With(placement: Placement.At(position, box.MinDimension));

            IEnumerable<BoxCore> PlaceChildren()
            {
                var nextPosition = position;

                foreach (var child in box.Children)
                {
                    var placed = child.WithPlacement(box, nextPosition);

                    yield return placed;

                    nextPosition = move(placed.Placement);
                }
            }
        }

        private static BoxCore ValueWithPlacement(
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

        private static BoxCore ProtoWithPlacement(this BoxCore box, Position position) =>
            box.With(
                slots: box.Slots.Map(slot => slot.With(
                    box: slot.Box.WithPlacement(
                        parent: box,
                        position: slot.Position.ValueUnsafe().AbsoluteFrom(position)))),
                placement: Placement.At(position, box.MinDimension));
    }
}