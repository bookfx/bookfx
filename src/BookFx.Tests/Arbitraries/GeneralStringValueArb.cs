namespace BookFx.Tests.Arbitraries
{
    using FsCheck;
    using JetBrains.Annotations;

    public static class GeneralStringValueArb
    {
        [UsedImplicitly]
        public static Arbitrary<string> Value() =>
            Arb.Default
                .String()
                .Filter(s => s != null && !s.StartsWith("=") && !s.StartsWith("'"));
    }
}