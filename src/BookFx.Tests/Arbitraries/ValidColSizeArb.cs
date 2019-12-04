namespace BookFx.Tests.Arbitraries
{
    using System;
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidColSizeArb
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
                                (int)Constraint.MinColSize * 1000,
                                (int)Constraint.MaxColSize * 1000)
                            .Select(x => x / 1000d)
                            .Select(TrackSize.Some)),
                    Tuple.Create(1, Gen.Constant(TrackSize.Fit)))
                .ToArbitrary();
    }
}