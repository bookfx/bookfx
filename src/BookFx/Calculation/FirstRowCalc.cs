namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static HeightCalc;

    internal static class FirstRowCalc
    {
        public static Sc<Cache, int> FirstRow(BoxCore box, Structure structure) =>
            structure
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => FirstRow(parent, structure),
                        col: _ => structure
                            .Prev(box)
                            .Map(prev =>
                                from prevFirstRow in FirstRow(prev, structure)
                                from prevHeight in Height(prev, structure)
                                select prevFirstRow + prevHeight)
                            .OrElse(FirstRow(parent, structure))
                            .GetOrElse(ScOf(1)),
                        stack: _ => FirstRow(parent, structure),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            from parentFirstRow in FirstRow(parent, structure)
                            let slotRelativeRow = structure.Slot(box).Position.ValueUnsafe().Row
                            select parentFirstRow + slotRelativeRow - 1
                    )
                )
                .GetOrElse(ScOf(1));
    }
}