namespace BookFx.Functional
{
    /// <summary>
    /// Stateful computation.
    /// </summary>
    public delegate (TV Value, TS State) Sc<TS, TV>(TS state);
}