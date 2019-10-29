namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class InvalidFontSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<float> Size() =>
            Arb.Default.Float32().Filter(size => size < Constraint.MinFontSize || size > Constraint.MaxFontSize);
    }
}