namespace BookFx.Functional
{
    /// <summary>
    /// Stateful computation.
    /// </summary>
    /// <typeparam name="TS">State type.</typeparam>
    /// <typeparam name="TV">Value type.</typeparam>
    /// <param name="state">State.</param>
    /// <returns>(Value, State) tuple.</returns>
    public delegate (TV Value, TS State) Sc<TS, TV>(TS state);
}