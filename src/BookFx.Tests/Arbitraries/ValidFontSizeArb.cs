namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidFontSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<double> Size() =>
            Gen
                .Choose(
                    (int)Constraint.MinFontSize * 1000,
                    (int)Constraint.MaxFontSize * 1000)
                .Select(x => x / 1000d)
                .ToArbitrary();
    }
}