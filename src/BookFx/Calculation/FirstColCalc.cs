namespace BookFx.Calculation
{
    using System;
    using BookFx.Cores;
    using BookFx.Functional;
    using static BookFx.Functional.Sc<Cache>;
    using static WidthCalc;

    internal static class FirstColCalc
    {
        public static Sc<Cache, int> FirstCol(BoxCore box, Structure structure) =>
            structure
                .Parent(box)
                .Map(parent => parent
                    .Match(
                        row: _ => structure
                            .Prev(box)
                            .Map(prev =>
                                from prevFirstCol in FirstCol(prev, structure)
                                from prevWidth in Width(prev, structure)
                                select prevFirstCol + prevWidth + 1)
                            .OrElse(FirstCol(parent, structure))
                            .GetOrElse(ScOf(1)),
                        col: _ => FirstCol(parent, structure),
                        stack: _ => FirstCol(parent, structure),
                        value: _ => throw new InvalidOperationException(),
                        proto: _ =>
                            from parentFirstCol in FirstCol(parent, structure)
                            let slotRelativeCol = structure.Slot(box).Position.ValueUnsafe().Col
                            select parentFirstCol + slotRelativeCol - 1
                    )
                )
                .GetOrElse(ScOf(1));
    }
}