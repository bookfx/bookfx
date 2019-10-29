namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidIndentSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<int> IndentSize() =>
            Gen
                .Choose(Constraint.MinIndentSize, Constraint.MaxIndentSize)
                .ToArbitrary();
    }
}