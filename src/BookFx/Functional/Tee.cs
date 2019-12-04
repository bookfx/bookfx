namespace BookFx.Functional
{
    using Unit = System.ValueTuple;

    internal delegate Result<Unit> Act<in T>(T x);
}