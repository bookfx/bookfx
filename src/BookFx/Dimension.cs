namespace BookFx
{
    using System;

    internal readonly struct Dimension : IEquatable<Dimension>
    {
        private Dimension(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; }

        public int Width { get; }

        public bool IsEmpty => Height == 0 || Width == 0;

        public bool IsCell => Height == 1 && Width == 1;

        public static bool operator ==(Dimension left, Dimension right) => left.Equals(right);

        public static bool operator !=(Dimension left, Dimension right) => !left.Equals(right);

        public static Dimension Of(int height, int width) => new Dimension(height, width);

        public override string ToString() => $"{Height}×{Width}";

        public bool Equals(Dimension other) => Height == other.Height && Width == other.Width;

        public override bool Equals(object obj) => obj is Dimension other && Equals(other);

        public override int GetHashCode() => (Height << 16) ^ Width;
    }
}