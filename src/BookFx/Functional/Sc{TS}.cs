namespace BookFx.Functional
{
    using static F;
    using Unit = System.ValueTuple;

    /// <summary>
    /// Stateful computation.
    /// </summary>
    public static class Sc<TS>
    {
        public static Sc<TS, TS> GetState => state => (state, state);

        public static Sc<TS, TV> ScOf<TV>(TV value) => state => (value, state);

        public static Sc<TS, Unit> PutState(TS newState) => state => (Unit(), newState);
    }
}