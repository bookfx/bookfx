namespace BookFx
{
    using BookFx.Functional;

    internal readonly struct Placement
    {
        public static readonly Placement Empty = default;

        private Placement(Position position, Dimension dimension)
        {
            Position = position;
            Dimension = dimension;
        }

        public Position Position { get; }

        public Dimension Dimension { get; }

        public int ToRow => Position.Row + Dimension.Height - 1;

        public int ToCol => Position.Col + Dimension.Width - 1;

        public bool IsAbsent => Dimension.IsEmpty;

        public static Placement At(Position position, Dimension dimension) => new Placement(position, dimension);

        public static Result<Placement> At(int row, int col, int height, int width) =>
            Position
                .At(row, col)
                .Map(position => At(position, Dimension.Of(height, width)));

        public override string ToString() =>
            Dimension.IsCell
                ? $"{Position}"
                : $"{Position}:R{ToRow}C{ToCol}";
    }
}