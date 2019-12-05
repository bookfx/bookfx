namespace BookFx.Functional
{
    using System;
    using static F;
    using Unit = System.ValueTuple;

    internal static class ActionExt
    {
        public static Func<Unit> ToFunc(this Action action) =>
            () =>
            {
                action();
                return Unit();
            };

        public static Func<T, Unit> ToFunc<T>(this Action<T> action) =>
            x =>
            {
                action(x);
                return Unit();
            };

        public static Func<T1, T2, Unit> ToFunc<T1, T2>(this Action<T1, T2> action) =>
            (x1, x2) =>
            {
                action(x1, x2);
                return Unit();
            };
    }
}