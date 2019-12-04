namespace BookFx.Tests.Arbitraries
{
    using System;
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidRowSizeArb
    {
        [UsedImplicitly]
        public static Arbitrary<TrackSize> Generator() =>
            Gen
                .Frequency(
                    Tuple.Create(1, Gen.Constant(TrackSize.None)),
                    Tuple.Create(
                        2,
                        Gen
                            .Choose(
                                (int)Constraint.MinRowSize * 1000,
                                (int)Constraint.MaxRowSize * 1000)
                            .Select(x => x / 1000d)
                            .Select(TrackSize.Some)),
                    Tuple.Create(1, Gen.Constant(TrackSize.Fit)))
                .ToArbitrary();
    }
}