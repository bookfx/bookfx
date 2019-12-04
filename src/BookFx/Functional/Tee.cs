namespace BookFx.Functional
{
    using Unit = System.ValueTuple;

    /// <summary>
    /// For integrating unit functions into the pipeline.
    /// See ROP (<see href="https://fsharpforfunandprofit.com/posts/recipe-part2/"/>).
    /// </summary>
    internal delegate Result<Unit> Tee<in T>(T x);
}