namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidColumnNumberArb
    {
        [UsedImplicitly]
        public static Arbitrary<int> ColumnNumber() =>
            Gen
                .Choose(1, Constraint.MaxColumn)
                .ToArbitrary();
    }
}