namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class InvalidIndentSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<int> Size() =>
            Arb.Default.Int32().Filter(size => size < Constraint.MinIndentSize || size > Constraint.MaxIndentSize);
    }
}