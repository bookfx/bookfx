namespace BookFx
{
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

        [Pure]
        public static Position At(int row, int col) => new Position(row, col);

        [Pure]
        public Position RelatingTo(Position basePosition) =>
            At(Row - basePosition.Row + 1, Col - basePosition.Col + 1);

        [Pure]
        public Position AbsoluteFrom(Position basePosition) =>
            At(basePosition.Row + Row - 1, basePosition.Col + Col - 1);

        [Pure]
        public override string ToString() => $"R{Row}C{Col}";
    }
}