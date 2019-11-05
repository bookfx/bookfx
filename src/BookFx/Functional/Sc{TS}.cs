namespace BookFx.Functional
{
    using static F;
    using Unit = System.ValueTuple;

    /// <summary>
    /// Stateful computation.
    /// </summary>
    public static class Sc<TS>
    {
        public static Sc<TS, TS> Get => state => (state, state);

        public static Sc<TS, TV> Return<TV>(TV value) => state => (value, state);

        public static Sc<TS, Unit> Put(TS newState) => state => (Unit(), newState);
    }
}