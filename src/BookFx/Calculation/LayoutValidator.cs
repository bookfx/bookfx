namespace BookFx.Calculation
{
    using BookFx.Cores;
    using BookFx.Epplus;
    using BookFx.Functional;
    using static BookFx.Functional.F;

    internal static class LayoutValidator
    {
        public static Tee<BoxCore> Validate =>
            box => box
                .SelfAndDescendants()
                .Map(Position.Invoke)
                .HarvestErrors()
                .Map(_ => box);

        public static Tee<BoxCore> Position =>
            box =>
                box.Placement.Position.Row < 1 ||
                box.Placement.Position.Row > Constraint.MaxRow ||
                box.Placement.Position.Col < 1 ||
                box.Placement.Position.Col > Constraint.MaxColumn
                    ? Errors.Position.IsInvalid(box.Placement.Position.Row, box.Placement.Position.Col)
                    : Valid(box);
    }
}