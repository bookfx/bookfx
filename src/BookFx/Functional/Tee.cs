namespace BookFx.Functional
{
    using Unit = System.ValueTuple;

    internal delegate Result<Unit> Tee<in T>(T x);
}