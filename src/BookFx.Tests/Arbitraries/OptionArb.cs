namespace BookFx.Tests.Arbitraries
{
    using BookFx.Functional;
    using FsCheck;
    using static BookFx.Functional.F;

    public static class OptionArb
    {
        public static Arbitrary<Option<T>> Option<T>()
        {
            var gen = from isSome in Arb.Generate<bool>()
                from val in Arb.Generate<T>()
                select isSome && val != null ? Some(val) : None;

            return gen.ToArbitrary();
        }
    }
}