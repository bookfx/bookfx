namespace BookFx
{
    internal readonly struct Dimension
    {
        public static readonly Dimension Empty = default;

        private Dimension(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; }

        public int Width { get; }

        public bool IsEmpty => Height == 0 || Width == 0;

        public bool IsCell => Height == 1 && Width == 1;

        public static Dimension Of(int height, int width) => new Dimension(height, width);

        public override string ToString() => $"{Height}×{Width}";
    }
}