namespace BookFx.Functional
{
    using System;
    using static F;
    using Unit = System.ValueTuple;

    /// <summary>
    /// Stateful computation.
    /// </summary>
    /// <typeparam name="TS">State type.</typeparam>
    /// <typeparam name="TV">Value type.</typeparam>
    /// <param name="state">State.</param>
    /// <returns>(Value, State) tuple.</returns>
    internal delegate (TV Value, TS State) Sc<TS, TV>(TS state);

    /// <summary>
    /// Stateful computation.
    /// </summary>
    internal static class Sc
    {
        public static TV Run<TS, TV>(this Sc<TS, TV> f, TS state) => f(state).Value;

        public static Sc<TS, Unit> PutState<TS>(TS newState) => state => (Unit(), newState);

        public static Sc<TS, TS> GetState<TS>() => state => (state, state);

        public static Sc<TS, TR> Select<TS, TV, TR>(
            this Sc<TS, TV> f,
            Func<TV, TR> project) =>
            state0 =>
            {
                var (v, state1) = f(state0);
                return (project(v), state1);
            };

        public static Sc<TS, TR> SelectMany<TS, TV, TR>(
            this Sc<TS, TV> sc,
            Func<TV, Sc<TS, TR>> f) =>
            state0 =>
            {
                var (v, state1) = sc(state0);
                return f(v)(state1);
            };

        public static Sc<TS, TR> SelectMany<TS, TV, TR1, TR>(
            this Sc<TS, TV> f,
            Func<TV, Sc<TS, TR1>> bind,
            Func<TV, TR1, TR> project) =>
            state0 =>
            {
                var (v, state1) = f(state0);
                var (r1, state2) = bind(v)(state1);
                var r = project(v, r1);
                return (r, state2);
            };
    }
}