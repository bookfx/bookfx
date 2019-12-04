namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class InvalidFontSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<double> Size() =>
            Arb.Default.Float().Filter(size => size < Constraint.MinFontSize || size > Constraint.MaxFontSize);
    }
}