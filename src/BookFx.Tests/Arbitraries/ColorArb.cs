namespace BookFx.Tests.Arbitraries
{
    using System.Drawing;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ColorArb
    {
        [UsedImplicitly]
        public static Arbitrary<Color> Color() =>
            Arb
                .Generate<byte>()
                .Three()
                .Select(rgb => System.Drawing.Color.FromArgb(0xff, rgb.Item1, rgb.Item2, rgb.Item3))
                .ToArbitrary();
    }
}