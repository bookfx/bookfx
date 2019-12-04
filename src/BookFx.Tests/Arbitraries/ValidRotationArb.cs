namespace BookFx.Tests.Arbitraries
{
    using BookFx.Epplus;
    using FsCheck;
    using JetBrains.Annotations;

    public static class ValidRotationArb
    {
        [UsedImplicitly]
        public static Arbitrary<int> Rotation() =>
            Gen
                .Choose(Constraint.MinRotation, Constraint.MaxRotation)
                .ToArbitrary();
    }
}