namespace BookFx
{
    using BookFx.Epplus;
    using BookFx.Functional;
    using JetBrains.Annotations;

    internal readonly struct Position
    {
        public static readonly Position Initial = new Position(1, 1);

        private Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }

        public int Col { get; }

        public static Result<Position> At(int row, int col)
        {
            if (row < 1 || row > Constraint.MaxRow || col < 1 || col > Constraint.MaxColumn)
            {
                return Errors.Position.IsInvalid(row, col);
            }

            return new Position(row, col);
        }

        [Pure]
        public Result<Position> RelatingTo(Position basePosition) =>
            At(Row - basePosition.Row + 1, Col - basePosition.Col + 1);

        [Pure]
        public Result<Position> AbsoluteFrom(Position basePosition) =>
            At(basePosition.Row + Row - 1, basePosition.Col + Col - 1);

        [Pure]
        public override string ToString() => $"R{Row}C{Col}";
    }
}