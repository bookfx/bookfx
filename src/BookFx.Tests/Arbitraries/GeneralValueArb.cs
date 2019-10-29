namespace BookFx.Tests.Arbitraries
{
    using FsCheck;
    using JetBrains.Annotations;
    using Unit = System.ValueTuple;

    public static class GeneralValueArb
    {
        [UsedImplicitly]
        public static Arbitrary<object> Value() =>
            Arb.Default
                .Object()
                .Filter(o => o switch
                {
                    Unit _ => false,
                    string s when s.StartsWith("=") || s.StartsWith("'") => false,
                    _ => true
                });
    }
}